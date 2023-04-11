namespace Library
{
    public class DataManager
    {
        private BookManager bookManager;
        private UserManager userManager;
        
        public DataManager()
        {
            bookManager = new BookManager();
            userManager = new UserManager();
        }
        
    }
}