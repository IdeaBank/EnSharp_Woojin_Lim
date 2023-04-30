using System.Collections.Generic;

namespace Library.Model
{
    public class TotalData
    {
        private List<User> users;
        private List<User> administrators;
        private List<Book> books;

        public List<User> Users
        {
            get => this.users;
            set => this.users = value;
        }

        public List<User> Administrators
        {
            get => this.administrators;
            set => this.administrators = value;
        }

        public List<Book> Books
        {
            get => this.books;
            set => this.books = value;
        }

        public TotalData()
        {
            users = new List<User>();
            administrators = new List<User>();
            books = new List<Book>();
        }
    }
}