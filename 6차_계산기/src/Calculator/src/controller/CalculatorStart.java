package controller;

import view.MainFrame;

import java.awt.*;
import java.awt.event.KeyEvent;

public class CalculatorStart {
    MainFrame mainFrame;

    public CalculatorStart() {

    }

    public void start() {
        mainFrame = new MainFrame();

        KeyboardFocusManager manager = KeyboardFocusManager.getCurrentKeyboardFocusManager();
        manager.addKeyEventDispatcher(new KeyEventDispatcher() {
            @Override
            public boolean dispatchKeyEvent(KeyEvent e) {
                if (e.getID() == KeyEvent.KEY_RELEASED) {
                    handleKeyRelease(e);
                }

                return false;
            }
        });
    }

    private void handleKeyRelease(KeyEvent e) {
        switch(e.getKeyChar())
        {
            case '0':
                appendZero();
                break;
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                appendNumber(e.getKeyChar());
                break;
            case '+':
            case '-':
            case '*':
            case '/':
                appendOperator(e.getKeyChar());
                break;
            case '=':
                calculate();
                break;
            case '.':
                appendDot();
                break;
        }

        if(e.getKeyCode() == KeyEvent.VK_ESCAPE)
        {
            System.out.println("C");
        }

        else if(e.getKeyCode() == KeyEvent.VK_DELETE)
        {
            System.out.println("CE");
        }
    }

    private void appendZero()
    {
        appendNumber('0');
    }

    private void appendNumber(char ch)
    {
        System.out.println(ch); 
    }

    private void appendOperator(char ch)
    {
        System.out.println(ch);
    }

    private void appendDot()
    {
        System.out.println(".");
    }

    private void calculate()
    {
        System.out.println("=");
    }
}
