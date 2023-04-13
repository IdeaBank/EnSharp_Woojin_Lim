using System;

namespace Library.Model
{
    public class BorrowedBook: Book
    {
        public string borrowedDate { get; set; }
        public string returnedDate { get; set; }

        public BorrowedBook(int bookId, string borrowedDate)
        {
            this.bookId = bookId;
            this.borrowedDate = borrowedDate;
        }

        public BorrowedBook(string name, string author, string publisher, int quantity, int price, string publishedDate, string isbn, string description): base( name,  author,  publisher,  quantity,  price,
             publishedDate,  isbn,  description)
        {
        }
    }
}