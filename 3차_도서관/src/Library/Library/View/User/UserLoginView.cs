using System;

namespace Library.View.User
{
    public class UserLoginView: ViewFrame
    {
        public static void Print()
        {
            Console.WriteLine("ID:".PadRight(19, ' ') + ": ");
            Console.WriteLine("Password: ".PadRight(19, ' ') + ": ");
        }
    }
}