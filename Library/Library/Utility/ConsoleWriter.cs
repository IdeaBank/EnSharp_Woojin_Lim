using System;
using Library.Constants;

namespace Library.Utility
{
    public class ConsoleWriter
    {
        private ConsoleWriter _instance;

        private ConsoleWriter()
        {
            
        }

        public ConsoleWriter getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new ConsoleWriter();
                }

                return _instance;
            }
        }

        public static void WriteOnPosition(int cursorX, int cursorY, string str)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write(str);
        }

        public static void WriteOnPositionWithAlign(int cursorX, int cursorY, string str, AlignType alignType, ConsoleColor color=ConsoleColor.White)
        {
            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;
            
            switch (alignType)
            {
                case AlignType.LEFT:
                    Console.SetCursorPosition(cursorX - str.Length, cursorY);
                    break;
                case AlignType.RIGHT:
                    Console.SetCursorPosition(cursorX, cursorY);
                    break;
                case AlignType.CENTER:
                    Console.SetCursorPosition(cursorX - str.Length / 2, cursorY);
                    break;
            }

            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ResetColor();
            
            Console.SetCursorPosition(currentX, currentY);
        }

        public static void DrawContour(int countourWidth, int contourHeight)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;
            
            WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - contourHeight / 2,
                new string('#', countourWidth), AlignType.CENTER);

            for (int i = 0; i < contourHeight - 2; ++i)
            {
                WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - contourHeight / 2 + 1 + i,
                    '#' + new string(' ', countourWidth - 2) + '#', AlignType.CENTER);
            }

            WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + contourHeight / 2 - 1,
                new string('#', countourWidth), AlignType.CENTER);
            
            Console.SetCursorPosition(currentX, currentY);
        }
    }
}