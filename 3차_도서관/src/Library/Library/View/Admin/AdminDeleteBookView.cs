using System;

namespace Library.View.Admin
{
    public class AdminDeleteBookView : ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.Write("Enter book ID to delete: ");
        }
    }
}