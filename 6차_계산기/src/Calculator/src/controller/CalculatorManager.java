package controller;

import model.CalculatorHistory;

import javax.swing.*;
import java.util.ArrayList;

public class CalculatorManager {
    private ArrayList<CalculatorHistory> calculatorHistories;
    private JList historyList;
    private JTextPane historyPane;
    private JTextPane inputPane;

    public void setHistoryList(JList historyList) {
        this.historyList = historyList;
    }

    public void setHistoryPane(JTextPane historyPane) {
        this.historyPane = historyPane;
    }

    public void setInputPane(JTextPane inputPane) {
        this.inputPane = inputPane;
    }

    public CalculatorManager()
    {

    }

    public CalculatorManager(JList historyList, JTextPane historyPane, JTextPane inputPane)
    {
        calculatorHistories = new ArrayList<>();
        this.historyList = historyList;
        this.historyPane = historyPane;
        this.inputPane = inputPane;
    }

    public void clearEntry()
    {
        System.out.println("CE");
    }

    public void clear()
    {
        System.out.println("C");
    }

    public void deleteEntry()
    {
        System.out.println("DEL");
    }

    public void divide()
    {
        System.out.println("DIVIDE");
    }

    public void multiply()
    {
        System.out.println("X");
    }

    public void subtract()
    {
        System.out.println("-");
    }

    public void add()
    {
        System.out.println("+");
    }

    public void negate()
    {
        System.out.println("+/-");
    }

    public void appendZero()
    {
        appendNumber('0');
    }

    public void appendNumber(char ch)
    {
        System.out.println(ch);
    }

    public void appendDot()
    {
        System.out.println(".");
    }

    public void calculate()
    {
        System.out.println("=");
    }
}
