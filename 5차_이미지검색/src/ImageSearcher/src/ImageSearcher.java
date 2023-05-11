import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.net.URL;
import java.nio.file.Paths;

public class ImageSearcher extends JFrame implements ActionListener {
    private JTextField inputMsg;

    public ImageSearcher()
    {
        InitializeFrame();

        setLayout(new FlowLayout());

        //문자열을 입력할수 있는 UI
        inputMsg=new JTextField(10);


        //전송버튼
        JButton sendBtn=new JButton("전송");
        sendBtn.setActionCommand("send");
        sendBtn.addActionListener(this);
        add(sendBtn);

        //삭제버튼
        JButton deleteBtn = new JButton("삭제");
        deleteBtn.setActionCommand("delete");
        deleteBtn.addActionListener(this);

        //패널 객체를 생성해서
        JPanel panel = new JPanel();
        //패널에 UI를 추가하고
        panel.add(inputMsg);
        panel.add(sendBtn);
        panel.add(deleteBtn);
        //패널 통채로 프레임에 추가하기
        add(panel);

        // 프레임을 보이도록 설정
        setVisible(true);
    }

    private void InitializeFrame()
    {
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        int width = (int)screenSize.getWidth();
        int height = (int)screenSize.getHeight();

        String currentWorkingDirectory = Paths.get("").toAbsolutePath().toString();
        String iconImagePath = currentWorkingDirectory + "/resource/icon.jpg";

        Image iconImage = Toolkit.getDefaultToolkit().getImage(iconImagePath);

        setIconImage(iconImage);
        setTitle("Search Image");
        setSize((int)(width * 0.75), (int)(height * 0.75));
        setLocationRelativeTo(null);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        //이벤트가 발생한 버튼에 설정된 action command 읽어오기
        String command=e.getActionCommand();
        if(command.equals("send")) {
            //JTextField에 입력한 문자열 읽어오기
            String msg = inputMsg.getText();
            JOptionPane.showMessageDialog(this, msg);
        }else if (command.equals("delete")) {
            //빈 문자열을 넣어주어서 삭제하기
            inputMsg.setText("");
        }
    }
}
