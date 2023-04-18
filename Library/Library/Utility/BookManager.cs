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

        private KeyValuePair<ResultCode, int> GetBookIndex(int bookId)
        {
            // 책 데이터를 읽고
            for (int i = 0; i < totalData.Books.Count; ++i)
            {
                // 아이디를 발견했으면
                if (totalData.Books[i].Id == bookId)
                {
                    // 성공 여부와 인덱스 반환
                    return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, i);
                }
            }
            
            // 실패 여부 반환
            return new KeyValuePair<ResultCode, int>(ResultCode.NO_BOOK, -1);
        }
        
        public ResultCode AddBook(string name, string author, string publisher, int quantity, int price, string publishedDate, string isbn, string description)
        {
            // 새로운 책 생성
            Book book = new Book(name, author, publisher, quantity, price, publishedDate, isbn, description);

            // 책 저장한 개수를 1 증가시킴
            totalData.AddedBookCount += 1;
            
            // 아이디에 해당 개수를 저장
            book.Id = totalData.AddedBookCount;
            
            // 총 데이터에 책을 넣음
            totalData.Books.Add(book);
            
            return ResultCode.SUCCESS;
        }

        public List<Book> SearchBook(string name, string author, string publisher)
        {
            // 책 검색 결과를 저장하기 위한 리스트 선언
            List<Book> searchResult = new List<Book>();
            
            // 책을 순회하며
            foreach (Book book in totalData.Books)
            {
                // 이름, 저자, 출판사에 해당하는 책을 리스트에 넣음
                if ((book.Name.Contains(name) || name.Length == 0) &&
                    (book.Author.Contains(author) || author.Length == 0) &&
                    (book.Publisher.Contains(publisher) || publisher.Length == 0))
                {
                    searchResult.Add(book);
                }
            }

            // 책 검색 결과 반환
            return searchResult;
        }
        
        public ResultCode BorrowBook(int userIndex, int bookId)
        {
            // 아이디에 해당하는 책을 찾고 결과과 인덱스 값을 저장
            KeyValuePair<ResultCode, int> findResult = GetBookIndex(bookId);
            int bookIndex = findResult.Value;

            // 찾았으면
            if (findResult.Key == ResultCode.SUCCESS)
            {
                // 해당 책이 1권 이상 있다면
                if (totalData.Books[bookIndex].Quantity > 0)
                {
                    // 책을 대여
                    BorrowedBook borrowedBook = new BorrowedBook(bookId, DateTime.Now.ToString());
                    
                    // 책의 개수를 1개 감소시키고 해당 유저의 빌린 책 리스트에 추가
                    totalData.Books[bookIndex].Quantity -= 1;
                    totalData.Users[userIndex].BorrowedBooks.Add(borrowedBook);

                    // 성공했다는 결과 반환
                    return ResultCode.SUCCESS;
                }

                // 책이 1권 미만이라면 책이 부족하다는 결과 반환
                return ResultCode.BOOK_NOT_ENOUGH;
            }

            // 책을 찾지 못했다면 책이 없다는 결과 반환
            return ResultCode.NO_BOOK;
        }

        public ResultCode ReturnBook(int userIndex, int bookId)
        {
            // 빌린 책 리스트를 순회
            foreach (BorrowedBook borrowedBook in totalData.Users[userIndex].BorrowedBooks)
            {
                // 빌린 책 리스트에서 해당 책 아이디와 같은 책을 찾았으면
                if (borrowedBook.BookId == bookId)
                {
                    // 빌린 책 리스트에서 삭제하고 반납한 책 리스트에 추가
                    totalData.Users[userIndex].BorrowedBooks.Remove(borrowedBook);
                    borrowedBook.ReturnedDate = DateTime.Now.ToString();
                    totalData.Users[userIndex].ReturnedBooks.Add(borrowedBook);

                    // 성공했다는 결과 반환
                    return ResultCode.SUCCESS;
                }
            }
            
            // 책을 못 찾았다면 책이 없다는 결과 반환
            return ResultCode.NO_BOOK;
        }

        public ResultCode EditBook(int bookId, Book book)
        {
            // 아이디에 해당하는 책을 찾고 결과과 인덱스 값을 저장
            KeyValuePair<ResultCode, int> findResult = GetBookIndex(bookId);
            int bookIndex = findResult.Value;
            
            // 책을 찾는데 성공했으면 책을 수정
            if (findResult.Key == ResultCode.SUCCESS)
            {
                totalData.Books[bookIndex].Name = book.Name;
                totalData.Books[bookIndex].Author = book.Author;
                totalData.Books[bookIndex].Publisher = book.Publisher;
                totalData.Books[bookIndex].Quantity = book.Quantity;
                totalData.Books[bookIndex].Price = book.Price;
                totalData.Books[bookIndex].PublishedDate = book.PublishedDate;
                totalData.Books[bookIndex].Isbn = book.Isbn;
                totalData.Books[bookIndex].Description = book.Description;

                // 성공했다는 결과 반환
                return ResultCode.SUCCESS;
            }

            // 책을 못 찾았다면 책이 없다는 결과 반환
            return ResultCode.NO_BOOK;
        }

        public ResultCode RemoveBook(int bookId)
        {
            // 아이디에 해당하는 책을 찾고 결과과 인덱스 값을 저장
            KeyValuePair<ResultCode, int> findResult = GetBookIndex(bookId);
            int bookIndex = findResult.Value;

            // 책을 찾는데 성공했다면
            if (findResult.Key == ResultCode.SUCCESS)
            {
                // 해당 책을 삭제하고 성공했다는 결과 반환
                totalData.Books.RemoveAt(bookIndex);

                return ResultCode.SUCCESS;
            }

            // 책을 못 찾았다면 책이 없다는 결과 반환
            return ResultCode.NO_BOOK;
        }
    }
}