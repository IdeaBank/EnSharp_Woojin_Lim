package view;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class MainFrame extends JFrame {
    private final CalculatorForm calculatorForm;
    private final HistoryForm historyForm;

    public MainFrame()
    {
        calculatorForm = new CalculatorForm();
        calculatorForm.setFocusable(false);
        setContentPane(calculatorForm.getMainPanel());
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(600, 700));
        setVisible(true);

        JPanel glassPane = (JPanel)getGlassPane();
        historyForm = new HistoryForm();

        glassPane.setLayout(new GridLayout(2, 1));
        JPanel transparentPanel = new JPanel();
        transparentPanel.setBackground(new Color(0, 0, 0, 178));
        glassPane.add(transparentPanel);
        glassPane.add(historyForm.getHistoryPanel());
        glassPane.setVisible(true);

        transparentPanel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                glassPane.setVisible(false);
            }
        });

        calculatorForm.getHistoryButton().addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                glassPane.setVisible(true);
            }
        });
    }

    public CalculatorForm getCalculatorForm()
    {
        return this.calculatorForm;
    }

    public void showHistoryPane()
    {
        getGlassPane().setVisible(true);
    }

    public void removeHistoryPane()
    {
        getGlassPane().setVisible(false);
    }

    public boolean isHistoryPaneOpened()
    {
        if(getGlassPane().isVisible())
        {
            return true;
        }

        return false;
    }
}
