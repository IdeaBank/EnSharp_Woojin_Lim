import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.net.URL;
import java.nio.file.Paths;

public class ImageSearcher extends JFrame {
    public ImageSearcher()
    {
        InitializeFrame();

        // 라벨 생성
        JLabel label = new JLabel("안녕, 세계!", SwingConstants.LEFT);
        add(label); // 라벨을 프레임에 추가

        label.addMouseListener(new MouseAdapter()
        {
            public void mouseClicked(MouseEvent e)
            {
                // you can open a new frame here as
                // i have assumed you have declared "frame" as instance variable
                setVisible(false);
            }
        });

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
}
