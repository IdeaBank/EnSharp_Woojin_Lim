using System;
using Library.Model;

namespace Library.View.Admin
{
    public class AdminModifyBookView : ViewFrame
    {
        public static void Print()
        {
            Console.WriteLine("없앨 책의 ID를 입력하세요: ");
        }
        public static void Print(Data data, int bookId)
        {
            Console.Clear();

            string[] bookInfoList =
            {
                "Name".PadRight(25, ' ') + ": ",
                "Author".PadRight(25, ' ') + ": ",
                "Publisher".PadRight(25, ' ') + ": ",
                "Quantity".PadRight(25, ' ') + ": ",
                "Price".PadRight(25, ' ') + ": ",
                "Published date".PadRight(25, ' ') + ": ",
                "ISBN".PadRight(25, ' ') + ": ",
                "Description".PadRight(25, ' ') + ": "
            };

            foreach (Book book in data.books)
            {
                if (book.bookId == bookId)
                {
                    Console.WriteLine("========Original information========");
                    
                    Console.Write(bookInfoList[0]);
                    Console.WriteLine(book.name);
                    Console.Write(bookInfoList[1]);
                    Console.WriteLine(book.author);
                    Console.Write(bookInfoList[2]);
                    Console.WriteLine(book.publisher);
                    Console.Write(bookInfoList[3]);
                    Console.WriteLine(book.quantity);
                    Console.Write(bookInfoList[4]);
                    Console.WriteLine(book.price);
                    Console.Write(bookInfoList[5]);
                    Console.WriteLine(book.publishedDate);
                    Console.Write(bookInfoList[6]);
                    Console.WriteLine(book.isbn);
                    Console.Write(bookInfoList[7]);
                    Console.WriteLine(book.description);
                    Console.WriteLine();
                    
                    Console.WriteLine("========New information========");

                    foreach (string info in bookInfoList)
                    {
                        Console.WriteLine(info);
                    }
                }
            }
        }
    }
}