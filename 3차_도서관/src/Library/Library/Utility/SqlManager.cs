using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Library.Utility
{
    public sealed class SqlManager
    {
        private static MySqlConnection conn;
        private static SqlManager _instance;

        private SqlManager()
        {
            string strConn = "Server=localhost;Database=woojin_library;Uid=root;Pwd=Dnltjd01!;";
            
            conn = new MySqlConnection(strConn);
        }
        
        public static SqlManager getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SqlManager();
                }

                return _instance;
            }
        }
        public MySqlConnection Conn
        {
            get => conn;
        }
        

        public DataSet ExecuteSql(string sql, string table)
        {
            DataSet dataSet = new DataSet();
            
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(dataSet, table);
            
            return dataSet.Copy();
        }
    }
}