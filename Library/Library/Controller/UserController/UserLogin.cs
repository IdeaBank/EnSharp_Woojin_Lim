using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;

namespace Library.Controller.UserController
{
    public class UserLogin: ControllerInterface
    {
        private int currentUserIndex;
        
        public UserLogin(TotalData totalData, CombinedManager combinedManager): base(totalData, combinedManager)
        {
            this.currentUserIndex = -1;
        }

        public void TryLogin()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            bool[] isLoggedIn = new bool[2] { false, false };
            string[] loginHint = new string[2] { "", "" };
            
            while (!isLoggedIn[0] || !isLoggedIn[1])
            {
                UserLoginOrRegisterView.PrintLogin(loginHint[0], loginHint[1]);

                KeyValuePair<FailCode, string> inputId = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                    windowHeightHalf, InputMax.MAX_ID_PASSWORD_LENGTH, false, "");

                if (inputId.Key == FailCode.ESC_PRESSED)
                {
                    return;
                }

                KeyValuePair<FailCode, string> inputPassword = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                    windowHeightHalf + 1, InputMax.MAX_ID_PASSWORD_LENGTH, false, "");
                
                if (inputPassword.Key == FailCode.ESC_PRESSED)
                {
                    return;
                }

                KeyValuePair<FailCode, int> loginResult = combinedManager.UserManager.LoginAsUser(inputId.Value, inputPassword.Value);

                if (loginResult.Key == FailCode.SUCCESS)
                {
                    isLoggedIn[0] = isLoggedIn[1] = true;
                }
                
                if (loginResult.Key == FailCode.NO_ID)
                {
                    loginHint[0] = "No ID";
                }

                if (loginResult.Key == FailCode.WRONG_PASSWORD)
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
                
                this.currentUserIndex = loginResult.Value;
            }
        }
    }
}