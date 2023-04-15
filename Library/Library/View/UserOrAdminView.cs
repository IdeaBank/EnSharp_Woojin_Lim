namespace Library.View
{
    public class UserOrAdminView
    {
        private UserOrAdminView _instance;

        private UserOrAdminView()
        {
            
        }

        public UserOrAdminView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserOrAdminView();
                }

                return _instance;
            }
        }

        public static void PrintUserOrAdmin(int index)
        {
             
        }
    }
}