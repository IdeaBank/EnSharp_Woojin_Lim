using System;
using System.Collections.Generic;
using System.Data;
using Library.Constant;
using Library.Model.DTO;
using Library.Utility;

namespace Library.View
{
    public class SearchResultView
    {
        private static SearchResultView _instance;

        private SearchResultView()
        {

        }

        public static SearchResultView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SearchResultView();
                }

                return _instance;
            }
        }

        private void PrintChooseUserContour()
        {
            ConsoleWriter.getInstance.DrawContour(60, 4);
        }

        public void SearchBook()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.DrawContour(50, 17);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 2, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 1, "Author: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 0, "Publisher: ",
                AlignType.LEFT);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 2,
                new string('=', 15) + "EXAMPLE" + new string('=', 15), AlignType.CENTER);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4,
                "세이노의 가르침", AlignType.RIGHT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5, "Author: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5,
                "세이노", AlignType.RIGHT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6, "Publisher: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6,
                "데이윈", AlignType.RIGHT);

        }

        public void PrintSearchUser()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.DrawContour(50, 17);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 2, "ID: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 1, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 0, "Address: ",
                AlignType.LEFT);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 2,
                new string('=', 15) + "EXAMPLE" + new string('=', 15), AlignType.CENTER);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4, "ID: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 4,
                "userid12", AlignType.RIGHT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5, "Name: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 5,
                "Woojin", AlignType.RIGHT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6, "Address: ",
                AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 6,
                "경기도 고양시", AlignType.RIGHT);
        }

        public void ViewSearchBookResult(List<BookDTO> books)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));

            foreach (BookDTO book in books)
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

        public void ViewSearchUserResult(List<UserDTO> users)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));

            foreach (UserDTO user in users)
            {
                Console.Write("Name: ".PadLeft(15, ' '));
                Console.WriteLine(user.Name);

                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(user.Id);

                Console.Write("Age: ".PadLeft(15, ' '));
                Console.WriteLine(DateTime.Now.Year - int.Parse(user.BirthYear.ToString()) + 1);


                Console.Write("Address: ".PadLeft(15, ' '));
                Console.WriteLine(user.Address);
                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 36));
        }

        public void PrintBorrowedBooks(string userName, List<BorrowedBookDTO> books)
        {
            Console.WriteLine(new string('=', 15) + userName + new string('=', 15));

            foreach (BorrowedBookDTO book in books)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookId);

                Console.Write("Borrowed date: ".PadLeft(15, ' '));
                Console.WriteLine(book.BorrowedDate);
            }

            Console.WriteLine(new string('=', 36));
            Console.WriteLine();
        }
        
        public void PrintReturnedBooks(string userName, List<BorrowedBookDTO> books)
        {
            Console.WriteLine(new string('=', 15) + userName + new string('=', 15));

            foreach (BorrowedBookDTO book in books)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookId);

                Console.Write("Borrowed date: ".PadLeft(15, ' '));
                Console.WriteLine(book.BorrowedDate);
                
                Console.Write("Returned date: ".PadLeft(15, ' '));
                Console.WriteLine(book.ReturnedDate);
            }

            Console.WriteLine(new string('=', 36));
            Console.WriteLine();
        }

        public void PrintDeleteUser()
        {
            Console.Clear();
            PrintChooseUserContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of user to remove: ", AlignType.LEFT);
        }
    }
}