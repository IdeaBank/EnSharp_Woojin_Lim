using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Utility;
using Library.Model;
using Library.Utility;
using Library.View.Admin;

namespace Library
{
    public class AdminAddBookViewer: Viewer.ViewerClass
    {
        public AdminAddBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser) : base(data, dataManager, inputFromUser)
        {
        }

        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }
        
        public void AddBook()
        {
            AdminAddBookView.Print();
            KeyValuePair<bool, string> bookName, bookAuthor, bookPublisher, bookQuantity, bookPrice, bookPublishedDate, bookIsbn, bookDescription;

            while (true)
            {
                bookName = inputFromUser.ReadInputFromUser(26, 1, 20, false, false);

                if (!bookName.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookName.Value, @"^[0-9a-zA-Zㄱ-힇?!+=]+"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 1, "영어, 한글, 숫자, ?!+= 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 1);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookAuthor = inputFromUser.ReadInputFromUser(26, 2, 20, false, true);

                if (!bookAuthor.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookAuthor.Value, @"^[a-zA-Zㄱ-힇]+"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 2, "영어, 한글 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 2);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookPublisher = inputFromUser.ReadInputFromUser(26, 3, 20, false, true);

                if (!bookPublisher.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookPublisher.Value, @"^[a-zA-Zㄱ-힇]+"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 3, "영어, 한글 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 3);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookQuantity = inputFromUser.ReadInputFromUser(26, 4, 3, false, false);

                if (!bookQuantity.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookQuantity.Value, @"^[1-9]{1}[0-9]{0,2}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 4, "1-999사이의 자연수", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 4);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookPrice = inputFromUser.ReadInputFromUser(26, 5, 7, false, false);

                if (!bookPrice.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookPrice.Value, @"^[1-9]{1}[0-9]{0,6}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 5, "1-9999999사이의 자연수", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 5);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookPublishedDate = inputFromUser.ReadInputFromUser(26, 6, 10, false, false);

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

                FramePrinter.PrintOnPosition(26, 6, "19xx or 20xx-xx-xx", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 6);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookIsbn = inputFromUser.ReadInputFromUser(26, 7, 24, false, false);

                if (!bookIsbn.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookIsbn.Value, @"[0-9]{9}[a-zA-Z]{1} {1}[0-9]{13}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 7, "정수9개 + 영어1개 + 공백 + 정수13개", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 7);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                bookDescription = inputFromUser.ReadInputFromUser(26, 8, 30, false, true);

                if (!bookDescription.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(bookDescription.Value, @"[0-9a-zA-Z\d\b]+"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 8, "최소1개의 문자(공백포함)", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 8);
                Console.Write(new string(' ', 50));
            }

            dataManager.bookManager.AddBook(data,
                new Book(bookName.Value, bookAuthor.Value, bookPublisher.Value,
                    Int32.Parse(bookQuantity.Value), Int32.Parse(bookPrice.Value),
                    bookPublishedDate.Value, bookIsbn.Value, bookDescription.Value));
            
            Console.Clear();
        }
    }
}