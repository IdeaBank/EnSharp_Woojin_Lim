using System;
using System.Collections.Generic;
using Library.Exception;
using Library.Model;

namespace Library
{
    public class AdminLoginViewer
    {
        private string id;
        private string password;
        
        private Data data;
        private InputFromUser inputFromUser;

        public AdminLoginViewer(Data data, InputFromUser inputFromUser)
        {
            this.data = data;
            this.inputFromUser = inputFromUser;
        }

        public void TryLogin()
        {
            bool isInputValid = true;
            bool isLoggedIn = false;
            
            while (!isLoggedIn)
            {
                KeyValuePair<bool, string> inputId = inputFromUser.ReadInputFromUser(0, 0, 10, false);
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

                UserManager userManager = new UserManager();
                isLoggedIn = userManager.LoginAsAdministrator(data, inputId.Value, inputPassword.Value);
            }

            AdminMenuViewer adminMenuViewer = new AdminMenuViewer(data, inputFromUser);
            adminMenuViewer.ShowAdminMenu();
        }
    }
}