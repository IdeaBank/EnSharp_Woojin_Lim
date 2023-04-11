using System;
using System.Collections.Generic;

namespace Library.Model
{
    public class User
    {
        public int userNumber { get; set; }
        public string id  { get; set; }
        public string password { get; set; }
        public string name  { get; set; }
        public int age  { get; set; }
        public string phoneNumber  { get; set; }
        public string address { get; set; }
        public List<BorrowedBook> borrowedBooks  { get; set; }
        public List<BorrowedBook> returnedBooks { get; set; }

        public User()
        {
            this.borrowedBooks = new List<BorrowedBook>();
            this.returnedBooks = new List<BorrowedBook>();
        }

        public User(int userNumber, string id, string password, string name, int age, string phoneNumber, string address)
        {
            this.userNumber = userNumber;
            this.id = id;
            this.password = password;
            this.name = name;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.borrowedBooks = new List<BorrowedBook>();
            this.returnedBooks = new List<BorrowedBook>();
        }
    }
}