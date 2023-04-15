using System;
using Library.Constants;
using Library.Utility;

namespace Library.View.UserView
{
    public class UserLoginOrRegisterView
    {
        private UserLoginOrRegisterView _instance;

        private UserLoginOrRegisterView()
        {
            
        }

        public UserLoginOrRegisterView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserLoginOrRegisterView();
                }

                return _instance;
            }
        }

        public static void PrintLoginOrRegisterContour()
        {
            ConsoleWriter.DrawContour(30, 8);
        }
        
        public static void PrintLoginOrRegister(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "Login", "Register" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;
            
            for (int i = 0; i < MenuCount.USER_LOGIN_OR_REGISTER; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER);
                    
                    Console.ResetColor();
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER);
                }
            }
        }
    }
}