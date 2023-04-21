using LTT.Constant;
using LTT.Utility;
using System;
using System.Reflection;

namespace LTT.View
{
    public class MainMenuView
    {
        private ConsoleWriter consoleWriter;
        
        public MainMenuView(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public void MakeView()
        {
            Console.Clear();

            consoleWriter.PrintLogo(5);
        }

        public void UpdateView(int selectionIndex)
        {
            string[] mainMenuList =
            {
                "강의 시간표 조회",
                "관심 과목 담기",
                "수강 신청",
                "수강 내역 조회"
            };

            for (int i = 0; i < MenuCount.MAIN_MENU; ++i)
            {
                if (i == selectionIndex)
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, mainMenuList[i], Align.CENTER, ConsoleColor.Green);
                }

                else
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, mainMenuList[i], Align.CENTER, ConsoleColor.White);
                }
            }
        }
    }
}