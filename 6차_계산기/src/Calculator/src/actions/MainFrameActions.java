package actions;

import com.sun.tools.javac.Main;
import constant.CalculatorState;
import constant.CalculatorSymbols;
import dispatcher.CalculatorDispatcher;
import customizedComponent.JRoundButton;
import store.DataStore;
import view.HistoryForm;
import view.MainFrame;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.lang.reflect.Array;
import java.math.BigDecimal;
import java.util.ArrayList;

public class MainFrameActions {
    private static MainFrameActions _instance;
    private CalculatorDispatcher calculatorDispatcher;
    private JPanel mainWithHistoryPanel;
    private JPanel mainPanel;
    private JList historyList;

    public static MainFrameActions getInstance()
    {
        if(_instance == null)
        {
            _instance = new MainFrameActions();
        }

        return _instance;
    }

    private MainFrameActions()
    {
    }

    public void setCalculatorManager(CalculatorDispatcher calculatorDispatcher) {
        this.calculatorDispatcher = calculatorDispatcher;
    }

    public void setHistoryList(JList historyList)
    {
        this.historyList = historyList;
    }

    public void addTransparentPanelAction(JPanel transparentPanel, JPanel glassPane)
    {
        transparentPanel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                glassPane.setVisible(false);
            }
        });
    }

    public void addHistoryButtonAction(JLabel historyButton, JPanel glassPane)
    {
        historyButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                glassPane.setVisible(true);
            }
        });
    }

    public void addMainFrameAction(JFrame mainFrame, JPanel glassPane)
    {
        mainFrame.addComponentListener(new ComponentAdapter() {
            public void componentResized(ComponentEvent componentEvent) {
                glassPane.setVisible(false);
            }
        });
    }

    public void addKeyboardInputAction()
    {
        KeyboardFocusManager manager = KeyboardFocusManager.getCurrentKeyboardFocusManager();
        manager.addKeyEventDispatcher(new KeyEventDispatcher() {
            @Override
            public boolean dispatchKeyEvent(KeyEvent e) {
                if (e.getID() == KeyEvent.KEY_PRESSED) {
                    handleKeyPress(e);
                }

                return false;
            }
        });
    }

    private void handleKeyPress(KeyEvent e) {
        switch(e.getKeyChar())
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '+':
            case '-':
            case '*':
            case '/':
            case '=':
            case '.':
                calculatorDispatcher.handleButtonPressed(String.valueOf(e.getKeyChar()));
                break;
        }

        if(e.getKeyCode() == KeyEvent.VK_ESCAPE)
        {
            calculatorDispatcher.handleButtonPressed(CalculatorSymbols.C);
        }

        else if(e.getKeyCode() == KeyEvent.VK_DELETE)
        {
            calculatorDispatcher.handleButtonPressed(CalculatorSymbols.CE);
        }

        else if(e.getKeyCode() == KeyEvent.VK_BACK_SPACE)
        {
            calculatorDispatcher.handleButtonPressed(CalculatorSymbols.DEL);
        }
    }

    public void addButtonAction(ArrayList<JRoundButton> numberButtons, ArrayList<JRoundButton> operatorButtons)
    {
        for(JRoundButton button : numberButtons)
        {
            button.addMouseListener(new MouseAdapter() {
                @Override
                public void mouseReleased(MouseEvent e) {
                    calculatorDispatcher.handleButtonPressed(button.getText());
                }
            });
        }

        for(JRoundButton button : operatorButtons)
        {
            button.addMouseListener(new MouseAdapter() {
                @Override
                public void mouseReleased(MouseEvent e) {
                    calculatorDispatcher.handleButtonPressed(button.getText());
                }
            });
        }
    }

    public void addResizeAction(JFrame frame, HistoryForm historyForm){
        frame.addComponentListener(new ComponentAdapter()
        {
            public void componentResized(ComponentEvent evt) {
                if(frame.getWidth() > 800 && mainWithHistoryPanel == null)
                {

                    mainPanel = ((MainFrame)frame).getMainPanel();
                    mainWithHistoryPanel = new JPanel(new GridLayout(0, 2));
                    mainWithHistoryPanel.add(mainPanel);
                    mainWithHistoryPanel.add(historyForm.getHistoryPanel());

                    frame.remove(mainPanel);
                    frame.setContentPane(mainWithHistoryPanel);
                    ((MainFrame) frame).getCalculatorForm().getHistoryButton().setVisible(false);
                }

                else if(frame.getWidth() <= 800)
                {
                    if(mainWithHistoryPanel != null)
                    {
                        frame.remove(mainWithHistoryPanel);
                        mainWithHistoryPanel = null;
                        frame.setContentPane(mainPanel);
                        ((JPanel)frame.getGlassPane()).add(historyForm.getHistoryPanel());
                        ((MainFrame) frame).getCalculatorForm().getHistoryButton().setVisible(true);
                    }
                }
            }
        });
    }

    public void addListListener(JList historyList) {
        historyList.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent mouseEvent) {
                JList theList = (JList) mouseEvent.getSource();

                int index = theList.locationToIndex(mouseEvent.getPoint());
                if (index >= 0) {
                    ArrayList<DataStore> logDataList = calculatorDispatcher.getLogDataList();
                    DataStore data = logDataList.get(index);

                    calculatorDispatcher.getCalculationData().setFirstOperand(data.getFirstOperand());
                    calculatorDispatcher.getCalculationData().setOperatorChar(data.getOperatorChar());
                    calculatorDispatcher.getCalculationData().setSecondOperand(data.getSecondOperand());

                    calculatorDispatcher.updateHistoryPane(data.getLastHistory());
                    calculatorDispatcher.updateInputPane(calculatorDispatcher.getInputDecimal(data.getFirstOperand()));

                    calculatorDispatcher.setCalculatorState(CalculatorState.ENTER_KEY_PRESSED);
                }
            }
        });
    }

    public void addHistoryClearLabelListener(JLabel jLabel)
    {
        jLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                calculatorDispatcher.resetLog();
            }
        });
    }
}
