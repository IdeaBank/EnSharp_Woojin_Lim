package view;

import javax.swing.*;
import java.awt.*;

public class SignUpFrame extends JFrame {
    private JLabel nameLabel, idLabel, passwordLabel, passwordCheckLabel, birthDateLabel, emailLabel, phoneNumberLabel, addressLabel, addressNumberLabel;
    private JTextField nameField, idField, emailField, birthDateField, phoneNumberMiddleField, phoneNumberLastField, addressField, addressNumberField;
    private JPasswordField passwordField, passwordCheckField;
    private JButton idCheckButton, findAddressButton, signUpButton;
    private JComboBox<String> emailDropdown, phoneNumberFirstDropdown;

    public SignUpFrame() {
        createUIComponents();
    }

    public void createUIComponents() {
        // 프레임 초기화
        setTitle("회원가입 폼");
        setSize(500, 300); // 크기를 조정하셔서 원하는 크기로 변경하세요.
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLayout(new BorderLayout());

        // 배경 이미지 추가
        ImageIcon backgroundImage = new ImageIcon("etc/LoginPage.jpg"); // 원하는 배경 이미지 파일로 경로를 수정하세요.
        JLabel backgroundLabel = new JLabel(backgroundImage);
        backgroundLabel.setBounds(0, 0, backgroundImage.getIconWidth(), backgroundImage.getIconHeight());
        add(backgroundLabel);

        // 폼 패널 생성
        JPanel formPanel = new JPanel();
        formPanel.setOpaque(false);
        formPanel.setLayout(null);

        // 레이블 및 필드 생성
        nameLabel = new JLabel("이름:");
        nameLabel.setBackground(Color.white);
        nameLabel.setOpaque(true);
        nameLabel.setFont(new Font("Arial", Font.BOLD, 20));
        nameLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        nameLabel.setBounds(50, 50, 80, 25);

        nameField = new JTextField();
        nameField.setBounds(140, 50, 200, 25);

        idLabel = new JLabel("아이디:");
        idLabel.setBackground(Color.white);
        idLabel.setOpaque(true);
        idLabel.setFont(new Font("Arial", Font.BOLD, 20));
        idLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        idLabel.setBounds(50, 50, 80, 25);

        idField = new JTextField();
        idField.setBounds(140, 50, 200, 25);

        idCheckButton = new JButton("아이디 중복 체크");
        idCheckButton.setBounds(140, 50, 200, 24);

        passwordLabel = new JLabel("비밀번호:");
        passwordLabel.setBackground(Color.white);
        passwordLabel.setOpaque(true);
        passwordLabel.setFont(new Font("Arial", Font.BOLD, 20));
        passwordLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        passwordLabel.setBounds(50, 110, 80, 25);

        passwordField = new JPasswordField();
        passwordField.setBounds(140, 110, 200, 25);

        passwordCheckLabel = new JLabel("비밀번호 체크:");
        passwordCheckLabel.setBackground(Color.white);
        passwordCheckLabel.setOpaque(true);
        passwordCheckLabel.setFont(new Font("Arial", Font.BOLD, 20));
        passwordCheckLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        passwordCheckLabel.setBounds(50, 110, 80, 25);

        passwordCheckField = new JPasswordField();
        passwordCheckField.setBounds(140, 110, 200, 25);

        birthDateLabel = new JLabel("생년월일:");
        birthDateLabel.setBackground(Color.white);
        birthDateLabel.setOpaque(true);
        birthDateLabel.setFont(new Font("Arial", Font.BOLD, 20));
        birthDateLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        birthDateLabel.setBounds(50, 110, 80, 25);

        birthDateField = new JTextField();
        birthDateField.setBounds(140, 110,200,25);

        emailLabel = new JLabel("이메일:");
        emailLabel.setBackground(Color.white);
        emailLabel.setOpaque(true);
        emailLabel.setFont(new Font("Arial", Font.BOLD, 20));
        emailLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        emailLabel.setBounds(50, 80, 80, 25);

        emailField = new JTextField();
        emailField.setBounds(140, 80, 200, 25);

        String[] emailList = { "gmail.com", "naver.com", "hanmail.net" };
        emailDropdown = new JComboBox<>(emailList);

        phoneNumberLabel = new JLabel("전화번호:");
        phoneNumberLabel.setBackground(Color.white);
        phoneNumberLabel.setOpaque(true);
        phoneNumberLabel.setFont(new Font("Arial", Font.BOLD, 20));
        phoneNumberLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        phoneNumberLabel.setBounds(50, 80, 80, 25);

        String[] phoneNumberList = { "010", "011", "012", "013", "014", "015", "016", "017", "018", "019" };
        phoneNumberFirstDropdown = new JComboBox<>(phoneNumberList);

        phoneNumberMiddleField = new JTextField();
        phoneNumberMiddleField.setBounds(140, 80, 200, 25);

        phoneNumberLastField = new JTextField();
        phoneNumberLastField.setBounds(140, 80, 200, 25);

        addressLabel = new JLabel("주소:");
        addressLabel.setBackground(Color.white);
        addressLabel.setOpaque(true);
        addressLabel.setFont(new Font("Arial", Font.BOLD, 20));
        addressLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        addressLabel.setBounds(50, 80, 80, 25);

        addressField = new JTextField();
        addressField.setBounds(140, 80, 200, 25);

        addressNumberLabel = new JLabel("우편번호:");
        addressNumberLabel.setBackground(Color.white);
        addressNumberLabel.setOpaque(true);
        addressNumberLabel.setFont(new Font("Arial", Font.BOLD, 20));
        addressNumberLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        addressNumberLabel.setBounds(50, 80, 80, 25);

        addressNumberField = new JTextField();
        addressNumberField.setBounds(140, 80, 200, 25);

        signUpButton = new JButton("가입하기");
        signUpButton.setBounds(50, 150, 100, 25);

        // 폼 패널에 컴포넌트 추가
        formPanel.add(nameLabel);
        formPanel.add(nameField);
        formPanel.add(emailLabel);
        formPanel.add(emailField);
        formPanel.add(passwordLabel);
        formPanel.add(passwordField);
        formPanel.add(signUpButton);

        // 폼 패널을 배경 이미지에 추가
        backgroundLabel.setLayout(null);
        formPanel.setBounds(100, 50, 340, 200); // 폼 패널의 위치와 크기를 지정
        backgroundLabel.add(formPanel);

        // 가입하기 버튼 클릭 이벤트 처리
        signUpButton.addActionListener(e -> {
            String name = nameField.getText();
            String email = emailField.getText();
            String password = new String(passwordField.getPassword());

            // 이곳에서 회원가입 로직을 처리하면 됩니다.
            // 예를 들면, 서버로 데이터를 전송하거나 데이터베이스에 저장할 수 있습니다.

            // 가입 완료 메시지 박스 표시
            JOptionPane.showMessageDialog(this, "가입이 완료되었습니다.");
        });

        this.pack();
        this.setVisible(true);
    }
}
