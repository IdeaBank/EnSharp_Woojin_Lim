using System;
using System.Collections.Generic;
using Library.Model;

namespace Library.View.Admin
{
    public class AdminSearchBookView : ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.WriteLine("Name".PadRight(10, ' ') + ": ");
            Console.WriteLine("Author".PadRight(10, ' ') + ": ");
            Console.WriteLine("Publisher".PadRight(10, ' ') + ": ");

            Console.WriteLine("===========================\n\n");
        }

        public static void Print(List<Book> books)
        {
            Console.Clear();
            
            string[] bookInfoList =
            {
                "ID".PadRight(19, ' ') + ": ",
                "Name".PadRight(19, ' ') + ": ",
                "Author".PadRight(19, ' ') + ": ",
                "Publisher".PadRight(19, ' ') + ": ",
                "Quantity".PadRight(19, ' ') + ": ",
                "Price".PadRight(19, ' ') + ": ",
                "Published date".PadRight(19, ' ') + ": ",
                "ISBN".PadRight(19, ' ') + ": ",
                "Description".PadRight(19, ' ') + ": "
            };
            
            foreach (Book book in books)
            {
                Console.Write(bookInfoList[0]);
                Console.WriteLine(book.bookId);
                Console.Write(bookInfoList[1]);
                Console.WriteLine(book.name);
                Console.Write(bookInfoList[2]);
                Console.WriteLine(book.author);
                Console.Write(bookInfoList[3]);
                Console.WriteLine(book.publisher);
                Console.Write(bookInfoList[4]);
                Console.WriteLine(book.quantity);
                Console.Write(bookInfoList[5]);
                Console.WriteLine(book.price);
                Console.Write(bookInfoList[6]);
                Console.WriteLine(book.publishedDate);
                Console.Write(bookInfoList[7]);
                Console.WriteLine(book.isbn);
                Console.Write(bookInfoList[8]);
                Console.WriteLine(book.description);
                Console.WriteLine("\n==============================\n");   
            }
        }
    }
}