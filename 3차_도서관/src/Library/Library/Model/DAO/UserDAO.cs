using Library.Constant;
using Library.Model.DTO;
using Library.Utility;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Library.Model.DAO
{
    public class UserDAO
    {
        private static UserDAO _instance;

        private UserDAO()
        {
        }

        public static UserDAO getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserDAO();
                }

                return _instance;
            }
        }

        public bool UserExists(string userId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_USER_WITH_ID;
            command.Parameters.AddWithValue("@id", userId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "user");

            if (dataSet.Tables["user"].Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        private UserDTO DataRowToUserDTO(DataRow row)
        {
            UserDTO user = new UserDTO
            {
                Id = row["id"].ToString(),
                Password = row["password"].ToString(),
                Name = row["name"].ToString(),
                BirthYear = int.Parse(row["birth_year"].ToString()),
                PhoneNumber = row["phone_number"].ToString(),
                Address = row["address"].ToString()
            };

            return user;
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_ALL_USER;

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "user");

            foreach (DataRow row in dataSet.Tables["user"].Rows)
            {
                users.Add(DataRowToUserDTO(row));
            }

            return users;
        }

        public UserDTO GetUserInfo(string userId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_USER_WITH_ID;

            command.Parameters.AddWithValue("@id", userId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "user");
            DataRow dataRow = dataSet.Tables["user"].Rows[0];

            return DataRowToUserDTO(dataRow);
        }

        public ResultCode AddUser(UserDTO user)
        {
            if (UserExists(user.Id))
            {
                return ResultCode.USER_ID_EXISTS;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INSERT_USER;

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@birth_year", user.BirthYear);
            command.Parameters.AddWithValue("@phone_number", user.PhoneNumber);
            command.Parameters.AddWithValue("@address", user.Address);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }

        public ResultCode LoginAsUser(string userId, string userPassword)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_USER_WITH_ID;

            command.Parameters.AddWithValue("@id", userId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "user");

            if (dataSet.Tables["user"].Rows.Count == 0)
            {
                return ResultCode.NO_ID;
            }

            if (dataSet.Tables["user"].Rows[0]["password"].ToString() == userPassword)
            {
                return ResultCode.SUCCESS;
            }

            return ResultCode.WRONG_PASSWORD;
        }

        public ResultCode LoginAsAdministrator(string userId, string userPassword)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_ADMINISTRATOR_WITH_ID;

            command.Parameters.AddWithValue("@id", userId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "administrator");

            if (dataSet.Tables["administrator"].Rows.Count == 0)
            {
                return ResultCode.NO_ID;
            }

            if (dataSet.Tables["administrator"].Rows[0]["password"].ToString() == userPassword)
            {
                return ResultCode.SUCCESS;
            }

            return ResultCode.WRONG_PASSWORD;
        }

        public void EditUser(UserDTO user)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.EDIT_USER;

            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@birth_year", user.BirthYear);
            command.Parameters.AddWithValue("@phone_number", user.PhoneNumber);
            command.Parameters.AddWithValue("@address", user.Address);

            command.Parameters.AddWithValue("@id", user.Id);

            DatabaseConnection.getInstance.ExecuteCommand(command);
        }

        public ResultCode DeleteUser(string userId)
        {
            if (!UserExists(userId))
            {
                return ResultCode.NO_USER;
            }

            if (BookDAO.getInstance.GetBorrowedBooks(userId).Count != 0)
            {
                return ResultCode.MUST_RETURN_BOOK;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.DELETE_USER;

            command.Parameters.AddWithValue("@id", userId);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }

        public List<UserDTO> SearchUser(string name, string id, string address)
        {
            if (name == "" && id == "" && address == "")
            {
                return new List<UserDTO>();
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_USER_WITH_SEARCH_STRING;

            command.Parameters.AddWithValue("@name", "%" + name + "%");
            command.Parameters.AddWithValue("@id", "%" + id + "%");
            command.Parameters.AddWithValue("@address", "%" + address + "%");

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "user");

            List<UserDTO> users = new List<UserDTO>();

            foreach (DataRow row in dataSet.Tables["user"].Rows)
            {
                users.Add(DataRowToUserDTO(row));
            }

            return users;
        }
    }
}