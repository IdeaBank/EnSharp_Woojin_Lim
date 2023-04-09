using System;

namespace Library.View.User
{
    public class UserStartView
    {
        public static void Print(int index)
        {
            Console.Clear();

            string[] userStartList =
            {
                "로그인",
                "회원가입"
            };

            for (int i = 0; i < userStartList.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(userStartList[i]);
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine(userStartList[i]);
                }
            }
        }
    }
}