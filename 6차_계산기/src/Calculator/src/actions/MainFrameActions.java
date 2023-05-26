package actions;

import constant.CalculatorSymbols;
import dispatcher.CalculatorDispatcher;
import customizedComponent.JRoundButton;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;

public class MainFrameActions {
    private static MainFrameActions _instance;
    private CalculatorDispatcher calculatorDispatcher;

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
}
