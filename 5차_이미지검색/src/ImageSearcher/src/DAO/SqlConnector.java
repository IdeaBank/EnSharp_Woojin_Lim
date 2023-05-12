package DAO;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.Statement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class SqlConnector {
    public static SqlConnector _instance;
    private Connection conn = null;
    private Statement statement = null;
    private ResultSet resultSet = null;
    private SqlConnector()
    {
    }

    public static SqlConnector GetInstance()
    {
        if(_instance == null)
        {
            _instance = new SqlConnector();
        }

        return _instance;
    }


}
