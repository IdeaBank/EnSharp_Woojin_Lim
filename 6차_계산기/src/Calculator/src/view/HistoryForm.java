package view;

import javax.swing.*;

public class HistoryForm {
    private JPanel historyPanel;
    private JList historyList;
    private JScrollPane historyScrollPane;
    private JLabel historyClearLabel;
    private JPanel historyClearPanel;

    public HistoryForm()
    {
        historyScrollPane = new JScrollPane();
        historyList = new JList();
    }

    public JPanel getHistoryPanel()
    {
        return historyPanel;
    }

    public JList getHistoryList()
    {
        return historyList;
    }

    public void createUIComponents() {
        
    }
}
