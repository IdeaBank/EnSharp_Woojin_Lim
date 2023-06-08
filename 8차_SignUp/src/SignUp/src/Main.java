import com.mysql.cj.jdbc.exceptions.SQLError;
import controller.DatabaseConnector;
import controller.EmailSender;
import model.UserDAO;
import view.LoginFrame;
import view.MainMenuFrame;
import view.SignUpFrame;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class Main {
    public static void main(String[] args) {
        //LoginFrame loginFrame = new LoginFrame();
        //loginFrame.start();
        SignUpFrame signup = new SignUpFrame();
    }
}