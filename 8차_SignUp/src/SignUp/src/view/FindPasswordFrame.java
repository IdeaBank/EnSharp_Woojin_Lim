package view;

import controller.EmailSender;
import model.UserDAO;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.Random;

public class FindPasswordFrame extends JFrame {

    private JLabel nameLabel;
    private JTextField nameField;
    private JLabel idLabel;
    private JTextField idField;
    private JLabel emailLabel;
    private JTextField emailField;
    private JButton searchButton;

    public FindPasswordFrame() {
        setTitle("비밀번호 reset");

        nameLabel = new JLabel("이름:");
        nameField = new JTextField(20);
        idLabel = new JLabel("ID:");
        idField = new JTextField(20);
        emailLabel = new JLabel("이메일:");
        emailField = new JTextField(20);
        searchButton = new JButton("찾기");

        JFrame frame = this;

        searchButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                String name = nameField.getText();
                String id = idField.getText();
                String email = emailField.getText();

                if(name.equals("")) {
                    JOptionPane.showMessageDialog(frame, "이름을 입력해주세요!");
                    return;
                }

                if(id.equals("")) {
                    JOptionPane.showMessageDialog(frame, "아이디를 입력해주세요!");
                    return;
                }

                if(email.equals("")) {
                    JOptionPane.showMessageDialog(frame, "이메일을 입력해주세요!");
                    return;
                }

                String result = UserDAO.getInstance().findPassword(name, id, email);

                if(result != null)  {
                    JFrame inputFrame = new JFrame();

                    JLabel codeLabel = new JLabel("코드:");
                    JTextField code = new JTextField(20);
                    JButton submitButton = new JButton("제출");

                    inputFrame.setLayout(new FlowLayout());

                    inputFrame.add(codeLabel);
                    inputFrame.add(code);
                    inputFrame.add(submitButton);

                    inputFrame.pack();
                    inputFrame.setVisible(true);
                    inputFrame.setLocationRelativeTo(null);

                    Random rand = new Random();
                    int randomCode = rand.nextInt(10000);

                    EmailSender.sendMail("패스워드 초기화", "코드: " + randomCode, email);

                    submitButton.addMouseListener(new MouseAdapter() {
                        @Override
                        public void mouseReleased(MouseEvent e) {
                            super.mouseReleased(e);

                            if(code.getText().equals(String.valueOf(randomCode))) {
                                JFrame resetPassword = new JFrame();
                                resetPassword.setLayout(new FlowLayout());

                                JLabel newPasswordLabel = new JLabel("신규 패스워드:");
                                JTextField newPasswordField = new JTextField(20);

                                JLabel newPasswordCheckLabel = new JLabel("패스워드 확인:");
                                JTextField newPasswordCheckField = new JTextField(20);

                                JButton submitButton = new JButton("제출");

                                resetPassword.add(newPasswordLabel);
                                resetPassword.add(newPasswordField);
                                resetPassword.add(newPasswordCheckLabel);
                                resetPassword.add(newPasswordCheckField);
                                resetPassword.add(submitButton);

                                resetPassword.setVisible(true);
                                resetPassword.pack();
                                resetPassword.setLocationRelativeTo(null);

                                submitButton.addMouseListener(new MouseAdapter() {
                                    @Override
                                    public void mouseReleased(MouseEvent e) {
                                        super.mouseReleased(e);

                                        if(newPasswordField.getText().equals("") || newPasswordCheckField.getText().equals("")) {
                                            JOptionPane.showMessageDialog(resetPassword, "입력 값을 모두 채워주세요!");
                                            return;
                                        }

                                        if(newPasswordField.getText().equals(newPasswordCheckField.getText())) {
                                            UserDAO.getInstance().editPassword(id, newPasswordField.getText());
                                            JOptionPane.showMessageDialog(resetPassword, "성공적으로 변경하였습니다!");
                                            frame.dispose();
                                            resetPassword.dispose();
                                            inputFrame.dispose();
                                        }

                                        else {
                                            JOptionPane.showMessageDialog(resetPassword, "패스워드가 일치하지 않습니다!");
                                        }
                                    }
                                });

                                resetPassword.setVisible(true);
                            }

                            else {
                                JOptionPane.showMessageDialog(inputFrame, "코드를 다시 입력해주세요!");
                            }
                        }
                    });

                    inputFrame.setVisible(true);
                    inputFrame.pack();
                    inputFrame.setLocationRelativeTo(null);
                }

                else {
                    JOptionPane.showMessageDialog(frame, "해당 이름 혹은 이메일을 가지고 있는 유저를 찾지 못하였습니다!");
                }
            }
        });

        setLayout(new FlowLayout());

        add(nameLabel);
        add(nameField);
        add(idLabel);
        add(idField);
        add(emailLabel);
        add(emailField);
        add(searchButton);

        pack();
        setLocationRelativeTo(null);
        setVisible(true);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
    }
}
