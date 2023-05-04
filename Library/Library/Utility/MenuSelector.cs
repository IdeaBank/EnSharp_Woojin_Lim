using System;
using System.Collections.Generic;
using Library.Constant;
using Library.Model;

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

        private int MoveCursorInMenu(MoveDirection direction, int currentSelectionIndex, int MAX_SELECTION)
        {
            switch (direction)
            {
                case MoveDirection.UP:
                    currentSelectionIndex = (currentSelectionIndex + MAX_SELECTION - 1) % MAX_SELECTION;
                    break;
                case MoveDirection.DOWN:
                    currentSelectionIndex = (currentSelectionIndex + 1) % MAX_SELECTION;
                    break;
            }

            return currentSelectionIndex;
        }

        private void ShowView(Constant.Menu.Type currentMenu, int currentSelectionIndex)
        {
            switch (currentMenu)
            {
                case Constant.Menu.Type.USER_OR_ADMIN:
                    UserOrAdminView.PrintUserOrAdmin(currentSelectionIndex);
                    break;
                case Constant.Menu.Type.USER_LOGIN_OR_REGISTER:
                    UserLoginOrRegisterView.PrintLoginOrRegister(currentSelectionIndex);
                    break;
                case Constant.Menu.Type.USER:
                    UserMenuView.PrintUserMenu(currentSelectionIndex);
                    break;
                case Constant.Menu.Type.ADMIN:
                    AdminMenuView.PrintAdminMenu(currentSelectionIndex);
                    break;
            }
        }

        private ReturnedValue ChangeSelection(int currentSelectionIndex, int MAX_SELECTION,
            Constant.Menu.Type currentMenu)
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
                    return new ReturnedValue(ResultCode.ENTER_PRESSED, currentSelectionIndex);

                case ConsoleKey.Escape:
                    return new ReturnedValue(ResultCode.ESC_PRESSED, -1);
            }

            return new ReturnedValue(ResultCode.SUCCESS, currentSelectionIndex);
        }

        public ReturnedValue ChooseMenu(int currentSelectionIndex, int MAX_SELECTION,
            Constant.Menu.Type currentMenu)
        {
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);

            while (result.ResultCode != ResultCode.ESC_PRESSED && result.ResultCode != ResultCode.ENTER_PRESSED)
            {
                result = ChangeSelection(currentSelectionIndex, MAX_SELECTION, currentMenu);

                currentSelectionIndex = result.ReturnedInt;

                if (result.ResultCode == ResultCode.ENTER_PRESSED)
                {
                    return new ReturnedValue(ResultCode.SUCCESS, currentSelectionIndex);
                }
            }

            return new ReturnedValue(ResultCode.ESC_PRESSED, -1);
        }
    }
}