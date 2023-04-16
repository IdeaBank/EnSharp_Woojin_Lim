using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;

namespace Library.Controller.UserController
{
    public class UserRegister: ControllerInterface
    {
        public UserRegister(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {
        }

        public void Register()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            string[] warning = { "", "", "", "", "", "" };
            bool allRegexPassed = false;

            while (!allRegexPassed)
            {
                UserLoginOrRegisterView.PrintRegister(warning);
                
                KeyValuePair<FailCode, string> idInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, windowHeightHalf / 2, 15, false, false);
                bool idInputValid = false;

                while (!idInputValid)
                {
                    if (idInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(@"^[0-9a-zA-Z]{8,15}", idInputResult.Value))
                    {
                        warning[0] = "8~15글자 영어, 숫자포함";
                        continue;
                    }

                    idInputValid = true;
                }

                KeyValuePair<FailCode, string> passwordInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, windowHeightHalf / 2, 15, true, false);
                bool passwordInputValid = false;

                while (!passwordInputValid)
                {
                    if (passwordInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(@"^[0-9a-zA-Z]{8,15}", idInputResult.Value))
                    {
                        warning[0] = "8~15글자 영어, 숫자포함";
                        continue;
                    }

                    idInputValid = true;
                }
                string password;
                string name;
                string birthdate;
                string phoneNumber;
                string address;
            }
        }
    }
}