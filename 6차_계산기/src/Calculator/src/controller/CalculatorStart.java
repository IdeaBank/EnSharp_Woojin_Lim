package controller;

import constant.CalculatorState;
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
    private MainFrame mainFrame;
    private CalculatorManager calculatorManager;
    private CalculatorState calculatorState;

    public CalculatorStart() {
        this.calculatorManager = new CalculatorManager();
        this.calculatorState = CalculatorState.START;
    }

    public void start() {
        this.mainFrame = new MainFrame();
        mainFrame.setLocationRelativeTo(null);
        JTextPane historyPane = mainFrame.getCalculatorForm().getHistoryPane();
        JTextPane inputPane = mainFrame.getCalculatorForm().getInputPane();
        JPanel glassPane = (JPanel)mainFrame.getGlassPane();

        calculatorManager.setHistoryList(mainFrame.getHistoryForm().getHistoryList());
        calculatorManager.setHistoryPane(historyPane);
        calculatorManager.setInputPane(inputPane);
        calculatorManager.setGlassPanel(glassPane);

        SimpleAttributeSet attributeSet = new SimpleAttributeSet();
        StyleConstants.setAlignment(attributeSet, StyleConstants.ALIGN_RIGHT);
        StyleConstants.setFontFamily(attributeSet, "Malgun Gothic");
        StyleConstants.setFontSize(attributeSet, 24);
        StyleConstants.setForeground(attributeSet, Color.gray);
        historyPane.setParagraphAttributes(attributeSet, true);


        StyleConstants.setFontSize(attributeSet, 36);
        StyleConstants.setForeground(attributeSet, Color.black);
        inputPane.setParagraphAttributes(attributeSet, true);

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
                    calculatorManager.handleButtonPressed(button.getText());
                }
            });
        }

        for(JRoundButton button : mainFrame.getCalculatorForm().getOperatorButtons())
        {
            button.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                calculatorManager.handleButtonPressed(button.getText());
            }
        });
        }
    }

    private void handleKeyRelease(KeyEvent e) {
        switch(e.getKeyChar())
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '+':
            case '-':
            case '*':
            case '/':
            case '=':
            case '.':
                calculatorManager.handleButtonPressed(String.valueOf(e.getKeyChar()));
                break;
        }

        if(e.getKeyCode() == KeyEvent.VK_ESCAPE)
        {
            calculatorManager.handleButtonPressed(CalculatorSymbols.C);
        }

        else if(e.getKeyCode() == KeyEvent.VK_DELETE)
        {
            calculatorManager.handleButtonPressed(CalculatorSymbols.CE);
        }

        else if(e.getKeyCode() == KeyEvent.VK_BACK_SPACE)
        {
            calculatorManager.handleButtonPressed(CalculatorSymbols.DEL);
        }
    }
}
