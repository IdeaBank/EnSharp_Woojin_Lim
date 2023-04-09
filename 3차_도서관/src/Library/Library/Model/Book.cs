using System.Diagnostics;

namespace Library.Model
{
    public class Book
    {
        public int bookId { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string publishedDate { get; set; }
        public string isbn { get; set; }
        public string description { get; set; }

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