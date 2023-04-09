using System;

namespace Library.View.Admin
{
    public class AdminAddBookView
    {
        public static void Print()
        {
            string[] instructions =
            {
                "이름을 입력하세요".PadRight(19, ' ') + ": ",
                "작가를 입력하세요".PadRight(19, ' ') + ": ",
                "출판사를 입력하세요".PadRight(19, ' ') + ": ",
                "수량을 입력하세요".PadRight(19, ' ') + ": ",
                "가격을 입력하세요".PadRight(19, ' ') + ": ",
                "출판 날짜를 입력하세요".PadRight(19, ' ') + ": ",
                "ISBN을 입력하세요".PadRight(19, ' ') + ": ",
                "설명을 입력하세요".PadRight(19, ' ') + ": "
            };
            
            foreach (string instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
        }
    }
}