namespace Library.Model
{
    public class BorrowedBook
    {
        public int bookId { get; set; }
        public string borrowedDate { get; set; }

        public BorrowedBook()
        {
            
        }

        public BorrowedBook(int bookId, string borrowedDate)
        {
            this.bookId = bookId;
            this.borrowedDate = borrowedDate;
        }
    }
}