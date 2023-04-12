using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Library.Exception;
using Library.Model;

namespace Library
{
    public class SearchBookViewer: Viewer
    {
        public SearchBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public void SearchBook()
        {
            bool isEscPressed = false;

            while (!isEscPressed)
            {
                KeyValuePair<bool, string> bookName = inputFromUser.ReadInputFromUser(0, 0, 10, false, true);

                if (!bookName.Key)
                {
                    isEscPressed = true;
                    return;
                }

                KeyValuePair<bool, string> bookAuthor = inputFromUser.ReadInputFromUser(0, 1, 10, false, true);

                if (!bookAuthor.Key)
                {
                    isEscPressed = true;
                    return;
                }

                KeyValuePair<bool, string> bookPublisher = inputFromUser.ReadInputFromUser(0, 2, 10, false, true);

                if (!bookPublisher.Key)
                {
                    isEscPressed = true;
                    return;
                }

                List<Book> books = dataManager.bookManager.SearchBook(this.data, bookName.Value, bookAuthor.Value,
                    bookPublisher.Value);
                //UserSearchBookView.Print(books);
            }
        }
    }
}