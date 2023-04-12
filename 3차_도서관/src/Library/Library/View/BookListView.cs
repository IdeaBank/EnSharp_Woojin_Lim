using System;
using System.Collections.Generic;
using Library.Model;

namespace Library.View
{
    public class BookListView
    {
        public static void PrintBookSearch()
        {
            Console.WriteLine("Book name: ".PadLeft(15, ' '));
            Console.WriteLine("Author: ".PadLeft(15, ' '));
            Console.WriteLine("Publisher: ".PadLeft(15, ' '));
        }
        
        public static void PrintBookList(List<Book> books)
        {
            Console.Clear();

            foreach (Book book in books)
            {
                Console.WriteLine(new string('=', 30));
                Console.Write("Book ID: ".PadLeft(20, ' '));
                Console.WriteLine(book.bookId);
                
                Console.Write("Book name: ".PadLeft(20, ' '));
                Console.WriteLine(book.name);
                
                Console.Write("Book Author: ".PadLeft(20, ' '));
                Console.WriteLine(book.author);
                
                Console.Write("Publisher: ".PadLeft(20, ' '));
                Console.WriteLine(book.publisher);
                
                Console.Write("Quantity: ".PadLeft(20, ' '));
                Console.WriteLine(book.quantity);
                
                Console.Write("Price: ".PadLeft(20, ' '));
                Console.WriteLine(book.price);
                
                Console.Write("Published date: ".PadLeft(20, ' '));
                Console.WriteLine(book.publishedDate);
                
                Console.Write("ISBN: ".PadLeft(20, ' '));
                Console.WriteLine(book.isbn);
                
                Console.Write("Description: ".PadLeft(20, ' '));
                Console.WriteLine(book.description);
            }
        }
    }
}