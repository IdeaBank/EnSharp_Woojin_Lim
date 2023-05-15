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
    private BigDecimal pastInput;
    private char operatorChar;
    private BigDecimal currentInput;

    public CalculatorManager() {
        this.calculatorHistories = new ArrayList<>();
        this.calculatorState = CalculatorState.START;
        this.pastInput = null;
        this.operatorChar = '\0';
        this.currentInput = new BigDecimal(0);
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
                divide();
                break;

            case CalculatorSymbols.MULTIPLY:
            case CalculatorSymbols.MULTIPLY_SYMBOL:
                multiply();
                break;

            case CalculatorSymbols.SUBTRACT:
                subtract();
                break;

            case CalculatorSymbols.ADD:
                add();
                break;

            case CalculatorSymbols.EQUALS:
                calculate();
                break;

            case CalculatorSymbols.PLUS_OR_MINUS:
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
        currentInput = new BigDecimal(0);
    }

    public void clear() {
        resetHistory();
        clearEntry();
        pastInput = null;
    }

    public void deleteEntry() {
        String inputString = inputPane.getText();

        if (inputString.length() == 1) {
            inputString = "0";
        } else {
            inputString = inputString.substring(0, inputString.length() - 1);
        }

        currentInput = new BigDecimal(String.join("", inputString.split(",")));
        inputPane.setText(String.join("", inputString.split(",")));
        displayCurrentInput();
    }

    public void divide() {
        if (calculatorState != CalculatorState.OPERATION_KEY_PRESSED) {
            if (pastInput == null) {
                pastInput = new BigDecimal(currentInput.toString());
                currentInput = new BigDecimal(0);
                inputPane.setText("0");
            } else {
                if (currentInput.compareTo(new BigDecimal(0)) == 0) {
                    resetHistory();
                    if (pastInput.compareTo(new BigDecimal(0)) == 0) {
                        inputPane.setText("UNDEFINED");
                    } else {
                        inputPane.setText("DIV BY ZERO");
                    }

                    calculatorState = CalculatorState.ERROR;

                    return;
                } else {
                    pastInput = pastInput.divide(currentInput, 10, RoundingMode.CEILING);
                    inputPane.setText(pastInput.toString());
                }
            }

        }

        operatorChar = '/';

        historyPane.setText(pastInput.toString() + " " + operatorChar);

        displayCurrentInput();
        calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
    }

    public void multiply() {
        if (calculatorState != CalculatorState.OPERATION_KEY_PRESSED) {
            if (pastInput == null) {
                pastInput = new BigDecimal(currentInput.toString());
                currentInput = new BigDecimal(0);
                inputPane.setText("0");
            } else {

                pastInput = pastInput.multiply(currentInput);
                inputPane.setText(pastInput.toString());
            }

        }

        operatorChar = '*';

        historyPane.setText(pastInput.toString() + " " + operatorChar);

        displayCurrentInput();
        calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
    }

    public void subtract() {
        if (calculatorState != CalculatorState.OPERATION_KEY_PRESSED) {
            if (pastInput == null) {
                pastInput = new BigDecimal(currentInput.toString());
                currentInput = new BigDecimal(0);
                inputPane.setText("0");
            } else {
                pastInput = pastInput.subtract(currentInput);
                inputPane.setText(pastInput.toString());
            }

        }

        operatorChar = '-';

        historyPane.setText(pastInput.toString() + " " + operatorChar);

        displayCurrentInput();
        calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
    }

    public void add() {
        if (calculatorState != CalculatorState.OPERATION_KEY_PRESSED) {
            if (pastInput == null) {
                pastInput = new BigDecimal(currentInput.toString());
                currentInput = new BigDecimal(0);
                inputPane.setText("0");
            } else {
                pastInput = pastInput.add(currentInput);
                inputPane.setText(pastInput.toString());
            }
        }

        operatorChar = '+';

        historyPane.setText(pastInput.toString() + " " + operatorChar);

        displayCurrentInput();
        calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
    }

    public void negate() {
        if (inputPane.getText().startsWith("-")) {
            inputPane.setText(inputPane.getText().substring(1));
        } else {
            inputPane.setText("-" + inputPane.getText());
        }

        currentInput = new BigDecimal(String.join("", inputPane.getText().split(",")));
        //inputPane.setText(currentInput);
    }

    public void appendZero() {
        appendNumber('0');
    }

    public void appendNumber(char ch) {
        if (calculatorState == CalculatorState.ERROR)
        {
            inputPane.setText(String.valueOf(ch));
        }

        else if (calculatorState == CalculatorState.START || calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            String tempString = String.join("", inputPane.getText().split(","));
            tempString = String.join("", tempString.split("[.]"));

            if (tempString.length() > 15) {
                return;
            }

            if (inputPane.getText().equals("0")) {
                inputPane.setText(String.valueOf(ch));
            } else if (inputPane.getText().equals("-0")) {
                inputPane.setText("-" + ch);
            } else {
                inputPane.setText(inputPane.getText() + ch);
            }

        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            resetHistory();

            inputPane.setText(String.valueOf(ch));
            currentInput = new BigDecimal(String.valueOf(ch));
            calculatorState = CalculatorState.NUMBER_KEY_PRESSED;

            displayCurrentInput();

            return;

        } else if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            inputPane.setText(String.valueOf(ch));
        }

        currentInput = new BigDecimal(String.join("", inputPane.getText().split(",")));
        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
        displayCurrentInput();
    }

    public void appendDot() {
        if (!inputPane.getText().contains(".")) {
            inputPane.setText(inputPane.getText() + ".");
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void calculate() {
        if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            if(operatorChar == '\0')
            {
                historyPane.setText(inputPane.getText() + " =");
                inputPane.setText("0");
            }

            else
            {
                switch(operatorChar)
                {
                    case '+':
                        add();
                        break;
                    case '-':
                        subtract();
                        break;
                    case '*':
                        multiply();
                        break;
                    case '/':
                        divide();
                        break;
                }

                if(calculatorState != CalculatorState.ERROR)
                    historyPane.setText(historyPane.getText().substring(0, historyPane.getText().length() - 1) + " =");
            }
        }

        else if(calculatorState == CalculatorState.ENTER_KEY_PRESSED)
        {
            switch(operatorChar)
            {
                case '+':
                    add();
                    break;
                case '-':
                    subtract();
                    break;
                case '*':
                    multiply();
                    break;
                case '/':
                    divide();
                    break;
        }

            if(calculatorState != CalculatorState.ERROR)
                historyPane.setText(historyPane.getText().substring(0, historyPane.getText().length() - 1) + " =");

        }

        //if(calculatorState != CalculatorState.OPERATION_KEY_PRESSED)
            calculatorState = CalculatorState.ENTER_KEY_PRESSED;
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
        String inputString = String.join("", inputPane.getText().split(","));
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
