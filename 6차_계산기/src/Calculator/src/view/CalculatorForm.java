package view;

import customizedComponent.JRoundButton;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class CalculatorForm extends JFrame {

    public JPanel getMainPanel() {
        return mainPanel;
    }
    public JPanel getInputAndResultPanel() {
        return inputAndResultPanel;
    }

    public JPanel getButtonsPanel() {
        return buttonsPanel;
    }

    public JPanel getCeButtonPanel() {
        return ceButtonPanel;
    }

    public JPanel getcButtonPanel() {
        return cButtonPanel;
    }

    public JPanel getDeleteButtonPanel() {
        return deleteButtonPanel;
    }

    public JPanel getDivideButtonPanel() {
        return divideButtonPanel;
    }

    public JPanel getSevenButtonPanel() {
        return sevenButtonPanel;
    }

    public JPanel getEightButtonPanel() {
        return eightButtonPanel;
    }

    public JPanel getNineButtonPanel() {
        return nineButtonPanel;
    }

    public JPanel getMultiplyButtonPanel() {
        return multiplyButtonPanel;
    }

    public JPanel getFourButtonPanel() {
        return fourButtonPanel;
    }

    public JPanel getFiveButtonPanel() {
        return fiveButtonPanel;
    }

    public JPanel getSixButtonPanel() {
        return sixButtonPanel;
    }

    public JPanel getMinusButtonPanel() {
        return minusButtonPanel;
    }

    public JPanel getOneButtonPanel() {
        return oneButtonPanel;
    }

    public JPanel getTwoButtonPanel() {
        return twoButtonPanel;
    }

    public JPanel getThreeButtonPanel() {
        return threeButtonPanel;
    }

    public JPanel getPlusButtonPanel() {
        return plusButtonPanel;
    }

    public JPanel getPlusOrMinusButtonPanel() {
        return plusOrMinusButtonPanel;
    }

    public JPanel getZeroButtonPanel() {
        return zeroButtonPanel;
    }

    public JPanel getDotButtonPanel() {
        return dotButtonPanel;
    }

    public JPanel getEqualsButtonPanel() {
        return equalsButtonPanel;
    }

    public JLabel getHistoryButton() {
        return historyButton;
    }

    public JPanel getHistoryButtonPanel() {
        return historyButtonPanel;
    }

    public JPanel getHistoryPanePanel() {
        return historyPanePanel;
    }

    public JPanel getInputPanePanel() {
        return inputPanePanel;
    }

    public JRoundButton getCeButton() {
        return ceButton;
    }

    public JRoundButton getcButton() {
        return cButton;
    }

    public JRoundButton getDeleteButton() {
        return deleteButton;
    }

    public JRoundButton getDivideButton() {
        return divideButton;
    }

    public JRoundButton getSevenButton() {
        return sevenButton;
    }

    public JRoundButton getEightButton() {
        return eightButton;
    }

    public JRoundButton getNineButton() {
        return nineButton;
    }

    public JRoundButton getMultiplyButton() {
        return multiplyButton;
    }

    public JRoundButton getFourButton() {
        return fourButton;
    }

    public JRoundButton getFiveButton() {
        return fiveButton;
    }

    public JRoundButton getSixButton() {
        return sixButton;
    }

    public JRoundButton getMinusButton() {
        return minusButton;
    }

    public JRoundButton getOneButton() {
        return oneButton;
    }

    public JRoundButton getTwoButton() {
        return twoButton;
    }

    public JRoundButton getThreeButton() {
        return threeButton;
    }

    public JRoundButton getPlusButton() {
        return plusButton;
    }

    public JRoundButton getPlusOrMinusButton() {
        return plusOrMinusButton;
    }

    public JRoundButton getZeroButton() {
        return zeroButton;
    }

    public JRoundButton getDotButton() {
        return dotButton;
    }

    public JRoundButton getEqualsButton() {
        return equalsButton;
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
        numberButtons = new ArrayList<>();
        operatorButtons = new ArrayList<>();

        ceButton = new JRoundButton();
        ceButton.setText("CE");

        cButton = new JRoundButton();
        cButton.setText("C");

        deleteButton = new JRoundButton();
        deleteButton.setText("DEL");

        divideButton = new JRoundButton();
        divideButton.setText("÷");

        sevenButton = new JRoundButton();
        sevenButton.setText("7");

        eightButton = new JRoundButton();
        eightButton.setText("8");

        nineButton = new JRoundButton();
        nineButton.setText("9");

        multiplyButton = new JRoundButton();
        multiplyButton.setText("X");

        fourButton = new JRoundButton();
        fourButton.setText("4");

        fiveButton = new JRoundButton();
        fiveButton.setText("5");

        sixButton = new JRoundButton();
        sixButton.setText("6");

        minusButton = new JRoundButton();
        minusButton.setText("-");

        oneButton = new JRoundButton();
        oneButton.setText("1");

        twoButton = new JRoundButton();
        twoButton.setText("2");

        threeButton = new JRoundButton();
        threeButton.setText("3");

        plusButton = new JRoundButton();
        plusButton.setText("+");

        plusOrMinusButton = new JRoundButton();
        plusOrMinusButton.setText("+/-");

        zeroButton = new JRoundButton();
        zeroButton.setText("0");

        dotButton = new JRoundButton();
        dotButton.setText(".");

        equalsButton = new JRoundButton();
        equalsButton.setText("=");

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
