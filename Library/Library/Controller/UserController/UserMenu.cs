using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;

namespace Library.Controller.UserController
{
    public class UserMenu : ControllerInterface
    {
        private int currentSelectionIndex;

        public UserMenu(TotalData data, CombinedManager combinedManager, int currentSelectionIndex) : base(data, combinedManager)
        {
            this.currentSelectionIndex = currentSelectionIndex;
        }

        public void SelectUserMenu()
        {
            Console.Clear();
            KeyValuePair<FailCode, int> result = new KeyValuePair<FailCode, int>(FailCode.SUCCESS, -1);

            while (result.Key != FailCode.ESC_PRESSED)
            {
                UserMenuView.PrintUserMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.USER, MenuType.USER);

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

                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
            }
        }
    }
}