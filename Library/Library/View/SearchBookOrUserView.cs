using Library.Constants;
using Library.Model;
using Library.Utility;
using System;
using System.Collections.Generic;
using System.Data;

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

        public static void ViewSearchBookResult(DataSet books)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));

            foreach (DataRow book in books.Tables["Book"].Rows)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(book["id"]);

                Console.Write("Name: ".PadLeft(15, ' '));
                Console.WriteLine(book["name"]);

                Console.Write("Author: ".PadLeft(15, ' '));
                Console.WriteLine(book["author"]);

                Console.Write("Publisher: ".PadLeft(15, ' '));
                Console.WriteLine(book["publisher"]);

                Console.Write("Description: ".PadLeft(15, ' '));
                Console.WriteLine(book["description"]);
                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 36));
        }

        public static void ViewSearchUserResult(DataSet users)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));

            foreach (DataRow user in users.Tables["User"].Rows)
            {
                Console.Write("Name: ".PadLeft(15, ' '));
                Console.WriteLine(user["name"]);

                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(user["id"]);

                Console.Write("Age: ".PadLeft(15, ' '));
                Console.WriteLine(DateTime.Now.Year - int.Parse(user["birth_year"].ToString()) + 1);


                Console.Write("Address: ".PadLeft(15, ' '));
                Console.WriteLine(user["address"]);
                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 36));
        }

        public static void PrintBorrowedBooks(string userName, DataSet books)
        {
            Console.WriteLine(new string('=', 15) + userName + new string('=', 15));

            foreach (DataRow row in books.Tables["Borrowed_Book"].Rows)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(row["book_id"]);

                Console.Write("Borrowed date: ".PadLeft(15, ' '));
                Console.WriteLine(row["borrowed_date"]);
            }

            Console.WriteLine(new string('=', 36));
            Console.WriteLine();
        }
        
        public static void PrintReturnedBooks(string userName, DataSet books)
        {
            Console.WriteLine(new string('=', 15) + userName + new string('=', 15));

            foreach (DataRow row in books.Tables["Returned_Book"].Rows)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(row["book_id"]);

                Console.Write("Borrowed date: ".PadLeft(15, ' '));
                Console.WriteLine(row["borrowed_date"]);
                
                Console.Write("Returned date: ".PadLeft(15, ' '));
                Console.WriteLine(row["returned_date"]);
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