namespace Library.Model
{
    public class Administrator: User
    {
        public Administrator()
        {
            
        }

        public Administrator(string id, string password, string name, int age, string phoneNumber, string address) : base(id, password, name,
            age, phoneNumber, address)
        {
            
        }
    }
}