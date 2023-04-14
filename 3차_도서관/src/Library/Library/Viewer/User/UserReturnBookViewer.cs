using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Model;
using Library.Utility;

namespace Library.Viewer.User
{
    public class UserReturnBookViewer: UserViewer
    {
        public UserReturnBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser,
            int currentUserNumber) : base(data, dataManager, inputFromUser, currentUserNumber)
        {
            
        }

        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }
        
        public void PrintUserReturnBook()
        {
            UserBorrowedBookPrinter userBorrowedBookPrinter = new UserBorrowedBookPrinter(data, dataManager, inputFromUser, currentUserNumber);
            userBorrowedBookPrinter.PrintUserBorrowedList();
            
            KeyValuePair<bool, string> returnBookId;
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Book ID to return:".PadLeft(25, ' '));
                returnBookId = inputFromUser.ReadInputFromUser(26, 1, 3, false, false);

                if (!returnBookId.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(returnBookId.Value, @"^[0-9]+"))
                {
                    if (dataManager.bookManager.ReturnBook(data, currentUserNumber, Int32.Parse(returnBookId.Value)))
                    {
                        Console.WriteLine("Return success!");
                    }

                    else
                    {
                        Console.WriteLine("Return Failed!");
                    }
                    
                    Console.ReadKey(true);
                    break;
                }
            }
            
        }
    }
}