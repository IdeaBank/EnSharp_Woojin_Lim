using Library.Constants;
using Library.Utility;
using System;

namespace Library.View
{
    public class UserOrAdminView
    {
        private UserOrAdminView _instance;

        private UserOrAdminView()
        {

        }

        public UserOrAdminView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserOrAdminView();
                }

                return _instance;
            }
        }

        public static void PrintUserOrAdminContour()
        {
            ConsoleWriter.DrawContour(30, 8);
        }

        public static void PrintUserOrAdmin(int currentSelectionIndex)
        {
            Console.Clear();
            PrintUserOrAdminContour();

            string[] userOrAdminInstruction = new[] { "User", "Administrator" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < MenuCount.MAIN; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        userOrAdminInstruction[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        userOrAdminInstruction[i], AlignType.CENTER);
                }
            }
        }
    }
}