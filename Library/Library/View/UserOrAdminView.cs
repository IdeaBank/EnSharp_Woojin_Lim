using System;
using Library.Constants;
using Library.Utility;

namespace Library.View
{
    public class UserOrAdminView
    {
        private UserOrAdminView _instance;

        private UserOrAdminView()
        {
            
        }

        public UserOrAdminView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserOrAdminView();
                }

                return _instance;
            }
        }

        public static void PrintUserOrAdminContour()
        {
            ConsoleWriter.DrawContour(30, 8);
        }
        
        public static void PrintUserOrAdmin(int currentSelectionIndex)
        {
            string[] userOrAdminInstruction = new[] { "User", "Administrator" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;
            
            for (int i = 0; i < MenuCount.USER_LOGIN_OR_REGISTER; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        userOrAdminInstruction[i], AlignType.CENTER);
                    
                    Console.ResetColor();
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        userOrAdminInstruction[i], AlignType.CENTER);
                }
            }
        }
    }
}