using System.Data;
using MySql.Data.MySqlClient;

namespace Library.Utility
{
    public class SqlManager
    {
        private DataSet dataSet;
        private MySqlConnection conn;

        public MySqlConnection Conn
        {
            get => this.conn;
        }
        
        public SqlManager()
        {
            string strConn = "Server=localhost;Database=Library;Uid=root;Pwd=Dnltjd01!;";
            
            this.dataSet = new DataSet();
            this.conn = new MySqlConnection(strConn);
        }

        public DataSet ExecuteSql(string sql, string table)
        {
            this.dataSet.Clear();
            
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(dataSet, table);
            
            return this.dataSet;
        }
    }
}