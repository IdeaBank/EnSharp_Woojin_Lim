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
        
        public void ModifyMemberInfo(Data data, int userNumber, string id, string name, string address)
        {
            int userIndex = GetUserIndex(data, userNumber);

            if (userIndex != -1)
            {
                if (id != "")
                {
                    data.users[userIndex].id = id;
                }

                if (name != "")
                {
                    data.users[userIndex].name = name;
                }

                if (address != "")
                {
                    data.users[userIndex].address = address;
                }
            }
        }
        
        public bool BorrowBookWithUserID(Data data, BookManager bookManager, int bookId, int userNumber)
        {
            KeyValuePair<bool, bool> borrowResult = bookManager.BorrowBook(data, bookId);

            if (borrowResult.Value)
            {
                int userIndex = GetUserIndex(data, userNumber);
                data.users[userIndex].borrowedBooks.Add(new BorrowedBook(bookId, "ASDF"));
                return true;
            }

            return false;
        }

        public bool ReturnBook(Data data, BookManager bookManager, int bookId, int userNumber)
        {
            int userIndex = GetUserIndex(data, userNumber);

            if (data.users[userIndex].borrowedBooks.Count == 0)
            {
                return false;
            }

            foreach (BorrowedBook book in data.users[userIndex].borrowedBooks)
            {
                if (book.bookId == bookId)
                {
                    bool isReturnSuccess = bookManager.ReturnBook(data, bookId);

                    if (isReturnSuccess)
                    {
                        return true;
                    }
                }
            }

            return false;
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

        public KeyValuePair<bool, User> LoginAsUser(Data data, string id, string password)
        {
            foreach(User user in data.users)
            {
                if (user.id == id)
                {
                    if (user.password == password)
                    {
                        return new KeyValuePair<bool, User>(true, user);
                    }
                    
                    return new KeyValuePair<bool, User>(false, null);
                }
            }
            
            return new KeyValuePair<bool, User>(false, null);
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