using System.Data;
using MySql.Data.MySqlClient;

namespace Library.Utility
{
    public class DatabaseConnection
    {
        private MySqlConnection conn;
        private static DatabaseConnection _instance;

        private DatabaseConnection()
        {
            string strConn = "Server=localhost;Database=woojin_library;Uid=root;Pwd=Dnltjd01!;";

            conn = new MySqlConnection(strConn);
        }

        public static DatabaseConnection getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection();
                }

                return _instance;
            }
        }

        public MySqlConnection Conn
        {
            get => this.conn;
        }

        public DataSet ExecuteSelection(MySqlCommand command, string tableName)
        {
            DataSet dataSet = new DataSet();

            MySqlDataAdapter adpt = new MySqlDataAdapter();
            adpt.SelectCommand = command;
            
            conn.Open();
            adpt.Fill(dataSet, tableName);
            conn.Close();

            return dataSet;
        }

        public void ExecuteCommand(MySqlCommand command)
        {
            this.conn.Open();

            command.ExecuteNonQuery();

            this.conn.Close();
        }
    }
}