package customizedComponent;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class JRoundButton extends JButton {

    public boolean isMouseOver() {
        return mouseOver;
    }

    public void setMouseOver(boolean mouseOver) {
        this.mouseOver = mouseOver;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
        setBackground(color);
    }

    public Color getColorOver() {
        return colorOver;
    }

    public void setColorOver(Color colorOver) {
        this.colorOver = colorOver;
    }

    public Color getColorClick() {
        return colorClick;
    }

    public void setColorClick(Color colorClick) {
        this.colorClick = colorClick;
    }

    public int getRadius() {
        return radius;
    }

    public void setRadius(int radius) {
        this.radius = radius;
    }

    public JRoundButton() {
        //  Init Color

        setColor(new Color(220, 220, 220));
        colorOver = new Color(210, 210, 210);
        colorClick = new Color(180, 180, 180);
        setContentAreaFilled(false);
        //  Add event mouse
        addMouseListener(new MouseAdapter() {
            @Override
            public void mouseEntered(MouseEvent me) {
                setBackground(colorOver);
                mouseOver = true;
            }

            @Override
            public void mouseExited(MouseEvent me) {
                setBackground(color);
                mouseOver = false;
            }

            @Override
            public void mousePressed(MouseEvent me) {
                setBackground(colorClick);
            }

            @Override
            public void mouseReleased(MouseEvent me) {
                if (mouseOver) {
                    setBackground(colorOver);
                } else {
                    setBackground(color);
                }
            }
        });
    }

    private boolean mouseOver;
    private Color color;
    private Color colorOver;
    private Color colorClick;
    private int radius = 20;

    @Override
    protected void paintComponent(Graphics graphics) {
        Graphics2D graphics2D = (Graphics2D) graphics;
        graphics2D.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
        graphics2D.setColor(getBackground());
        setBorderPainted(false);
        setFocusPainted(false);
        setFocusable(false);
        setVerticalAlignment(JButton.CENTER);
        graphics2D.fillRoundRect(3, 3, getWidth() - 6, getHeight() - 6, radius, radius);
        super.paintComponent(graphics);
    }
}