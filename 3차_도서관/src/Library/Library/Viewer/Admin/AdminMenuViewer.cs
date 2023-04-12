using System;
using System.Collections.Generic;
using Library.Exception;
using Library.Model;
using Library.View;

namespace Library
{
    public class AdminMenuViewer: Viewer
    {
        public AdminMenuViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public void AddBook()
        {
            InputBookInfo();
        }
        
        public void InputBookInfo()
        {
            while (true)
            {
                KeyValuePair<bool, string> inputResult = inputFromUser.ReadInputFromUser(0, 0, 10, false, true);

                if (!inputResult.Key)
                {
                    return;
                }
                
                inputResult = inputFromUser.ReadInputFromUser(1, 0, 10, false, true);

                if (!inputResult.Key)
                {
                    return;
                }

                if (VerifyInformation.IsValidBookInformation())
                {
                    
                }
            }
        }
    }
}