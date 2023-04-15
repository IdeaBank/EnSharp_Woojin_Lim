namespace Library.View.AdminView
{
    public class AdminMenu
    {
        private AdminMenu _instance;

        private AdminMenu()
        {
            
        }

        public AdminMenu getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new AdminMenu();
                }

                return _instance;
            }
        }
    }
}