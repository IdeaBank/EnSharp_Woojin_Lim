package DAO;

import java.sql.*;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class SearchLogDAO {
    private static SearchLogDAO _instance;
    private Connection conn;

    private SearchLogDAO()
    {
        String url = "jdbc:mysql://localhost:3306/woojin_search_image";
        String id = "root";
        String password = "Dnltjd01!";

        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            conn = DriverManager.getConnection(url + "?createDatabaseIfNotExist=true", id, password);
        } catch(Exception e) {
            System.out.println(e.toString());
        }
    }

    public static SearchLogDAO GetInstance()
    {
        if(_instance == null)
        {
            _instance = new SearchLogDAO();
        }

        return _instance;
    }

    public void GetAllLog()
    {
        String sql = "select * from search_log";

        Statement stmt = null;

        try{
            stmt = conn.prepareStatement(sql);
            ResultSet rs = stmt.executeQuery(sql);

            if(rs.next() == false)
            {
                System.out.println("NUL");
            }

        }catch(Exception e) {
           System.out.println(e.toString());
        }
    }

    public boolean LogExists(String query)
    {
        String sql = "select * from search_log where search_query='" + query + "'";

        Statement stmt = null;

        try{
            stmt = conn.prepareStatement(sql);
            ResultSet rs = stmt.executeQuery(sql);

            if(rs.next() == false)
            {
                return false;
            }

        }catch(Exception e) {
            System.out.println(e.toString());
        }

        return true;
    }

    public void InsertLog(String query) {
        String sql;
        PreparedStatement pstmt = null;

        if(LogExists(query)) {
            sql = "update search_log set search_time=? where search_query=?";

            TryInsertLog(query, sql);
        }

        else {
            sql = "insert into search_log(search_time, search_query) values(?, ?)";


            TryInsertLog(query, sql);
        }
    }

    private void TryInsertLog(String query, String sql) {
        PreparedStatement pstmt;

        try {
            pstmt = conn.prepareStatement(sql);
            pstmt.setString(1, LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss")));
            pstmt.setString(2, query);

            pstmt.executeUpdate();

            pstmt.close();
        }catch(Exception e)
        {
            System.out.println(e.toString());
        }
    }
}
