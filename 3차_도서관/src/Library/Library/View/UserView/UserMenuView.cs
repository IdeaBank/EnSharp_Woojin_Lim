using Library.Constants;
using Library.Utility;
using System;

namespace Library.View.UserView
{
    public class UserMenuView
    {
        private static UserMenuView _instance;

        private UserMenuView()
        {

        }

        public static UserMenuView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserMenuView();
                }

                return _instance;
            }
        }

        public void PrintUserMenuContour()
        {
            ConsoleWriter.getInstance.DrawContour(40, 15);
        }

        private void PrintChooseBookContour()
        {
            ConsoleWriter.getInstance.DrawContour(40, 4);
        }

        public void PrintUserMenu(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "도서 찾기", "도서 대여", "도서 대여 확인", "도서 반납", "도서 반납 내역", "회원 정보 수정", "회원 탈퇴" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < MenuCount.USER; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf - 4, consoleWindowHeightHalf - 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf - 4, consoleWindowHeightHalf - 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.White);
                }
            }
        }

        public void PrintBorrowOrReturnBook()
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of book: ", AlignType.LEFT);
        }

        public void PrintBorrowOrReturnBookResult(string str)
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, str, AlignType.LEFT);

        }
    }
}