using Library.Controller;

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

        public CombinedManager(BookManager bookManager, UserManager userManagerx)
        {
            this.bookManager = bookManager;
            this.userManager = userManager;
        }
    }
}