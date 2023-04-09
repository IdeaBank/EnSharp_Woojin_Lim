using System.Collections.Generic;

namespace Library
{
    public class Data
    {
        public List<User> users { get; set; }
        public List<Book> books { get; set; }
        public List<BorrowedBook> borrowedBooks { get; set; }
    }
}