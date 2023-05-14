package controller;

import constant.CalculatorSymbols;
import model.CalculatorHistory;

import javax.swing.*;
import java.util.ArrayList;

public class CalculatorManager {
    private ArrayList<CalculatorHistory> calculatorHistories;
    private JList historyList;
    private JTextPane historyPane;
    private JTextPane inputPane;
    private JPanel glassPane;

    public CalculatorManager() {
        this.calculatorHistories = new ArrayList<>();
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
    }

    public void clear() {
        historyPane.setText("");
        inputPane.setText("0");
    }

    public void deleteEntry() {
        System.out.println("DEL");
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
        System.out.println("+/-");
    }

    public void appendZero() {
        appendNumber('0');
    }

    public void appendNumber(char ch) {
        System.out.println(ch);
    }

    public void appendDot() {
        System.out.println(isGlassPaneVisible());
    }

    public void calculate() {
        System.out.println("=");
    }
}
