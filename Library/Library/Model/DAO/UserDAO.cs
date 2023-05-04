namespace Library.Model.DAO
{
    public class UserDAO
    {
        private static UserDAO _instance;

        private UserDAO()
        {
            
        }

        public UserDAO getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserDAO();
                }

                return _instance;
            }
        }
        
        
    }
}