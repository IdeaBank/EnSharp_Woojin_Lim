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
            // 서버에 접속하기 위한 정보
            string strConn = "Server=localhost;Database=Library;Uid=root;Pwd=Dnltjd01!;";
            
            this.dataSet = new DataSet();
            this.conn = new MySqlConnection(strConn);
        }

        public DataSet ExecuteSql(string sql, string table)
        {
            // 데이터를 얻어올 경우, 기존 데이터를 지우고 그 곳에 넣음
            this.dataSet.Clear();
            
            // SQL 쿼리문 실행 결과를 dataSet에 넣음
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(dataSet, table);
            
            // 이를 복사하여 전달 (dataSet을 여러 번 사용하는 코드에서 결과 꼬이는 것을 방지)
            return this.dataSet.Copy();
        }
    }
}