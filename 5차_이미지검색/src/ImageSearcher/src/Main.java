import DAO.ImageInformationDAO;

import javax.swing.*;
import java.awt.*;
import java.nio.file.Paths;
import java.sql.Timestamp;
import java.util.Date;

public class Main {
    public static void main(String[] args) throws Exception {
        SwingUtilities.invokeLater(() -> {
            new ImageSearcher();
            new ImageSearcher();

            JFrame frame = new JFrame("App");
            frame.setContentPane(new ImageSearcherView().MyJPanel);
            frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
            frame.pack();
            frame.setVisible(true);
        });

        ImageInformationDAO.GetInstance().Test();
        //System.out.println(new Timestamp(System.currentTimeMillis()).getTime());
    }
}