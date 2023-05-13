using Library.Constant;
using Library.Model.DTO;
using Library.Utility;
using System;
using System.Collections.Generic;
using Library.Model.DAO;

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

            Console.WriteLine("제목: ");
            Console.WriteLine("작가: ");
            Console.WriteLine("출판사: \n\n");
        }

        public void PrintSearchUser()
        {
            Console.WriteLine("아이디: ");
            Console.WriteLine("이름: ");
            Console.WriteLine("주소: ");
        }

        public string GetBookNameWithId(int bookId)
        {
            List<BookDTO> books = BookDAO.getInstance.GetAllBooks();

            foreach (BookDTO book in books)
            {
                if (book.Id == bookId)
                {
                    return book.Name;
                }
            }

            return "<존재하지 않는 책>";
        }

        public void ViewSearchBookResult(List<BookDTO> books)
        {
            Console.WriteLine(new string('=', 15) + "RESULT" + new string('=', 15));

            if (books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("검색 결과가 없습니다!");
                Console.ResetColor();
            }

            foreach (BookDTO book in books)
            {
                Console.Write("ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.Id);

                Console.Write("책 제목: ".PadLeft(15, ' '));
                Console.WriteLine(book.Name);

                Console.Write("작가: ".PadLeft(15, ' '));
                Console.WriteLine(book.Author);

                Console.Write("출판사: ".PadLeft(15, ' '));
                Console.WriteLine(book.Publisher);

                Console.Write("책 수량: ".PadLeft(15, ' '));
                Console.WriteLine(book.Quantity);

                Console.Write("설명: ".PadLeft(15, ' '));
                Console.WriteLine(book.Description);
                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 36));
        }

        public void ViewSearchUserResult(List<UserDTO> users)
        {
            Console.WriteLine(new string('=', 36));

            if (users.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("검색 결과가 없습니다!");
                Console.ResetColor();
            }

            foreach (UserDTO user in users)
            {
                Console.Write("이름: ".PadLeft(15, ' '));
                Console.WriteLine(user.Name);

                Console.Write("아이디: ".PadLeft(15, ' '));
                Console.WriteLine(user.Id);

                Console.Write("나이: ".PadLeft(15, ' '));
                Console.WriteLine(DateTime.Now.Year - int.Parse(user.BirthYear.ToString()) + 1);

                Console.Write("주소: ".PadLeft(15, ' '));
                Console.WriteLine(user.Address);
                Console.WriteLine();
            }

            Console.WriteLine(new string('=', 36));
        }

        public void PrintBorrowedBooks(string userName, List<BorrowedBookDTO> books)
        {
            Console.WriteLine(new string('=', 15) + userName + new string('=', 14));

            if (books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("빌린 책이 없습니다!");
                Console.ResetColor();
            }

            foreach (BorrowedBookDTO book in books)
            {
                Console.Write("책 ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookId);

                Console.Write("책 이름: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookName);

                Console.Write("책 작가: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookAuthor);

                Console.Write("책 출판사: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookPublisher);

                Console.Write("빌린 날짜: ".PadLeft(15, ' '));
                Console.WriteLine(book.BorrowedDate);
                
                Console.Write("반납 날짜: ".PadLeft(15, ' '));
                Console.WriteLine(book.ReturnedDate);
            }

            Console.WriteLine(new string('=', 36));
            Console.WriteLine();
        }

        public void PrintReturnedBooks(string userName, List<BorrowedBookDTO> books)
        {
            Console.WriteLine(new string('=', 15) + userName + new string('=', 15));

            if (books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("반납한 책이 없습니다!");
                Console.ResetColor();
            }

            foreach (BorrowedBookDTO book in books)
            {
                Console.Write("책 ID: ".PadLeft(15, ' '));
                Console.WriteLine(book.BookId);

                Console.Write("책 이름: ".PadLeft(15, ' '));
                Console.WriteLine(GetBookNameWithId(book.BookId));
                
                Console.Write("빌린 날짜: ".PadLeft(15, ' '));
                Console.WriteLine(book.BorrowedDate);

                Console.Write("반납한 날짜: ".PadLeft(15, ' '));
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

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "삭제할 유저 아이디: ", AlignType.LEFT);
        }
    }
}