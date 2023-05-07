using System;
using System.Linq;
using Library.Controller;
using Library.Utility;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
            };
            
            LibraryStart libraryStart = new LibraryStart();
            libraryStart.StartLibrary();
        }
    }
}