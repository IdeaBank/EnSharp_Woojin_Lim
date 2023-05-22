package controller;

import constant.CalculatorState;
import constant.CalculatorSymbols;
import model.CalculatorHistory;

import javax.swing.*;
import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.ArrayList;

public class CalculatorManager {
    private final ArrayList<CalculatorHistory> calculatorHistories;
    private JList historyList;
    private JTextPane historyPane;
    private JTextPane inputPane;
    private JPanel glassPane;
    private CalculatorState calculatorState;
    private BigDecimal firstOperand;
    private char operatorChar;
    private BigDecimal secondOperand;
    private boolean newNumberInput;

    public CalculatorManager() {
        this.calculatorHistories = new ArrayList<>();
        this.calculatorState = CalculatorState.START;
        this.firstOperand = null;
        this.operatorChar = '\0';
        this.secondOperand = null;
        this.newNumberInput = true;
    }

    private static String format(BigDecimal x) {
        NumberFormat formatter = new DecimalFormat("0.0E0");
        formatter.setRoundingMode(RoundingMode.HALF_UP);
        formatter.setMinimumFractionDigits((x.scale() > 0) ? x.precision() : x.scale());
        return formatter.format(x);
    }

    public void setHistoryList(JList historyList) {
        this.historyList = historyList;
    }

    public void setHistoryPane(JTextPane historyPane) {
        this.historyPane = historyPane;
    }

    public void setInputPane(JTextPane inputPane) {
        this.inputPane = inputPane;
    }

    public void setGlassPanel(JPanel glassPane) {
        this.glassPane = glassPane;
    }

    public boolean isGlassPaneVisible() {
        return glassPane.isVisible();
    }

    public void handleButtonPressed(String text) {
        switch (text) {
            case CalculatorSymbols.CE:
                clearEntry();
                break;

            case CalculatorSymbols.C:
                clear();
                break;

            case CalculatorSymbols.DEL:
                deleteEntry();
                break;

            case CalculatorSymbols.DIVIDE:
            case CalculatorSymbols.DIVIDE_SYMBOL:
                getCalculation(CalculatorSymbols.DIVIDE_CHAR);
                break;

            case CalculatorSymbols.MULTIPLY:
            case CalculatorSymbols.MULTIPLY_SYMBOL:
                getCalculation(CalculatorSymbols.MULTIPLY_CHAR);
                break;

            case CalculatorSymbols.SUBTRACT:
                getCalculation(CalculatorSymbols.SUBTRACT_CHAR);
                break;

            case CalculatorSymbols.ADD:
                getCalculation(CalculatorSymbols.ADD_CHAR);
                break;

            case CalculatorSymbols.EQUALS:
                calculate();
                if (calculatorState != CalculatorState.ERROR && calculatorState != CalculatorState.START)
                    calculatorState = CalculatorState.ENTER_KEY_PRESSED;
                break;

            case CalculatorSymbols.NEGATE:
                negate();
                break;

            case CalculatorSymbols.DOT:
                appendDot();
                break;

            case CalculatorSymbols.ZERO:
            case CalculatorSymbols.ONE:
            case CalculatorSymbols.TWO:
            case CalculatorSymbols.THREE:
            case CalculatorSymbols.FOUR:
            case CalculatorSymbols.FIVE:
            case CalculatorSymbols.SIX:
            case CalculatorSymbols.SEVEN:
            case CalculatorSymbols.EIGHT:
            case CalculatorSymbols.NINE:
                appendNumber(text.charAt(0));
                break;
        }
    }

    public void clearEntry() {
        if(calculatorState == CalculatorState.ERROR)
        {
            resetHistory();
            firstOperand = null;
            secondOperand = null;
            operatorChar = '\0';
            calculatorState = CalculatorState.START;
        }

        inputPane.setText("0");
    }

    public void clear() {
        resetHistory();
        clearEntry();
        firstOperand = null;
        secondOperand = null;
        operatorChar = '\0';
        calculatorState = CalculatorState.START;
    }

