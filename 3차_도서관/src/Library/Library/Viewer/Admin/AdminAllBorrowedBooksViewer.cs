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
                Console.WriteLine(new string('=', 25) + user.name +new string('=', 25));
                foreach (BorrowedBook book in user.borrowedBooks)
                {
                    Book bookData = dataManager.bookManager.GetBook(data, book.bookId);
                    Console.WriteLine("ID: ".PadLeft(20, ' ') + bookData.bookId);
                    Console.WriteLine("Name: ".PadLeft(20, ' ') + bookData.name);
                    Console.WriteLine("Author: ".PadLeft(20, ' ') + bookData.author);
                    Console.WriteLine("Publisher: ".PadLeft(20, ' ') + bookData.publisher);
                    Console.WriteLine("Quantity: ".PadLeft(20, ' ') + bookData.quantity);
                    Console.WriteLine("Price: ".PadLeft(20, ' ') + bookData.price);
                    Console.WriteLine("Published Date: ".PadLeft(20, ' ') + bookData.publishedDate);
                    Console.WriteLine("ISBN: ".PadLeft(20, ' ') + bookData.isbn);
                    Console.WriteLine("Description: ".PadLeft(20, ' ') + bookData.description);
                    Console.WriteLine("Borrowed Date: ".PadLeft(20, ' ') + book.borrowedDate);
                    Console.WriteLine("\n\n");
                }
            }

            Console.ReadKey(true);
        }
    }
}