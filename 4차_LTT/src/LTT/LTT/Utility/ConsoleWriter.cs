using System;

namespace LTT.Utility
{
    public class ConsoleWriter
    {
        public void WriteOnPosition(int cursorX, int cursorY, string str)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write(str);
        }
    }
}