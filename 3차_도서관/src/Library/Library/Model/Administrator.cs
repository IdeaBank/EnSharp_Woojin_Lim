namespace Library.Model
{
    public class Administrator: User
    {
        
        public Administrator()
        {
            
        }

        public Administrator(int number, string id, string password, string name, int age, string phoneNumber, string address) : base(number, id, password, name,
            age, phoneNumber, address)
        {
            
        }
    }
}