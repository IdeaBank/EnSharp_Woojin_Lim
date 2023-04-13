using Library.Model;
using Library.Utility;

namespace Library.Viewer.User
{
    public class UserViewer : ViewerClass
    {
        protected int currentUserNumber;
        public UserViewer(Data data, DataManager dataManager, InputFromUser inputFromUser, int currentUserNumber): base(data, dataManager, inputFromUser)
        {
            this.currentUserNumber = currentUserNumber;
        }
    }
}