using System;
using Library.Constants;
using Library.Utility;

namespace Library.View
{
    public class MainMenuView
    {
        public static void PrintMenu(int index)
        {
            FramePrinter.PrintFrame(ViewMaxIndex.ENTRY_MENU_MAX_INDEX);
            FramePrinter.PrintLibrary(ViewMaxIndex.ENTRY_MENU_MAX_INDEX);
            
            string[] mainMenuList =
            {
                "User",
                "Administrator"
            };

            for (int i = 0; i < mainMenuList.Length; ++i)
            {
                if (i == index)
                {
                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 1, mainMenuList[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 1, mainMenuList[i], AlignType.CENTER, ConsoleColor.White);
                }
            }
        }
        
        public static void PrintLogin()
        {
            Console.Clear();

            FramePrinter.PrintFrame(ViewMaxIndex.ENTRY_MENU_MAX_INDEX);
            FramePrinter.PrintLibrary(ViewMaxIndex.ENTRY_MENU_MAX_INDEX);
            FramePrinter.PrintOnPosition(Console.WindowWidth / 2 , Console.WindowHeight / 2 + 1, "ID: ".PadLeft(10, ' '), AlignType.RIGHT, ConsoleColor.White);
            FramePrinter.PrintOnPosition(Console.WindowWidth / 2 , Console.WindowHeight / 2 + 2, "PASSWORD: ".PadLeft(10, ' '), AlignType.RIGHT, ConsoleColor.White);
        }
    }
}