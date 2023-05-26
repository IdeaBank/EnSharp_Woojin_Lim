package view;

import actions.MainFrameActions;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;

public class MainFrame extends JFrame {
    private final CalculatorForm calculatorForm;
    private final HistoryForm historyForm;

    public MainFrame()
    {
        this.calculatorForm = new CalculatorForm();

        BufferedImage historyIcon = null;
        try{
            historyIcon = ImageIO.read(new File("image/history_icon.png"));
        } catch(Exception e) {
            System.out.println(e.toString());
        }

        Image dimg = historyIcon.getScaledInstance(20, 20, Image.SCALE_SMOOTH);

        ImageIcon imageIcon = new ImageIcon(dimg);

        calculatorForm.getHistoryButton().setIcon(imageIcon);

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

        MainFrameActions.getInstance().addTransparentPanelAction(transparentPanel, glassPane);
        MainFrameActions.getInstance().addHistoryButtonAction(getCalculatorForm().getHistoryButton(), glassPane);
        MainFrameActions.getInstance().addMainFrameAction(this, glassPane);
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
