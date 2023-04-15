using System;
using System.Collections.Generic;
using Library.Constants;

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
        
        public static KeyValuePair<FailCode, int> SelectMenu(int MAX_SELECTION)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int currentSelectionIndex = 0;


            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    currentSelectionIndex = MoveCursorInMenu(MoveDirection.UP, currentSelectionIndex, MAX_SELECTION);
                    break;

                case ConsoleKey.DownArrow:
                    currentSelectionIndex = MoveCursorInMenu(MoveDirection.DOWN, currentSelectionIndex, MAX_SELECTION);
                    break;

                case ConsoleKey.Enter:
                    return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, currentSelectionIndex);

                case ConsoleKey.Escape:
                    return new KeyValuePair<FailCode, int>(FailCode.ESC_PRESSED, -1);
            }

            return new KeyValuePair<FailCode, int>(FailCode.FATAL_ERROR, -1);
        }
    }
}