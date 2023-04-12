using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View;

namespace Library
{
    public class AdminLoginViewer: Viewer
    {
        public AdminLoginViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public void TryLogin()
        {
            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                MainMenuView.PrintLogin();
                KeyValuePair<bool, string> inputId = inputFromUser.ReadInputFromUser(Console.WindowWidth / 2, Console.WindowHeight / 2 + 1, InputMax.MAX_ID_PASSWORD_LENGTH, false, false);
                
                bool isInputValid = true;
                isInputValid = inputId.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> inputPassword = inputFromUser.ReadInputFromUser(Console.WindowWidth / 2,Console.WindowHeight / 2 + 2, InputMax.MAX_ID_PASSWORD_LENGTH, true, false);
                isInputValid = inputPassword.Key;

                if (!isInputValid)
                {
                    return;
                }

                isLoggedIn = dataManager.userManager.LoginAsAdministrator(data, inputId.Value, inputPassword.Value);
            }

            AdminAllMenuViewer adminAllMenuViewer = new AdminAllMenuViewer(data, dataManager, inputFromUser);
            adminAllMenuViewer.ShowAdminMenu();
        }
    }
}