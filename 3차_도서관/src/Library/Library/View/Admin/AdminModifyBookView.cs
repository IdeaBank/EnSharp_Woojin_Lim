using System;
using Library.Model;

namespace Library.View.Admin
{
    public class AdminModifyBookView : ViewFrame
    {
        public static void Print(Data data, int bookId)
        {
            Console.Clear();

            string[] bookInfoList =
            {
                "책 제목".PadRight(19, ' ') + ": ",
                "작가".PadRight(19, ' ') + ": ",
                "출판사".PadRight(19, ' ') + ": ",
                "수량".PadRight(19, ' ') + ": ",
                "가격".PadRight(19, ' ') + ": ",
                "출판일".PadRight(19, ' ') + ": ",
                "ISBN".PadRight(19, ' ') + ": ",
                "책 정보".PadRight(19, ' ') + ": "
            };

            foreach (Book book in data.books)
            {
                if (book.bookId == bookId)
                {
                    Console.WriteLine("========원래 정보=======");
                    
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
                    
                    Console.WriteLine("========수정할 정보========");

                    foreach (string info in bookInfoList)
                    {
                        Console.WriteLine(info);
                    }
                }
            }
        }
    }
}