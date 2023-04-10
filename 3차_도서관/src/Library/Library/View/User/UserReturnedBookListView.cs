using Library.Model;

namespace Library.View.User
{
    public class UserReturnedBookListView: ViewFrame
    {
        public static void Print(Model.User user)
        {
            foreach (BorrowedBook returnedBook in user.returnedBooks)
            {
                
            }
        }
    }
}