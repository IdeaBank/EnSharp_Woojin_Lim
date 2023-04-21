using System;
using LTT.Constant;
using LTT.Utility;

namespace LTT.View
{
    public class StudentLoginView
    {
        private ConsoleWriter consoleWriter;

        public StudentLoginView(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }
        
        public void PrintLogin(string idHint, string passwordHint)
        {
            Console.Clear();
            consoleWriter.PrintLogo(10);
            
            int targetLeft = Console.WindowWidth / 4 * 3;
            int targetTop = Console.WindowHeight / 4 * 3;
            
            consoleWriter.PrintOnPosition(targetLeft, targetTop, "ID:" + new string(' ', 16), Align.LEFT);
            consoleWriter.PrintOnPosition(targetLeft, targetTop + 1, "PW:" + new string(' ', 20), Align.LEFT);

            consoleWriter.PrintOnPosition(targetLeft + 5, targetTop, idHint, Align.LEFT, ConsoleColor.Red);
            consoleWriter.PrintOnPosition(targetLeft + 5, targetTop + 1, passwordHint, Align.LEFT, ConsoleColor.Red);
        }

    }
}