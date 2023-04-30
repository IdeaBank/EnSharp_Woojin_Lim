namespace Library.Utility
{
    public class CombinedManager
    {
        private BookManager bookManager;
        private UserManager userManager;
        private SqlManager sqlManager;

        public BookManager BookManager
        {
            get => this.bookManager;
        }

        public UserManager UserManager
        {
            get => this.userManager;
        }

        public SqlManager SqlManager
        {
            get => this.sqlManager;
        }

        
        public CombinedManager(BookManager bookManager, UserManager userManager, SqlManager sqlManager)
        {
            this.bookManager = bookManager;
            this.userManager = userManager;
            this.sqlManager = sqlManager;
        }
    }
}