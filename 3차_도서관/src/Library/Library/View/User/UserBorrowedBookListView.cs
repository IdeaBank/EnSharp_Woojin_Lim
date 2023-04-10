using System;
using Library.Model;

namespace Library.View.User
{
    public class UserBorrowedBookListView: ViewFrame
    {
        public static void Print(Model.User user)
        {
            Console.Clear();

            Console.WriteLine(user.name);
            
            foreach (BorrowedBook borrowedBook in user.borrowedBooks)
            {
                Console.WriteLine("==============================\n");
                Console.Write("ID: ");
                Console.WriteLine(borrowedBook.bookId);
                Console.Write("Borrowed date: ");
                Console.WriteLine(borrowedBook.borrowedDate);
                Console.WriteLine("\n==============================\n");
            }
        }
    }
}