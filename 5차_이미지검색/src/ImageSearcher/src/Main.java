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
        });

        //System.out.println(new Timestamp(System.currentTimeMillis()).getTime());
    }
}