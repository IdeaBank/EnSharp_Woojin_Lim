using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Library.Exception;
using Library.Model;
using Library.View.User;

namespace Library
{
    public class SearchBookViewer: Viewer
    {
        public SearchBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser)
        {
            this.data = data;
            this.dataManager = dataManager;
            this.inputFromUser = inputFromUser;
        }

        public void SearchBook()
        {
            bool isEscPressed = false;

            while (!isEscPressed)
            {
                KeyValuePair<bool, string> bookName = inputFromUser.ReadInputFromUser(0, 0, 10, false);

                if (!bookName.Key)
                {
                    isEscPressed = true;
                    return;
                }

                KeyValuePair<bool, string> bookAuthor = inputFromUser.ReadInputFromUser(0, 0, 10, false);

                if (!bookAuthor.Key)
                {
                    isEscPressed = true;
                    return;
                }

                KeyValuePair<bool, string> bookPublisher = inputFromUser.ReadInputFromUser(0, 0, 10, false);

                if (!bookPublisher.Key)
                {
                    isEscPressed = true;
                    return;
                }

                List<Book> books = dataManager.bookManager.SearchBook(this.data, bookName.Value, bookAuthor.Value,
                    bookPublisher.Value);
                UserSearchBookView.Print(books);
            }
        }
    }
}