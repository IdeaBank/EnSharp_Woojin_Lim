using System;
using System.Collections.Generic;
using LTT.Constant;
using LTT.View;

namespace LTT.Utility
{
    public class MenuSelector
    {
        private ViewList viewList;
        
        public MenuSelector(ViewList viewList)
        {
            this.viewList = viewList;
        }
        
        private int MoveCursorInMenu(MoveDirection direction, int currentSelectionIndex, int MAX_SELECTION)
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

        private void ShowView(MenuType currentMenu, int currentSelectionIndex)
        {
            switch (currentMenu)
            {
                case MenuType.MAIN_MENU:
                    viewList.MainMenuView.UpdateView(currentSelectionIndex);
                    break;
                case MenuType.RESERVE_COURSE:
                    viewList.ReserveMenuView.UpdateView(currentSelectionIndex);
                    break;
                case MenuType.ENLIST_COURSE:
                    viewList.EnlistMenuView.UpdateView(currentSelectionIndex);
                    break;
            }
        }
        
        private KeyValuePair<ResultCode, int> ChangeSelection(int currentSelectionIndex, int MAX_SELECTION, MenuType currentMenu)
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
                    return new KeyValuePair<ResultCode, int>(ResultCode.ENTER_PRESSED, currentSelectionIndex);

                case ConsoleKey.Escape:
                    return new KeyValuePair<ResultCode, int>(ResultCode.ESC_PRESSED, -1);
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, currentSelectionIndex);
        }

        public KeyValuePair<ResultCode, int> ChooseMenu(int currentSelectionIndex, int MAX_SELECTION,
            MenuType currentMenu)
        {
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED && result.Key != ResultCode.ENTER_PRESSED)
            {
                result = ChangeSelection(currentSelectionIndex, MAX_SELECTION, currentMenu);

                currentSelectionIndex = result.Value;

                if (result.Key == ResultCode.ENTER_PRESSED)
                {
                    return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, currentSelectionIndex);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.ESC_PRESSED, -1);
        }
    }
}