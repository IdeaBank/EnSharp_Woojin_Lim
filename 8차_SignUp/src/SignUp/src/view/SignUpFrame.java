package view;

import constant.RegularExpression;
import model.UserDAO;
import model.UserDTO;

import javax.print.URIException;
import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.FocusAdapter;
import java.awt.event.FocusEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLEncoder;

public class SignUpFrame extends JFrame {
    private JLabel nameLabel, idLabel, passwordLabel, passwordCheckLabel, birthDateLabel, emailLabel, phoneNumberLabel, addressLabel, addressNumberLabel;
    private JTextField nameField, idField, emailField, birthdateField, phoneNumberMiddleField, phoneNumberLastField, addressField, addressNumberField;
    private JPasswordField passwordField, passwordCheckField;
    private JButton idCheckButton, findAddressButton, signUpButton;
    private JComboBox<String> emailDropdown, phoneNumberFirstDropdown;

    public SignUpFrame() {
        createUIComponents();

        setResizable(false);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
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
        formPanel.setOpaque(true);
        formPanel.setBackground(new Color(0,0,0, 128));
        formPanel.setLayout(null);

        // 레이블 및 필드 생성
        nameLabel = new JLabel("이름:");
        nameLabel.setForeground(Color.white);
        nameLabel.setFont(new Font("Arial", Font.BOLD, 20));
        nameLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        nameLabel.setBounds(50, 50, 100, 25);

        nameField = new JTextField();
        nameField.setBounds(160, 50, 200, 25);
        nameField.setOpaque(true);
        nameField.setBorder(null);

        idLabel = new JLabel("아이디:");
        idLabel.setForeground(Color.white);
        idLabel.setFont(new Font("Arial", Font.BOLD, 20));
        idLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        idLabel.setBounds(50, 80, 100, 25);

        idField = new JTextField();
        idField.setBounds(160, 80, 200, 25);
        idField.setOpaque(true);
        idField.setBorder(null);

        idCheckButton = new JButton("아이디 중복 체크");
        idCheckButton.setBounds(370, 80, 120, 24);

        passwordLabel = new JLabel("비밀번호:");
        passwordLabel.setForeground(Color.white);
        passwordLabel.setFont(new Font("Arial", Font.BOLD, 20));
        passwordLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        passwordLabel.setBounds(50, 110, 100, 25);

        passwordField = new JPasswordField();
        passwordField.setBounds(160, 110, 200, 25);
        passwordField.setOpaque(true);
        passwordField.setBorder(null);

        passwordCheckLabel = new JLabel("비밀번호 체크:");
        passwordCheckLabel.setForeground(Color.white);
        passwordCheckLabel.setFont(new Font("Arial", Font.BOLD, 16));
        passwordCheckLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        passwordCheckLabel.setBounds(50, 140, 100, 25);

        passwordCheckField = new JPasswordField();
        passwordCheckField.setBounds(160, 140, 200, 25);
        passwordCheckField.setOpaque(true);
        passwordCheckField.setBorder(null);

        birthDateLabel = new JLabel("생년월일:");
        birthDateLabel.setForeground(Color.white);
        birthDateLabel.setFont(new Font("Arial", Font.BOLD, 20));
        birthDateLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        birthDateLabel.setBounds(50, 170, 100, 25);

        birthdateField = new JTextField();
        birthdateField.setBounds(160, 170,200,25);
        birthdateField.setOpaque(true);
        birthdateField.setBorder(null);

        emailLabel = new JLabel("이메일:");
        emailLabel.setForeground(Color.white);
        emailLabel.setFont(new Font("Arial", Font.BOLD, 20));
        emailLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        emailLabel.setBounds(50, 200, 100, 25);

        emailField = new JTextField();
        emailField.setBounds(160, 200, 200, 25);
        emailField.setOpaque(true);
        emailField.setBorder(null);

        String[] emailList = { "gmail.com", "naver.com", "hanmail.net" };
        emailDropdown = new JComboBox<>(emailList);
        emailDropdown.setBounds(370, 200, 150, 25);

        phoneNumberLabel = new JLabel("전화번호:");
        phoneNumberLabel.setForeground(Color.white);
        phoneNumberLabel.setFont(new Font("Arial", Font.BOLD, 20));
        phoneNumberLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        phoneNumberLabel.setBounds(50, 230, 100, 25);

        String[] phoneNumberList = { "010", "011", "012", "013", "014", "015", "016", "017", "018", "019" };
        phoneNumberFirstDropdown = new JComboBox<>(phoneNumberList);
        phoneNumberFirstDropdown.setBounds(160, 230, 80, 25);

        phoneNumberMiddleField = new JTextField();
        phoneNumberMiddleField.setBounds(240, 230, 58, 25);
        phoneNumberMiddleField.setOpaque(true);
        phoneNumberMiddleField.setBorder(null);

        phoneNumberLastField = new JTextField();
        phoneNumberLastField.setBounds(302, 230, 58, 25);
        phoneNumberLastField.setOpaque(true);
        phoneNumberLastField.setBorder(null);

        addressLabel = new JLabel("주소:");
        addressLabel.setForeground(Color.white);
        addressLabel.setFont(new Font("Arial", Font.BOLD, 20));
        addressLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        addressLabel.setBounds(50, 260, 100, 25);

        addressField = new JTextField();
        addressField.setBounds(160, 260, 200, 25);
        addressField.setOpaque(true);
        addressField.setBorder(null);

        addressNumberLabel = new JLabel("우편번호:");
        addressNumberLabel.setForeground(Color.white);
        addressNumberLabel.setFont(new Font("Arial", Font.BOLD, 20));
        addressNumberLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        addressNumberLabel.setBounds(50, 290, 100, 25);

        addressNumberField = new JTextField();
        addressNumberField.setBounds(160, 290, 100, 25);
        addressNumberField.setOpaque(true);
        addressNumberField.setBorder(null);

        findAddressButton = new JButton("우편번호 찾기");
        findAddressButton.setBounds(260, 290, 100, 25);

        signUpButton = new JButton("가입하기");
        signUpButton.setFont(new Font("Arial", Font.PLAIN, 20));
        signUpButton.setHorizontalTextPosition(SwingConstants.CENTER);
        signUpButton.setBounds(100, 340, 300, 80);

        // 폼 패널에 컴포넌트 추가
        formPanel.add(nameLabel);
        formPanel.add(idLabel);
        formPanel.add(passwordLabel);
        formPanel.add(passwordCheckLabel);
        formPanel.add(birthDateLabel);
        formPanel.add(emailLabel);
        formPanel.add(phoneNumberLabel);
        formPanel.add(addressLabel);
        formPanel.add(addressNumberLabel);

        formPanel.add(nameField);
        formPanel.add(idField);
        formPanel.add(emailField);
        formPanel.add(passwordField);
        formPanel.add(passwordCheckField);
        formPanel.add(birthdateField);

        formPanel.add(phoneNumberMiddleField);
        formPanel.add(phoneNumberLastField);

        formPanel.add(addressField);
        formPanel.add(addressNumberField);

        formPanel.add(idCheckButton);
        formPanel.add(findAddressButton);
        formPanel.add(signUpButton);

        formPanel.add(emailDropdown);
        formPanel.add(phoneNumberFirstDropdown);

        // 폼 패널을 배경 이미지에 추가
        backgroundLabel.setLayout(null);
        formPanel.setBounds(80, 30, 530, 600); // 폼 패널의 위치와 크기를 지정
        backgroundLabel.add(formPanel);

        idCheckButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);

