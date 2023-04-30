using Library.Constants;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Library.Utility
{
    public class BookManager
    {
        private TotalData totalData;
        private SqlManager sqlManager;
        private DataSet dataSet;

        public BookManager(TotalData totalData, SqlManager sqlManager)
        {
            this.totalData = totalData;
            this.sqlManager = sqlManager;
            this.dataSet = new DataSet();
        }

        public bool IsBookExist(string id)
        {
            dataSet = sqlManager.ExecuteSql("select * from Book where id=" + id, "Book");

            if (dataSet.Tables["Book"].Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        private KeyValuePair<bool, string> IsBookExistInBorrowedBook(string bookId, string userId)
        {
            dataSet = sqlManager.ExecuteSql("select * from Borrowed_Book where book_id=" + bookId + " and user_id=\'" + userId + "\'", "Borrowed_Book");
            
            if (dataSet.Tables["Borrowed_Book"].Rows.Count == 0)
            {
                return new KeyValuePair<bool, string>(false, "");
            }

            return new KeyValuePair<bool, string>(true, dataSet.Tables["Borrowed_Book"].Rows[0]["borrowed_date"].ToString());
        }
        
        private bool IsBookAvailable(string id)
        {
            dataSet = sqlManager.ExecuteSql("select * from Book where id=" + id, "Book");

            if (int.Parse(dataSet.Tables["Book"].Rows[0]["quantity"].ToString()) == 0)
            {
                return false;
            }

            return true;
        } 

        public void AddBook(string name, string author, string publisher, string quantity, string price, string publishedDate, string isbn, string description)
        {
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "INSERT INTO Book(name, author, publisher, quantity, price, published_date, isbn, description) VALUES(@name, @author, @publisher, @quantity, @price, @published_date, @isbn, @description)";
            comm.Parameters.AddWithValue("@name", name);
            comm.Parameters.AddWithValue("@author", author);
            comm.Parameters.AddWithValue("@publisher", publisher);
            comm.Parameters.AddWithValue("@quantity", quantity);
            comm.Parameters.AddWithValue("@price", price);
            comm.Parameters.AddWithValue("@published_date", publishedDate);
            comm.Parameters.AddWithValue("@isbn", isbn);
            comm.Parameters.AddWithValue("@description", description);
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();
        }

        public DataSet SearchBook(string name, string author, string publisher)
        {
            // 책을 순회하며
            dataSet = sqlManager.ExecuteSql(
                "select * from Book where name like \'%" + name + "%\' and author like \'%" + author +
                "%\' and publisher like \'%" + publisher + "%\'", "Book");

            // 책 검색 결과 반환
            return dataSet;
        }
        
        public ResultCode BorrowBook(string userId, string bookId)
        {
            if (!IsBookExist(bookId))
            {
                return ResultCode.NO_BOOK;
            }

            if (!IsBookAvailable(bookId))
            {
                return ResultCode.BOOK_NOT_ENOUGH;
            }
            
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "update Book set quantity = quantity - 1 where id=" + bookId;
            comm.ExecuteNonQuery();
            
            comm.CommandText = "INSERT INTO Borrowed_Book(user_id, book_id, borrowed_date) VALUES(@user_id, @book_id, @borrowed_date)";
            comm.Parameters.AddWithValue("@user_id", userId);
            comm.Parameters.AddWithValue("@book_id", bookId);
            comm.Parameters.AddWithValue("@borrowed_date", DateTime.Now.ToString());
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();

            return ResultCode.SUCCESS;
        }

        public ResultCode ReturnBook(string bookId, string userId)
        {
            KeyValuePair<bool, string> borrowedBookInfo = IsBookExistInBorrowedBook(bookId, userId);
            if (!borrowedBookInfo.Key)
            {
                return ResultCode.NO_BOOK;
            }

            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "update Book set quantity = quantity + 1 where id=" + bookId;
            comm.ExecuteNonQuery();
            
            comm.CommandText = "INSERT INTO Returned_Book(user_id, book_id, borrowed_date, returned_date) VALUES(@user_id, @book_id, @borrowed_date, @returned_date)";
            comm.Parameters.AddWithValue("@user_id", userId);
            comm.Parameters.AddWithValue("@book_id", bookId);
            comm.Parameters.AddWithValue("@borrowed_date", borrowedBookInfo.Value);
            comm.Parameters.AddWithValue("@returned_date", DateTime.Now.ToString());
            comm.ExecuteNonQuery();

            comm.CommandText = "DELETE from Borrowed_Book where book_id=" + bookId + " and user_id=\'" + userId + "\'";
            comm.ExecuteNonQuery();

            sqlManager.Conn.Close();
            
            return ResultCode.SUCCESS;
        }

        public void EditBook(string bookId, string name, string author, string publisher, string quantity, string price, string publishedDate, string isbn, string description)
        {
            string updateQuery = "set ";

            updateQuery += "name=\'" + name + "\', ";
            updateQuery += "author=\'" + author + "\', ";
            updateQuery += "publisher=\'" + publisher + "\', ";
            updateQuery += "quantity=" + quantity + ", ";
            updateQuery += "price=" + price + ", ";
            updateQuery += "published_date=\'" + publishedDate + "\', ";
            updateQuery += "isbn=\'" + isbn + "\', ";
            updateQuery += "description=\'" + description + "\' ";
            
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "update Book " + updateQuery + " where id=" + bookId;
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();
        }

        private bool IsBookBorrowed(string bookId)
        {
            dataSet = sqlManager.ExecuteSql("select * from Borrowed_Book where book_id=" + bookId, "Borrowed_Book");

            if (dataSet.Tables["Borrowed_Book"].Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        public ResultCode RemoveBook(string bookId)
        {
            if (IsBookBorrowed(bookId))
            {
                return ResultCode.FAIL;
            }
            
            sqlManager.Conn.Open();
            
            MySqlCommand comm = sqlManager.Conn.CreateCommand();
            comm.CommandText = "delete from Book where id=" + bookId;
            comm.ExecuteNonQuery();
            
            sqlManager.Conn.Close();
            
            // 책을 못 찾았다면 책이 없다는 결과 반환
            return ResultCode.NO_BOOK;
        }

        public DataSet GetBorrowedBooks(string userId)
        {
            dataSet.Clear();
            dataSet = sqlManager.ExecuteSql("select * from Borrowed_Book where user_id=\'" + userId + "\'", "Borrowed_Book");

            return dataSet;
        }
        
        public DataSet GetReturnedBooks(string userId)
        {
            dataSet = sqlManager.ExecuteSql("select * from Returned_Book where user_id=\'" + userId + "\'", "Returned_Book");

            return dataSet;
        }

        public DataSet GetBook(string bookId)
        {
            dataSet = sqlManager.ExecuteSql("select * from Book where id=" + bookId, "Book");

            return dataSet;
        }
    }
}