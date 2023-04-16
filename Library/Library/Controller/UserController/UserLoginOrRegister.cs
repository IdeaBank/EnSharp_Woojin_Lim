using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;

namespace Library.Controller.UserController
{
    public class UserLoginOrRegister: ControllerInterface
    {
        private int currentSelectionIndex;
        
        public UserLoginOrRegister(TotalData data, CombinedManager combinedManager): base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
        }

        public void SelectLoginOrRegister()
        {
            KeyValuePair<FailCode, int> result = new KeyValuePair<FailCode, int>(FailCode.SUCCESS, -1);

            while (result.Key != FailCode.ESC_PRESSED)
            {
                UserLoginOrRegisterView.PrintLoginOrRegisterContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.USER_LOGIN_OR_REGISTER, MenuType.USER_LOGIN_OR_REGISTER);

                if (result.Key == FailCode.ESC_PRESSED)
                {
                    return;
                }

                this.currentSelectionIndex = result.Value;

                EnterNextMenu();
                Console.Clear();
            }
        }

        private void EnterNextMenu()
        {
            switch (this.currentSelectionIndex)
            {
                case 0:
                    UserLogin userLogin = new UserLogin(data, combinedManager);
                    userLogin.TryLogin();
                    break;
                case 1:
                    Console.WriteLine("2");
                    break;
            }
        }
    }
}