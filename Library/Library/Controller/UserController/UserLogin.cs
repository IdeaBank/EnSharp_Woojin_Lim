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
        public UserLogin(TotalData totalData, CombinedManager combinedManager): base(totalData, combinedManager)
        {
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
                
                KeyValuePair<ResultCode, string> inputId = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                windowHeightHalf, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN, "");

                if (inputId.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                KeyValuePair<ResultCode, string> inputPassword = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                    windowHeightHalf + 1, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN, "");
                
                if (inputPassword.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                KeyValuePair<ResultCode, int> loginResult = combinedManager.UserManager.LoginAsUser(inputId.Value, inputPassword.Value);

                if (loginResult.Key == ResultCode.SUCCESS)
                {
                    isLoggedIn[0] = isLoggedIn[1] = true;
                }
                
                if (loginResult.Key == ResultCode.NO_ID)
                {
                    loginHint[0] = "No ID";
                }

                if (loginResult.Key == ResultCode.WRONG_PASSWORD)
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
                    continue;
                }

                UserMenu userMenu = new UserMenu(data, combinedManager, loginResult.Value, loginResult.Value);
                userMenu.SelectUserMenu();
            }
        }
    }
}