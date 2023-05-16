package view;

import constant.CalculatorSymbols;
import customizedComponent.JRoundButton;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class CalculatorForm extends JFrame {

    public JPanel getMainPanel() {
        return mainPanel;
    }

    public JLabel getHistoryButton() {
        return historyButton;
    }

    public JTextPane getHistoryPane() {
        return historyPane;
    }

    public JTextPane getInputPane() {
        return inputPane;
    }

    public ArrayList<JRoundButton> getNumberButtons() {
        return numberButtons;
    }

    public ArrayList<JRoundButton> getOperatorButtons() {
        return operatorButtons;
    }

    private JPanel mainPanel;
    private JPanel inputAndResultPanel;
    private JPanel buttonsPanel;
    private JPanel minusButtonPanel;
    private JPanel oneButtonPanel;
    private JPanel twoButtonPanel;
    private JPanel threeButtonPanel;
    private JPanel plusButtonPanel;
    private JPanel plusOrMinusButtonPanel;
    private JPanel zeroButtonPanel;
    private JPanel dotButtonPanel;
    private JPanel equalsButtonPanel;
    private JLabel historyButton;
    private JPanel historyButtonPanel;
    private JPanel historyPanePanel;
    private JPanel inputPanePanel;
    private JPanel ceButtonPanel;
    private JPanel cButtonPanel;
    private JPanel deleteButtonPanel;
    private JPanel divideButtonPanel;
    private JPanel sevenButtonPanel;
    private JPanel eightButtonPanel;
    private JPanel nineButtonPanel;
    private JPanel multiplyButtonPanel;
    private JPanel fourButtonPanel;
    private JPanel fiveButtonPanel;
    private JPanel sixButtonPanel;

    private JRoundButton ceButton;
    private JRoundButton cButton;
    private JRoundButton deleteButton;
    private JRoundButton divideButton;
    private JRoundButton sevenButton;
    private JRoundButton eightButton;
    private JRoundButton nineButton;
    private JRoundButton multiplyButton;
    private JRoundButton fourButton;
    private JRoundButton fiveButton;
    private JRoundButton sixButton;
    private JRoundButton minusButton;
    private JRoundButton oneButton;
    private JRoundButton twoButton;
    private JRoundButton threeButton;
    private JRoundButton plusButton;
    private JRoundButton plusOrMinusButton;
    private JRoundButton zeroButton;
    private JRoundButton dotButton;
    private JRoundButton equalsButton;
    private JTextPane historyPane;
    private JTextPane inputPane;
    private ArrayList<JRoundButton> numberButtons;
    private ArrayList<JRoundButton> operatorButtons;


    public void createUIComponents() {
        this.numberButtons = new ArrayList<>();
        this.operatorButtons = new ArrayList<>();

        this.ceButton = new JRoundButton();
        ceButton.setText(CalculatorSymbols.CE);

        this.cButton = new JRoundButton();
        cButton.setText(CalculatorSymbols.C);

        this.deleteButton = new JRoundButton();
        deleteButton.setText(CalculatorSymbols.DEL);

        this.divideButton = new JRoundButton();
        divideButton.setText(CalculatorSymbols.DIVIDE_SYMBOL);

        this.sevenButton = new JRoundButton();
        sevenButton.setText(CalculatorSymbols.SEVEN);

        this.eightButton = new JRoundButton();
        eightButton.setText(CalculatorSymbols.EIGHT);

        this.nineButton = new JRoundButton();
        nineButton.setText(CalculatorSymbols.NINE);

        this.multiplyButton = new JRoundButton();
        multiplyButton.setText(CalculatorSymbols.MULTIPLY_SYMBOL);

        this.fourButton = new JRoundButton();
        fourButton.setText(CalculatorSymbols.FOUR);

        this.fiveButton = new JRoundButton();
        fiveButton.setText(CalculatorSymbols.FIVE);

        this.sixButton = new JRoundButton();
        sixButton.setText(CalculatorSymbols.SIX);

        this.minusButton = new JRoundButton();
        minusButton.setText(CalculatorSymbols.SUBTRACT);

        this.oneButton = new JRoundButton();
        oneButton.setText(CalculatorSymbols.ONE);

        this.twoButton = new JRoundButton();
        twoButton.setText(CalculatorSymbols.TWO);

        this.threeButton = new JRoundButton();
        threeButton.setText(CalculatorSymbols.THREE);

        this.plusButton = new JRoundButton();
        plusButton.setText(CalculatorSymbols.ADD);

        this.plusOrMinusButton = new JRoundButton();
        plusOrMinusButton.setText(CalculatorSymbols.NEGATE);

        this.zeroButton = new JRoundButton();
        zeroButton.setText(CalculatorSymbols.ZERO);

        this.dotButton = new JRoundButton();
        dotButton.setText(CalculatorSymbols.DOT);

        this.equalsButton = new JRoundButton();
        equalsButton.setText(CalculatorSymbols.EQUALS);

        numberButtons.add(zeroButton);
        numberButtons.add(oneButton);
        numberButtons.add(twoButton);
        numberButtons.add(threeButton);
        numberButtons.add(fourButton);
        numberButtons.add(fiveButton);
        numberButtons.add(sixButton);
        numberButtons.add(sevenButton);
        numberButtons.add(eightButton);
        numberButtons.add(nineButton);

        operatorButtons.add(ceButton);
        operatorButtons.add(cButton);
        operatorButtons.add(deleteButton);
        operatorButtons.add(divideButton);
        operatorButtons.add(multiplyButton);
        operatorButtons.add(minusButton);
        operatorButtons.add(plusButton);
        operatorButtons.add(plusOrMinusButton);
        operatorButtons.add(dotButton);
        operatorButtons.add(equalsButton);

        for (JRoundButton button : numberButtons) {
            button.setColor(Color.WHITE);
            button.setFont(new Font("Arial", Font.PLAIN, 20));
        }

        for (JRoundButton button : operatorButtons) {
            button.setFont(new Font("Arial", Font.PLAIN, 20));
        }

        equalsButton.setForeground(Color.WHITE);
        equalsButton.setColor(new Color(31, 31, 150));
        equalsButton.setColorOver(new Color(31, 31, 136));
        equalsButton.setColorClick(new Color(31, 31, 120));
    }
}
