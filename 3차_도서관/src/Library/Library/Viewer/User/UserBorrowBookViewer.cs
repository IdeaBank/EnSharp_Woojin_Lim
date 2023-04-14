using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Model;
using Library.Utility;

namespace Library.Viewer.User
{
    public class UserBorrowBookViewer : UserViewer
    {
        public UserBorrowBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser,
            int currentUserNumber) : base(data, dataManager, inputFromUser, currentUserNumber)
        {
        }
        
        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }
        
        public void ShowBorrowBookView()
        {
            SearchBookViewer searchBookViewer = new SearchBookViewer(data, dataManager, inputFromUser);
            searchBookViewer.SearchBook();

            KeyValuePair<bool, string> bookID;

            Console.WriteLine("Book ID to borrow:".PadLeft(15, ' '));
            bookID = inputFromUser.ReadInputFromUser(16, 0, 3, false, false);

            if (!bookID.Key)
            {
                Console.Clear();
                return;
            }

            if (IsInputValid(bookID.Value, @"^[0-9]+"))
            {
                dataManager.bookManager.BorrowBook(data, currentUserNumber, Int32.Parse(bookID.Value));
            }
            
            Console.Clear();
        }
    }
}