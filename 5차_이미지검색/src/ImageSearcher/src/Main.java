import DAO.ImageInformationDAO;
import DAO.SearchLogDAO;

import javax.swing.*;
import java.awt.*;
import java.nio.file.Paths;
import java.sql.Timestamp;
import java.util.Date;

public class Main {
    public static void main(String[] args) throws Exception {
        SwingUtilities.invokeLater(() -> {
            try {
                new ImageSearcher();
            } catch (Exception e) {
                throw new RuntimeException(e);
            }
        });

        System.out.println("Working Directory = " + System.getProperty("user.dir"));
    }
}