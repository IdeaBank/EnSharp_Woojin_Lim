using System;

namespace Library.View.User
{
    public class UserRegisterView
    {
        public static void Print()
        {
            Console.Clear();
            
            string[] instructions =
            {
                "Enter id".PadRight(25, ' ') + ": ",
                "Enter password".PadRight(25, ' ') + ": ",
                "Enter name".PadRight(25, ' ') + ": ",
                "Enter age".PadRight(25, ' ') + ": ",
                "Enter phone number".PadRight(25, ' ') + ": ",
                "User Address".PadRight(25, ' ') + ": ",
            };
            
            Console.WriteLine(new string('#', 50));
            foreach (string instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
            Console.WriteLine(new string('#', 50));
        }
    }
}