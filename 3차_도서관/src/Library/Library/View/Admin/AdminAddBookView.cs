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
                "Enter name".PadRight(25, ' ') + ": ",
                "Enter author".PadRight(25, ' ') + ": ",
                "Enter publisher".PadRight(25, ' ') + ": ",
                "Enter quantity".PadRight(25, ' ') + ": ",
                "Enter price".PadRight(25, ' ') + ": ",
                "Enter published date".PadRight(25, ' ') + ": ",
                "Enter ISBN".PadRight(25, ' ') + ": ",
                "Enter description".PadRight(25, ' ') + ": "
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