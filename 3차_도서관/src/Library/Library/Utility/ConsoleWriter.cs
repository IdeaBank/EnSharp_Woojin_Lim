using Library.Constant;
using System;

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

        public void DrawLogo(int cursorX, int cursorY)
        {
            WriteOnPosition(cursorX, cursorY + 0, "   _                   ____     ____        _       ____     __   __ ");
            WriteOnPosition(cursorX, cursorY + 1, "  |\"|        ___    U | __\")uU |  _\"\\ u U  /\"\\  uU |  _\"\\ u  \\ \\ / / ");
            WriteOnPosition(cursorX, cursorY + 2, "U | | u     |_\"_|    \\|  _ \\/ \\| |_) |/  \\/ _ \\/  \\| |_) |/   \\ V /  ");
            WriteOnPosition(cursorX, cursorY + 3, " \\| |/__     | |      | |_) |  |  _ <    / ___ \\   |  _ <    U_|\"|_u ");
            WriteOnPosition(cursorX, cursorY + 4, "  |_____|  U/| |\\u    |____/   |_| \\_\\  /_/   \\_\\  |_| \\_\\     |_|   ");
            WriteOnPosition(cursorX, cursorY + 5, "  //  \\\\.-,_|___|_,-._|| \\\\_   //   \\\\_  \\\\    >>  //   \\\\_.-,//|(_  ");
            WriteOnPosition(cursorX, cursorY + 6, " (_\")(\"_)\\_)-' '-(_/(__) (__) (__)  (__)(__)  (__)(__)  (__)\\_) (__) ");
        }

        public void WriteOnPosition(int cursorX, int cursorY, string str)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write(str);
        }

        public void WriteOnPositionWithAlign(int cursorX, int cursorY, string str, AlignType alignType,
            ConsoleColor color = ConsoleColor.White)
        {
            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;

            Console.ForegroundColor = color;

            switch (alignType)
            {
                case AlignType.LEFT:
                    WriteOnPosition(cursorX - str.Length - UserInputManager.getInstance.GetHangeulCount(str), cursorY, str);
                    break;
                case AlignType.RIGHT:
                    WriteOnPosition(cursorX, cursorY, str);
                    break;
                case AlignType.CENTER:
                    WriteOnPosition(cursorX - str.Length / 2 - UserInputManager.getInstance.GetHangeulCount(str.Substring(0, str.Length / 2)), cursorY, str);
                    break;
            }

            Console.ResetColor();
            Console.SetCursorPosition(currentX, currentY);
        }

        public void PrintWarning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(warning);
            Console.ResetColor();
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