using System;
using Library.Constants;

namespace Library.View
{
    public class ViewFrame
    {
        public void PrintOnPosition(int x, int y, string str, int align, ConsoleColor color)
        {
            // 마지막 커서의 위치 저장
            int lastCursorX = Console.CursorLeft;
            int lastCursorY = Console.CursorTop;

            // 각 경우의 수에 맞게 정렬
            switch (align)
            {
                case ALIGN.LEFT:
                    Console.SetCursorPosition(x, y);
                    break;
                case ALIGN.CENTER:
                    Console.SetCursorPosition(x - str.Length / 2, y);
                    break;
                case ALIGN.RIGHT:
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