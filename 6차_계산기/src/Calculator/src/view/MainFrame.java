package view;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class MainFrame extends JFrame {
    private final CalculatorForm calculatorForm;
    private final HistoryForm historyForm;

    public MainFrame()
    {
        this.calculatorForm = new CalculatorForm();
        calculatorForm.setFocusable(false);
        setContentPane(calculatorForm.getMainPanel());
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(420, 560));
        setVisible(true);

        JPanel glassPane = (JPanel)getGlassPane();
        this.historyForm = new HistoryForm();

        glassPane.setLayout(new GridLayout(2, 1));
        JPanel transparentPanel = new JPanel();
        transparentPanel.setBackground(new Color(0, 0, 0, 178));
        glassPane.add(transparentPanel);
        glassPane.add(historyForm.getHistoryPanel());
        glassPane.setVisible(false);

        transparentPanel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                glassPane.setVisible(false);
            }
        });

        calculatorForm.getHistoryButton().addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                glassPane.setVisible(true);
            }
        });

        addComponentListener(new ComponentAdapter() {
            public void componentResized(ComponentEvent componentEvent) {
                glassPane.setVisible(false);
            }
        });
    }

    public CalculatorForm getCalculatorForm()
    {
        return calculatorForm;
    }

    public HistoryForm getHistoryForm()
    {
        return historyForm;
    }

    public void showHistoryPane()
    {
        getGlassPane().setVisible(true);
    }

    public void removeHistoryPane()
    {
        getGlassPane().setVisible(false);
    }
}
