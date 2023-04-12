using System.Collections.Generic;

namespace Library.Model
{
    public class Data
    {
        public List<User> users { get; set; }
        public List<Administrator> admins { get; set; }
        public List<Book> books { get; set; }

        public int userIdCount;
        public int bookIdCount;

        public Data()
        {
            users = new List<User>();
            admins = new List<Administrator>();
            books = new List<Book>();

            userIdCount = 0;
            bookIdCount = 0;
        }
    }
}