using System;
using System.Collections.Generic;
using Library.Constants;
using Library.View;

namespace Library.Utility
{
    public class MenuSelector
    {
        private static MenuSelector _instance;

        private MenuSelector()
        {
            
        }

        public static MenuSelector getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MenuSelector();
                }

                return _instance;
            }
        }

        private static int MoveCursorInMenu(MoveDirection direction, int currentSelectionIndex, int MAX_SELECTION)
        {
            switch (direction)
            {
                case MoveDirection.UP:
                    currentSelectionIndex =
                        (currentSelectionIndex + MAX_SELECTION - 1) % MAX_SELECTION;
                    break;
                case MoveDirection.DOWN:
                    currentSelectionIndex =
                        (currentSelectionIndex + 1) % MAX_SELECTION;
                    break;
            }

            return currentSelectionIndex;
        }

        private static void ShowView(MenuType currentMenu, int currentSelectionIndex)
        {
            switch (currentMenu)
            {
                case MenuType.USER_OR_ADMIN:
                    UserOrAdminView.PrintUserOrAdmin(currentSelectionIndex);
                    break;
                case MenuType.USER_LOGIN_OR_REGISTER:
                    
                    break;
                case MenuType.USER:
                    
                    break;
                case MenuType.ADMIN:
                    
                    break;
            }
        }
        
        private static KeyValuePair<FailCode, int> ChangeSelection(int currentSelectionIndex, int MAX_SELECTION, MenuType currentMenu)
        {
            ShowView(currentMenu, currentSelectionIndex);
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    currentSelectionIndex = MoveCursorInMenu(MoveDirection.UP, currentSelectionIndex, MAX_SELECTION);
                    break;

                case ConsoleKey.DownArrow:
                    currentSelectionIndex = MoveCursorInMenu(MoveDirection.DOWN, currentSelectionIndex, MAX_SELECTION);
                    break;

                case ConsoleKey.Enter:
                    return new KeyValuePair<FailCode, int>(FailCode.ENTER_PRESSED, currentSelectionIndex);

                case ConsoleKey.Escape:
                    return new KeyValuePair<FailCode, int>(FailCode.ESC_PRESSED, -1);
            }

            return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, currentSelectionIndex);
        }

        public static KeyValuePair<FailCode, int> ChooseMenu(int currentSelectionIndex, int MAX_SELECTION, MenuType currentMenu)
        {
            KeyValuePair<FailCode, int> result = new KeyValuePair<FailCode, int>(FailCode.SUCCESS, -1);
            
            while (result.Key != FailCode.ESC_PRESSED && result.Key != FailCode.ENTER_PRESSED)
            {
                result = MenuSelector.ChangeSelection(currentSelectionIndex, MAX_SELECTION, currentMenu);

                currentSelectionIndex = result.Value;
                
                if (result.Key == FailCode.ENTER_PRESSED)
                {
                    return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, currentSelectionIndex);
                }
            }

            return new KeyValuePair<FailCode, int>(FailCode.ESC_PRESSED, -1);
        }
    }
}