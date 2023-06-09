package view;

import constant.LoginResult;
import model.UserDAO;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class LoginFrame extends JFrame implements Runnable{
    private JPanel marioPanel;
    private JLabel mario;
    private boolean isRunning;
    private Thread thread;
    private static final int DELAY = 5;
    private static final int FIRST_X = 60;
    private static final int SECOND_X = 200;
    private static final int DOOR_X = 500;
    private int currentIndex;
    private JTextField idTextField;
    private JPasswordField passwordField;


    public LoginFrame() {
        this.isRunning = false;
        this.currentIndex = 0;

        makeMarioPanel();
        createUIComponents();
    }

    public void createUIComponents() {
        JPanel mainPanel = new JPanel();
        JPanel backgroundPanel = new JPanel();
        JLabel imageLabel = new JLabel();

        ImageIcon backgroundImage = new ImageIcon("etc/LoginPage.jpg");

        imageLabel.setIcon(backgroundImage);

        backgroundPanel.add(imageLabel);
        mainPanel.add(backgroundPanel);

        mainPanel.setBounds(0, 0, mainPanel.getWidth(), mainPanel.getHeight());

        JLabel registerLabel = new JLabel();
        ImageIcon registerImage = new ImageIcon("etc/green_mushroom.png");
        Image tempImage = registerImage.getImage();
        registerImage = new ImageIcon(tempImage.getScaledInstance(40, 40,  Image.SCALE_SMOOTH));
        registerLabel.setIcon(registerImage);
        registerLabel.setBounds(530, 20, 40, 40);

        JLabel findIdLabel = new JLabel();
        ImageIcon findIdImage = new ImageIcon("etc/brick.png");
        tempImage = findIdImage.getImage();
        findIdImage = new ImageIcon(tempImage.getScaledInstance(40, 40,  Image.SCALE_SMOOTH));
        findIdLabel.setIcon(findIdImage);
        findIdLabel.setBounds(580, 20, 40, 40);

        JLabel findPasswordLabel = new JLabel();
        ImageIcon findPasswordImage = new ImageIcon("etc/brick_normal.png");
        tempImage = findPasswordImage.getImage();
        findPasswordImage = new ImageIcon(tempImage.getScaledInstance(40, 40,  Image.SCALE_SMOOTH));
        findPasswordLabel.setIcon(findPasswordImage);
        findPasswordLabel.setBounds(630, 20, 40, 40);

        JLabel idLabel = new JLabel("ID 입력");
        idLabel.setFont(new Font("Arial", Font.PLAIN, 20));
        idLabel.setBackground(new Color(255, 255, 255, 128));
        idLabel.setOpaque(true);

        idTextField = new JTextField(10);

        JLabel passwordLabel = new JLabel("비밀번호 입력");
        passwordLabel.setFont(new Font("Arial", Font.PLAIN, 20));
        passwordLabel.setBackground(new Color(255, 255, 255, 128));
        passwordLabel.setOpaque(true);

        passwordField = new JPasswordField(10);

        JLabel loginButton = new JLabel();

        ImageIcon loginImage = new ImageIcon("etc/JumpingMario.png");
        tempImage = loginImage.getImage();
        loginImage = new ImageIcon(tempImage.getScaledInstance(80, 160,  Image.SCALE_SMOOTH));

        loginButton.setIcon(loginImage);

        JLabel loginLabel = new JLabel("LOGIN");
        loginLabel.setFont(new Font("Arial", Font.PLAIN, 20));
        loginLabel.setBackground(Color.white);
        loginLabel.setOpaque(true);
        loginLabel.setHorizontalAlignment(SwingConstants.CENTER);

        idLabel.setBounds(50, 350, 70, 30);
        idTextField.setBounds(50, 380, 300, 50);
        passwordLabel.setBounds(50, 430, 120, 30);
        passwordField.setBounds(50, 460, 300, 50);
        loginButton.setBounds(380, 340, 80, 160);
        loginLabel.setBounds(380, 500, 80, 30);

        this.add(registerLabel);
        this.add(findIdLabel);
        this.add(findPasswordLabel);

        this.add(idLabel);
        this.add(idTextField);
        this.add(passwordLabel);
        this.add(passwordField);
        this.add(loginButton);
        this.add(loginLabel);

        this.add(mainPanel);

        this.setVisible(true);
        this.pack();
        this.setLocationRelativeTo(null);
        this.setResizable(false);
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        addFocusEvent(idTextField, passwordField, loginButton, loginLabel);
        addClickEvent(registerLabel, findIdLabel, findPasswordLabel);
    }

    public void makeMarioPanel() {
        if(!isRunning) {
            this.marioPanel = new JPanel(null);
            this.mario = new JLabel();

            ImageIcon marioMovingRightImage = new ImageIcon("etc/SuperMarioMovingRight.png");
            Image tempImage = marioMovingRightImage.getImage();
            marioMovingRightImage = new ImageIcon(tempImage.getScaledInstance(80, 80, Image.SCALE_SMOOTH));

            mario.setIcon(marioMovingRightImage);

            marioPanel.setSize(this.getWidth(), this.getHeight());
            marioPanel.setOpaque(false);

            mario.setBounds(60, 528, 80, 80);
            marioPanel.add(mario);

            marioPanel.setBounds(60, 528, 80, 80);

            this.setGlassPane(marioPanel);
            this.getGlassPane().setVisible(true);
        }
    }

    public synchronized void stop() {
        isRunning = false;
        thread.interrupt();
    }

    public synchronized  void start() {
        if (!isRunning) {
            isRunning = true;
            thread = new Thread(this);
            thread.start();
        }
    }

    @Override
    public void run() {
        while(isRunning) {
            try {
                Thread.sleep(10);

                switch(currentIndex) {
                    case 0:
                        moveToFirst();
                        break;
                    case 1:
                        moveToSecond();
                        break;
                    case 2:
                        moveToDoor();
                        break;
                }

                if(marioPanel.getLocation().x > 400) {
                    if(checkLogin()) {
                        this.dispose();
                        new MainMenuFrame(idTextField.getText());

                        stop();
                        thread.stop();
                    }
                    else {
                        currentIndex = 1;
                    }
                }

                repaint();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    private void moveToFirst() {
        Point marioLocation = marioPanel.getLocation();

        if (marioLocation.x > FIRST_X) {
            ImageIcon marioMovingLeftImage = new ImageIcon("etc/SuperMarioMovingLeft.png");
            Image tempImage = marioMovingLeftImage.getImage();
            marioMovingLeftImage = new ImageIcon(tempImage.getScaledInstance(80, 80, Image.SCALE_SMOOTH));

            mario.setIcon(marioMovingLeftImage);
            marioPanel.setLocation(marioLocation.x - 2, marioLocation.y);
        }

        else {
            ImageIcon marioStandingImage = new ImageIcon("etc/SuperMarioStanding.png");
            Image tempImage = marioStandingImage.getImage();
            marioStandingImage = new ImageIcon(tempImage.getScaledInstance(80, 80, Image.SCALE_SMOOTH));

            mario.setIcon(marioStandingImage);

            marioPanel.setLocation(FIRST_X, marioLocation.y);
        }
    }

    private void moveToSecond() {
        Point marioLocation = marioPanel.getLocation();

        if(marioLocation.x > SECOND_X) {
            ImageIcon marioMovingLeftImage = new ImageIcon("etc/SuperMarioMovingLeft.png");
            Image tempImage = marioMovingLeftImage.getImage();
            marioMovingLeftImage = new ImageIcon(tempImage.getScaledInstance(80, 80,  Image.SCALE_SMOOTH));

            mario.setIcon(marioMovingLeftImage);
            marioPanel.setLocation(marioLocation.x - 2, marioLocation.y);
        }

        else if (marioLocation.x < SECOND_X) {
            ImageIcon marioMovingRightImage = new ImageIcon("etc/SuperMarioMovingRight.png");
            Image tempImage = marioMovingRightImage.getImage();
            marioMovingRightImage = new ImageIcon(tempImage.getScaledInstance(80, 80,  Image.SCALE_SMOOTH));

            mario.setIcon(marioMovingRightImage);
            marioPanel.setLocation(marioLocation.x + 2, marioLocation.y);
        }

        else {
            ImageIcon marioStandingImage = new ImageIcon("etc/SuperMarioStanding.png");
            Image tempImage = marioStandingImage.getImage();
            marioStandingImage = new ImageIcon(tempImage.getScaledInstance(80, 80,  Image.SCALE_SMOOTH));

            mario.setIcon(marioStandingImage);

            marioPanel.setLocation(SECOND_X, marioLocation.y);
        }
    }

    private void moveToDoor() {
        Point marioLocation = marioPanel.getLocation();

        if(marioLocation.x < DOOR_X) {
            ImageIcon marioMovingRightImage = new ImageIcon("etc/SuperMarioMovingRight.png");
            Image tempImage = marioMovingRightImage.getImage();
            marioMovingRightImage = new ImageIcon(tempImage.getScaledInstance(80, 80,  Image.SCALE_SMOOTH));

            mario.setIcon(marioMovingRightImage);
            marioPanel.setLocation(marioLocation.x + 2, marioLocation.y);
        }
    }

    private void addFocusEvent(JTextField idTextField, JPasswordField passwordField, JLabel loginButton, JLabel loginLabel) {
        idTextField.addFocusListener(new FocusAdapter() {
            @Override
            public void focusGained(FocusEvent e) {
                super.focusGained(e);
                currentIndex = 0;
            }
        });

        passwordField.addFocusListener(new FocusAdapter() {
            @Override
            public void focusGained(FocusEvent e) {
                super.focusGained(e);
                currentIndex = 1;
            }
        });

        loginButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseClicked(e);
                currentIndex = 2;
            }
        });

        loginLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseClicked(e);
                currentIndex = 2;
            }
        });

        idTextField.addKeyListener(new KeyAdapter() {
            @Override
            public void keyReleased(KeyEvent e) {
                super.keyReleased(e);

                if(e.getKeyCode() == KeyEvent.VK_ENTER) {
                    currentIndex = 2;
                }
            }
        });

        passwordField.addKeyListener(new KeyAdapter() {
            @Override
            public void keyReleased(KeyEvent e) {
                super.keyReleased(e);

                if(e.getKeyCode() == KeyEvent.VK_ENTER) {
                    currentIndex = 2;
                }
            }
        });
    }

    private void addClickEvent(JLabel registerLabel, JLabel findIdLabel, JLabel findPasswordLabel) {
        registerLabel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);

                new SignUpFrame();
            }
        });
    }

    public boolean checkLogin() {
        String id = idTextField.getText();
        String password = passwordField.getText();

        if(id.equals("")) {
            JOptionPane.showMessageDialog(this, "아이디를 입력해주세요!");
            return false;
        }

        if(password.equals("")) {
            JOptionPane.showMessageDialog(this, "비밀번호를 입력해주세요!");
            return false;
        }

        LoginResult loginResult = UserDAO.getInstance().tryLogin(id, password);

        switch(loginResult) {
            case NO_ID:
                JOptionPane.showMessageDialog(this, "존재하지 않는 아이디입니다!");
                return false;
            case WRONG_PASSWORD:
                JOptionPane.showMessageDialog(this, "비밀번호가 잘못 되었습니다!");
                return false;
            case SUCCESS:
                return true;
        }

        return false;
    }
}
