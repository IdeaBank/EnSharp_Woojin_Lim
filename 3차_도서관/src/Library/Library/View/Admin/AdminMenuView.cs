using System;
using Library.Constants;
using Library.Utility;

namespace Library.View.Admin
{
    public class AdminMenuView
    {
        public static void PrintMenu(int index)
        {
            FramePrinter.PrintFrame(ViewMaxIndex.ADMIN_MENU_MAX_INDEX);
            FramePrinter.PrintLibrary(ViewMaxIndex.ADMIN_MENU_MAX_INDEX);

            string[] adminMenuList =
            {
                "도서찾기",
                "도서추가",
                "도서삭제",
                "도서수정",
                "회원관리",
                "대여상황"
            };

            for (int i = 0; i < adminMenuList.Length; ++i)
            {
                if (i == index)
                {
                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i - 1, adminMenuList[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i - 1, adminMenuList[i], AlignType.CENTER, ConsoleColor.White);
                }
            }
        }
    }
}