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
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED)
            {
                UserLoginOrRegisterView.PrintLoginOrRegisterContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.USER_LOGIN_OR_REGISTER, MenuType.USER_LOGIN_OR_REGISTER);

                if (result.Key == ResultCode.ESC_PRESSED)
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
                    UserRegister userRegister = new UserRegister(data, combinedManager);
                    userRegister.Register();
                    break;
            }
        }
    }
}