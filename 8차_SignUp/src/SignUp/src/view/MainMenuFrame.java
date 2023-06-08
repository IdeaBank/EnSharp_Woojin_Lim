package view;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class MainMenuFrame extends JFrame {
    public MainMenuFrame() {
        createUIComponents();

        this.pack();
        this.setVisible(true);
        this.setResizable(false);
        this.setLocationRelativeTo(null);
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);
    }

    public void createUIComponents() {
        JPanel mainPanel = new JPanel();
        JPanel backgroundPanel = new JPanel();
        JLabel imageLabel = new JLabel();

        ImageIcon backgroundImage = new ImageIcon("etc/MainMenu.jpg");
        Image tempImage = backgroundImage.getImage();
        backgroundImage = new ImageIcon(tempImage.getScaledInstance(960, 540, Image.SCALE_SMOOTH));

        JLabel editLabel = new JLabel("EDIT");
        editLabel.setFont(new Font("Arial", Font.BOLD, 24));
        editLabel.setBounds(82, 360, 120, 30);
        editLabel.setOpaque(true);
        editLabel.setBackground(Color.white);
        editLabel.setHorizontalAlignment(SwingConstants.CENTER);

        ImageIcon marioImage = new ImageIcon("etc/Mario.png");
        tempImage = marioImage.getImage();
        marioImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));
        JLabel marioLabel = new JLabel();

        marioLabel.setIcon(marioImage);
        marioLabel.setBounds(30, 100, 225, 245);

        JLabel logoutLabel = new JLabel("Logout");
        logoutLabel.setFont(new Font("Arial", Font.BOLD, 24));
        logoutLabel.setBounds(422, 360, 120, 30);
        logoutLabel.setOpaque(true);
        logoutLabel.setBackground(Color.white);
        logoutLabel.setHorizontalAlignment(SwingConstants.CENTER);

        ImageIcon luigiImage = new ImageIcon("etc/Luigi.png");
        tempImage = luigiImage.getImage();
        luigiImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));
        JLabel luigiLabel = new JLabel();

        luigiLabel.setIcon(luigiImage);
        luigiLabel.setBounds(370, 100, 225, 245);

        JLabel withdrawLabel = new JLabel("Withdraw");
        withdrawLabel.setFont(new Font("Arial", Font.BOLD, 24));
        withdrawLabel.setBounds(752, 360, 120, 30);
        withdrawLabel.setOpaque(true);
        withdrawLabel.setBackground(Color.white);
        withdrawLabel.setHorizontalAlignment(SwingConstants.CENTER);

        ImageIcon bowserImage = new ImageIcon("etc/Bowser.png");
        tempImage = bowserImage.getImage();
        bowserImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));
        JLabel bowserLabel = new JLabel();

        bowserLabel.setIcon(bowserImage);
        bowserLabel.setBounds(700, 100, 225, 245);

        imageLabel.setIcon(backgroundImage);

        add(editLabel);
        add(logoutLabel);
        add(withdrawLabel);
        add(marioLabel);
        add(luigiLabel);
        add(bowserLabel);
        backgroundPanel.add(imageLabel);
        mainPanel.add(backgroundPanel);

        this.add(mainPanel);
    }

    private void addClickEvent(JLabel editLabel, JLabel logoutLabel, JLabel withdrawLabel) {
        editLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);
            }
        });

        logoutLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);
            }
        });

        withdrawLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);
            }
        });
    }
}
