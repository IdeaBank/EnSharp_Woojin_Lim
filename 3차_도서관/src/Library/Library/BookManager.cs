using System;
using System.Collections.Generic;
using Library.Model;

namespace Library
{
    public class BookManager
    {
        public bool AddBook(Data data, Book book)
        {
            book.bookId = data.bookIdCount;
            
            foreach (Book tempBook in data.books)
            {
                if (tempBook.bookId == book.bookId)
                {
                    return false;
                }
            }

            data.books.Add(book);
            data.bookIdCount = data.bookIdCount + 1;
            
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

        public KeyValuePair<bool, bool> BorrowBook(Data data, int bookID)
        {
            // Boo with bookID exists / Have enough book to lend
            bool bookExists = false;
            bool isBookAvailable = false;
            
            foreach (Book book in data.books)
            {
                if (book.bookId == bookID)
                {
                    bookExists = true;
                    if (book.quantity > 0)
                    {
                        book.quantity = book.quantity - 1;
                        isBookAvailable = true;
                    }
                    break;
                }
            }

            return new KeyValuePair<bool, bool>(bookExists, isBookAvailable);
        }

        public bool ReturnBook(Data data, int bookID)
        {
            foreach (Book book in data.books)
            {
                if (book.bookId == bookID)
                {
                    book.quantity = book.quantity + 1;
                    return true;
                }
            }

            return false;
        }

        public bool BookExists(Data data, int bookID)
        {
            foreach (Book book in data.books)
            {
                if (book.bookId == bookID)
                {
                    return true;
                }
            }

            return false;
        }

        public Book GetBook(Data data, int bookID)
        {
            foreach (Book book in data.books)
            {
                if (book.bookId == bookID)
                {
                    return book;
                }
            }

            return null;
        }
        
        public bool EditBook(Data data, Book book, int bookId)
        {
            foreach (Book bookInData in data.books)
            {
                if (bookInData.bookId == bookId)
                {
                    bookInData.name = book.name;
                    bookInData.author = book.author;
                    bookInData.publisher = book.publisher;
                    bookInData.quantity = book.quantity;
                    bookInData.price = book.price;
                    bookInData.publishedDate = book.publishedDate;
                    bookInData.isbn = book.isbn;
                    bookInData.description = book.description;
                    
                    return true;
                }
            }
            
            return false;
        }

        public bool RemoveBook(Data data, int bookID)
        {
            foreach (Book book in data.books)
            {
                if (book.bookId == bookID)
                {
                    data.books.Remove(book);
                    return true;
                }
            }

            return false;
        }

        public bool DeleteMember(Data data, int userNumber)
        {
            foreach(User user in data.users)
            {
                if (user.userNumber == userNumber)
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