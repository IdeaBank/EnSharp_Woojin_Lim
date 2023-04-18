using System.Collections.Generic;
using Library.Constants;
using Library.Model;

namespace Library.Utility
{
    public class UserManager
    {
        private TotalData totalData;

        public UserManager(TotalData totalData)
        {
            this.totalData = totalData;
        }

        private KeyValuePair<ResultCode, int> GetUserIndex(int userNumber)
        {
            for(int i = 0; i < totalData.Users.Count; ++i)
            {
                if (totalData.Users[i].Number == userNumber)
                {
                    return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, i);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.NO_USER, -1);
        }
        
        public ResultCode AddUser(string id, string password, string name, int birthYear, string phoneNumber, string address)
        {
            foreach (User tempUser in totalData.Users)
            {
                if (tempUser.Id == id)
                {
                    return ResultCode.USER_ID_EXISTS;
                }
            }
            
            User user = new User();
            
            totalData.AddedUserCount += 1;
            user.Number = totalData.AddedUserCount;

            user.Id = id;
            user.Password = password;
            user.Name = name;
            user.BirthYear = birthYear;
            user.PhoneNumber = phoneNumber;
            user.Address = address;

            totalData.Users.Add(user);
            
            return ResultCode.SUCCESS;
        }

        public KeyValuePair<ResultCode, int> LoginAsUser(string id, string password)
        {
            for (int i = 0; i < totalData.Users.Count; ++i)
            {
                if (totalData.Users[i].Id == id)
                {
                    if (totalData.Users[i].Password == password)
                    {
                        return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, i);
                    }

                    return new KeyValuePair<ResultCode, int>(ResultCode.WRONG_PASSWORD, -1);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.NO_ID, -1);
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

        public ResultCode DeleteUser(int userNumber)
        {
            KeyValuePair<ResultCode, int> findResult = GetUserIndex(userNumber);
            
            int userIndex = findResult.Value;

            if (findResult.Key == ResultCode.SUCCESS)
            {
                if (totalData.Users[userIndex].BorrowedBooks.Count > 0)
                {
                    return ResultCode.MUST_RETURN_BOOK;
                }
                
                totalData.Users.RemoveAt(userIndex);
                
                return ResultCode.SUCCESS;
            }

            return ResultCode.NO_USER;
        }
    }
}