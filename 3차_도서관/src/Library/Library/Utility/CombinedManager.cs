namespace Library.Utility
{
    public class CombinedManager
    {
        private BookManager bookManager;
        private UserManager userManager;

        public BookManager BookManager
        {
            get => this.bookManager;
        }

        public UserManager UserManager
        {
            get => this.userManager;
        }
        
        public CombinedManager(BookManager bookManager, UserManager userManager)
        {
            this.bookManager = bookManager;
            this.userManager = userManager;
        }
    }
}