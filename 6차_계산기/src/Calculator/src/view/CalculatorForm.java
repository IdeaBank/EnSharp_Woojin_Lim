package view;

import customizedComponent.JRoundButton;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class CalculatorForm extends JFrame {
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
    private JPanel historyStringPanel;
    private JLabel historyString;
    private JPanel inputStringPanel;
    private JLabel inputString;
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
    private ArrayList<JRoundButton> numberButtons;
    private ArrayList<JRoundButton> operatorButtons;

    public JPanel getMainPanel()
    {
        return this.mainPanel;
    }

    public void createUIComponents()
    {
        numberButtons = new ArrayList<>();
        operatorButtons = new ArrayList<>();


        ceButton = new JRoundButton();
        cButton = new JRoundButton();
        deleteButton = new JRoundButton();
        divideButton = new JRoundButton();
        sevenButton = new JRoundButton();
        eightButton = new JRoundButton();
        nineButton = new JRoundButton();
        multiplyButton = new JRoundButton();
        fourButton = new JRoundButton();
        fiveButton = new JRoundButton();
        sixButton = new JRoundButton();
        minusButton = new JRoundButton();
        oneButton = new JRoundButton();
        twoButton = new JRoundButton();
        threeButton = new JRoundButton();
        plusButton = new JRoundButton();
        plusOrMinusButton = new JRoundButton();
        zeroButton = new JRoundButton();
        dotButton = new JRoundButton();
        equalsButton = new JRoundButton();

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

        for(JRoundButton button: numberButtons)
        {
            button.setColor(Color.WHITE);
        }
    }
}
