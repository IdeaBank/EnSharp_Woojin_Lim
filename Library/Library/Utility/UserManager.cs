using Library.Constants;
using Library.Model;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Library.Utility
{
    public class UserManager
    {
        private TotalData totalData;
        private SqlManager sqlManager;
        private DataSet dataSet;
        
        public UserManager(TotalData totalData, SqlManager sqlManager)
        {
            this.totalData = totalData;
            this.sqlManager = sqlManager;
        }

        private bool IsUserExist(string id)
        {
            dataSet = sqlManager.ExecuteSql("select * from User where id=\'" + id + "\'", "User");

            if (dataSet.Tables["User"].Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        public ResultCode AddUser(string id, string password, string name, int birthYear, string phoneNumber, string address)
        {
            if (IsUserExist(id))
            {
                return ResultCode.USER_ID_EXISTS;
            }
            
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "INSERT INTO User(id, password, name, birth_year, phone_number, address) VALUES(@id, @password, @name, @birth_year, @phone_number, @address)";
            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@password", password);
            comm.Parameters.AddWithValue("@name", name);
            comm.Parameters.AddWithValue("@birth_year", birthYear);
            comm.Parameters.AddWithValue("@phone_number", phoneNumber);
            comm.Parameters.AddWithValue("@address", address);
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();
            
            return ResultCode.SUCCESS;
        }

        public KeyValuePair<ResultCode, string> LoginAsUser(string id, string password)
        {
            dataSet = sqlManager.ExecuteSql("select id, password from User", "User");

            foreach (DataRow row in dataSet.Tables["User"].Rows)
            {
                if (row["id"].ToString() == id)
                {
                    if (row["password"].ToString() == password)
                    {
                        return new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, row["id"].ToString());
                    }

                    return new KeyValuePair<ResultCode, string>(ResultCode.WRONG_PASSWORD, "");
                }
            }

            return new KeyValuePair<ResultCode, string>(ResultCode.NO_ID, "");
        }

        public KeyValuePair<ResultCode, int> LoginAsAdministrator(string id, string password)
        {
            for (int i = 0; i < totalData.Administrators.Count; ++i)
            {
                if (totalData.Administrators[i].Id == id)
                {
                    if (totalData.Administrators[i].Password == password)
                    {
                        return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, i);
                    }

                    return new KeyValuePair<ResultCode, int>(ResultCode.WRONG_PASSWORD, -1);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.NO_ID, -1);
        }
        
        public void EditUser(string userId, string password, string name, string birthYear, string phoneNumber, string address)
        {
            string updateQuery = "set ";

            updateQuery += "password=\'" + password + "\', ";
            updateQuery += "name=\'" + name + "\', ";
            updateQuery += "birth_year=" + birthYear + ", ";
            updateQuery += "phone_number=\'" + phoneNumber + "\', ";
            updateQuery += "address=\'" + address + "\'";
            
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "update User " + updateQuery + " where id=\'" + userId + "\'";
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();
        }

        public ResultCode DeleteUser(string userId)
        {
            this.dataSet = sqlManager.ExecuteSql("select * from Borrowed_Book where user_id=\'" + userId + "\'", "Borrowed_Book");

            if (!IsUserExist(userId))
            {
                return ResultCode.NO_USER;
            }
            
            if (dataSet.Tables["Borrowed_Book"].Rows.Count != 0)
            {
                return ResultCode.MUST_RETURN_BOOK;
            }
            
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "delete from User where id=\'" + userId + "\'";
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();

            return ResultCode.SUCCESS;
        }

        public DataSet SearchUser(string name, string id, string address)
        {
            // 유저 검색 결과를 저장하기 위한 리스트 선언
            this.dataSet = sqlManager.ExecuteSql("select * from User where name like \'%" + name + "%\' and id like \'%" + id + "%\' and address like \'%" + address + "%\'", "User");

            return dataSet;
        }

        public DataSet GetUser(string userId)
        { 
            dataSet = sqlManager.ExecuteSql("select * from User where id=\'" + userId + "\'", "User");

            return dataSet;
        }
    }
}