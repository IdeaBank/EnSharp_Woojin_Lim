using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Library.Constant;
using Library.Model.DTO;
using Library.Utility;
using MySql.Data.MySqlClient;

namespace Library.Model.DAO
{
    public class LogDAO
    {
        private static LogDAO _instance;

        private LogDAO()
        {
            
        }
        
        public static LogDAO getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogDAO();
                }

                return _instance;
            }
        }

        public LogDTO DataRowToLogDTO(DataRow row)
        {
            LogDTO log = new LogDTO();

            log.LogId = (int)row["log_id"];
            log.LogTime = row["log_time"].ToString();
            log.UserName = row["user_name"].ToString();
            log.LogContents = row["log_contents"].ToString();
            log.LogAction = row["log_action"].ToString();

            return log;
        }
        
        public List<LogDTO> GetAllLog()
        {
            List<LogDTO> logs = new List<LogDTO>();
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_ALL_LOG;

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "log_data");

            foreach (DataRow row in dataSet.Tables["log_data"].Rows)
            {
                LogDTO log = DataRowToLogDTO(row);
                
                logs.Add(log);
            }

            return logs;
        }
        
        public void InsertLog(LogDTO log)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INSERT_LOG;
            
            command.Parameters.AddWithValue("@log_time", log.LogTime);
            command.Parameters.AddWithValue("@user_name", log.UserName);
            command.Parameters.AddWithValue("@log_contents", log.LogContents);
            command.Parameters.AddWithValue("@log_action", log.LogAction);

            DatabaseConnection.getInstance.ExecuteCommand(command);
        }

        public ResultCode DeleteLog(int logId)
        {
            List<LogDTO> logs = GetAllLog();

            foreach (LogDTO log in logs)
            {
                if (log.LogId == logId)
                {
                    MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
                    command.CommandText = SqlQuery.DELETE_LOG;
            
                    command.Parameters.AddWithValue("@log_id", log.LogId);

                    DatabaseConnection.getInstance.ExecuteCommand(command);
                    
                    return ResultCode.SUCCESS;
                }
            }

            return ResultCode.NO_LOG;
        }

        public ResultCode DeleteFile()
        {
            string desktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileLocation = desktopLocation + "/Log.txt";
            
            if(!File.Exists(fileLocation))
            {
                return ResultCode.FAIL;
            }
            
            File.Delete(fileLocation);

            return ResultCode.SUCCESS;
        }

        public void SaveFile()
        {
            string desktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileLocation = desktopLocation + "/Log.txt";
            
            List<LogDTO> logs = GetAllLog();

            DeleteFile();
            
            using (FileStream fs = File.Create(fileLocation))
            {
                foreach(LogDTO log in logs)
                {
                    string line = string.Empty;

                    line += "순서: " + log.LogId + " / ";
                    line += "시간: " + log.LogTime + " / ";
                    line += "사용자: " + log.UserName + " / ";
                    line += "내용: " + log.LogContents + " / ";
                    line += "명령: " + log.LogAction + "\r\n";
                    
                    AddText(fs, line);
                }
            }

            //Open the stream and read it back.
            using (FileStream fs = File.OpenRead(fileLocation))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b,0,b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
        }
        
        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        public void ResetLog()
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.DELETE_ALL_LOG;
            
            DatabaseConnection.getInstance.ExecuteCommand(command);
        }
    }
}