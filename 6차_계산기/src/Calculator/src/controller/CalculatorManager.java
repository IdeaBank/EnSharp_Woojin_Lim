package controller;

import constant.CalculatorState;
import constant.CalculatorSymbols;
import model.CalculatorHistory;

import javax.swing.*;
import javax.swing.text.NumberFormatter;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.math.RoundingMode;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.ArrayList;

public class CalculatorManager {
    private ArrayList<CalculatorHistory> calculatorHistories;
    private JList historyList;
    private JTextPane historyPane;
    private JTextPane inputPane;
    private JPanel glassPane;
    private CalculatorState calculatorState;
    private BigDecimal currentInput;

    public CalculatorManager() {
        this.calculatorHistories = new ArrayList<>();
        this.calculatorState = CalculatorState.START;
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

    public void setGlassPanel(JPanel glassPane)
    {
        this.glassPane = glassPane;
    }

    public boolean isGlassPaneVisible()
    {
        if(glassPane.isVisible())
        {
            return true;
        }

        return false;
    }

    public void handleButtonPressed(String text)
    {
        switch(text)
        {
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
                divide();
                break;
            case CalculatorSymbols.MULTIPLY:
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
        historyPane.setText("");
        inputPane.setText("0");
        currentInput = new BigDecimal(0);
    }

    public void deleteEntry() {
        if(!inputPane.getText().equals("0") && inputPane.getText().length() > 1)
        {
            String tempInput = currentInput.toString();

            if(tempInput.charAt(tempInput.length() - 2) == '.')
            {
                tempInput = tempInput.substring(0, tempInput.length() - 1);
            } else {
                tempInput = tempInput.substring(0, tempInput.length());
            }

            currentInput = new BigDecimal(tempInput);
        }

        else if(inputPane.getText().length() == 1)
        {
            currentInput = new BigDecimal(0);
        }

        inputPane.setText(getFormattedNumber(currentInput));
    }

    public void divide() {
        System.out.println("DIVIDE");
    }

    public void multiply() {
        System.out.println("X");
    }

    public void subtract() {
        System.out.println("-");
    }

    public void add() {
        System.out.println("+");
    }

    public void negate() {
        currentInput = currentInput.negate();

        inputPane.setText(getFormattedNumber(currentInput));
    }

    public void appendZero() {
        appendNumber('0');
    }

    public void appendNumber(char ch) {
        if(calculatorState == CalculatorState.START || calculatorState == CalculatorState.NUMBER_KEY_PRESSED)
        {
            if(inputPane.getText().equals("0")) {
                currentInput = new BigDecimal(ch);
                inputPane.setText(getFormattedNumber(currentInput));
            }

            else {
                if((inputPane.getText().length() < 16 && inputPane.getText().contains("."))
                        || (inputPane.getText().length() < 15 && !inputPane.getText().contains(".")))
                {
                    currentInput = new BigDecimal(currentInput.toString() + ch);
                    inputPane.setText(getFormattedNumber(currentInput));
                }
            }
        }

        else if(calculatorState == CalculatorState.ENTER_KEY_PRESSED)
        {
            historyPane.setText("");
            currentInput = new BigDecimal(ch);

            inputPane.setText(getFormattedNumber(currentInput));
        }

        calculatorState = CalculatorState.NUMBER_KEY_PRESSED;
    }

    public void appendDot() {
        if(!inputPane.getText().contains("."))
        {
            inputPane.setText(inputPane.getText() + ".");
        }
    }

    public void calculate() {
        if(calculatorState == CalculatorState.NUMBER_KEY_PRESSED)
        {
            historyPane.setText(inputPane.getText() + " =");
        }

        calculatorState = CalculatorState.ENTER_KEY_PRESSED;
    }

    private String getFormattedNumber(BigDecimal bigDecimal)
    {
        if(isInteger(bigDecimal))
        {
            return bigDecimal.toString();
        }

        NumberFormat formatter = new DecimalFormat("0.0E0");
        formatter.setRoundingMode(RoundingMode.HALF_UP);
        formatter.setMinimumFractionDigits((bigDecimal.scale() > 16) ? bigDecimal.precision() : bigDecimal.scale());
        return formatter.format(bigDecimal);
    }

    private boolean isInteger(BigDecimal input) {
        return input.stripTrailingZeros().scale() <= 0;
    }

    private boolean isDecimalOverLimit(BigDecimal input) {
        return input.stripTrailingZeros().scale() > 16;
    }
}
