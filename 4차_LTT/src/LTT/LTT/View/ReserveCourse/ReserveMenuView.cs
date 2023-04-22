using LTT.Constant;
using LTT.Utility;
using System;

namespace LTT.View.ReserveCourse
{
    public class ReserveMenuView
    {
        private ConsoleWriter consoleWriter;

        public ReserveMenuView(ConsoleWriter consoleWriter)
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
            string[] reserveMenuList =
            {
                "관심 과목 검색",
                "관심 과목 내역",
                "관심 과목 시간표",
                "관심 과목 삭제"
            };

            for (int i = 0; i < MenuCount.RESERVE_MENU; ++i)
            {
                if (i == selectionIndex)
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, reserveMenuList[i], Align.CENTER, ConsoleColor.Green);
                }

                else
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, reserveMenuList[i], Align.CENTER, ConsoleColor.White);
                }
            }
        }
    }
}