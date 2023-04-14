using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View;

namespace Library
{
    public class SearchBookViewer : Viewer.ViewerClass
    {
        public SearchBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser) : base(data,
            dataManager, inputFromUser)
        {
        }

        public void SearchBook()
        {
            Console.Clear();
            // BookListView.PrintBookList(data.books);
            BookListView.PrintBookSearch();
            KeyValuePair<bool, string> bookName, bookAuthor, bookPublisher;

            bookName = inputFromUser.ReadInputFromUser(15, 0, 10, false, true);

            if (!bookName.Key)
            {
                Console.Clear();
                return;
            }

            bookAuthor = inputFromUser.ReadInputFromUser(15, 1, 10, false, true);

            if (!bookAuthor.Key)
            {
                Console.Clear();
                return;
            }

            bookPublisher = inputFromUser.ReadInputFromUser(15, 2, 10, false, true);

            if (!bookPublisher.Key)
            {
                Console.Clear();
                return;
            }

            BookListView.PrintBookList(dataManager.bookManager.SearchBook(data, bookName.Value, bookAuthor.Value,
                bookPublisher.Value));
            Console.ReadKey();
            Console.Clear();
        }
    }
}