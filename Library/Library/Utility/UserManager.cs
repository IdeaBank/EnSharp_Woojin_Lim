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

        private KeyValuePair<FailCode, int> GetUserIndex(int userNumber)
        {
            for(int i = 0; i < totalData.Users.Count; ++i)
            {
                if (totalData.Users[i].Number == userNumber)
                {
                    return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, i);
                }
            }

            return new KeyValuePair<FailCode, int>(FailCode.NO_USER, -1);
        }
        
        public FailCode AddUser(string id, string password, string name, int birthYear, string phoneNumber, string address)
        {
            foreach (User tempUser in totalData.Users)
            {
                if (tempUser.Id == id)
                {
                    return FailCode.USER_ID_EXISTS;
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
            
            return FailCode.SUCCESS;
        }

        public KeyValuePair<FailCode, int> LoginAsUser(string id, string password)
        {
            for (int i = 0; i < totalData.Users.Count; ++i)
            {
                if (totalData.Users[i].Id == id)
                {
                    if (totalData.Users[i].Password == password)
                    {
                        return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, i);
                    }

                    return new KeyValuePair<FailCode, int>(FailCode.WRONG_PASSWORD, -1);
                }
            }

            return new KeyValuePair<FailCode, int>(FailCode.NO_ID, -1);
        }

        public KeyValuePair<FailCode, int> LoginAsAdministrator(string id, string password)
        {
            for (int i = 0; i < totalData.Administrators.Count; ++i)
            {
                if (totalData.Administrators[i].Id == id)
                {
                    if (totalData.Administrators[i].Password == password)
                    {
                        return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, i);
                    }

                    return new KeyValuePair<FailCode, int>(FailCode.WRONG_PASSWORD, -1);
                }
            }
            
            return new KeyValuePair<FailCode, int>(FailCode.NO_ID, -1);
        }

        public FailCode DeleteUser(int userNumber)
        {
            KeyValuePair<FailCode, int> findResult = GetUserIndex(userNumber);
            
            int userIndex = findResult.Value;

            if (findResult.Key == FailCode.SUCCESS)
            {
                totalData.Users.RemoveAt(findResult.Value);
                
                return FailCode.SUCCESS;
            }

            return FailCode.NO_USER;
        }
    }
}