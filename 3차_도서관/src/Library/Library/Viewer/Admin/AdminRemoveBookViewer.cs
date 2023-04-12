using System;
using System.Collections.Generic;
using Library.Exception;
using Library.Model;

namespace Library
{
    public class AdminRemoveBookViewer: Viewer.Viewer
    {
        public AdminRemoveBookViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public void RemoveBookWithInput()
        {
            bool isEscPressed = false;

            while (!isEscPressed)
            {
                KeyValuePair<bool, string> bookID = inputFromUser.ReadInputFromUser(0, 0, 10, false, true);

                if (!bookID.Key)
                {
                    isEscPressed = true;
                    return;
                }

                bool success = dataManager.bookManager.RemoveBook(data, Int32.Parse(bookID.Value));

                if (success)
                {
                    return;
                }
            }
        }
    }
}