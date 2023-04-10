using System;

namespace Library.View.Admin
{
    public class AdminLoginView
    {
        public static void Print()
        {
            Console.Clear();

            Console.WriteLine("ID:".PadRight(18, ' ') + ": ");
            Console.WriteLine("Password: ".PadRight(18, ' ') + ": ");
        }
    }
}