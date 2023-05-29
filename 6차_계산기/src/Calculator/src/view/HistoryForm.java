package view;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

public class HistoryForm {
    private JPanel historyPanel;
    private JLabel historyClearLabel;
    private JPanel historyClearPanel;
    private JPanel logPanel;
    private DefaultListModel logList;
    private JList historyList;

    public HistoryForm() {
        GridBagConstraints c = new GridBagConstraints();

        historyPanel = new JPanel(new GridBagLayout());
        historyClearPanel = new JPanel();
        historyClearLabel = new JLabel();
        historyClearLabel.setText("clear");
        logPanel = new JPanel();

        logList = new DefaultListModel();

        historyList = new JList(logList);
        historyList.setModel(logList);

        JScrollPane jScrollPane = new JScrollPane(historyList);
        logPanel.add(jScrollPane);

        historyClearPanel.add(historyClearLabel);

        c.gridx = 0;
        c.gridy = 0;
        c.weighty = 0.9;
        c.fill = GridBagConstraints.HORIZONTAL;

        historyPanel.add(logPanel, c);

        c.gridx = 0;
        c.gridy = 1;
        c.weighty = 0.1;
        historyPanel.add(historyClearPanel, c);
    }

    public JPanel getHistoryPanel() {
        return historyPanel;
    }

    public JList getHistoryList() {
        return historyList;
    }

    public JLabel getHistoryClearLabel() {
        return historyClearLabel;
    }

    public DefaultListModel getLogList() {
        return logList;
    }

    public void createUIComponents() {
        
    }
}
