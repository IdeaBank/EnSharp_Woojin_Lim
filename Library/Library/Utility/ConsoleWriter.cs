using Library.Constant;
using System;
using System.Text;

namespace Library.Utility
{
    public class ConsoleWriter
    {
        private static ConsoleWriter _instance;

        private ConsoleWriter()
        {

        }

        public static ConsoleWriter getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConsoleWriter();
                }

                return _instance;
            }
        }

        public void WriteOnPosition(int cursorX, int cursorY, string str)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write(str);
        }

        public void WriteOnPositionWithAlign(int cursorX, int cursorY, string str, AlignType alignType, ConsoleColor color = ConsoleColor.White)
        {
            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;

            Console.ForegroundColor = color;
            
            switch (alignType)
            {
                case AlignType.LEFT:
                    WriteOnPosition(cursorX - str.Length, cursorY, str);
                    break;
                case AlignType.RIGHT:
                    WriteOnPosition(cursorX, cursorY, str);
                    break;
                case AlignType.CENTER:
                    WriteOnPosition(cursorX - Encoding.Default.GetByteCount(str) / 2, cursorY, str);
                    break;
            }

            Console.ResetColor();
            Console.SetCursorPosition(currentX, currentY);
        }

        public void DrawContour(int contourWidth, int contourHeight)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;

            WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - contourHeight / 2,
                new string('#', contourWidth), AlignType.CENTER);

            for (int i = 0; i < contourHeight - 2; ++i)
            {
                WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - contourHeight / 2 + 1 + i,
                    '#' + new string(' ', contourWidth - 2) + '#', AlignType.CENTER);
            }

            WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + contourHeight / 2 - 1,
                new string('#', contourWidth), AlignType.CENTER);

            Console.SetCursorPosition(currentX, currentY);
        }
    }
}