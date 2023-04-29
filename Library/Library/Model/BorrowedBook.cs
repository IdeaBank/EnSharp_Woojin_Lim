namespace Library.Model
{
    public class BorrowedBook
    {
        private int bookId;
        private string borrowedDate;
        private string returnedDate;

        public int BookId
        {
            get => this.bookId;
        }

        public string BorrowedDate
        {
            get => this.borrowedDate;
            set => this.borrowedDate = value;
        }

        public string ReturnedDate
        {
            get => this.returnedDate;
            set => this.returnedDate = value;
        }

        public BorrowedBook(int bookId, string borrowedDate)
        {
            this.bookId = bookId;
            this.borrowedDate = borrowedDate;
            this.returnedDate = borrowedDate;
        }
    }
}