using System;
using System.Collections.Generic;
using Library.Model;

namespace Library.View.Admin
{
    public class AdminSearchBookView : ViewFrame
    {
        public static void Print(Data data)
        {
            Console.Clear();

            string[] bookInfoList =
            {
                "책 아이디".PadRight(19, ' ') + ": ",
                "책 제목".PadRight(19, ' ') + ": ",
                "작가".PadRight(19, ' ') + ": ",
                "출판사".PadRight(19, ' ') + ": ",
                "수량".PadRight(19, ' ') + ": ",
                "가격".PadRight(19, ' ') + ": ",
                "출판일".PadRight(19, ' ') + ": ",
                "ISBN".PadRight(19, ' ') + ": ",
                "책 정보".PadRight(19, ' ') + ": "
            };

            Console.WriteLine("책 이름: ");
            Console.WriteLine("작가: ");
            Console.WriteLine("출판사: ");

            Console.WriteLine("===========================\n\n");

            foreach (Book book in data.books)
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

        public static void Print(List<Book> books)
        {
            string[] bookInfoList =
            {
                "책 아이디".PadRight(19, ' ') + ": ",
                "책 제목".PadRight(19, ' ') + ": ",
                "작가".PadRight(19, ' ') + ": ",
                "출판사".PadRight(19, ' ') + ": ",
                "수량".PadRight(19, ' ') + ": ",
                "가격".PadRight(19, ' ') + ": ",
                "출판일".PadRight(19, ' ') + ": ",
                "ISBN".PadRight(19, ' ') + ": ",
                "책 정보".PadRight(19, ' ') + ": "
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