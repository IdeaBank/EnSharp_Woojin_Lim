using System;

namespace Library.View.User
{
    public class UserRegisterView : ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            string[] instructions =
            {
                "ID".PadRight(18, ' ') + ": ",
                "Password".PadRight(18, ' ') + ": ",
                "Name".PadRight(18, ' ') + ": ",
                "Age".PadRight(18, ' ') + ": ",
                "Phone Number".PadRight(18, ' ') + ": ",
                "Address".PadRight(18, ' ') + ": ",
            };

            foreach (string instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
        }
    }
}