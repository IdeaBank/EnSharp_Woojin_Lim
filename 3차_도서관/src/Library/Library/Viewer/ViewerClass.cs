using Library.Exception;
using Library.Model;
using Library.Utility;

namespace Library.Viewer
{
    public class ViewerClass
    {
        protected Data data;
        protected DataManager dataManager;
        protected InputFromUser inputFromUser;

        public ViewerClass(Data data, DataManager dataManager, InputFromUser inputFromUser)
        {
            this.data = data;
            this.dataManager = dataManager;
            this.inputFromUser = inputFromUser;
        }
    }
}