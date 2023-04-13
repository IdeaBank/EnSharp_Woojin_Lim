using System;
using Library.Constants;
using Library.Utility;

namespace Library.View.User
{
    public class UserMenuView
    {
        public static void PrintMenu(int index)
        {
            FramePrinter.PrintFrame(ViewMaxIndex.USER_MENU_MAX_INDEX);
            FramePrinter.PrintLibrary(ViewMaxIndex.USER_MENU_MAX_INDEX);

            string[] userMenuList =
            {
                "도서찾기",
                "도서대여",
                "도서대여확인",
                "도서반납",
                "도서반납내역",
                "회원정보수정",
                "회원탈퇴"
            };

            for (int i = 0; i < userMenuList.Length; ++i)
            {
                if (i == index)
                {
                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i - 1, userMenuList[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i - 1, userMenuList[i], AlignType.CENTER, ConsoleColor.White);
                }
            }
        }
    }
}