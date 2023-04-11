using System;

namespace Library.View
{
    public class GeneralOutputWriter
    {
        public static void WriteOnPosition(int cursorX, int cursorY, string str)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write(str);
        }
    }
}