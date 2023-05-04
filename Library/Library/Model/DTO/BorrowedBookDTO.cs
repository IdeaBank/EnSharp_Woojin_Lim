namespace Library.Model.DTO
{
    public class BorrowedBookDTO
    {
        private int bookId;
        private string userId;
        private string borrowedDate;
        private string returnedDate;

        public BorrowedBookDTO(int bookId, string userId, string borrowedDate, string returnedDate)
        {
            this.bookId = bookId;
            this.userId = userId;
            this.borrowedDate = borrowedDate;
            this.returnedDate = returnedDate;
        }

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
    }
}