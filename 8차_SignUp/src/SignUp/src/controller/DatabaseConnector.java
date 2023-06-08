package controller;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DatabaseConnector {
    private static String id = "root";
    private static String password = "Dnltjd01!";

    private static DatabaseConnector _instance;
    private Connection conn;

    private DatabaseConnector() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            conn = DriverManager.getConnection("jdbc:mysql://localhost/woojin_signup", id, password);
        }
        catch (ClassNotFoundException | SQLException e) {
            e.printStackTrace();
        }
    }

    public static DatabaseConnector getInstance() {
        if(_instance == null) {
            _instance = new DatabaseConnector();
        }

        return _instance;
    }

    public Connection getConn() {
        return conn;
    }

}
