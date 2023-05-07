using Library.Constant;
using Library.Model.DTO;
using Library.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Policy;

namespace Library.Model.DAO
{
    public class BookDAO
    {
        private static BookDAO _instance;

        private BookDAO()
        {
        }

        public static BookDAO getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BookDAO();
                }

                return _instance;
            }
        }

        public bool BookExists(int bookId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BOOK_WITH_ID;
            command.Parameters.AddWithValue("@id", bookId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");

            if (dataSet.Tables["book"].Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        public bool IsBookAvailable(int bookId)
        {
            if (!BookExists(bookId))
            {
                return false;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BOOK_WITH_ID;
            command.Parameters.AddWithValue("@id", bookId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");

            if ((int)dataSet.Tables["book"].Rows[0]["quantity"] == 0)
            {
                return false;
            }

            return true;
        }

        public bool IsBookBorrowed(int bookId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = Constant.SqlQuery.SELECT_BORROWED_BOOK_WITH_BOOK_ID;

            command.Parameters.AddWithValue("@book_id", bookId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "borrowed_book");

            if (dataSet.Tables["borrowed_book"].Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        private BookDTO DataRowToBookDTO(DataRow row)
        {
            BookDTO book = new BookDTO();

            book.Id = int.Parse(row["id"].ToString());
            book.Name = row["name"].ToString();
            book.Author = row["author"].ToString();
            book.Publisher = row["publisher"].ToString();
            book.Quantity = int.Parse(row["quantity"].ToString());
            book.Price = int.Parse(row["price"].ToString());
            book.PublishedDate = row["published_date"].ToString();
            book.Isbn = row["isbn"].ToString();
            book.Description = row["description"].ToString();

            return book;
        }
        private RequestedBookDTO DataRowToRequestedBookDTO(DataRow row)
        {
            RequestedBookDTO book = new RequestedBookDTO();

            book.Isbn = row["isbn"].ToString();
            book.Name = row["name"].ToString();
            book.Author = row["author"].ToString();
            book.Publisher = row["publisher"].ToString();
            book.Price = int.Parse(row["price"].ToString());
            book.PublishedDate = row["published_date"].ToString();
            book.Description = row["description"].ToString();

            return book;
        }

        public List<BookDTO> GetAllBooks()
        {
            List<BookDTO> books = new List<BookDTO>();

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_ALL_BOOK;

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");

            foreach (DataRow row in dataSet.Tables["book"].Rows)
            {
                BookDTO book = DataRowToBookDTO(row);

                books.Add(book);
            }

            return books;
        }

        public List<RequestedBookDTO> GetAllRequestedBooks()
        {
            List<RequestedBookDTO> books = new List<RequestedBookDTO>();

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_ALL_REQUESTED_BOOK;

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "requested_book");

            foreach (DataRow row in dataSet.Tables["requested_book"].Rows)
            {
                RequestedBookDTO book = DataRowToRequestedBookDTO(row);

                books.Add(book);
            }

            return books;
        }

        public BookDTO GetBookInfo(int bookId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BOOK_WITH_ID;

            command.Parameters.AddWithValue("@id", bookId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");
            DataRow dataRow = dataSet.Tables["book"].Rows[0];

            BookDTO book = new BookDTO();

            book.Name = dataRow["name"].ToString();
            book.Author = dataRow["author"].ToString();
            book.Publisher = dataRow["publisher"].ToString();
            book.Quantity = int.Parse(dataRow["quantity"].ToString());
            book.Price = int.Parse(dataRow["price"].ToString());
            book.PublishedDate = dataRow["published_date"].ToString();
            book.Isbn = dataRow["isbn"].ToString();
            book.Description = dataRow["description"].ToString();

            return book;
        }

        public BorrowedBookDTO GetBorrowedBookInfo(string userId, int bookId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BORROWED_BOOK_WITH_BOOK_AND_USER_ID;

            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@book_id", bookId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "borrowed_book");

            if (dataSet.Tables["borrowed_book"].Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataSet.Tables["borrowed_book"].Rows[0];

            command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BOOK_WITH_ID;

            command.Parameters.AddWithValue("@id", int.Parse(row["book_id"].ToString()));

            DataSet bookDataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");
            DataRow bookRow = bookDataSet.Tables["book"].Rows[0];

            return new BorrowedBookDTO(bookId, userId, bookRow["name"].ToString(), bookRow["author"].ToString(), bookRow["publisher"].ToString(), row["borrowed_date"].ToString(), "");
        }

        public List<BorrowedBookDTO> GetBorrowedBooks(string userId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BORROWED_BOOK_WITH_USER_ID;

            command.Parameters.AddWithValue("@user_id", userId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "borrowed_book");
            List<BorrowedBookDTO> borrowedBooks = new List<BorrowedBookDTO>();

            foreach (DataRow row in dataSet.Tables["borrowed_book"].Rows)
            {
                command = DatabaseConnection.getInstance.Conn.CreateCommand();
                command.CommandText = SqlQuery.SELECT_BOOK_WITH_ID;

                command.Parameters.AddWithValue("@id", int.Parse(row["book_id"].ToString()));

                DataSet bookDataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");
                DataRow bookRow = bookDataSet.Tables["book"].Rows[0];

                BorrowedBookDTO borrowedBook = new BorrowedBookDTO(int.Parse(row["book_id"].ToString()), userId, bookRow["name"].ToString(), bookRow["author"].ToString(), bookRow["publisher"].ToString(),
                    row["borrowed_date"].ToString(), row["returned_date"].ToString());

                borrowedBooks.Add(borrowedBook);
            }

            return borrowedBooks;
        }

        public List<BorrowedBookDTO> GetReturnedBooks(string userId)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_RETURNED_BOOK_WITH_USER_ID;

            command.Parameters.AddWithValue("@user_id", userId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "returned_book");
            List<BorrowedBookDTO> returnedBooks = new List<BorrowedBookDTO>();

            foreach (DataRow row in dataSet.Tables["returned_book"].Rows)
            {
                command = DatabaseConnection.getInstance.Conn.CreateCommand();
                command.CommandText = SqlQuery.SELECT_BOOK_WITH_ID;

                command.Parameters.AddWithValue("@id", int.Parse(row["book_id"].ToString()));

                DataSet bookDataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");
                DataRow bookRow = bookDataSet.Tables["book"].Rows[0];

                BorrowedBookDTO returnedBook = new BorrowedBookDTO(int.Parse(row["book_id"].ToString()), userId, bookRow["name"].ToString(), bookRow["author"].ToString(), bookRow["publisher"].ToString(),
                    row["borrowed_date"].ToString(), row["returned_date"].ToString());

                returnedBooks.Add(returnedBook);
            }

            return returnedBooks;
        }

        public void AddBook(BookDTO book)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INSERT_BOOK;

            command.Parameters.AddWithValue("@name", book.Name);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@publisher", book.Publisher);
            command.Parameters.AddWithValue("@quantity", book.Quantity);
            command.Parameters.AddWithValue("@price", book.Price);
            command.Parameters.AddWithValue("@published_date", book.PublishedDate);
            command.Parameters.AddWithValue("@isbn", book.Isbn);
            command.Parameters.AddWithValue("@description", book.Description);

            DatabaseConnection.getInstance.ExecuteCommand(command);
        }

        public List<BookDTO> SearchBook(string name, string author, string publisher)
        {
            if (name == "" && author == "" && publisher == "")
            {
                return new List<BookDTO>();
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BOOK_WITH_SEARCH_STRING;

            command.Parameters.AddWithValue("@name", "%" + name + "%");
            command.Parameters.AddWithValue("@author", "%" + author + "%");
            command.Parameters.AddWithValue("@publisher", "%" + publisher + "%");

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "book");
            List<BookDTO> books = new List<BookDTO>();

            foreach (DataRow row in dataSet.Tables["book"].Rows)
            {
                BookDTO book = new BookDTO();

                book.Id = int.Parse(row["id"].ToString());
                book.Author = row["author"].ToString();
                book.Publisher = row["publisher"].ToString();
                book.Name = row["name"].ToString();
                book.Quantity = int.Parse(row["quantity"].ToString());
                book.Price = int.Parse(row["price"].ToString());
                book.Isbn = row["isbn"].ToString();
                book.Description = row["description"].ToString();

                books.Add(book);
            }

            return books;
        }

        public ResultCode BorrowBook(string userId, int bookId)
        {
            if (!BookExists(bookId))
            {
                return ResultCode.NO_BOOK;
            }

            if (!IsBookAvailable(bookId))
            {
                return ResultCode.BOOK_NOT_ENOUGH;
            }

            if (GetBorrowedBookInfo(userId, bookId) != null)
            {
                return ResultCode.ALREADY_BORROWED;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.DECREASE_BOOK_COUNT;

            command.Parameters.AddWithValue("@id", bookId);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INSERT_BORROWED_BOOK;

            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@book_id", bookId);
            command.Parameters.AddWithValue("@borrowed_date", DateTime.Now.ToString());
            command.Parameters.AddWithValue("@returned_date", DateTime.Now.AddDays(7).ToString());

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }

        public ResultCode ReturnBook(string userId, int bookId)
        {
            BorrowedBookDTO borrowedBookInfo = GetBorrowedBookInfo(userId, bookId);

            if (!BookExists(bookId) || borrowedBookInfo == null)
            {
                return ResultCode.NO_BOOK;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INCREASE_BOOK_COUNT;

            command.Parameters.AddWithValue("@id", bookId);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INSERT_RETURNED_BOOK;

            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@book_id", bookId);
            command.Parameters.AddWithValue("@borrowed_date", borrowedBookInfo.BorrowedDate);
            command.Parameters.AddWithValue("@returned_date", DateTime.Now.ToString());

            DatabaseConnection.getInstance.ExecuteCommand(command);

            command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.DELETE_BORROWED_BOOK;

            command.Parameters.AddWithValue("@book_id", bookId);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }

        public void EditBook(BookDTO book)
        {
            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = Constant.SqlQuery.EDIT_BOOK;

            command.Parameters.AddWithValue("@name", book.Name);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@publisher", book.Publisher);
            command.Parameters.AddWithValue("@quantity", book.Quantity);
            command.Parameters.AddWithValue("@price", book.Price);
            command.Parameters.AddWithValue("@published_date", book.PublishedDate);
            command.Parameters.AddWithValue("@isbn", book.Isbn);
            command.Parameters.AddWithValue("@description", book.Description);
            command.Parameters.AddWithValue("@id", book.Id);

            DatabaseConnection.getInstance.ExecuteCommand(command);
        }

        public ResultCode RemoveBook(int bookId)
        {
            if (IsBookBorrowed(bookId))
            {
                return ResultCode.FAIL;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = Constant.SqlQuery.DELETE_BOOK;

            command.Parameters.AddWithValue("@id", bookId);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }

        public ResultCode TryRequestBook(RequestedBookDTO requestingBook)
        {
            List<BookDTO> books = GetAllBooks();
            List<RequestedBookDTO> requestedBooks = GetAllRequestedBooks();

            foreach(RequestedBookDTO requestedBook in requestedBooks)
            {
                if(requestedBook.Isbn == requestingBook.Isbn)
                {
                    return ResultCode.ALREADY_REQUESTED;
                }
            }

            foreach(BookDTO book in books)
            {
                if(book.Isbn == requestingBook.Isbn)
                {
                    return ResultCode.ALREADY_IN_DATABASE;
                }
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = Constant.SqlQuery.INSERT_REQUESTED_BOOK;

            command.Parameters.AddWithValue("@isbn", requestingBook.Isbn);
            command.Parameters.AddWithValue("@name", requestingBook.Name);
            command.Parameters.AddWithValue("@author", requestingBook.Author);
            command.Parameters.AddWithValue("@price", requestingBook.Price);
            command.Parameters.AddWithValue("@publisher", requestingBook.Publisher);
            command.Parameters.AddWithValue("@published_date", requestingBook.PublishedDate);
            command.Parameters.AddWithValue("@description", requestingBook.Description);

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }
        
        public ResultCode TryAddRequestedBook(RequestedBookDTO requestedBook)
        {
            List<BookDTO> books = GetAllBooks();
            MySqlCommand command;
            
            foreach(BookDTO book in books)
            {
                if(book.Isbn == requestedBook.Isbn)
                {
                    command = DatabaseConnection.getInstance.Conn.CreateCommand();
                    command.CommandText = SqlQuery.INCREASE_BOOK_COUNT;

                    command.Parameters.AddWithValue("@id", book.Id);

                    DatabaseConnection.getInstance.ExecuteCommand(command);
                }
            }

            command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = Constant.SqlQuery.INSERT_BOOK;

            command.Parameters.AddWithValue("@name", requestedBook.Name);
            command.Parameters.AddWithValue("@author", requestedBook.Author);
            command.Parameters.AddWithValue("@publisher", requestedBook.Publisher);
            command.Parameters.AddWithValue("@quantity", 1);
            command.Parameters.AddWithValue("@price", requestedBook.Price);
            command.Parameters.AddWithValue("@published_date", requestedBook.PublishedDate);
            command.Parameters.AddWithValue("@isbn", requestedBook.Isbn);
            command.Parameters.AddWithValue("@description", requestedBook.Description);

            DatabaseConnection.getInstance.ExecuteCommand(command);
            
            command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = Constant.SqlQuery.DELETE_REQUESTED_BOOK;

            command.Parameters.AddWithValue("@isbn", requestedBook.Isbn);

            DatabaseConnection.getInstance.ExecuteCommand(command);



            return ResultCode.SUCCESS;
        }
    }
}