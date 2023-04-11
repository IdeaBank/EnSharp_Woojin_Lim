namespace Library.Model
{
    public class Administrator: User
    {
        public Administrator()
        {
            
        }

        public Administrator(int userNumber, string id, string password, string name, int age, string phoneNumber, string address) : base(userNumber, id, password, name,
            age, phoneNumber, address)
        {
            
        }
    }
}