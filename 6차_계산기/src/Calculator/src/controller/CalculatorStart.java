package controller;

import view.MainFrame;

import java.awt.*;
import java.awt.event.KeyEvent;

public class CalculatorStart {
    MainFrame mainFrame;

    public CalculatorStart()
    {

    }

    public void start()
    {
        mainFrame = new MainFrame();

        KeyboardFocusManager manager = KeyboardFocusManager.getCurrentKeyboardFocusManager();
        manager.addKeyEventDispatcher(new KeyEventDispatcher() {
            @Override
            public boolean dispatchKeyEvent(KeyEvent e) {
                if (e.getID() == KeyEvent.KEY_RELEASED) {
                    System.out.println(e.getKeyChar());
                }

                return false;
            }
        });
    }
}
