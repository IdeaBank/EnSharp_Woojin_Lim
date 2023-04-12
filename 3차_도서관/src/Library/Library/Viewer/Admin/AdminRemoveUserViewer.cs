using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;

namespace Library
{
    public class AdminRemoveUserViewer: Viewer.Viewer
    {
        public AdminRemoveUserViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }
        
        public void RemoveUser()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter User ID to remove:".PadLeft(30, ' '));
                Console.Write(new string(' ', 50));
                KeyValuePair<bool, string> userID = inputFromUser.ReadInputFromUser(31, 0, 10, false, true);

                if (!userID.Key)
                {
                    return;
                }

                if (IsInputValid(userID.Value, @"[0-9]+"))
                {
                    bool success = dataManager.userManager.DeleteMember(data, Int32.Parse(userID.Value));

                    if (success)
                    {
                        Console.Clear();
                        return;
                    }
                    
                    FramePrinter.PrintOnPosition(31, 0, "유저 ID 혹은 해당 유저가 빌린 책이 있는지를 확인해주세요", AlignType.LEFT, ConsoleColor.Red);
                    Console.ReadKey(true);
                    Console.SetCursorPosition(31, 0);
                }
                
                Console.Clear();
            }
        }
    }
}