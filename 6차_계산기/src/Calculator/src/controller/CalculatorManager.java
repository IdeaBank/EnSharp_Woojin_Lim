package controller;

import constant.CalculatorState;
import constant.CalculatorSymbols;
import model.CalculatorHistory;

import javax.swing.*;
import java.math.BigDecimal;
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
    private BigDecimal currentInput;

    public CalculatorManager() {
        this.calculatorHistories = new ArrayList<>();
        this.calculatorState = CalculatorState.START;
        this.firstOperand = null;
        this.operatorChar = '\0';
        this.secondOperand = null;
        this.currentInput = new BigDecimal("0");
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
                break;

            case CalculatorSymbols.NEGATE:
                negate();
                break;

            case CalculatorSymbols.DOT:
                appendDot();
                break;

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
            case CalculatorSymbols.ZERO:
                appendZero();
                break;
        }
    }

    public void clearEntry() {
        inputPane.setText("0");
        secondOperand = null;
        currentInput = new BigDecimal(0);
    }

    public void clear() {
        resetHistory();
        clearEntry();
        firstOperand = null;
        secondOperand = null;
        currentInput = new BigDecimal(0);
        calculatorState = CalculatorState.START;
    }

    public void deleteEntry() {
        String inputString = inputPane.getText();

        if (inputString.length() == 1) {
            inputString = "0";
        } else {
            inputString = inputString.substring(0, inputString.length() - 1);
        }

        secondOperand = new BigDecimal(String.join("", inputString.split(",")));
        inputPane.setText(String.join("", inputString.split(",")));
        displayCurrentInput();
    }

    public void divide() {
        if (secondOperand.compareTo(new BigDecimal(0)) == 0) {
            resetHistory();
            if (firstOperand.compareTo(new BigDecimal(0)) == 0) {
                inputPane.setText("UNDEFINED");
            } else {
                inputPane.setText("DIV BY ZERO");
            }

            calculatorState = CalculatorState.ERROR;
        } else {
            secondOperand = firstOperand.divide(secondOperand);
        }
    }

    public void getCalculation(char operatorChar) {
        if(calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            if(firstOperand == null) {
                firstOperand = new BigDecimal(currentInput.toPlainString());
                this.operatorChar = operatorChar;
                historyPane.setText(firstOperand.toPlainString() + " " + operatorChar);
                return;
            } else {
                switch(operatorChar)
                {
                    case CalculatorSymbols.ADD_CHAR:
                        firstOperand = firstOperand.add(currentInput);
                        break;
                    case CalculatorSymbols.SUBTRACT_CHAR:
                        firstOperand = firstOperand.subtract(currentInput);
                        break;
                    case CalculatorSymbols.MULTIPLY_CHAR:
                        firstOperand = firstOperand.multiply(currentInput);
                        break;
                    case CalculatorSymbols.DIVIDE_CHAR:
                        firstOperand = firstOperand.divide(currentInput);
                        break;
                }

                this.operatorChar = operatorChar;
                historyPane.setText(firstOperand.toPlainString() + " " + operatorChar);
            }
        } else if(calculatorState == CalculatorState.OPERATION_KEY_PRESSED){
            this.operatorChar = operatorChar;
            historyPane.setText(firstOperand + " " + operatorChar);
        }
    }

    public void negate() {
        if (inputPane.getText().startsWith("-")) {
            inputPane.setText(inputPane.getText().substring(1));
        } else {
            inputPane.setText("-" + inputPane.getText());
        }

        secondOperand = new BigDecimal(String.join("", inputPane.getText().split(",")));
        //inputPane.setText(currentInput);
    }

    public void appendZero() {
        appendNumber('0');
    }

    public void appendNumber(char ch) {
        if(calculatorState == CalculatorState.OPERATION_KEY_PRESSED)
        {
            currentInput = new BigDecimal(String.valueOf(ch));
        }
        else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            inputPane.setText("0");
            currentInput = new BigDecimal(String.valueOf(ch));

            if (operatorChar != '\0') {
                historyPane.setText("");
            }
        }
        else
        {
            if(calculatorState == CalculatorState.ERROR)
            {
                firstOperand = null;
                operatorChar = '\0';
                historyPane.setText("");
                secondOperand = null;
                currentInput = new BigDecimal(String.valueOf(ch));
                calculatorState = CalculatorState.START;
            }

            String tempString = String.join("", inputPane.getText().split(","));
            tempString = String.join("", tempString.split("[.]"));

            if (tempString.length() > 15) {
                return;
            }

            if(inputPane.getText().endsWith("."))
            {
                currentInput = new BigDecimal(String.join("", inputPane.getText().split(",")) + ch);
            }
            else if(currentInput.toString().equals("0"))
            {
                currentInput = new BigDecimal(String.valueOf(ch));
            }

            else{
                currentInput = new BigDecimal(currentInput.toString() + ch);
            }
        }

        displayCurrentInput();
        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void appendDot() {
        if (!inputPane.getText().contains(".")) {
            inputPane.setText(inputPane.getText() + ".");
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void calculate() {
        if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            if (operatorChar == '\0') {
                historyPane.setText(inputPane.getText() + " =");
                inputPane.setText("0");
            } else {
                getCalculation(operatorChar);
            }
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            getCalculation(operatorChar);

            if (calculatorState != CalculatorState.ERROR)
                historyPane.setText(historyPane.getText().substring(0, historyPane.getText().length() - 1) + " =");

        }

        //if(calculatorState != CalculatorState.OPERATION_KEY_PRESSED)
        calculatorState = CalculatorState.ENTER_KEY_PRESSED;
        displayCurrentInput();
    }

    private String getFormattedNumber(BigDecimal bigDecimal) {
        NumberFormat formatter = new DecimalFormat("0.0E0");
        formatter.setRoundingMode(RoundingMode.HALF_UP);
        formatter.setMinimumFractionDigits((bigDecimal.scale() > 0) ? bigDecimal.precision() : bigDecimal.scale());
        return formatter.format(bigDecimal);
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

    private void displayCurrentInput() {
        String inputString = String.join("", currentInput.toString().split(","));
        boolean isEndWithDot = inputString.endsWith(".");
        String[] str = inputString.split("[.]");
        String temp_result = "";
        String reverse_result = "";
        char[] charList = str[0].toCharArray();
        int count = 0;

        for (int i = str[0].length() - 1; i >= 0; --i) {
            if (count % 3 == 0 && count != 0) {
                temp_result += ',';
            }
            temp_result += str[0].toCharArray()[i];
            count += 1;
        }

        for (int i = temp_result.length() - 1; i >= 0; --i) {
            reverse_result += temp_result.toCharArray()[i];
        }

        str[0] = reverse_result;

        if (str[0].startsWith("-,")) {
            str[0] = "-" + str[0].substring(2);
        }

        inputPane.setText(String.join(".", str));

        if (isEndWithDot) {
            inputPane.setText(inputPane.getText() + ".");
        }
    }
}
