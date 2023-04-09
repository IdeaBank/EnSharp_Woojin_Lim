using System.Collections.Generic;
using Library.Model;

namespace Library.Model
{
    public class Data
    {
        public List<User> users { get; set; }
        public List<Administrator> admins { get; set; }
        public List<Book> books { get; set; }

        public Data()
        {
            users = new List<User>();
            admins = new List<Administrator>();
            books = new List<Book>();
        }
    }
}