                if(isIdExists()) {
                    System.out.println("ID EXISTS");
                }
            }
        });

        // 가입하기 버튼 클릭 이벤트 처리
        signUpButton.addActionListener(e -> {
            String name = nameField.getText();
            String email = emailField.getText();
            String password = new String(passwordField.getPassword());

            // 이곳에서 회원가입 로직을 처리하면 됩니다.
            // 예를 들면, 서버로 데이터를 전송하거나 데이터베이스에 저장할 수 있습니다.

            // 가입 완료 메시지 박스 표시
            register();

            //JOptionPane.showMessageDialog(this, "가입이 완료되었습니다.");
        });

        addFocusEvent(nameField, RegularExpression.NAME_EXPRESSION);
        addFocusEvent(idField, RegularExpression.ID_EXPRESSION);
        addFocusEvent(passwordField, RegularExpression.PASSWORD_EXPRESSION);
        addFocusEvent(birthdateField, RegularExpression.BIRTHDATE_EXPRESSION);
        addFocusEvent(phoneNumberMiddleField, RegularExpression.PHONE_NUMBER_MIDDLE_EXPRESSION);
        addFocusEvent(phoneNumberLastField, RegularExpression.PHONE_NUMBER_LAST_EXPRESSION);
        addFocusEvent(addressNumberField, RegularExpression.ADDRESS_NUMBER_EXPRESSION);

        addFocusEmptyCheckEvent(emailField);
        addFocusEmptyCheckEvent(addressField);

        addClickEvent();
        checkPassword();

        this.pack();
        this.setVisible(true);
    }

    private void addClickEvent() {
        JFrame frame = this;

        idCheckButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);

                if(isIdExists()) {
                    JOptionPane.showMessageDialog(frame, "존재하는 아이디입니다!");
                }

                else {
                    JOptionPane.showMessageDialog(frame, "사용할 수 있는 아이디입니다");
                }
            }
        });

        findAddressButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseReleased(MouseEvent e) {
                super.mouseReleased(e);

                String encodedAddress = URLEncoder.encode(addressField.getText());

                try {
                    URI target = new URI("https://www.juso.go.kr/support/AddressMainSearch.do?searchKeyword=" + encodedAddress);
                    java.awt.Desktop.getDesktop().browse(target);
                }
                catch(URISyntaxException | IOException err) {
                    err.printStackTrace();
                }
            }
        });
    }

    private void addFocusEmptyCheckEvent(JTextField field) {
        resetField(field);
        field.addFocusListener(new FocusAdapter() {
            @Override
            public void focusLost(FocusEvent e) {
                super.focusLost(e);

                if(field.getText().equals("")) {
                    field.setBorder(new LineBorder(Color.red, 2));
                }
            }
        });
    }

    private void addFocusEvent(JTextField field, String expression) {
        resetField(field);
        field.addFocusListener(new FocusAdapter() {
            @Override
            public void focusLost(FocusEvent e) {
                super.focusLost(e);

                if(!field.getText().matches(expression)) {
                    field.setBorder(new LineBorder(Color.red, 2));
                }
            }
        });
    }

    private void resetField(JTextField field) {
        field.addFocusListener(new FocusAdapter() {
            @Override
            public void focusGained(FocusEvent e) {
                super.focusGained(e);

                field.setBorder(null);
            }
        });
    }

    private void checkPassword() {
        resetField(passwordCheckField);
        passwordCheckField.addFocusListener(new FocusAdapter() {
            @Override
            public void focusLost(FocusEvent e) {
                super.focusGained(e);

                if(!passwordCheckField.getText().equals(passwordField.getText()) || passwordCheckField.getText().equals("")) {
                    passwordCheckField.setBorder(new LineBorder(Color.red, 2));
                }
            }
        });
    }

    private boolean isIdExists() {
        return UserDAO.getInstance().isIdExists(idField.getText());
    }

    private void register() {
        String email = emailField.getText() + "@" + emailDropdown.getSelectedItem().toString();
        String phoneNumber = phoneNumberFirstDropdown.getSelectedItem().toString() + "-" + phoneNumberMiddleField.getText() + "-" + phoneNumberLastField.getText();

        nameField.requestFocus();
        idField.requestFocus();
        passwordField.requestFocus();
        passwordCheckField.requestFocus();
        birthdateField.requestFocus();
        emailField.requestFocus();
        phoneNumberMiddleField.requestFocus();
        phoneNumberLastField.requestFocus();
        addressField.requestFocus();
        addressNumberField.requestFocus();

        if(!isInputValid()) {
            return;
        }


        if(isIdExists()) {
            JOptionPane.showMessageDialog(this, "존재하는 아이디입니다!");
            return;
        }

        if(UserDAO.getInstance().isEmailExists(email)) {
            JOptionPane.showMessageDialog(this, "존재하는 이메일입니다!");
            return;
        }

        if(UserDAO.getInstance().isPhoneNumberExists(phoneNumber)) {
            JOptionPane.showMessageDialog(this, "존재하는 번호입니다!");
            return;
        }

        UserDTO user = new UserDTO();

        user.setName(nameField.getText());
        user.setId(idField.getText());
        user.setPassword(passwordField.getText());
        user.setBirthdate(birthdateField.getText());
        user.setEmail(emailField.getText() + "@" + emailDropdown.getSelectedItem().toString());
        user.setPhoneNumber(phoneNumberFirstDropdown.getSelectedItem().toString() + "-" + phoneNumberMiddleField.getText() + "-" + phoneNumberLastField.getText());
        user.setAddress(addressField.getText());
        user.setAddressNumber(Integer.parseInt(addressNumberField.getText()));

        UserDAO.getInstance().register(user);

        JOptionPane.showMessageDialog(this, "회원가입 성공!");

        this.dispose();
    }

    public boolean isInputValid() {
        return nameField.getText().matches(RegularExpression.NAME_EXPRESSION) &&
                idField.getText().matches(RegularExpression.ID_EXPRESSION) &&
                passwordField.getText().matches(RegularExpression.PASSWORD_EXPRESSION) &&
                birthdateField.getText().matches(RegularExpression.BIRTHDATE_EXPRESSION) &&
                phoneNumberMiddleField.getText().matches(RegularExpression.PHONE_NUMBER_MIDDLE_EXPRESSION) &&
                phoneNumberLastField.getText().matches(RegularExpression.PHONE_NUMBER_LAST_EXPRESSION) &&
                addressNumberField.getText().matches(RegularExpression.ADDRESS_NUMBER_EXPRESSION) &&
                passwordField.getText().equals(passwordCheckField.getText()) &&
                !emailField.getText().equals("") &&
                !addressField.getText().equals("");
    }
}
