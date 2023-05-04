using Library.Constants;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Library.Utility
{
    public class UserManager
    {
        private DataSet dataSet;
        
        public UserManager()
        {
        }

        private bool IsUserExist(string id)
        {
            dataSet = SqlManager.getInstance.ExecuteSql("select * from User where id=\'" + id + "\'", "User");

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
            
            SqlManager.getInstance.Conn.Open();
            
            MySqlCommand comm = SqlManager.getInstance.Conn.CreateCommand();
            comm.CommandText = "INSERT INTO User(id, password, name, birth_year, phone_number, address) VALUES(@id, @password, @name, @birth_year, @phone_number, @address)";
            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@password", password);
            comm.Parameters.AddWithValue("@name", name);
            comm.Parameters.AddWithValue("@birth_year", birthYear);
            comm.Parameters.AddWithValue("@phone_number", phoneNumber);
            comm.Parameters.AddWithValue("@address", address);
            comm.ExecuteNonQuery();
            
            SqlManager.getInstance.Conn.Close();
            
            return ResultCode.SUCCESS;
        }

        public KeyValuePair<ResultCode, string> LoginAsUser(string id, string password)
        {
            dataSet = SqlManager.getInstance.ExecuteSql("select id, password from User", "user");

            foreach (DataRow row in dataSet.Tables["user"].Rows)
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

        public KeyValuePair<ResultCode, string> LoginAsAdministrator(string id, string password)
        {
            dataSet = SqlManager.getInstance.ExecuteSql("select id, password from Administrator", "administrator");

            foreach (DataRow row in dataSet.Tables["administrator"].Rows)
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
        
        public void EditUser(string userId, string password, string name, string birthYear, string phoneNumber, string address)
        {
            string updateQuery = "set ";

            updateQuery += "password=\'" + password + "\', ";
            updateQuery += "name=\'" + name + "\', ";
            updateQuery += "birth_year=" + birthYear + ", ";
            updateQuery += "phone_number=\'" + phoneNumber + "\', ";
            updateQuery += "address=\'" + address + "\'";
            
            SqlManager.getInstance.Conn.Open();
            
            MySqlCommand comm = SqlManager.getInstance.Conn.CreateCommand();
            comm.CommandText = "update User " + updateQuery + " where id=\'" + userId + "\'";
            comm.ExecuteNonQuery();
            
            SqlManager.getInstance.Conn.Close();
        }

        public ResultCode DeleteUser(string userId)
        {
            this.dataSet = SqlManager.getInstance.ExecuteSql("select * from Borrowed_Book where user_id=\'" + userId + "\'", "Borrowed_Book");

            if (!IsUserExist(userId))
            {
                return ResultCode.NO_USER;
            }
            
            if (dataSet.Tables["Borrowed_Book"].Rows.Count != 0)
            {
                return ResultCode.MUST_RETURN_BOOK;
            }
            
            SqlManager.getInstance.Conn.Open();
            
            MySqlCommand comm = SqlManager.getInstance.Conn.CreateCommand();
            comm.CommandText = "delete from User where id=\'" + userId + "\'";
            comm.ExecuteNonQuery();
            
            SqlManager.getInstance.Conn.Close();

            return ResultCode.SUCCESS;
        }

        public DataSet SearchUser(string name, string id, string address)
        {
            // 유저 검색 결과를 저장하기 위한 리스트 선언
            this.dataSet = SqlManager.getInstance.ExecuteSql("select * from User where name like \'%" + name + "%\' and id like \'%" + id + "%\' and address like \'%" + address + "%\'", "User");

            return dataSet;
        }

        public DataSet GetUser(string userId)
        { 
            dataSet = SqlManager.getInstance.ExecuteSql("select * from User where id=\'" + userId + "\'", "User");

            return dataSet;
        }
    }
}