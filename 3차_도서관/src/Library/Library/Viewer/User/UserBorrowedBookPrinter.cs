using System;
using Library.Model;
using Library.Utility;

namespace Library.Viewer.User
{
    public class UserBorrowedBookPrinter: ViewerClass
    {
        private int currentUserNumber;
        
        public UserBorrowedBookPrinter(Data data, DataManager dataManager, InputFromUser inputFromUser, int currentUserNumber): base(data, dataManager, inputFromUser)
        {
            this.currentUserNumber = currentUserNumber;
        }

        public void PrintUserBorrowedList()
        {
            Console.Clear();
            
            Console.WriteLine(new string('=', 30));

            foreach (Model.User user in data.users)
            {
                if (user.userNumber == currentUserNumber)
                {
                    foreach (BorrowedBook borrowedBook in user.borrowedBooks)
                    {
                        foreach (Book book in data.books)
                        {
                            if (book.bookId == borrowedBook.bookId)
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
                                Console.WriteLine("Borrowed Date: ".PadLeft(20, ' ') + borrowedBook.borrowedDate);
                                Console.WriteLine("\n\n");
                            }
                        }
                    }
                }
            }

            Console.ReadKey(true);
            Console.Clear();
        }
    }
}