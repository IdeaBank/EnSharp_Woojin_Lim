using System;
using System.Collections.Generic;
using Library.Model;

namespace Library
{
    public class BookManagement
    {
        public bool AddBook(Data data, Book book)
        {
            foreach (Book tempBook in data.books)
            {
                if (tempBook.bookId == book.bookId)
                {
                    return false;
                }
            }
            
            data.books.Add(book);
            return true;
        }

        public List<Book> SearchBook(Data data, string name, string author, string publisher)
        {
            bool matchesName, matchesAuthor, matchesPublisher;
            List<Book> result = new List<Book>();
            
            foreach (Book book in data.books)
            {
                if ((book.name.Contains(name) || name.Length == 0) &&
                    (book.author.Contains(author) || author.Length == 0) &&
                    (book.publisher.Contains(publisher) || publisher.Length == 0))
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public bool DeleteBook(Data data, int bookId)
        {
            foreach(Book book in data.books)
            {
                if (book.bookId == bookId)
                {
                    data.books.Remove(book);
                    return true;
                }
            }

            return false;
        }

        public bool EditBook(Data data, string name, string author, string publisher, int quantity, int price, string publishedDate, string isbn, string description)
        {
            
            return false;
        }
        
        public bool ModifyInformation(Data data)
        {
            return false;
        }

        public List<User> SearchMember(Data data)
        {
            List<User> searchResult = new List<User>();

            return searchResult;
        }

        public bool DeleteMember(Data data, int userNumber)
        {
            foreach(User user in data.users)
            {
                if (user.number == userNumber)
                {
                    data.users.Remove(user);
                    return true;
                }
            }

            Console.WriteLine("해당 번호의 유저가 존재하지 않습니다!");
            return false;
        }
    }
}