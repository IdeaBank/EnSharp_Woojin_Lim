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

        public void UpdateView(int currentSelectionIndex)
        {
            
        }
    }
}