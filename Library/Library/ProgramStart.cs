using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Controller;
using Library.Utility;

namespace Library
{
    class ProgramStart
    {
        public static void Main(string[] args)
        {
            // LibraryStart libraryStart = new LibraryStart();
            // libraryStart.StartLibrary();

            Console.WriteLine(MenuSelector.SelectMenu(3).Value);
        }
    }
}