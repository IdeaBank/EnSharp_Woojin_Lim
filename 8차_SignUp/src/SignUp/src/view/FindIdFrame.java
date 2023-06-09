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

public class FindIdFrame extends JFrame {

    private JLabel nameLabel;
    private JTextField nameField;
    private JLabel emailLabel;
    private JTextField emailField;
    private JButton searchButton;

    public FindIdFrame() {
        setTitle("아이디 찾기");

        nameLabel = new JLabel("이름:");
        nameField = new JTextField(20);
        emailLabel = new JLabel("이메일:");
        emailField = new JTextField(20);
        searchButton = new JButton("찾기");

        JFrame frame = this;

        searchButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                String name = nameField.getText();
                String email = emailField.getText();

                if(name.equals("")) {
                    JOptionPane.showMessageDialog(frame, "이름을 입력해주세요!");
                    return;
                }

                if(email.equals("")) {
                    JOptionPane.showMessageDialog(frame, "이메일을 입력해주세요!");
                    return;
                }

                String result = UserDAO.getInstance().findID(name, email);

                if(result != null)  {
                    JFrame inputFrame = new JFrame();

                    JLabel codeLabel = new JLabel("코드:");
                    JTextField code = new JTextField(20);
                    JButton submitButton = new JButton("제출");

                    inputFrame.setLayout(new FlowLayout());

                    inputFrame.add(codeLabel);
                    inputFrame.add(code);
                    inputFrame.add(submitButton);

                    Random rand = new Random();
                    int randomCode = rand.nextInt(10000);

                    EmailSender.sendMail("ID 찾기", "코드: " + randomCode, email);

                    submitButton.addMouseListener(new MouseAdapter() {
                        @Override
                        public void mouseReleased(MouseEvent e) {
                            super.mouseReleased(e);

                            if(code.getText().equals(String.valueOf(randomCode))) {
                                JOptionPane.showMessageDialog(inputFrame, "아이디: " + result);
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
        add(emailLabel);
        add(emailField);
        add(searchButton);

        pack();
        setLocationRelativeTo(null);
        setVisible(true);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
    }
}
