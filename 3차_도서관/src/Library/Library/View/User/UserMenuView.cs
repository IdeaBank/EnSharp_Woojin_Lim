using System;

namespace Library.View.User
{
    public class UserMenuView: ViewFrame
    {
        public static void Print(int index)
        {
            Console.Clear();

            string[] userMenuList =
            {
                "도서찾기",
                "도서대여",
                "도서대여확인",
                "도서반납",
                "회원정보수정",
                "회원탈퇴"
            };

            for (int i = 0; i < userMenuList.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(userMenuList[i]);
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine(userMenuList[i]);
                }
            }
        }
    }
}