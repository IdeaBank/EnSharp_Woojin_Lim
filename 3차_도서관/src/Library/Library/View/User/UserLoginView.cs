using System;

namespace Library.View.User
{
    public class UserLoginView: ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.WriteLine("ID:".PadRight(18, ' ') + ": ");
            Console.WriteLine("Password: ".PadRight(18, ' ') + ": ");
        }
    }
}