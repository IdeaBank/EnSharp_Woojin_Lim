using System.Collections.Generic;

namespace Library.Model
{
    public class User
    {
        private int number;
        private string id;
        private string password;
        private string name;
        private int birthYear;
        private string phoneNumber;
        private string address;

        private List<BorrowedBook> borrowedBooks;
        private List<BorrowedBook> returnedBooks;

        public int Number
        {
            get => this.number;

            set => this.number = value;
        }

        public string Id
        {
            get => this.id;
            set => this.id = value;
        }

        public string Password
        {
            get => this.password;
            set => this.password = value;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public int BirthYear
        {
            get => this.birthYear;
            set => this.birthYear = value;
        }

        public string PhoneNumber
        {
            get => this.phoneNumber;
            set => this.phoneNumber = value;
        }

        public string Address
        {
            get => this.address;
            set => this.address = value;
        }

        public List<BorrowedBook> BorrowedBooks
        {
            get => this.borrowedBooks;
            set => this.borrowedBooks = value;
        }

        public List<BorrowedBook> ReturnedBooks
        {
            get => this.returnedBooks;
            set => this.returnedBooks = value;
        }

        public User()
        {
            this.borrowedBooks = new List<BorrowedBook>();
            this.returnedBooks = new List<BorrowedBook>();
        }
    }
}