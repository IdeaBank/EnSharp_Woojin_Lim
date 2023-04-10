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
                "Enter name".PadRight(23, ' ') + ": ",
                "Enter author".PadRight(23, ' ') + ": ",
                "Enter publisher".PadRight(23, ' ') + ": ",
                "Enter quantity".PadRight(23, ' ') + ": ",
                "Enter price".PadRight(23, ' ') + ": ",
                "Enter published date".PadRight(23, ' ') + ": ",
                "Enter ISBN".PadRight(23, ' ') + ": ",
                "Enter description".PadRight(23, ' ') + ": "
            };
            
            foreach (string instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
        }
    }
}