using System;

namespace Library.View.Admin
{
    public class AdminDeleteBookView : ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.Write("삭제할 책의 ID를 입력하세요: ");
        }
    }
}