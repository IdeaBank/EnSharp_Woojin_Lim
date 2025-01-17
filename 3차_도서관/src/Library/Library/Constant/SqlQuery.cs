namespace Library.Constant
{
    public class SqlQuery
    {

        public const string SELECT_BOOK_WITH_ID = "SELECT * FROM book WHERE id = @id";
        public const string SELECT_USER_WITH_ID = "SELECT * FROM user WHERE id = @id";
        public const string SELECT_ALL_BOOK = "SELECT * FROM book";
        public const string SELECT_ALL_USER = "SELECT * FROM user";
        public const string SELECT_ALL_LOG = "SELECT * FROM log_data";
        public const string SELECT_ALL_REQUESTED_BOOK = "SELECT * FROM requested_book";
        public const string SELECT_ADMINISTRATOR_WITH_ID = "SELECT * FROM administrator WHERE ID = @id";

        public const string SELECT_BOOK_WITH_SEARCH_STRING =
            "SELECT * FROM book WHERE name LIKE @name AND author LIKE @author AND publisher LIKE @publisher";

        public const string SELECT_USER_WITH_SEARCH_STRING =
            "SELECT * FROM user WHERE name LIKE @name AND id LIKE @id AND address LIKE @address";

        public const string SELECT_BORROWED_BOOK_WITH_BOOK_ID = "SELECT * FROM borrowed_Book WHERE book_id = @book_id";

        public const string SELECT_BORROWED_BOOK_WITH_BOOK_AND_USER_ID =
            "SELECT * FROM borrowed_Book WHERE book_id = @book_id AND user_id = @user_id";

        public const string SELECT_BORROWED_BOOK_WITH_USER_ID = "SELECT * FROM borrowed_book WHERE user_id = @user_id";
        public const string SELECT_RETURNED_BOOK_WITH_USER_ID = "SELECT * FROM returned_book WHERE user_id = @user_id";

        public const string INSERT_BOOK =
            "INSERT INTO book(name, author, publisher, quantity, price, published_date, isbn, description) VALUES(@name, @author, @publisher, @quantity, @price, @published_date, @isbn, @description)";

        public const string INSERT_USER =
            "INSERT INTO user(id, password, name, birth_year, phone_number, address) VALUES(@id, @password, @name, @birth_year, @phone_number, @address)";

        public const string INSERT_BORROWED_BOOK =
            "INSERT INTO borrowed_Book(user_id, book_id, borrowed_date, returned_date) VALUES(@user_id, @book_id, @borrowed_date, @returned_date)";

        public const string INSERT_RETURNED_BOOK =
            "INSERT INTO returned_book(user_id, book_id, borrowed_date, returned_date) VALUES(@user_id, @book_id, @borrowed_date, @returned_date)";

        public const string INSERT_REQUESTED_BOOK =
            "INSERT INTO requested_book(isbn, name, author, price, publisher, published_date, description) VALUES(@isbn, @name, @author, @price, @publisher, @published_date, @description)";

        public const string INSERT_LOG =
            "INSERT INTO log_data(log_time, user_name, log_contents, log_action) VALUES(@log_time, @user_name, @log_contents, @log_action)";
        
        public const string DECREASE_BOOK_COUNT = "UPDATE book SET quantity = quantity - 1 WHERE id = @id";
        public const string INCREASE_BOOK_COUNT = "UPDATE book SET quantity = quantity + 1 WHERE id = @id";

        public const string EDIT_BOOK =
            "UPDATE book SET name = @name, author = @author, publisher = @publisher, quantity = @quantity, price = @price, published_date = @published_date, isbn = @isbn, description = @description WHERE id = @id";

        public const string EDIT_USER =
            "UPDATE user SET password = @password, name = @name, birth_year = @birth_year, phone_number = @phone_number, address = @address WHERE id = @id";

        public const string DELETE_BORROWED_BOOK = "DELETE FROM borrowed_book WHERE book_id = @book_id";
        public const string DELETE_BOOK = "DELETE FROM book WHERE id = @id";
        public const string DELETE_USER = "DELETE FROM user WHERE id = @id";
        public const string DELETE_REQUESTED_BOOK = "DELETE FROM requested_book WHERE isbn = @isbn";
        public const string DELETE_LOG = "DELETE FROM log_data WHERE log_id = @log_id";
        public const string DELETE_ALL_LOG = "DELETE FROM log_data WHERE 1=1";
    }
}