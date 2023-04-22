using LTT.Constant;
using LTT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.View.EnlistCourse
{
    public class ReservedOrAllCourseView
    {
        private ConsoleWriter consoleWriter;

        public ReservedOrAllCourseView(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public void MakeView()
        {
            Console.Clear();

            consoleWriter.PrintLogo(3);
        }

        public void UpdateView(int selectionIndex)
        {
            string[] selectionView =
            {
                "관심 과목에서 선택",
                "직접 선택"
            };

            for (int i = 0; i < MenuCount.RESERVED_OR_ALL_MENU; ++i)
            {
                if (i == selectionIndex)
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, selectionView[i], Align.CENTER, ConsoleColor.Green);
                }

                else
                {
                    consoleWriter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + i + 3, selectionView[i], Align.CENTER, ConsoleColor.White);
                }
            }
        }
    }
}
