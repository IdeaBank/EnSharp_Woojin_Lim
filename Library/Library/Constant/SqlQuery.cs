namespace Library.Constant
{
    public class SqlQuery
    {
        public const string SELECT_BOOK_WITH_ID = "SELECT * from book WHERE id = @id";
        public const string SELECT_BOOK_WITH_SEARCH_STRING =
            "SELECT * FROM book WHERE name LIKE '%@name%' AND author LIKE '%@author%' AND publisher LIKE '%@publisher%'";

        public const string SELECT_BORROWED_BOOK_WITH_BOOK_ID = "SELECT * FROM borrowed_Book WHERE book_id=@book_id";
        public const string SELECT_BORROWED_BOOK_WITH_BOOK_AND_USER_ID = "SELECT * FROM borrowed_Book WHERE book_id=@book_id AND user_id=@user_id";

        public const string SELECT_BORROWED_BOOK_WITH_USER_ID = "SELECT * FROM borrowed_book WHERE user_id=@user_id";
        public const string SELECT_RETURNED_BOOK_WITH_USER_ID = "SELECT * FROM returned_book WHERE user_id=@user_id";
        
        public const string INSERT_BOOK =
            "INSERT INTO book(name, author, publisher, quantity, price, published_date, isbn, description) VALUES(@name, @author, @publisher, @quantity, @price, @published_date, @isbn, @description)";
        
        
        public const string INSERT_BORROWED_BOOK = "INSERT INTO borrowed_Book(user_id, book_id, borrowed_date) VALUES(@user_id, @book_id, @borrowed_date)";

        public const string DECREASE_BOOK_COUNT = "UPDATE book SET quantity = quantity - 1 WHERE id = @id";
        public const string INCREASE_BOOK_COUNT = "UPDATE book SET quantity = quantity + 1 WHERE id = @id";

        public const string EDIT_BOOK =
            "UPDATE book SET name=@name, author=@author, publisher=@publisher, quantity=@quantity, price=@price, published_date=@published_date, isbn=@isbn, description=@description WHERE id=@id";

        public const string DELETE_BOOK = "DELETE FROM book WHERE id=@id";
    }
}