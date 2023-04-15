using System;
using Library.Constants;
using Library.Model;
using Library.Utility;

namespace Library.Controller.UserController
{
    class UserLoginOrRegister: ControllerInterface
    {
        private int currentSelectionIndex;
        
        public UserLoginOrRegister(TotalData data, CombinedManager combinedManager): base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
        }
        
        private void MoveCursorInMenu(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.UP:
                    this.currentSelectionIndex =
                        (this.currentSelectionIndex + MenuCount.USER_LOGIN_REGISTER_MENU - 1) % MenuCount.USER_LOGIN_REGISTER_MENU;
                    break;
                case MoveDirection.DOWN:
                    this.currentSelectionIndex =
                        (this.currentSelectionIndex + 1) % MenuCount.USER_LOGIN_REGISTER_MENU;
                    break;
            }
        }
        
        private void EnterNextMenu()
        {
            switch (currentSelectionIndex)
            {
                case 0:
                    
                    break;
                case 1:
                    
                    break;
            }
        }
        
        private void ChooseMenu()
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey(true);
                
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveCursorInMenu(MoveDirection.UP);
                        break;
                    
                    case ConsoleKey.DownArrow:
                        MoveCursorInMenu(MoveDirection.DOWN);
                        break;
                    
                    case ConsoleKey.Enter:
                        EnterNextMenu();
                        break;
                    
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}