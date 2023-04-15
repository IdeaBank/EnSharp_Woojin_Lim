namespace Library.View.UserView
{
    public class UserMenu
    {
        private UserMenu _instance;

        private UserMenu()
        {
            
        }

        public UserMenu getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserMenu();
                }

                return _instance;
            }
        }
    }
}