using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.AdminView;

namespace Library.Controller.AdminController
{
    public class AdminMenu: ControllerInterface
    {
        private int currentSelectionIndex;

        public AdminMenu(TotalData data, CombinedManager combinedManager): base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
        }
        
        public void SelectAdminMenu()
        {
            Console.Clear();
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED)
            {
                AdminMenuView.PrintAdminMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.ADMIN, MenuType.ADMIN);

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
            }
        }
    }
}