using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;

namespace Library
{
    public class AdminRemoveBookViewer: Viewer.ViewerClass
    {
        public AdminRemoveBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }
        
        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }
        
        public void RemoveBookWithInput()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter Book ID to remove:".PadLeft(30, ' '));
                Console.Write(new string(' ', 50));
                KeyValuePair<bool, string> bookID = inputFromUser.ReadInputFromUser(31, 0, 10, false, true);

                if (!bookID.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookID.Value, @"[0-9]+"))
                {
                    bool success = dataManager.bookManager.DeleteBook(data, Int32.Parse(bookID.Value));

                    if (success)
                    {
                        Console.Clear();
                        return;
                    }
                    
                    FramePrinter.PrintOnPosition(31, 0, "해당 번호의 책이 있는지 다시 한 번 확인해주세요", AlignType.LEFT, ConsoleColor.Red);
                    Console.ReadKey(true);
                    Console.SetCursorPosition(31, 0);
                }
                
                Console.Clear();
            }
        }
    }
}