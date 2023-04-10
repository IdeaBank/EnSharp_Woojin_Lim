using System;

namespace Library.View.Admin
{
    public class AdminAddBookView
    {
        public static void Print()
        {
            Console.Clear();
            
            string[] instructions =
            {
                "Enter name".PadRight(18, ' ') + ": ",
                "Enter author".PadRight(18, ' ') + ": ",
                "Enter publisher".PadRight(18, ' ') + ": ",
                "Enter quantity".PadRight(18, ' ') + ": ",
                "Enter price".PadRight(18, ' ') + ": ",
                "Enter published date".PadRight(18, ' ') + ": ",
                "Enter ISBN".PadRight(18, ' ') + ": ",
                "Enter description".PadRight(18, ' ') + ": "
            };
            
            foreach (string instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
        }
    }
}