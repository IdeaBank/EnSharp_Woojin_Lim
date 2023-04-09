namespace Library.Constants
{
    public class VIEW
    {
        // ESC ON MAIN_MENU_VIEW
        public const int EXIT_VIEW = 0;
        
        // SELECT BETWEEN USER AND ADMIN
        public const int MAIN_MENU_VIEW = 1;
     
        // USER START
        public const int USER_LOGIN_VIEW = 10;
        public const int USER_REGISTER_VIEW = 11;
        
        // USER / ADMIN VIEW
        public const int USER_MENU_VIEW = 20;
        public const int ADMIN_MENU_VIEW = 21;
        
        // USER MENU LIST
        public const int USER_SEARCH_BOOK_VIEW = 200;
        public const int USER_BORROW_BOOK_VIEW = 201;
        public const int USER_BORROWED_BOOK_LIST_VIEW = 202;
        public const int USER_RETURN_BOOK_VIEW = 203;
        public const int USER_RETURNED_BOOK_LIST_VIEW = 204;
        public const int USER_MODIFY_MEMBER_VIEW = 205;
        public const int USER_WITHDRAW_VIEW = 206;
        
        // ADMIN MENU LIST
        public const int ADMIN_SEARCH_BOOK_VIEW = 210;
        public const int ADMIN_ADD_BOOK_VIEW = 211;
        public const int ADMIN_DELETE_BOOK_VIEW = 212;
        public const int ADMIN_MODIFY_BOOK_VIEW = 213;
        public const int ADMIN_DELETE_MEMBER_VIEW = 214;
        public const int ADMIN_ALL_BORROWED_BOOK_LIST_VIEW = 215;
    }
}