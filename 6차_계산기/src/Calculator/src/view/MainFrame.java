package view;

import javax.swing.*;
import java.awt.*;

public class MainFrame extends JFrame {
    private CalculatorForm calculatorForm;

    public MainFrame()
    {
        calculatorForm = new CalculatorForm();
        calculatorForm.setFocusable(false);
        setContentPane(calculatorForm.getMainPanel());
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(600, 700));
        setVisible(true);
    }

    public CalculatorForm getCalculatorForm()
    {
        return this.calculatorForm;
    }
}
