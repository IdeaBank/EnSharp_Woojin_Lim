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
                "���� ���� �˻�",
                "���� ���� ����",
                "���� ���� �ð�ǥ",
                "���� ���� ����"
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