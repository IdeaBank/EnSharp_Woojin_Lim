using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;

namespace Library
{
    public class AdminEditBookViewerClass : Viewer.ViewerClass
    {
        public AdminEditBookViewerClass(Data data, DataManager dataManager, InputFromUser inputFromUser) : base(data,
            dataManager, inputFromUser)
        {
        }

        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }

        public void ShowEditBookView()
        {
            SearchBookViewerClass searchBookViewerClass = new SearchBookViewerClass(data, dataManager, inputFromUser);
            searchBookViewerClass.SearchBook();

            KeyValuePair<bool, string> bookID;

            Console.WriteLine("ID:".PadLeft(15, ' '));
            bookID = inputFromUser.ReadInputFromUser(16, 0, 3, false, false);

            if (!bookID.Key)
            {
                Console.Clear();
                return;
            }

            if (IsInputValid(bookID.Value, @"^[0-9]+"))
            {
                if (dataManager.bookManager.BookExists(data, Int32.Parse(bookID.Value)))
                {
                    Book book = dataManager.bookManager.GetBook(data, Int32.Parse(bookID.Value));
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

                    Console.Clear();
                    Console.WriteLine(new string('=', 10) + "Original Data" + new string('=', 10));
                    Console.WriteLine(book.bookId);
                    Console.WriteLine(book.name);
                    Console.WriteLine(book.author);
                    Console.WriteLine(book.publisher);
                    Console.WriteLine(book.quantity);
                    Console.WriteLine(book.price);
                    Console.WriteLine(book.publishedDate);
                    Console.WriteLine(book.isbn);
                    Console.WriteLine(book.description);

                    Console.WriteLine(new string('=', 10) + "New Data" + new string('=', 10));

                    foreach (string instruction in instructions)
                    {
                        Console.WriteLine(instruction);
                    }

                    KeyValuePair<bool, string> bookName,
                        bookAuthor,
                        bookPublisher,
                        bookQuantity,
                        bookPrice,
                        bookPublishedDate,
                        bookIsbn,
                        bookDescription;

                    while (true)
                    {
                        bookName = inputFromUser.ReadInputFromUser(26, 5, 20, false, false);

                        if (!bookName.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookName.Value, @"^[0-9a-zA-Zㄱ-힇?!+=]+"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 5, "영어, 한글, 숫자, ?!+= 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 5);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookAuthor = inputFromUser.ReadInputFromUser(26, 6, 20, false, true);

                        if (!bookAuthor.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookAuthor.Value, @"^[a-zA-Zㄱ-힇]+"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 6, "영어, 한글 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 6);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookPublisher = inputFromUser.ReadInputFromUser(26, 7, 20, false, true);

                        if (!bookPublisher.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookPublisher.Value, @"^[a-zA-Zㄱ-힇]+"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 7, "영어, 한글 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 7);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookQuantity = inputFromUser.ReadInputFromUser(26, 8, 3, false, false);

                        if (!bookQuantity.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookQuantity.Value, @"^[1-9]{1}[0-9]{0,2}"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 8, "1-999사이의 자연수", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 8);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookPrice = inputFromUser.ReadInputFromUser(26, 9, 7, false, false);

                        if (!bookPrice.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookPrice.Value, @"^[1-9]{1}[0-9]{0,6}"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 9, "1-9999999사이의 자연수", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 9);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookPublishedDate = inputFromUser.ReadInputFromUser(26, 10, 10, false, false);

                        if (!bookPublishedDate.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookPublishedDate.Value,
                                @"[19|20]{1}[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 10, "19xx or 20xx-xx-xx", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 10);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookIsbn = inputFromUser.ReadInputFromUser(26, 11, 24, false, false);

                        if (!bookIsbn.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookIsbn.Value, @"[0-9]{9}[a-zA-Z]{1} {1}[0-9]{13}"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 11, "정수9개 + 영어1개 + 공백 + 정수13개", AlignType.LEFT,
                            ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 11);
                        Console.Write(new string(' ', 50));
                    }

                    while (true)
                    {
                        bookDescription = inputFromUser.ReadInputFromUser(26, 12, 30, false, true);

                        if (!bookDescription.Key)
                        {
                            Console.Clear();
                            return;
                        }

                        if (IsInputValid(bookDescription.Value, @"[0-9a-zA-Z\d\b]+"))
                        {
                            break;
                        }

                        FramePrinter.PrintOnPosition(26, 12, "최소1개의 문자(공백포함)", AlignType.LEFT, ConsoleColor.Red);
                        Console.ReadKey(true);
                        Console.SetCursorPosition(26, 12);
                        Console.Write(new string(' ', 50));
                    }

                    dataManager.bookManager.EditBook(data,
                        new Book(bookName.Value, bookAuthor.Value, bookPublisher.Value, Int32.Parse(bookQuantity.Value),
                            Int32.Parse(bookPrice.Value), bookPublisher.Value, bookIsbn.Value, bookDescription.Value), Int32.Parse(bookID.Value));
                    Console.Clear();
                }
            }
        }
    }
}