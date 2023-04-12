using System;
using System.Collections.Generic;
using Library.Exception;
using Library.Model;

namespace Library
{
    public class AdminRemoveUserViewer: Viewer
    {
        public AdminRemoveUserViewer(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
        }

        public void RemoveUser()
        {
            bool isEscPressed = false;

            while (!isEscPressed)
            {
                KeyValuePair<bool, string> userID = inputFromUser.ReadInputFromUser(0, 0, 10, false, true);

                if (!userID.Key)
                {
                    isEscPressed = true;
                    return;
                }

                bool success = dataManager.userManager.DeleteMember(data, Int32.Parse(userID.Value));

                if (success)
                {
                    return;
                }
            }
        }
    }
}