using System;
using Library.Model;

namespace Library.View.Admin
{
    public class AdminAllBorrowedBookListView: ViewFrame
    {
        public static void Print(Data data)
        {
            Console.Clear();
            
            string[] bookInfoList =
            {
                "ID".PadRight(18, ' ') + ": ",
                "Name".PadRight(18, ' ') + ": ",
                "Author".PadRight(18, ' ') + ": ",
                "Publisher".PadRight(18, ' ') + ": ",
                "Quantity".PadRight(18, ' ') + ": ",
                "Price".PadRight(18, ' ') + ": ",
                "Published date".PadRight(18, ' ') + ": ",
                "ISBN".PadRight(18, ' ') + ": ",
                "Description".PadRight(18, ' ') + ": "
            };
            
            foreach (Model.User user in data.users)
            {
                Console.WriteLine(user.name);
                Console.WriteLine("==============================");

                foreach (BorrowedBook borrowedBook in user.borrowedBooks)
                {
                    foreach (Book book in data.books)
                    {
                        if (borrowedBook.bookId == book.bookId)
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
                        }
                    }
                }
            }
        }
    }
}