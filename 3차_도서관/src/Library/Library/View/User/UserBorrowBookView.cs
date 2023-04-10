using System;

namespace Library.View.User
{
    public class UserBorrowBookView: ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.WriteLine("Enter book name to borrow: ");
        }
    }
}