using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;

namespace Library.View
{
    public class SearchBookOrUserView
    {
        private SearchBookOrUserView _instance;

        private SearchBookOrUserView()
        {
            
        }

        public SearchBookOrUserView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new SearchBookOrUserView();
                }

                return _instance;
            }
        }

        private static void PrintChooseUserContour()
        {
            ConsoleWriter.DrawContour(60, 4);
        }

        public static void SearchBook()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            ConsoleWriter.DrawContour(50, 17);
            
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 2, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 1, "Author: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 0, "Publisher: ",
                AlignType.LEFT);
            
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 2, 
                new string('=', 15) + "EXAMPLE" + new string('=', 15), AlignType.CENTER);
            
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4,
                "세이노의 가르침", AlignType.RIGHT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5, "Author: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5,
                "세이노", AlignType.RIGHT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6, "Publisher: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6,
                "데이윈", AlignType.RIGHT);
            
        }

        public static void PrintSearchUser()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            ConsoleWriter.DrawContour(50, 17);
            
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 2, "ID: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 1, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 0, "Address: ",
                AlignType.LEFT);
            
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 2, 
                new string('=', 15) + "EXAMPLE" + new string('=', 15), AlignType.CENTER);
            
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4, "ID: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4,
                "userid12", AlignType.RIGHT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5,
                "Woojin", AlignType.RIGHT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6, "Address: ",
                AlignType.LEFT);
            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6,
                "경기도 고양시", AlignType.RIGHT);
        }

        public static void ViewSearchBookResult(List<Book> books)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));
            
            foreach (Book book in books)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.Id);
                
                Console.Write("Name: ".PadLeft(15, ' '));
                Console.WriteLine(book.Name);
                
                Console.Write("Author: ".PadLeft(15, ' '));
                Console.WriteLine(book.Author);
                
                Console.Write("Publisher: ".PadLeft(15, ' '));
                Console.WriteLine(book.Publisher);
                
                Console.Write("Description: ".PadLeft(15, ' '));
                Console.WriteLine(book.Description);
                Console.WriteLine();
            }
            
            Console.WriteLine(new string('=', 36));
        }
        
        public static void ViewSearchUserResult(List<User> users)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));
            
            foreach (User user in users)
            {
                Console.Write("Number: ".PadLeft(15, ' '));
                Console.WriteLine(user.Number);
                
                Console.Write("Name: ".PadLeft(15, ' '));
                Console.WriteLine(user.Name);
                
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(user.Id);
                
                Console.Write("Age: ".PadLeft(15, ' '));
                Console.WriteLine(DateTime.Now.Year - user.BirthYear + 1);
                
                
                Console.Write("Address: ".PadLeft(15, ' '));
                Console.WriteLine(user.Address);
                Console.WriteLine();
            }
            
            Console.WriteLine(new string('=', 36));
        }

        public static void PrintBorrowedOrReturnedBooks(string userName, List<BorrowedBook> books)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + userName + new string('=', 15));
            
            foreach (BorrowedBook book in books)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookId);
                
                Console.Write("Borrowed date: ".PadLeft(15, ' '));
                Console.WriteLine(book.BorrowedDate);
                
                Console.Write("Returned date: ".PadLeft(15, ' '));
                Console.WriteLine(book.ReturnedDate);
                Console.WriteLine();
            }
            
            Console.WriteLine(new string('=', 36));
            Console.WriteLine();
        }
        
        public static void PrintDeleteUser()
        {
            Console.Clear();
            PrintChooseUserContour();
            
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "Number of user to remove: ", AlignType.LEFT);
        }
    }
}