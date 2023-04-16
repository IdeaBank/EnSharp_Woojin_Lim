using System;
using Library.Constants;
using Library.Utility;

namespace Library.View.UserView
{
    public class UserMenuView
    {
        private UserMenuView _instance;

        private UserMenuView()
        {
            
        }

        public UserMenuView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserMenuView();
                }

                return _instance;
            }
        }
        
        public static void PrintUserMenuContour()
        {
            ConsoleWriter.DrawContour(40, 15);
        }

        
        public static void PrintUserMenu(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "도서 찾기", "도서 대여", "도서 대여 확인", "도서 반납", "도서 반납 내역", "회원 정보 수정", "회원 탈퇴" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;
            
            for (int i = 0; i < MenuCount.USER; ++i)
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