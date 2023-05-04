using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.Constant;
using Library.Model.DTO;
using Library.Utility;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

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

            command.Parameters.AddWithValue("@id", bookId);

            DataSet dataSet = DatabaseConnection.getInstance.ExecuteSelection(command, "borrowed_book");

            if (dataSet.Tables["borrowed_book"].Rows.Count == 0)
            {
                return false;
            }

            return true;
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

            return new BorrowedBookDTO(bookId, userId, row["borrowed_date"].ToString(), "");
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
                BorrowedBookDTO borrowedBook = new BorrowedBookDTO(int.Parse(row["book_id"].ToString()), userId, row["borrowed_date"].ToString(), "");
                
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
                BorrowedBookDTO returnedBook = new BorrowedBookDTO(int.Parse(row["book_id"].ToString()), userId, row["borrowed_date"].ToString(), row["returned_date"].ToString());
                
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

        public DataSet SearchBook(string name, string author, string publisher)
        {
            if (name == "" && author == "" && publisher == "")
            {
                return new DataSet();
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.SELECT_BOOK_WITH_SEARCH_STRING;

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@publisher", publisher);

            return DatabaseConnection.getInstance.ExecuteSelection(command, "book");
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
            
            command.CommandText = SqlQuery.INSERT_BORROWED_BOOK;
            
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@book_id", bookId);
            command.Parameters.AddWithValue("@borrowed_date", DateTime.Now.ToString());

            DatabaseConnection.getInstance.ExecuteCommand(command);

            return ResultCode.SUCCESS;
        }

        public ResultCode ReturnBook(string userId, int bookId)
        {
            if (!BookExists(bookId) || GetBorrowedBookInfo(userId, bookId) == null)
            {
                return ResultCode.NO_BOOK;
            }

            MySqlCommand command = DatabaseConnection.getInstance.Conn.CreateCommand();
            command.CommandText = SqlQuery.INCREASE_BOOK_COUNT;

            command.Parameters.AddWithValue("@id", bookId);

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
    }
}