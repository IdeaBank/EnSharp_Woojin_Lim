using System.Diagnostics;

namespace Library
{
    public class Book
    {
        public int bookId { set; get; }
        public string name { set; get; }
        public string author { set; get; }
        public string publisher { set; get; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string publishedDate { set; get; }
        public string isbn { set; get; }
        public string description { set; get; }

        public Book()
        {
            
        }

        public Book(int bookId, string name, string author, string publisher, int quantity, int price,
            string publishedDate, string isbn, string description)
        {
            this.bookId = bookId;
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.quantity = quantity;
            this.price = price;
            this.publishedDate = publishedDate;
            this.isbn = isbn;
            this.description = description;
        }
    }
}