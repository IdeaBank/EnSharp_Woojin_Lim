package view;

import javax.swing.*;

public class CalculationLog  extends JPanel {

    private JTextField historyText;
    private JTextField resultText;
    public JTextField getHistoryText() {
        return historyText;
    }

    public JTextField getResultText() {
        return resultText;
    }

    public CalculationLog()
    {
        this.historyText = new JTextField();
        this.resultText = new JTextField();
    }
}
