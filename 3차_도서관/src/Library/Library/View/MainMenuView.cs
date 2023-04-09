using System;

namespace Library.View
{
    public class MainMenuView: ViewFrame
    {
        public static void Print(int index)
        {
            string[] mainMenuList =
            {
                "도서찾기",
                "도서추가",
                "도서삭제",
                "도서수정",
                "회원관리",
                "대여상황"
            };

            for (int i = 0; i < mainMenuList.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(mainMenuList[i]);
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine(mainMenuList[i]);
                }
            }
        }
    }
}