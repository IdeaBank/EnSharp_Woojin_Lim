using System;
using System.Collections.Generic;
using Library.Model;

namespace Library
{
    public class UserManager
    {
        private int GetUserIndex(Data data, int userNumber)
        {
            for(int i = 0; i < data.users.Count; ++i)
            {
                if (data.users[i].userNumber == userNumber)
                {
                    return i;
                }
            }

            return -1;
        }
        
        public void ModifyMemberInfo(Data data, int userNumber, string userId, string userPassword, string userName, int userAge, string userPhoneNumber, string userAddress)
        {
            int userIndex = GetUserIndex(data, userNumber);

            if (userIndex != -1)
            {
                data.users[userIndex].id = userId;
                data.users[userIndex].password = userPassword;
                data.users[userIndex].name = userName;
                data.users[userIndex].age = userAge;
                data.users[userIndex].phoneNumber = userPhoneNumber;
                data.users[userIndex].address = userAddress;
            }
        }

        public bool Register(Data data, User user)
        {
            foreach (User tempUser in data.users)
            {
                if (tempUser.id == user.id)
                {
                    return false;
                }
            }

            user.userNumber = data.userIdCount;
            data.userIdCount = data.userIdCount + 1;

            data.users.Add(user);
            return true;
        }

        public bool Withdraw(Data data, int userNumber)
        {
            foreach (User user in data.users)
            {
                if (user.userNumber == userNumber)
                {
                    data.users.Remove(user);
                    return true;
                }
            }

            return false;
        }

        public KeyValuePair<bool[], int> LoginAsUser(Data data, string id, string password)
        {
            foreach (User user in data.users)
            {
                if (user.id == id)
                {
                    if (user.password == password)
                    {
                        return new KeyValuePair<bool[], int>( new bool[]{true, true}, user.userNumber);
                    }

                    return new KeyValuePair<bool[], int>(new bool[] { true, false }, user.userNumber);
                }
            }

            return new KeyValuePair<bool[], int>(new bool[] { false, true }, 0);
        }

        public bool[] LoginAsAdministrator(Data data, string id, string password)
        {
            bool[] result = new bool[2] { true, true };
            foreach(Administrator admin in data.admins)
            {
                if (admin.id == id)
                {
                    if (admin.password == password)
                    {
                        return new bool[2] { true, true };
                    }
                    
                    return new bool[2] { true, false };
                }
            }
            
            return new bool[2] { false, true };
        }

        public List<User> SearchMember(Data data)
        {
            List<User> searchResult = new List<User>();
            
            return searchResult;
        }

        public bool DeleteMember(Data data, int userNumber)
        {
            foreach(User user in data.users)
            {
                if (user.userNumber == userNumber)
                {
                    if (user.borrowedBooks.Count == 0)
                    {
                        data.users.Remove(user);
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
}