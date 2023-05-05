namespace Library.Model.DTO
{
    public class BorrowedBookDTO
    {
        private int bookId;
        private string userId;
        private string bookName;
        private string bookAuthor;
        private string bookPublisher;
        private string borrowedDate;
        private string returnedDate;

        public BorrowedBookDTO(int bookId, string userId, string bookName, string bookAuthor, string bookPublisher, string borrowedDate, string returnedDate)
        {
            this.bookId = bookId;
            this.userId = userId;
            this.bookName = bookName;
            this.bookAuthor = bookAuthor;
            this.bookPublisher = bookPublisher;
            this.borrowedDate = borrowedDate;
            this.returnedDate = returnedDate;
        }

        public int BookId
        {
            get => this.bookId;
        }

        public string UserId
        {
            get => this.userId;
        }

        public string BookName
        {
            get => this.bookName;
        }

        public string BookAuthor
        {
            get => this.bookAuthor;
        }

        public string BookPublisher
        {
            get => this.bookPublisher;
        }

        public string BorrowedDate
        {
            get => this.borrowedDate;
        }

        public string ReturnedDate
        {
            get => this.returnedDate;
        }
    }
}