using System;
using System.Collections.Generic;

namespace Library
{
    public class User
    {
        protected int number;
        protected string id;
        protected string name;
        protected int age;
        protected string phoneNumber;
        protected string address;

        public User()
        {
            
        }

        public User(int number, string name, int age, string phoneNumber, string address)
        {
            this.number = number;
            this.name = name;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }

        public bool IsAdministrator()
        {
            return false;
        }
        
        public void ModifyMemberInfo(string id, string name, string address)
        {
            this.id = id;
            this.name = name;
            this.address = address;
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
                        data.borrowedBooks.Add(new BorrowedBook(this.number, book.bookId, "asdf"));
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