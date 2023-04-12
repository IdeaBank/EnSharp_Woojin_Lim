using System;

namespace Library.View.Admin
{
    public class AdminMenuView 
    {
        public static void Print(int index)
        {
            Console.Clear();

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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(adminMenuList[i]);
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine(adminMenuList[i]);
                }
            }
        }
    }
}