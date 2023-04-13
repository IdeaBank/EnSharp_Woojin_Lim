using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.View.User;

namespace Library.Viewer.User
{
    public class UserLoginViewer : ViewerClass
    {
        public UserLoginViewer(Data data, DataManager dataManager, InputFromUser inputFromUser) : base(data,
            dataManager, inputFromUser)
        {
        }
        
        public void TryUserLogin()
        {
            bool[] isLoggedIn = new bool[2] { false, false };
            string[] loginHint = new string[2] { "", "" };

            while (!isLoggedIn[0] || !isLoggedIn[1])
            {
                UserLoginOrRegisterView.PrintLogin(loginHint[0], loginHint[1]);

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

                isLoggedIn = dataManager.userManager.LoginAsUser(data, inputId.Value, inputPassword.Value);

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
                    UserLoginOrRegisterView.PrintLogin(loginHint[0], loginHint[1]);
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Console.CursorVisible = true;
                    loginHint[0] = loginHint[1] = "";
                }
            }

            UserAllMenuViewer userMenuView = new UserAllMenuViewer(data, dataManager, inputFromUser);
            userMenuView.ShowUserMenu();
        }
    }
}