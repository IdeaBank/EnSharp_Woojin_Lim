namespace Library.Model.DTO
{
    public class UserDTO
    {
        private int number;
        private string id;
        private string password;
        private string name;
        private int birthYear;
        private string phoneNumber;
        private string address;

        public UserDTO(string id, string password, string name, int birthYear, string phoneNumber, string address)
        {
            this.id = id;
            this.password = password;
            this.name = name;
            this.birthYear = birthYear;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }
        
        public int Number
        {
            get => this.number;

            set => this.number = value;
        }

        public string Id
        {
            get => this.id;
            set => this.id = value;
        }

        public string Password
        {
            get => this.password;
            set => this.password = value;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public int BirthYear
        {
            get => this.birthYear;
            set => this.birthYear = value;
        }

        public string PhoneNumber
        {
            get => this.phoneNumber;
            set => this.phoneNumber = value;
        }

        public string Address
        {
            get => this.address;
            set => this.address = value;
        }
    }
}