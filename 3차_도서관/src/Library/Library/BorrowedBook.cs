namespace Library
{
    public class BorrowedBook
    {
        public int userId { get; set; }
        public int bookId { get; set; }
        public string borrowedDate { get; set; }

        public BorrowedBook()
        {
            
        }

        public BorrowedBook(int userId, int bookId, string borrowedDate)
        {
            this.userId = userId;
            this.bookId = bookId;
            this.borrowedDate = borrowedDate;
        }
    }
}