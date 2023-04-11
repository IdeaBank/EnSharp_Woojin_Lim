namespace Library
{
    public class DataManager
    {
        public BookManager bookManager;
        public UserManager userManager;
        
        public DataManager()
        {
            bookManager = new BookManager();
            userManager = new UserManager();
        }
        
    }
}