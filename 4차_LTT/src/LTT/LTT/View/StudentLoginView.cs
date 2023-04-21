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
            consoleWriter.PrintLogo(5);
            
            int targetLeft = Console.WindowWidth / 4 * 3;
            int targetTop = Console.WindowHeight / 4 * 3;
            
            consoleWriter.PrintOnPosition(targetLeft, targetTop, "ID:" + new string(' ', 16), Align.LEFT);
            consoleWriter.PrintOnPosition(targetLeft, targetTop + 1, "PW:" + new string(' ', 20), Align.LEFT);

            consoleWriter.PrintOnPosition(targetLeft + 5, targetTop, idHint, Align.LEFT, ConsoleColor.Red);
            consoleWriter.PrintOnPosition(targetLeft + 5, targetTop + 1, passwordHint, Align.LEFT, ConsoleColor.Red);
        }

        public void PrintQuitConfirm()
        {
            Console.Clear();
            consoleWriter.PrintLogo(5);


            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;


            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf + 3, "정말 종료하시겠습니까?");
            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf + 4, "확인: Y, 취소: N");
        }
    }
}