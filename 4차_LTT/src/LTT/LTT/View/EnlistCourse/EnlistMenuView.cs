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

        public void UpdateView(int currentSelectionIndex)
        {
            
        }
    }
}