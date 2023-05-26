package controller;

import actions.MainFrameActions;
import dispatcher.CalculatorDispatcher;
import view.MainFrame;

import javax.swing.*;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import java.awt.*;

public class CalculatorStart {
    private MainFrame mainFrame;
    private CalculatorDispatcher calculatorDispatcher;
    private MainFrameActions mainFrameActions;

    public CalculatorStart() {
        this.calculatorDispatcher = new CalculatorDispatcher();
    }

    public void start() {
        this.mainFrame = new MainFrame();
        mainFrame.setLocationRelativeTo(null);
        JTextPane historyPane = mainFrame.getCalculatorForm().getHistoryPane();
        JTextPane inputPane = mainFrame.getCalculatorForm().getInputPane();
        JPanel glassPane = (JPanel)mainFrame.getGlassPane();

        calculatorDispatcher.setHistoryList(mainFrame.getHistoryForm().getHistoryList());
        calculatorDispatcher.setHistoryPane(historyPane);
        calculatorDispatcher.setInputPane(inputPane);
        calculatorDispatcher.setGlassPanel(glassPane);

        SimpleAttributeSet attributeSet = new SimpleAttributeSet();
        StyleConstants.setAlignment(attributeSet, StyleConstants.ALIGN_RIGHT);
        StyleConstants.setFontFamily(attributeSet, "Malgun Gothic");
        StyleConstants.setFontSize(attributeSet, 24);
        StyleConstants.setForeground(attributeSet, Color.gray);
        historyPane.setParagraphAttributes(attributeSet, true);

        StyleConstants.setFontSize(attributeSet, 56);
        StyleConstants.setForeground(attributeSet, Color.black);
        inputPane.setParagraphAttributes(attributeSet, true);

        MainFrameActions.getInstance().setCalculatorManager(calculatorDispatcher);

        MainFrameActions.getInstance().addKeyboardInputAction();
        MainFrameActions.getInstance().addButtonAction(mainFrame.getCalculatorForm().getNumberButtons(), mainFrame.getCalculatorForm().getOperatorButtons());
    }


}
