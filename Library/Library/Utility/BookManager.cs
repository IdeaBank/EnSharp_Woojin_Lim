using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;

namespace Library.Utility
{
    public class BookManager
    {
        private TotalData totalData;
        
        public BookManager(TotalData totalData)
        {
            this.totalData = totalData;
        }

        private KeyValuePair<FailCode, int> GetBookIndex(int bookId)
        {
            for (int i = 0; i < totalData.Books.Count; ++i)
            {
                if (totalData.Books[i].Id == bookId)
                {
                    return new KeyValuePair<FailCode, int>(FailCode.SUCCESS, i);
                }
            }
            
            return new KeyValuePair<FailCode, int>(FailCode.NO_BOOK, -1);
        }
        
        public FailCode AddBook(string name, string author, string publisher, int quantity, int price, string publishedDate, string isbn, string description)
        {
            Book book = new Book(name, author, publisher, quantity, price, publishedDate, isbn, description);

            totalData.AddedBookCount += 1;
            book.Id = totalData.AddedBookCount;
            
            totalData.Books.Add(book);
            
            return FailCode.SUCCESS;
        }

        public List<Book> SearchBook(string name, string author, string publisher)
        {
            List<Book> searchResult = new List<Book>();
            
            foreach (Book book in totalData.Books)
            {
                if ((book.Name.Contains(name) || name.Length == 0) &&
                    (book.Author.Contains(author) || author.Length == 0) &&
                    (book.Publisher.Contains(publisher) || publisher.Length == 0))
                {
                    searchResult.Add(book);
                }
            }

            return searchResult;
        }
        
        public FailCode BorrowBook(int userIndex, int bookId)
        {
            KeyValuePair<FailCode, int> findResult = GetBookIndex(bookId);
            int bookIndex = findResult.Value;

            if (findResult.Key == FailCode.SUCCESS)
            {
                if (totalData.Books[bookIndex].Quantity > 0)
                {
                    BorrowedBook borrowedBook = new BorrowedBook(bookId, DateTime.Now.ToString());
                    
                    totalData.Books[bookIndex].Quantity -= 1;
                    totalData.Users[userIndex].BorrowedBooks.Add(borrowedBook);

                    return FailCode.SUCCESS;
                }

                return FailCode.BOOK_NOT_ENOUGH;
            }

            return FailCode.NO_BOOK;
        }

        public FailCode ReturnBook(int userIndex, int bookId)
        {
            foreach (BorrowedBook borrowedBook in totalData.Users[userIndex].BorrowedBooks)
            {
                if (borrowedBook.BookId == bookId)
                {
                    totalData.Users[userIndex].BorrowedBooks.Remove(borrowedBook);
                    borrowedBook.ReturnedDate = DateTime.Now.ToString();
                    totalData.Users[userIndex].ReturnedBooks.Add(borrowedBook);

                    return FailCode.SUCCESS;
                }
            }
            
            return FailCode.NO_BOOK;
        }

        public FailCode EditBook(int bookId, Book book)
        {
            KeyValuePair<FailCode, int> findResult = GetBookIndex(bookId);
            int bookIndex = findResult.Value;
            
            if (findResult.Key == FailCode.SUCCESS)
            {
                totalData.Books[bookIndex].Name = book.Name;
                totalData.Books[bookIndex].Author = book.Author;
                totalData.Books[bookIndex].Publisher = book.Publisher;
                totalData.Books[bookIndex].Quantity = book.Quantity;
                totalData.Books[bookIndex].Price = book.Price;
                totalData.Books[bookIndex].PublishedDate = book.PublishedDate;
                totalData.Books[bookIndex].Isbn = book.Isbn;
                totalData.Books[bookIndex].Description = book.Description;

                return FailCode.SUCCESS;
            }

            return FailCode.NO_BOOK;
        }

        public FailCode RemoveBook(int bookId)
        {
            KeyValuePair<FailCode, int> findResult = GetBookIndex(bookId);
            int bookIndex = findResult.Value;

            if (findResult.Key == FailCode.SUCCESS)
            {
                totalData.Books.RemoveAt(bookIndex);

                return FailCode.SUCCESS;
            }

            return FailCode.NO_BOOK;
        }
    }
}