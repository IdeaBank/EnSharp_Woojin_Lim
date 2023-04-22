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

        private void ShowView(MenuType currentMenu, int currentSelectionIndex, int[] columnIndexList2)
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
                case MenuType.SEARCH_TIME_TABLE:
                    viewList.LectureTimeSearchView.UpdateView(currentSelectionIndex, columnIndexList2);
                    break;
            }
        }
        
        private KeyValuePair<ResultCode, int> ChangeSelection(int currentSelectionIndex, int MAX_SELECTION, MenuType currentMenu, int[] columnIndexList)
        {
            ShowView(currentMenu, currentSelectionIndex, columnIndexList);
            
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

        public KeyValuePair<ResultCode, int> ChooseMenu(int currentSelectionIndex, int MAX_SELECTION, MenuType currentMenu, int[] columnIndexList = null)
        {
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED && result.Key != ResultCode.ENTER_PRESSED)
            {
                result = ChangeSelection(currentSelectionIndex, MAX_SELECTION, currentMenu, columnIndexList);

                currentSelectionIndex = result.Value;

                if (result.Key == ResultCode.ENTER_PRESSED)
                {
                    return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, currentSelectionIndex);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.ESC_PRESSED, -1);
        }

        private int MoveCursorInColumn(MoveDirection direction, int currentSelectionIndex, int MAX_SELECTION)
        {
            switch (direction)
            {
                case MoveDirection.LEFT:
                    currentSelectionIndex =
                        (currentSelectionIndex + MAX_SELECTION - 1) % MAX_SELECTION;
                    break;
                case MoveDirection.RIGHT:
                    currentSelectionIndex =
                        (currentSelectionIndex + 1) % MAX_SELECTION;
                    break;
            }

            return currentSelectionIndex;
        }

        private KeyValuePair<ResultCode, int> ChangeColumnSelection(int rowSelectionIndex, int MAX_SELECTION, MenuType currentMenu, int[] columnIndexList)
        {
            ShowView(currentMenu, rowSelectionIndex, columnIndexList);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    columnIndexList[rowSelectionIndex] = MoveCursorInColumn(MoveDirection.LEFT, columnIndexList[rowSelectionIndex], MAX_SELECTION);
                    break;

                case ConsoleKey.RightArrow:
                    columnIndexList[rowSelectionIndex] = MoveCursorInColumn(MoveDirection.RIGHT, columnIndexList[rowSelectionIndex], MAX_SELECTION);
                    break;

                case ConsoleKey.Enter:
                    return new KeyValuePair<ResultCode, int>(ResultCode.ENTER_PRESSED, columnIndexList[rowSelectionIndex]);

                case ConsoleKey.Escape:
                    return new KeyValuePair<ResultCode, int>(ResultCode.ESC_PRESSED, -1);
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, columnIndexList[rowSelectionIndex]);
        }

        public KeyValuePair<ResultCode, int> ChooseColumn(int rowSelectionIndex, int MAX_SELECTION, MenuType currentMenu, int[] columnIndexList)
        {
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED && result.Key != ResultCode.ENTER_PRESSED)
            {
                result = ChangeColumnSelection(rowSelectionIndex, MAX_SELECTION, currentMenu, columnIndexList);

                columnIndexList[rowSelectionIndex] = result.Value;

                if (result.Key == ResultCode.ENTER_PRESSED)
                {
                    return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, columnIndexList[rowSelectionIndex]);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.ESC_PRESSED, -1);
        }
    }
}