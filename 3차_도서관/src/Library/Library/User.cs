using System;
using System.Collections.Generic;

namespace Library
{
    public class User
    {
        public int number { get; set; }
        public string id  { get; set; }
        public string password { get; set; }
        public string name  { get; set; }
        public int age  { get; set; }
        public string phoneNumber  { get; set; }
        public string address { get; set; }
        public List<BorrowedBook> borrowedBooks  { get; set; }

        public User()
        {
            this.borrowedBooks = new List<BorrowedBook>();
        }

        public User(int number, string name, int age, string phoneNumber, string address)
        {
            this.number = number;
            this.name = name;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.borrowedBooks = new List<BorrowedBook>();
        }

        public void ModifyMemberInfo(string id, string name, string address)
        {
            if (id != "")
            {
                this.id = id;
            }

            if (name != "")
            {
                this.name = name;
            }

            if (address != "")
            {
                this.address = address;
            }
        }

        public bool BorrowBook(Data data, int bookId)
        {
            foreach (Book book in data.books)
            {
                if (book.bookId == bookId)
                {
                    if (book.quantity > 0)
                    {
                        book.quantity -= 1;
                        borrowedBooks.Add(new BorrowedBook(book.bookId, "asdf"));
                        return true;
                    }

                    Console.WriteLine("책 수량이 부족합니다.");
                }

                else
                {
                    Console.WriteLine("존재하지 않는 책입니다.");
                }
            }

            return false;
        }
    }
}