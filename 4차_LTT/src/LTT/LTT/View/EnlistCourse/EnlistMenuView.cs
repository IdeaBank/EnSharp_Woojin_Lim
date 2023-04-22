using LTT.Constant;
using LTT.Utility;
using System;

namespace LTT.View.EnlistCourse
{
    public class EnlistMenuView
    {
        private ConsoleWriter consoleWriter;

        public EnlistMenuView(ConsoleWriter consoleWriter)
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
            string[] enlistMenuView =
            {
                "������û",
                "������û ����",
                "������û �ð�ǥ",
                "�������� ����"
            };

            for (int i = 0; i < MenuCount.ENLIST_MENU; ++i)
            {
                if (i == selectionIndex)
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, enlistMenuView[i], Align.CENTER, ConsoleColor.Green);
                }

                else
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, enlistMenuView[i], Align.CENTER, ConsoleColor.White);
                }
            }
        }

        public void MakeEnlistAddingView(int availableCredit, int reservedCredit)
        {
            string instruction = "��� ���� ���� : " + availableCredit + "    ����� ���� : " + reservedCredit + "   ����� ���� : " + new string(' ', 30);

            Console.Write(instruction);
        }
    }
}