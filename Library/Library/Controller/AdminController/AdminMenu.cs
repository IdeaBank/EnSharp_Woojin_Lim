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
            KeyValuePair<FailCode, int> result = new KeyValuePair<FailCode, int>(FailCode.SUCCESS, -1);

            while (result.Key != FailCode.ESC_PRESSED)
            {
                AdminMenuView.PrintAdminMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.ADMIN, MenuType.ADMIN);

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
            }
        }
    }
}