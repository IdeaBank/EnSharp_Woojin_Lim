using System;
using System.Collections.Generic;

namespace Library
{
    public class UserManagement
    {
        public bool Register(Data data, User user)
        {
            foreach (User tempUser in data.users)
            {
                if (tempUser.number == user.number)
                {
                    return false;
                }
            }
            
            data.users.Add(user);
            return true;
        }

        public bool Withdraw(Data data, int userNumber)
        {
            foreach (User user in data.users)
            {
                if (user.number == userNumber)
                {
                    data.users.Remove(user);
                    return true;
                }
            }

            return false;
        }

        public bool LoginAsUser(Data data, string id, string password)
        {
            foreach(User user in data.users)
            {
                if (user.id == id)
                {
                    if (user.password == password)
                    {
                        return true;
                    }
                    
                    Console.WriteLine("비밀번호가 틀렸습니다.");
                    return false;
                }
            }
            
            Console.WriteLine("존재하지 않는 아이디입니다.");
            return false;
        }

        public bool LoginAsAdministrator(Data data, string id, string password)
        {
            foreach(Administrator admin in data.admins)
            {
                if (admin.id == id)
                {
                    if (admin.password == password)
                    {
                        return true;
                    }
                    
                    Console.WriteLine("비밀번호가 틀렸습니다.");
                    return false;
                }
            }
            
            Console.WriteLine("존재하지 않는 아이디입니다.");
            return false;
        }
        
        public bool ModifyInformation(Data data)
        {
            return false;
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
                if (user.number == userNumber)
                {
                    data.users.Remove(user);
                    return true;
                }
            }

            Console.WriteLine("해당 번호의 유저가 존재하지 않습니다!");
            return false;
        }
    }
}