using System;

namespace Library.View
{
    public class MainMenuView: ViewFrame
    {
        public static void Print(int index)
        {
            Console.Clear();
            
            string[] mainMenuList =
            {
                "User",
                "Administrator"
            };

            for (int i = 0; i < mainMenuList.Length; ++i)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(mainMenuList[i]);
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