    public void deleteEntry() {
        boolean isEndWithDot = false;

        if(inputPane.getText().substring(0, inputPane.getText().length() - 1).endsWith("."))
        {
            isEndWithDot = true;
        }

        if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            historyPane.setText("");
        }

        else if (calculatorState == CalculatorState.ERROR) {
            clear();
        }

        else if(calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            String inputString = inputPane.getText();
            inputString = String.join("", inputString.split(","));

            if (inputString.length() == 1 || (inputString.length() == 2 && inputString.startsWith("-"))) {
                inputString = "0";
            } else {
                inputString = inputString.substring(0, inputString.length() - 1);
            }

            if(new BigDecimal(inputString).compareTo(new BigDecimal("0")) != 0) {
                inputPane.setText(getDecimalWithFormat(new BigDecimal(inputString), true));
            }

            else{
                if(inputString.endsWith("."))
                {
                    inputPane.setText(inputString.substring(0, inputString.length() - 1));
                }
                else {
                inputPane.setText(inputString);
                }
            }

            if(isEndWithDot)
            {
                inputPane.setText(inputPane.getText() + '.');
            }
        }
    }

    public void divide() {
        if (secondOperand.compareTo(new BigDecimal(0)) == 0) {
            resetHistory();

            if (firstOperand.compareTo(new BigDecimal(0)) == 0) {
                inputPane.setText("정의되지 않은 결과입니다.");
            } else {
                inputPane.setText("0으로 나눌 수 없습니다");
            }

            calculatorState = CalculatorState.ERROR;
            return;
        }

        else {
            firstOperand = firstOperand.divide(secondOperand, 20000, RoundingMode.HALF_UP);
        }

        if(firstOperand.abs().compareTo(new BigDecimal("1e+10000")) == 1)
        {
            historyPane.setText("");
            inputPane.setText("오버플로");
            calculatorState = CalculatorState.ERROR;
        }

        else if(firstOperand.abs().compareTo(new BigDecimal("1e-10000")) == -1 && firstOperand.abs().compareTo(new BigDecimal("0")) == 1)
        {
            historyPane.setText("");
            inputPane.setText("오버플로");
            calculatorState = CalculatorState.ERROR;
        }
    }

    public void multiply()
    {
        firstOperand = firstOperand.multiply(secondOperand, MathContext.UNLIMITED);

        if(firstOperand.abs().compareTo(new BigDecimal("1e+10000")) == 1)
        {
            historyPane.setText("");
            inputPane.setText("오버플로");
            calculatorState = CalculatorState.ERROR;
        }

        else if(firstOperand.abs().compareTo(new BigDecimal("1e-10000")) == -1 && firstOperand.abs().compareTo(new BigDecimal("0")) == 1)
        {
            historyPane.setText("");
            inputPane.setText("오버플로");
            calculatorState = CalculatorState.ERROR;
        }
    }

    public void getCalculation(char operatorChar) {
        if (calculatorState == CalculatorState.ERROR) {
            return;
        }

        else if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED || calculatorState == CalculatorState.START) {
            if (firstOperand == null) {
                firstOperand = getInputDecimal();

                historyPane.setText(getDecimalWithFormat(firstOperand, true) + " " + operatorChar);

                if(secondOperand != null)
                {
                    switch (this.operatorChar) {
                        case CalculatorSymbols.ADD_CHAR:
                            firstOperand = firstOperand.add(secondOperand);
                            break;
                        case CalculatorSymbols.SUBTRACT_CHAR:
                            firstOperand = firstOperand.subtract(secondOperand);
                            break;
                        case CalculatorSymbols.MULTIPLY_CHAR:
                            multiply();
                            break;
                        case CalculatorSymbols.DIVIDE_CHAR:
                            divide();
                            break;
                    }

                    if (calculatorState == CalculatorState.ERROR) {
                        return;
                    }

                    inputPane.setText(getDecimalWithFormat(firstOperand, true));
                    historyPane.setText(inputPane.getText() + " " + operatorChar);
                }

                this.operatorChar = operatorChar;
                calculatorState = CalculatorState.OPERATION_KEY_PRESSED;

                return;
            }

            else {
                if (secondOperand == null) {
                    secondOperand = getInputDecimal();
                }
            }

            if(secondOperand != null)
            {
                switch (this.operatorChar) {
                    case CalculatorSymbols.ADD_CHAR:
                        firstOperand = firstOperand.add(secondOperand);
                        break;
                    case CalculatorSymbols.SUBTRACT_CHAR:
                        firstOperand = firstOperand.subtract(secondOperand);
                        break;
                    case CalculatorSymbols.MULTIPLY_CHAR:
                        firstOperand = firstOperand.multiply(secondOperand, MathContext.UNLIMITED);
                        break;
                    case CalculatorSymbols.DIVIDE_CHAR:
                        divide();
                        break;
                }

                if (calculatorState == CalculatorState.ERROR) {
                    return;
                }

                inputPane.setText(getDecimalWithFormat(firstOperand, true));
                historyPane.setText(inputPane.getText() + " " + operatorChar);

                calculatorState = CalculatorState.OPERATION_KEY_PRESSED;

                this.operatorChar = operatorChar;

                return;
            }
        }

        else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {

            inputPane.setText(getDecimalWithFormat(firstOperand, true));

            this.operatorChar = operatorChar;

            historyPane.setText(inputPane.getText() + " " + operatorChar);

        }

        else if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            this.operatorChar = operatorChar;
            historyPane.setText(getDecimalWithFormat(firstOperand, false) + " " + operatorChar);

            inputPane.setText(getDecimalWithFormat(firstOperand, true));
        }

        this.operatorChar = operatorChar;

        if(calculatorState != CalculatorState.START) {
            calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
        }
    }

    public void negate() {
        if (inputPane.getText().equals("0")) {
            return;
        }

        if (inputPane.getText().startsWith("-")) {
            inputPane.setText(inputPane.getText().substring(1));
        } else {
            inputPane.setText("-" + inputPane.getText());
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void appendNumber(char inputNumber) {
        if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            secondOperand = null;
            inputPane.setText(String.valueOf(inputNumber));
        }

        else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            inputPane.setText(String.valueOf(inputNumber));
            firstOperand = null;

            if (operatorChar != '\0') {
                historyPane.setText("");
            }
        }

        else if (calculatorState == CalculatorState.ERROR) {
            clear();
            inputPane.setText(String.valueOf(inputNumber));
        }

        else {
            String stringWithoutComma = String.join("", inputPane.getText().split(","));
            String stringWithoutDot = String.join("", stringWithoutComma.split("[.]"));

            if (stringWithoutDot.length() > 15) {
                return;
            }

            if (inputPane.getText().equals("0")) {
                inputPane.setText(String.valueOf(inputNumber));
            }

            else {
                inputPane.setText(getInputDecimal(new BigDecimal(stringWithoutComma + inputNumber)));
            }
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void appendDot() {
        if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED || calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            inputPane.setText("0.");
        }

        if (!inputPane.getText().contains(".")) {
            inputPane.setText(inputPane.getText() + ".");
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void calculate() {
        if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            if (operatorChar == '\0') {
                String input = inputPane.getText();
                input = String.join("", input.split(","));

                firstOperand = getInputDecimal();
                historyPane.setText(getDecimalWithFormat(firstOperand, false) + " =");
                calculatorState = CalculatorState.ENTER_KEY_PRESSED;
            }

            else {
                String temp;

                if(firstOperand != null) {
                    temp = getDecimalWithFormat(firstOperand, true);
                    secondOperand = getInputDecimal();

                }

                else{
                    temp = getInputDecimal().stripTrailingZeros().toPlainString();
                }

                getCalculation(operatorChar);

                if (calculatorState != CalculatorState.ERROR) {
                    historyPane.setText(temp + " " + this.operatorChar + " " + getDecimalWithFormat(secondOperand, true) + " =");
                }
            }
        }

        else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {

            if (calculatorState != CalculatorState.ERROR && operatorChar == '\0')
                historyPane.setText(historyPane.getText().substring(0, historyPane.getText().length() - 2) + " =");

            else {
                String temp = inputPane.getText();

                switch (this.operatorChar) {
                    case CalculatorSymbols.ADD_CHAR:
                        firstOperand = firstOperand.add(secondOperand);
                        break;
                    case CalculatorSymbols.SUBTRACT_CHAR:
                        firstOperand = firstOperand.subtract(secondOperand);
                        break;
                    case CalculatorSymbols.MULTIPLY_CHAR:
                        multiply();
                        break;
                    case CalculatorSymbols.DIVIDE_CHAR:
                        divide();
                        break;
                }

                if(calculatorState == CalculatorState.ERROR)
                {
                    return;
                }

                historyPane.setText(temp + " " + this.operatorChar + " " + getDecimalWithFormat(secondOperand, true) + " =");
                inputPane.setText(getDecimalWithFormat(firstOperand, true));

            }
        }

        else if(calculatorState == CalculatorState.OPERATION_KEY_PRESSED)
        {
            secondOperand = new BigDecimal(firstOperand.toPlainString());
            String temp = getDecimalWithFormat(firstOperand, false);

            switch (this.operatorChar) {
                case CalculatorSymbols.ADD_CHAR:
                    firstOperand = firstOperand.add(secondOperand);
                    break;
                case CalculatorSymbols.SUBTRACT_CHAR:
                    firstOperand = firstOperand.subtract(secondOperand);
                    break;
                case CalculatorSymbols.MULTIPLY_CHAR:
                    multiply();
                    break;
                case CalculatorSymbols.DIVIDE_CHAR:
                    divide();
                    break;
            }

            if(calculatorState == CalculatorState.ERROR)
            {
                return;
            }

            historyPane.setText(temp + " " + this.operatorChar + " " + temp + " =");
            inputPane.setText(getDecimalWithFormat(firstOperand, true));
        }

        calculatorState = CalculatorState.ENTER_KEY_PRESSED;
    }

    private void resetHistory() {
        historyPane.setText("");
    }

    private boolean isInteger(BigDecimal input) {
        return input.stripTrailingZeros().scale() <= 0;
    }

    private boolean isDecimalOverLimit(BigDecimal input) {
        return input.stripTrailingZeros().scale() > 16;
    }

    private String getDecimalWithFormat(BigDecimal targetDecimal, boolean formatDecimal) {
        if(targetDecimal.compareTo(new BigDecimal("0")) == 0)
        {
            return targetDecimal.stripTrailingZeros().toPlainString();
        }

        if(targetDecimal.compareTo(new BigDecimal("0.001")) == -1 && targetDecimal.compareTo(new BigDecimal("0")) == 1)
        {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toPlainString());
            int count = 0;

            while(tempResult.compareTo(new BigDecimal("1")) == -1)
            {
                tempResult = tempResult.multiply(new BigDecimal("10"));
                count += 1;
            }

            if(tempResult.toPlainString().length() > 16) {
                if (tempResult.toPlainString().substring(0, 16).contains(".")) {
                    if (tempResult.toPlainString().length() > 16) {
                        return tempResult.toPlainString().substring(0, 16) + "e-" + count;
                    }

                    else {
                        return tempResult.stripTrailingZeros().toPlainString() + "e-" + count;
                    }
                }

                else {
                    if (tempResult.toPlainString().length() > 15) {
                        return tempResult.toPlainString().substring(0, 15) + "e-" + count;
                    }

                    else {
                        return tempResult.stripTrailingZeros().toPlainString() + "e-" + count;
                    }
                }
            }

            return tempResult.stripTrailingZeros().toPlainString() + "e-" + count;
        }

        else if(targetDecimal.compareTo(new BigDecimal("-0.001")) == 1 && targetDecimal.compareTo(new BigDecimal("0")) == -1)
        {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toString());
            tempResult = tempResult.stripTrailingZeros();
            int count = 0;

            while(tempResult.compareTo(new BigDecimal("-1")) == 1)
            {
                tempResult = tempResult.multiply(new BigDecimal("10"));
                count += 1;
            }

            if(tempResult.toPlainString().length() > 18) {
                if (tempResult.toPlainString().substring(0, 17).contains(".")) {
                    return tempResult.stripTrailingZeros().toPlainString().substring(0, 17) + "e-" + count;
                }

                else {
                    return tempResult.stripTrailingZeros().toPlainString() + "e-" + count;
                }
            }

            else {
                if(tempResult.toPlainString().length() > 16) {
                    return tempResult.stripTrailingZeros().toPlainString().substring(0, 16) + "e-" + count;
                }

                else {
                    return tempResult.stripTrailingZeros().toPlainString() + "e-" + count;
                }
            }
        }

        else if(targetDecimal.compareTo(new BigDecimal("9999999999999999")) == 1)
        {
            BigDecimal tempResult = new BigDecimal(targetDecimal.toString());
            int count = 0;

            while(tempResult.compareTo(new BigDecimal("10")) == 1)
            {
                tempResult = tempResult.divide(new BigDecimal("10"), 100, RoundingMode.HALF_UP);
                count += 1;
            }

            if(tempResult.compareTo(new BigDecimal("1")) == -1) {
                tempResult = tempResult.multiply(new BigDecimal("10"), MathContext.UNLIMITED);
                count -= 1;
            }

            if(tempResult.toPlainString().length() > 17) {
                return tempResult.toPlainString().substring(0, 16) + "e+" + count;
            }

            else {
                return tempResult.stripTrailingZeros().toPlainString() + "e+" + count;
            }
        }

        else
        {
            String result = targetDecimal.stripTrailingZeros().toPlainString();

            if(result.contains(".") && !result.endsWith("."))
            {
                if(result.length() > 18)
                {
                    result = result.substring(0, 18);
                }
            }

            else if(result.length() > 16) {
                result = result.substring(0, 16);
            }

            DecimalFormat formatter;
            BigDecimal tempResult = new BigDecimal(result);

            if(!formatDecimal)
            {
                if(tempResult.toPlainString().length() > 16)
                {
                    return tempResult.setScale(tempResult.scale() - 1, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString();
                }

                return tempResult.stripTrailingZeros().toPlainString();
            }

            tempResult = tempResult.setScale(16, RoundingMode.HALF_UP);

            if(tempResult.toPlainString().contains(".")) {

                int scale = tempResult.scale();
                tempResult = tempResult.setScale(scale - 1, RoundingMode.HALF_UP);
                tempResult = tempResult.stripTrailingZeros();

                if(!tempResult.stripTrailingZeros().toPlainString().contains("."))
                {
                    formatter = new DecimalFormat("#,##0");

                    return formatter.format(tempResult.stripTrailingZeros());
                }

                formatter = new DecimalFormat("#,##0." + "0".repeat(tempResult.scale()));
            }

            else {
                formatter = new DecimalFormat("#,##0");
            }

            return formatter.format(tempResult.stripTrailingZeros());
        }
    }

    public String getInputDecimal(BigDecimal targetDecimal)
    {
        if(targetDecimal.compareTo(new BigDecimal("0")) == 0)
        {
            return targetDecimal.toPlainString();
        }

        String result = targetDecimal.stripTrailingZeros().toPlainString();

        if(result.contains(".") && !result.endsWith("."))
        {
            if(result.length() > 17)
            {
                result.substring(0, 17);
            }
        }

        else if(result.length() > 16) {
            result = result.substring(0, 16);
        }

        DecimalFormat formatter;
        BigDecimal tempResult = new BigDecimal(result).stripTrailingZeros();

        if(result.contains(".")) {
            formatter = new DecimalFormat("#,##0." + "0".repeat(tempResult.scale()));
        }

        else {
            formatter = new DecimalFormat("#,##0");
        }

        return formatter.format(tempResult);
    }

    public BigDecimal getInputDecimal() {
        return new BigDecimal(String.join("", inputPane.getText().split(","))).stripTrailingZeros();
    }
}
