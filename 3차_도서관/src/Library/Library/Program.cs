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
            LibraryStart libraryStart = new LibraryStart();
            libraryStart.StartLibrary();
        }
    }
}