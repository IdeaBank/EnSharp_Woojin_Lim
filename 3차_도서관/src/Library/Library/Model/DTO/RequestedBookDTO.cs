using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.DTO
{
    public class RequestedBookDTO
    {
        private string isbn;
        private string name;
        private string author;
        private string publisher;
        private int price;
        private string publishedDate;
        private string description;

        public RequestedBookDTO()
        {

        }

        public RequestedBookDTO(string isbn, string name, string author, string publisher, int price, string publishedDate, string description) 
        {
            this.isbn = isbn;
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.publishedDate = DateTime.ParseExact(publishedDate, "yyyymmdd", null).ToString("yyyy-mm-dd");
            this.description = description;
        }

        public string Isbn
        {
            get => this.isbn;
            set => this.isbn = value;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public string Author
        {
            get => this.author; 
            set => this.author = value;
        }

        public string Publisher
        {
            get => this.publisher; 
            set => this.publisher = value;
        }

        public int Price
        {
            get => this.price; 
            set => this.price = value;
        }

        public string PublishedDate
        {
            get => this.publishedDate; 
            set => this.publishedDate = value;
        }

        public string Description
        {
            get => this.description; 
            set => this.description = value;
        }
    }
}
