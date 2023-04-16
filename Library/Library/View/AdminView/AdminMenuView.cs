using System;
using Library.Constants;
using Library.Utility;

namespace Library.View.AdminView
{
    public class AdminMenuView
    {
        private AdminMenuView _instance;

        private AdminMenuView()
        {
            
        }

        public AdminMenuView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new AdminMenuView();
                }

                return _instance;
            }
        }

        public static void PrintAdminMenuContour()
        {
            ConsoleWriter.DrawContour(40, 13);
        }

        
        public static void PrintAdminMenu(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "회원 관리", "대여 상황" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;
            
            for (int i = 0; i < MenuCount.ADMIN; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER, ConsoleColor.White);
                }
            }
        }
    }
}