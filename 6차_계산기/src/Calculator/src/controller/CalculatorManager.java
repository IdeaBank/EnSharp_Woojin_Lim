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
                calculatorState = CalculatorState.ENTER_KEY_PRESSED;
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
        if (calculatorState == CalculatorState.ENTER_KEY_PRESSED || calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            historyPane.setText("");
            return;
        } else if (calculatorState == CalculatorState.ERROR) {
            clear();
        } else {
            String inputString = inputPane.getText();

            if (inputString.length() == 1 || (inputString.length() == 2 && inputString.startsWith("-"))) {
                inputString = "0";
            }else
            {
                inputString = inputString.substring(0, inputString.length() - 1);
            }

            currentInput = new BigDecimal(String.join("", inputString.split(",")));
            inputPane.setText(String.join("", inputString.split(",")));
            displayCurrentInput();
        }
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
            secondOperand = firstOperand.divide(secondOperand, 10000, RoundingMode.HALF_UP);
        }
    }

    public void getCalculation(char operatorChar) {
        if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            this.operatorChar = operatorChar;
            if (firstOperand == null) {
                firstOperand = new BigDecimal(currentInput.toPlainString());
            } else {
                if(secondOperand == null)
                {
                    secondOperand = new BigDecimal(currentInput.toPlainString());
                }
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

                currentInput = new BigDecimal(firstOperand.toPlainString());
                historyPane.setText(firstOperand.stripTrailingZeros().toPlainString() + " " + operatorChar);
                calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
                displayCurrentInput();
                return;
            }
        }else if(calculatorState == CalculatorState.ENTER_KEY_PRESSED)
        {
            firstOperand = new BigDecimal(currentInput.toString());
            this.operatorChar = operatorChar;
            historyPane.setText(firstOperand.stripTrailingZeros().toPlainString() + operatorChar);
        }
        else if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            this.operatorChar = operatorChar;
            historyPane.setText(firstOperand.stripTrailingZeros().toPlainString() + " " + operatorChar);
        }

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

    public void appendNumber(char inputNumber) {
        if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            secondOperand = null;
            currentInput = new BigDecimal(String.valueOf(inputNumber));
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            currentInput = new BigDecimal(String.valueOf(inputNumber));

            if (operatorChar != '\0') {
                historyPane.setText("");
            }
        } else if (calculatorState == CalculatorState.ERROR) {
            clear();
            currentInput = new BigDecimal(String.valueOf(inputNumber));
        } else {
            String tempString = String.join("", inputPane.getText().split(","));
            tempString = String.join("", tempString.split("[.]"));

            if (tempString.length() > 15) {
                return;
            }

            if (inputPane.getText().endsWith(".")) {
                currentInput = new BigDecimal(String.join("", inputPane.getText().split(",")) + inputNumber);
            } else if (currentInput.toString().equals("0")) {
                currentInput = new BigDecimal(String.valueOf(inputNumber));
            } else {
                currentInput = new BigDecimal(currentInput.toString() + inputNumber);
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
                historyPane.setText(currentInput.stripTrailingZeros().toPlainString() + " =");
                calculatorState = CalculatorState.ENTER_KEY_PRESSED;
            } else {
                String temp = firstOperand.toPlainString();
                firstOperand = new BigDecimal(currentInput.toPlainString());
                getCalculation(operatorChar);
                historyPane.setText(temp + " " + this.operatorChar + " " + secondOperand.stripTrailingZeros().toPlainString() + " =");
            }
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            String temp = firstOperand.toPlainString();
            getCalculation(operatorChar);
            historyPane.setText(temp + " " + this.operatorChar + " " + secondOperand.stripTrailingZeros().toPlainString() + " =");

            if (calculatorState != CalculatorState.ERROR)
                historyPane.setText(historyPane.getText().substring(0, historyPane.getText().length() - 2) + " =");

        }

        //if(calculatorState != CalculatorState.OPERATION_KEY_PRESSED)
        displayCurrentInput();
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

    private static String format(BigDecimal x)
    {
        NumberFormat formatter = new DecimalFormat("0.0E0");
        formatter.setRoundingMode(RoundingMode.HALF_UP);
        formatter.setMinimumFractionDigits((x.scale() > 0) ? x.precision() : x.scale());
        return formatter.format(x);
    }

    private void displayCurrentInput() {
        DecimalFormat formatter = new DecimalFormat("#,##0");
        boolean isEndWithDot = false;

        if(inputPane.getText().endsWith("."))
        {
            isEndWithDot = true;
        }

        inputPane.setText(formatter.format(currentInput));

        if (isEndWithDot) {
            inputPane.setText(inputPane.getText() + ".");
        }
    }
}
