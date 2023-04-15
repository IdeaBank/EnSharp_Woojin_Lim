using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;

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
            KeyValuePair<FailCode, int> result = MenuSelector.ChooseMenu(0, MenuCount.USER_LOGIN_OR_REGISTER, MenuType.USER_LOGIN_OR_REGISTER);

            if (result.Key == FailCode.ESC_PRESSED)
            {
                return;
            }

            this.currentSelectionIndex = result.Value;
            
            EnterNextMenu();
        }

        private void EnterNextMenu()
        {
            switch (this.currentSelectionIndex)
            {
                case 0:
                    Console.WriteLine("1");
                    break;
                case 1:
                    Console.WriteLine("2");
                    break;
            }
        }
    }
}