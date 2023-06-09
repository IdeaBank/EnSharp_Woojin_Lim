package model;

import constant.LoginResult;
import controller.DatabaseConnector;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class UserDAO {
    private static UserDAO _instance;

    private UserDAO() {

    }

    public static UserDAO getInstance() {
        if(_instance == null) {
            _instance = new UserDAO();
        }

        return _instance;
    }

    private boolean isStringValueInUserExists(String column, String value) {
        try {
            PreparedStatement ps = DatabaseConnector.getInstance().getConn().prepareStatement("select " + column + " from user where " + column + "=?");
            ps.setString(1, value);
            ResultSet result = ps.executeQuery();

            return result.next();
        }
        catch(SQLException e) {
            return false;
        }
    }

    public boolean isIdExists(String id) {
        return isStringValueInUserExists("id", id);
    }

    public boolean isEmailExists(String email) {
        return isStringValueInUserExists("email", email);
    }

    public boolean isPhoneNumberExists(String phoneNumber) {
        return isStringValueInUserExists("phone_number", phoneNumber);
    }

    public LoginResult tryLogin(String id, String password) {
        try {
            PreparedStatement ps = DatabaseConnector.getInstance().getConn().prepareStatement("select name from user where id=?");
            ps.setString(1, id);
            ResultSet result = ps.executeQuery();

            if(!result.next()) {
                return LoginResult.NO_ID;
            }

            ps = DatabaseConnector.getInstance().getConn().prepareStatement("select name from user where id=? and password=?");
            ps.setString(1, id);
            ps.setString(2, password);
            result = ps.executeQuery();

            if(result.next()) {
                return LoginResult.SUCCESS;
            }

            return LoginResult.WRONG_PASSWORD;
        }
        catch(SQLException e) {
            return null;
        }
    }

    public void withDraw(String id) {
        try {
            PreparedStatement ps = DatabaseConnector.getInstance().getConn().prepareStatement("delete from user where id=?");
            ps.setString(1, id);
            ps.execute();
        }
        catch(SQLException e) {
            e.printStackTrace();
        }
    }
}
