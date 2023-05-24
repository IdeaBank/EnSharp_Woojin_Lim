package controller;

import actions.MainFrameActions;
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
import java.security.Key;

public class CalculatorStart {
    private MainFrame mainFrame;
    private CalculatorManager calculatorManager;
    private MainFrameActions mainFrameActions;

    public CalculatorStart() {
        this.calculatorManager = new CalculatorManager();
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

        StyleConstants.setFontSize(attributeSet, 48);
        StyleConstants.setForeground(attributeSet, Color.black);
        inputPane.setParagraphAttributes(attributeSet, true);

        MainFrameActions.getInstance().setCalculatorManager(calculatorManager);

        MainFrameActions.getInstance().addKeyboardInputAction();
        MainFrameActions.getInstance().addButtonAction(mainFrame.getCalculatorForm().getNumberButtons(), mainFrame.getCalculatorForm().getOperatorButtons());
    }


}
