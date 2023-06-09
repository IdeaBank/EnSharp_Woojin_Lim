package model;

import constant.LoginResult;
import controller.DatabaseConnector;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Arrays;

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

    public void register(UserDTO user) {
        try {
            PreparedStatement ps = DatabaseConnector.getInstance().getConn().prepareStatement("insert into user values(?, ?, ?, ?, ?, ?, ?, ?);");
            ps.setString(1, user.getId());
            ps.setString(2, user.getName());
            ps.setString(3, user.getPassword());
            ps.setString(4, user.getBirthdate());
            ps.setString(5, user.getEmail());
            ps.setString(6, user.getPhoneNumber());
            ps.setString(7, user.getAddress());
            ps.setInt(8, user.getAddressNumber());

            ps.execute();
        }

        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void editUser(UserDTO user) {
        try {
            PreparedStatement ps = DatabaseConnector.getInstance().getConn().prepareStatement("update user set name=?, password=?, birthdate=?, email=?, phone_number=?, address=?, address_number=? where id=?");
            ps.setString(1, user.getName());
            ps.setString(2, user.getPassword());
            ps.setString(3, user.getBirthdate());
            ps.setString(4, user.getEmail());
            ps.setString(5, user.getPhoneNumber());
            ps.setString(6, user.getAddress());
            ps.setInt(7, user.getAddressNumber());
            ps.setString(8, user.getId());

            ps.execute();
        }

        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public UserDTO getUser(String id) {
        try {
            PreparedStatement ps = DatabaseConnector.getInstance().getConn().prepareStatement("select *, FORMAT(VARCHAR(6), birthdate, 'ddMMyy') as formattedDate from user where id=?");
            ps.setString(1, id);
            ResultSet result = ps.executeQuery();

            result.next();

            UserDTO user = new UserDTO();

            user.setId(result.getString("id"));
            user.setName(result.getString("name"));
            user.setPassword(result.getString("password"));
            user.setBirthdate(result.getString("formattedDate"));
            user.setEmail(result.getString("email"));
            user.setPhoneNumber(result.getString("phone_number"));
            user.setAddress(result.getString("address"));
            user.setAddressNumber(result.getInt("address_number"));

            return user;
        }
        catch(SQLException e) {
            return new UserDTO();
        }
    }
}
