using System;

namespace Library.View.User
{
    public class UserBorrowBookView: ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.WriteLine("빌릴 책의 이름을 입력하세요: ");
        }
    }
}