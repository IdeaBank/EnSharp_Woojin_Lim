using System;
using Library.Constants;

namespace Library.Utility
{
    class FramePrinter
    {
        private static FramePrinter _instance;

        private FramePrinter()
        {
    
        }

        public static FramePrinter getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FramePrinter();
                }
                return _instance;
            }
        }

        public static void PrintFrame(int height)
        {
            height = height + 9;
            
            PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - height / 2 - 1, "BACK: ESC       ENTER: ENTER", AlignType.CENTER, ConsoleColor.White);
            PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - height / 2, new string('#', 40), AlignType.CENTER, ConsoleColor.White);
            
            for (int i = 0; i < height - 2; ++i)
            {
                PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - height / 2 + i + 1, '#' + new string(' ', 38) + '#', AlignType.CENTER, ConsoleColor.White);
            }
            
            PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + height / 2 - 1, new string('#', 40), AlignType.CENTER, ConsoleColor.White);
        }
        
        public static void PrintLibrary(int location)
        {
            PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 3 - location / 2, "╦  ╦╔╗ ╦═╗╔═╗╦═╗╦ ╦",
                AlignType.CENTER, ConsoleColor.White);
            PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 2 - location / 2, "║  ║╠╩╗╠╦╝╠═╣╠╦╝╚╦╝", 
                AlignType.CENTER, ConsoleColor.White);
            PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 1 - location / 2, "╩═╝╩╚═╝╩╚═╩ ╩╩╚═ ╩ ",
                AlignType.CENTER, ConsoleColor.White);
        }
        
        public static void PrintOnPosition(int x, int y, string str, AlignType align, ConsoleColor color)
        {
            // 마지막 커서의 위치 저장
            int lastCursorX = Console.CursorLeft;
            int lastCursorY = Console.CursorTop;

            // 각 경우의 수에 맞게 정렬
            switch (align)
            {
                case AlignType.LEFT:
                    Console.SetCursorPosition(x, y);
                    break;
                case AlignType.CENTER:
                    Console.SetCursorPosition(x - str.Length / 2, y);
                    break;
                case AlignType.RIGHT:
                    Console.SetCursorPosition(x - str.Length, y);
                    break;
                default:
                    return;
            }

            Console.ForegroundColor = color;
            
            // 원하는 문자열 출력
            Console.Write(str);
            
            Console.ResetColor();
            
            // 커서 복귀
            Console.SetCursorPosition(lastCursorX, lastCursorY);
        }
    }
}