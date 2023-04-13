using System;
using Library.Model;

namespace Library
{
    public class AdminAllBorrowedBooksViewer
    {
        public static void ShowAllBorrowedBooks(Data data, DataManager dataManager)
        {
            Console.Clear();
            
            foreach (User user in data.users)
            {
                Console.WriteLine(new string('=', 30));
                foreach (BorrowedBook book in user.borrowedBooks)
                {
                    Console.WriteLine("ID: ".PadLeft(20, ' ') + book.bookId);
                    Console.WriteLine("Name: ".PadLeft(20, ' ') + book.name);
                    Console.WriteLine("Author: ".PadLeft(20, ' ') + book.author);
                    Console.WriteLine("Publisher: ".PadLeft(20, ' ') + book.publisher);
                    Console.WriteLine("Quantity: ".PadLeft(20, ' ') + book.quantity);
                    Console.WriteLine("Price: ".PadLeft(20, ' ') + book.price);
                    Console.WriteLine("Published Date: ".PadLeft(20, ' ') + book.publishedDate);
                    Console.WriteLine("ISBN: ".PadLeft(20, ' ') + book.isbn);
                    Console.WriteLine("Description: ".PadLeft(20, ' ') + book.description);
                    Console.WriteLine("Borrowed Date: ".PadLeft(20, ' ') + book.borrowedDate);
                    Console.WriteLine("\n\n");
                }
            }

            Console.ReadKey(true);
        }
    }
}