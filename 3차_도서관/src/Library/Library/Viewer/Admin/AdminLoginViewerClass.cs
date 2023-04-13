using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View;

namespace Library
{
    public class AdminLoginViewerClass: Viewer.ViewerClass
    {
        private int inputIndex;
        
        public AdminLoginViewerClass(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
            this.inputIndex = 0;
        }

        public void TryAdminLogin()
        {
            bool[] isLoggedIn = new bool[2] { false, false };
            string[] loginHint = new string[2] { "", "" };

            while (!isLoggedIn[0] || !isLoggedIn[1])
            {
                MainMenuView.PrintLogin(loginHint[0], loginHint[1]);

                KeyValuePair<bool, string> inputId = inputFromUser.ReadInputFromUser(Console.WindowWidth / 2,
                    Console.WindowHeight / 2 + 1, InputMax.MAX_ID_PASSWORD_LENGTH, false, false);

                bool isInputValid = true;
                isInputValid = inputId.Key;

                if (!isInputValid)
                {
                    return;
                }

                KeyValuePair<bool, string> inputPassword = inputFromUser.ReadInputFromUser(Console.WindowWidth / 2,
                    Console.WindowHeight / 2 + 2, InputMax.MAX_ID_PASSWORD_LENGTH, true, false);
                isInputValid = inputPassword.Key;

                if (!isInputValid)
                {
                    return;
                }

                isLoggedIn = dataManager.userManager.LoginAsAdministrator(data, inputId.Value, inputPassword.Value);

                if (!isLoggedIn[0])
                {
                    loginHint[0] = "Wrong ID";
                }

                if (!isLoggedIn[1])
                {
                    loginHint[1] = "Wrong Password";
                }

                if (!isLoggedIn[0] || !isLoggedIn[1])
                {
                    MainMenuView.PrintLogin(loginHint[0], loginHint[1]);
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Console.CursorVisible = true;
                    loginHint[0] = loginHint[1] = "";
                }
            }

            AdminAllMenuViewerClass adminAllMenuViewerClass = new AdminAllMenuViewerClass(data, dataManager, inputFromUser);
            adminAllMenuViewerClass.ShowAdminMenu();
        }
    }
}