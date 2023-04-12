using System;
using System.Collections.Generic;
using Library.Exception;
using Library.Model;

namespace Library
{
    public class AdminLoginViewer: Viewer
    {
        private string id;
        private string password;

        public AdminLoginViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public void TryLogin()
        {
            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                KeyValuePair<bool, string> inputId = inputFromUser.ReadInputFromUser(0, 0, 10, false);
                bool isInputValid = true;
                isInputValid = inputId.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> inputPassword = inputFromUser.ReadInputFromUser(0,1, 10, true);
                isInputValid = inputPassword.Key;

                if (!isInputValid)
                {
                    return;
                }

                isLoggedIn = dataManager.userManager.LoginAsAdministrator(data, inputId.Value, inputPassword.Value);
            }

            AdminMenuViewer adminMenuViewer = new AdminMenuViewer(data, dataManager, inputFromUser);
            adminMenuViewer.ShowAdminMenu();
        }
    }
}