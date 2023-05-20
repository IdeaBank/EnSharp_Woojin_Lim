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
    private BigDecimal result;

    public CalculatorManager() {
        this.calculatorHistories = new ArrayList<>();
        this.calculatorState = CalculatorState.START;
        this.firstOperand = null;
        this.operatorChar = '\0';
        this.secondOperand = null;
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
        if (calculatorState == CalculatorState.ENTER_KEY_PRESSED || calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            historyPane.setText("");
            firstOperand = null;
            return;
        } else if (calculatorState == CalculatorState.ERROR) {
            clear();
        } else if(calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            String inputString = inputPane.getText();

            if (inputString.length() == 1 || (inputString.length() == 2 && inputString.startsWith("-"))) {
                inputString = "0";
            } else {
                inputString = inputString.substring(0, inputString.length() - 1);
            }

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
            firstOperand = firstOperand.divide(secondOperand, 10000, RoundingMode.HALF_UP);
        }
    }

    public void getCalculation(char operatorChar) {
        if (calculatorState == CalculatorState.ERROR) {
            return;
        } else if (calculatorState == CalculatorState.NUMBER_KEY_PRESSED) {
            if (firstOperand == null) {
                firstOperand = getInputDecimal();
                historyPane.setText(firstOperand.setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString() + " " + operatorChar);
            } else {
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

                if(calculatorState != CalculatorState.START) {

                    inputPane.setText(firstOperand.toPlainString());
                    historyPane.setText(firstOperand.stripTrailingZeros().setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString() + " " + operatorChar);

                    calculatorState = CalculatorState.OPERATION_KEY_PRESSED;
                    displayCurrentInput();
                    this.operatorChar = operatorChar;
                }
                return;
            }
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
                if(firstOperand == null) {
                    firstOperand = getInputDecimal();
                    inputPane.setText(firstOperand.toPlainString());

                    this.operatorChar = operatorChar;

                    historyPane.setText(firstOperand.stripTrailingZeros().setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString() + " " + operatorChar);
                }

                else{
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

                    inputPane.setText(firstOperand.stripTrailingZeros().toString());
                }
        } else if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            this.operatorChar = operatorChar;
            historyPane.setText(firstOperand.stripTrailingZeros().setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString() + " " + operatorChar);

            if(secondOperand != null)
            {
                String temp = firstOperand.stripTrailingZeros().toPlainString();

                switch (this.operatorChar) {
                    case CalculatorSymbols.ADD_CHAR:
                        firstOperand = firstOperand.add(secondOperand);
                        break;
                    case CalculatorSymbols.SUBTRACT_CHAR:
                        firstOperand = firstOperand.subtract(secondOperand);
                        firstOperand = firstOperand.subtract(secondOperand);
                        break;
                    case CalculatorSymbols.MULTIPLY_CHAR:
                        firstOperand = firstOperand.multiply(secondOperand, MathContext.UNLIMITED);
                        break;
                    case CalculatorSymbols.DIVIDE_CHAR:
                        divide();
                        break;
                }

                historyPane.setText(temp + " " + this.operatorChar + " " + secondOperand + " =");
                inputPane.setText(firstOperand.stripTrailingZeros().toPlainString());
            }

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
    }

    public void appendZero() {
        appendNumber('0');
    }

    public void appendNumber(char inputNumber) {
        if (calculatorState == CalculatorState.OPERATION_KEY_PRESSED) {
            secondOperand = null;
            inputPane.setText(String.valueOf(inputNumber));
        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            inputPane.setText(String.valueOf(inputNumber));
            firstOperand = null;

            if (operatorChar != '\0') {
                historyPane.setText("");
            }
        } else if (calculatorState == CalculatorState.ERROR) {
            clear();
            inputPane.setText(String.valueOf(inputNumber));
        } else {
            String tempString = String.join("", inputPane.getText().split(","));
            tempString = String.join("", tempString.split("[.]"));

            if (tempString.length() > 15) {
                return;
            }

            if (inputPane.getText().equals("0")) {
                inputPane.setText(String.valueOf(inputNumber));
            } else {
                inputPane.setText(inputPane.getText() + inputNumber);
            }
        }

        displayCurrentInput();
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
                historyPane.setText(inputPane.getText() + " =");
                calculatorState = CalculatorState.ENTER_KEY_PRESSED;
            } else {
                String temp;

                if(firstOperand != null) {
                    temp = firstOperand.setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString();
                    secondOperand = getInputDecimal();

                }

                else{
                    temp = getInputDecimal().stripTrailingZeros().toPlainString();
                }

                getCalculation(operatorChar);

                if (calculatorState != CalculatorState.ERROR) {
                    historyPane.setText(temp + " " + this.operatorChar + " " + secondOperand.stripTrailingZeros().setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString() + " =");
                }

            }

        } else if (calculatorState == CalculatorState.ENTER_KEY_PRESSED) {
            String temp = getInputDecimal().toPlainString();

            if (calculatorState != CalculatorState.ERROR && operatorChar == '\0')
                historyPane.setText(historyPane.getText().substring(0, historyPane.getText().length() - 2) + " =");


            else {
                firstOperand = getInputDecimal();
                getCalculation(operatorChar);
                historyPane.setText(temp + " " + this.operatorChar + " " + secondOperand.stripTrailingZeros().setScale(16, RoundingMode.HALF_UP).stripTrailingZeros().toPlainString() + " =");
            }

            firstOperand = null;
        }

        else if(calculatorState == CalculatorState.OPERATION_KEY_PRESSED)
        {
            secondOperand = getInputDecimal();
            getCalculation(this.operatorChar);

        }

        if (calculatorState != CalculatorState.ERROR) {
            displayCurrentInput();
        }
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
        BigDecimal currentInput = new BigDecimal(String.join("", inputPane.getText().split(","))).stripTrailingZeros();

        DecimalFormat formatter;
        if (currentInput.scale() <= 0) {
            formatter = new DecimalFormat("#,##0");
        } else {
            formatter = new DecimalFormat("#,##0." + "0".repeat(currentInput.scale()));
        }
        boolean isEndWithDot = false;

        if (inputPane.getText().endsWith(".") && currentInput.scale() == 0) {
            isEndWithDot = true;
        }

        String result = formatter.format(currentInput);

        if(result.length() > 18) {
            inputPane.setText(result.substring(0, 18));
        }else {
            inputPane.setText(result);
        }


        if (isEndWithDot) {
            inputPane.setText(inputPane.getText() + ".");
        }
    }

    private void displayCurrentInputWithDecimal(BigDecimal bigDecimal) {
        BigDecimal currentInput = bigDecimal;

        DecimalFormat formatter;
        if (currentInput.scale() == 0) {
            formatter = new DecimalFormat("#,##0");
        } else {
            formatter = new DecimalFormat("#,##0." + "0".repeat(currentInput.scale()));
        }
        boolean isEndWithDot = false;

        if (inputPane.getText().endsWith(".") && currentInput.scale() == 0) {
            isEndWithDot = true;
        }

        inputPane.setText(formatter.format(currentInput));

        if (isEndWithDot) {
            inputPane.setText(inputPane.getText() + ".");
        }
    }

    public BigDecimal getInputDecimal() {
        return new BigDecimal(String.join("", inputPane.getText().split(","))).stripTrailingZeros();
    }
}
