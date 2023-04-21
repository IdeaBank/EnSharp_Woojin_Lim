using System;
using LTT.Constant;

namespace LTT.Utility
{
    public class ConsoleWriter
    {
        public void PrintLogo(int up)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up - 4, " .d888b,?88   d8P d888b8b   d888b8b    88bd88b  d888b8b  ");
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up - 3, " ?8b,   d88   88 d8P' ?88  d8P' ?88    88P' ?8bd8P' ?88  ");
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up - 2, "   `?8b ?8(  d88 88b  ,88b 88b  ,88b  d88   88P88b  ,88b ");
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up - 1, "`?888P' `?88P'?8b`?88P'`88b`?88P'`88bd88'   88b`?88P'`88b");
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up + 0, "                        )88                           )88");
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up + 1, "                       ,88P                          ,88P");
            PrintOnPosition(windowWidthHalf, windowHeightHalf - up + 2, "                   `?8888P                       `?8888P ");
        }
        
        private int GetKoreanCount(string str)
        {
            // 문자열에서 한글의 개수를 세야 함 (영어는 한 칸, 한글은 두 칸 차지)
            int koreanCount = 0;
            
            // 문자열을 순회하며 한글의 개수를 셈
            foreach (char ch in str)
            {
                // '가' 에서 '힣', 'ㄱ'에서 ㆎ' 이면 카운트 1 증가
                if ((0xAC00 <= ch && ch <= 0xD7A3) || (0x3131 <= ch && ch <= 0x318E))
                {
                    koreanCount += 1;
                }
            }

            return koreanCount;
        }
        
        private string StringPadRight(string str, int count)
        {
            // 전체 문자열 개수에서 한글 개수를 뺀 수 만큼 오른쪽 칸을 비우고 반환
            return str.PadRight(count - GetKoreanCount(str), ' ');
        }
        public void SetCursorPositionWithHangeul(int x, int y, string str, Align align = Align.LEFT)
        {
            switch (align)
            {
                case Align.LEFT:
                    Console.SetCursorPosition(x, y);
                    break;

                case Align.CENTER:
                    string str_temp = str.ToString();
                    int korCount = GetKoreanCount(str_temp.Substring(0, (int)(str_temp.Length / 2.0 + 0.5)));
                    Console.SetCursorPosition(
                        x - str.Length / 2, y);
                    break;

                case Align.RIGHT:
                    Console.SetCursorPosition(x - GetKoreanCount(str) - str.Length, y);
                    break;
            }
        }
        
        public void PrintOnPosition(int x, int y, string str, Align align = Align.CENTER, ConsoleColor color = ConsoleColor.White)
        {
            SetCursorPositionWithHangeul(x, y, str, align);

            Console.ForegroundColor = color;
            
            // 원하는 문자열 출력
            Console.Write(str);
            
            Console.ResetColor();
        }
        
        
    }
}