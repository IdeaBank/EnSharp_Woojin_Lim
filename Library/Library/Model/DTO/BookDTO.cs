namespace Library.Model.DTO
{
    public class BookDTO
    {
        private int id;
        private string name;
        private string author;
        private string publisher;
        private int quantity;
        private int price;
        private string publishedDate;
        private string isbn;
        private string description;
        
        public BookDTO()
        {

        }

        public BookDTO(int id, string name, string author, string publisher, int quantity, int price, string publishedDate,
            string isbn, string description)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.quantity = quantity;
            this.price = price;
            this.publishedDate = publishedDate;
            this.isbn = isbn;
            this.description = description;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Author
        {
            get => author;
            set => author = value;
        }

        public string Publisher
        {
            get => publisher;
            set => publisher = value;
        }

        public int Quantity
        {
            get => quantity;
            set => quantity = value;
        }

        public int Price
        {
            get => price;
            set => price = value;
        }

        public string PublishedDate
        {
            get => publishedDate;
            set => publishedDate = value;
        }

        public string Isbn
        {
            get => isbn;
            set => isbn = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }
    }
}