package view;

import model.UserDAO;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

public class MainMenuFrame extends JFrame {
    private String id;

    public MainMenuFrame(String id) {
        createUIComponents();
        this.id = id;

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

        ImageIcon marioImage = new ImageIcon("etc/MarioDefault.png");
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

        ImageIcon luigiImage = new ImageIcon("etc/LuigiDefault.png");
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

        ImageIcon bowserImage = new ImageIcon("etc/BowserDefault.png");
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

        addClickEvent(editLabel, logoutLabel, withdrawLabel);
        addClickEvent(marioLabel, luigiLabel, bowserLabel);
        addHoverEvent(marioLabel, luigiLabel, bowserLabel);
    }

    private void addClickEvent(JLabel editLabel, JLabel logoutLabel, JLabel withdrawLabel) {
        JFrame jFrame = this;

        editLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);
                new SignUpFrame();
            }
        });


        logoutLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);

                ImageIcon coinImage = new ImageIcon("etc/coin.png");
                Image tempImage = coinImage.getImage();
                coinImage = new ImageIcon(tempImage.getScaledInstance(80, 80, Image.SCALE_SMOOTH));

                int result = JOptionPane.showConfirmDialog(jFrame, "로그아웃 하시겠습니까?", "로그아웃 확인", JOptionPane.YES_NO_OPTION, JOptionPane.WARNING_MESSAGE, coinImage);

                if(result == 0) {
                    startLoginFrame();

                    jFrame.dispose();
                }
            }
        });

        withdrawLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);

                ImageIcon starImage = new ImageIcon("etc/star.png");
                Image tempImage = starImage.getImage();
                starImage = new ImageIcon(tempImage.getScaledInstance(80, 80, Image.SCALE_SMOOTH));

                int result = JOptionPane.showConfirmDialog(jFrame, "회원 탈퇴하시겠습니까?", "회원 탈퇴", JOptionPane.YES_NO_OPTION, JOptionPane.WARNING_MESSAGE, starImage);

                if(result == 0) {
                    UserDAO.getInstance().withDraw(id);
                    startLoginFrame();

                    jFrame.setVisible(false);

                    JOptionPane.showMessageDialog(jFrame, "성공적으로 탈퇴하셨습니다!", "회원 탈퇴", JOptionPane.INFORMATION_MESSAGE, starImage);

                    jFrame.dispose();
                }
            }
        });
    }

    private void addHoverEvent(JLabel mario, JLabel luigi, JLabel bowser) {
        mario.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseEntered(MouseEvent e) {
                super.mouseEntered(e);

                ImageIcon marioImage = new ImageIcon("etc/Mario.png");
                Image tempImage = marioImage.getImage();
                marioImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));

                mario.setIcon(marioImage);
            }

            @Override
            public void mouseExited(MouseEvent e){
                super.mouseExited(e);

                ImageIcon marioImage = new ImageIcon("etc/MarioDefault.png");
                Image tempImage = marioImage.getImage();
                marioImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));

                mario.setIcon(marioImage);
            }
        });

        luigi.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseEntered(MouseEvent e) {
                super.mouseEntered(e);

                ImageIcon luigiImage = new ImageIcon("etc/Luigi.png");
                Image tempImage = luigiImage.getImage();
                luigiImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));

                luigi.setIcon(luigiImage);
            }

            @Override
            public void mouseExited(MouseEvent e){
                super.mouseExited(e);

                ImageIcon luigiImage = new ImageIcon("etc/LuigiDefault.png");
                Image tempImage = luigiImage.getImage();
                luigiImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));

                luigi.setIcon(luigiImage);
            }
        });

        bowser.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseEntered(MouseEvent e) {
                super.mouseEntered(e);

                ImageIcon bowserImage = new ImageIcon("etc/Bowser.png");
                Image tempImage = bowserImage.getImage();
                bowserImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));

                bowser.setIcon(bowserImage);
            }

            @Override
            public void mouseExited(MouseEvent e){
                super.mouseExited(e);

                ImageIcon bowserImage = new ImageIcon("etc/BowserDefault.png");
                Image tempImage = bowserImage.getImage();
                bowserImage = new ImageIcon(tempImage.getScaledInstance(225, 245, Image.SCALE_SMOOTH));

                bowser.setIcon(bowserImage);
            }
        });
    }

    private void startLoginFrame() {
        LoginFrame loginFrame = new LoginFrame();
        loginFrame.start();
    }
}
