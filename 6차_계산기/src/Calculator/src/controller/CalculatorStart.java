package controller;

import constant.CalculatorSymbols;
import customizedComponent.JRoundButton;
import view.MainFrame;

import javax.swing.*;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import java.awt.*;
import java.awt.event.KeyEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class CalculatorStart {
    MainFrame mainFrame;
    CalculatorManager calculatorManager;

    public CalculatorStart() {
        calculatorManager = new CalculatorManager();
    }

    public void start() {
        mainFrame = new MainFrame();
        JTextPane historyPane = mainFrame.getCalculatorForm().getHistoryPane();
        JTextPane inputPane = mainFrame.getCalculatorForm().getInputPane();

        calculatorManager.setHistoryList(mainFrame.getHistoryForm().getHistoryList());
        calculatorManager.setHistoryPane(historyPane);
        calculatorManager.setInputPane(inputPane);

        SimpleAttributeSet attribs = new SimpleAttributeSet();
        StyleConstants.setAlignment(attribs, StyleConstants.ALIGN_RIGHT);
        StyleConstants.setFontFamily(attribs, "Malgun Gothic");
        StyleConstants.setFontSize(attribs, 24);
        historyPane.setParagraphAttributes(attribs, true);


        StyleConstants.setFontSize(attribs, 36);
        inputPane.setParagraphAttributes(attribs, true);

        KeyboardFocusManager manager = KeyboardFocusManager.getCurrentKeyboardFocusManager();
        manager.addKeyEventDispatcher(new KeyEventDispatcher() {
            @Override
            public boolean dispatchKeyEvent(KeyEvent e) {
                if (e.getID() == KeyEvent.KEY_RELEASED) {
                    handleKeyRelease(e);
                }

                return false;
            }
        });

        for(JRoundButton button : mainFrame.getCalculatorForm().getNumberButtons())
        {
            button.addMouseListener(new MouseAdapter() {
                @Override
                public void mouseReleased(MouseEvent e) {
                    handleButtonPressed(button.getText());
                }
            });
        }

        for(JRoundButton button : mainFrame.getCalculatorForm().getOperatorButtons())
        {
            button.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                handleButtonPressed(button.getText());
            }
        });
        }
    }

    private void handleKeyRelease(KeyEvent e) {
        switch(e.getKeyChar())
        {
            case '0':
                calculatorManager.appendZero();
                break;
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                calculatorManager.appendNumber(e.getKeyChar());
                break;
            case '+':
                calculatorManager.add();
                break;
            case '-':
                calculatorManager.subtract();
                break;
            case '*':
                calculatorManager.multiply();
                break;
            case '/':
                calculatorManager.divide();
                break;
            case '=':
                calculatorManager.calculate();
                break;
            case '.':
                calculatorManager.appendDot();
                break;
        }

        if(e.getKeyCode() == KeyEvent.VK_ESCAPE)
        {
            System.out.println("C");
        }

        else if(e.getKeyCode() == KeyEvent.VK_DELETE)
        {
            System.out.println("CE");
        }

        else if(e.getKeyCode() == KeyEvent.VK_BACK_SPACE)
        {
            System.out.println("DEL");
        }
    }

    private void handleButtonPressed(String text)
    {
        switch(text)
        {
            case CalculatorSymbols.CE:
                calculatorManager.clearEntry();
                break;
            case CalculatorSymbols.C:
                calculatorManager.clear();
                break;
            case CalculatorSymbols.DEL:
                calculatorManager.deleteEntry();
                break;
            case CalculatorSymbols.DIVIDE:
                calculatorManager.divide();
                break;
            case CalculatorSymbols.MULTIPLY:
                calculatorManager.multiply();
                break;
            case CalculatorSymbols.SUBTRACT:
                calculatorManager.subtract();
                break;
            case CalculatorSymbols.ADD:
                calculatorManager.add();
                break;
            case CalculatorSymbols.EQUALS:
                calculatorManager.calculate();
                break;
            case CalculatorSymbols.PLUS_OR_MINUS:
                calculatorManager.negate();
                break;

            case CalculatorSymbols.DOT:
                calculatorManager.appendDot();
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
                calculatorManager.appendNumber(text.charAt(0));
                break;
            case CalculatorSymbols.ZERO:
                calculatorManager.appendZero();
                break;
        }
    }
}
