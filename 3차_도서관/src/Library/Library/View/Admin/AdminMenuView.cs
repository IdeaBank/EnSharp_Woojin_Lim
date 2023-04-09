using System;

namespace Library.View.Admin
{
    public class AdminMenuView: ViewFrame
    {
        public static void Print(int index)
        {
            string[] AdminMenuList =
            {
                "도서찾기",
                "도서추가",
                "도서삭제",
                "도서수정",
                "회원관리",
                "대여상황"
            };

            for (int i = 0; i < AdminMenuList.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(AdminMenuList[i]);
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine(AdminMenuList[i]);
                }
            }
        }
    }
}