using System.Collections.Generic;
using Library.Exception;
using Library.Model;

namespace Library
{
    public class AdminAddBookViewer: Viewer
    {
        public AdminAddBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser) : base(data, dataManager, inputFromUser)
        {
        }

        public void AddBook()
        {
            bool isEscPressed = false;

            while (!isEscPressed)
            {
                KeyValuePair<bool, string> bookName = inputFromUser.ReadInputFromUser(0, 0, 10, false, true);
                bool isInputValid = true;
                isInputValid = bookName.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> bookAuthor = inputFromUser.ReadInputFromUser(0,1, 10, true, true);
                isInputValid = bookAuthor.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> bookPublisher = inputFromUser.ReadInputFromUser(0,1, 10, true, true);
                isInputValid = bookPublisher.Key;

                if (!isInputValid)
                {
                    return;
                }

                KeyValuePair<bool, string> bookQuantity = inputFromUser.ReadInputFromUser(0,1, 10, true, false);
                isInputValid = bookQuantity.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> bookPrice = inputFromUser.ReadInputFromUser(0,1, 10, true, false);
                isInputValid = bookPrice.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> bookPublishedDate = inputFromUser.ReadInputFromUser(0,1, 10, true, false);
                isInputValid = bookPublishedDate.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> bookIsbn = inputFromUser.ReadInputFromUser(0,1, 10, true, false);
                isInputValid = bookIsbn.Key;

                if (!isInputValid)
                {
                    return;
                }
                
                KeyValuePair<bool, string> bookDescription = inputFromUser.ReadInputFromUser(0,1, 10, true, true);
                isInputValid = bookDescription.Key;

                if (!isInputValid)
                {
                    return;
                }

                
            }
        }
    }
}