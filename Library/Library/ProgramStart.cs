using Library.Controller;

namespace Library
{
    class ProgramStart
    {
        public static void Main(string[] args)
        {
            LibraryStart libraryStart = new LibraryStart();
            libraryStart.StartLibrary();
        }
    }
}