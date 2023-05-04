using System;
using Library.Constant;
using Library.Utility;

namespace Library.View
{
    public class UserOrAdminView
    {
        private static UserOrAdminView _instance;

        private UserOrAdminView()
        {

        }

        public static UserOrAdminView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserOrAdminView();
                }

                return _instance;
            }
        }

        public void PrintUserOrAdminContour()
        {
            ConsoleWriter.getInstance.DrawContour(30, 8);
        }

        public void PrintUserOrAdmin(int currentSelectionIndex)
        {
            PrintUserOrAdminContour();

            string[] userOrAdminInstruction = new[] { "User", "Administrator" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < Constant.Menu.Count.MAIN; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        userOrAdminInstruction[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        userOrAdminInstruction[i], AlignType.CENTER);
                }
            }
        }
    }
}