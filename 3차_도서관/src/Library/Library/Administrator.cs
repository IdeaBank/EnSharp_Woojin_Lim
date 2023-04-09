namespace Library
{
    public class Administrator: User
    {
        public Administrator()
        {
            
        }

        public Administrator(int number, string name, int age, string phoneNumber, string address) : base(number, name,
            age, phoneNumber, address)
        {
            
        }
    }
}