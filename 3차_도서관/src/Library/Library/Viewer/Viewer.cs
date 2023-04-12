using Library.Exception;
using Library.Model;

namespace Library.Viewer
{
    public class Viewer
    {
        protected Data data;
        protected DataManager dataManager;
        protected InputFromUser inputFromUser;

        public Viewer(Data data, DataManager dataManager, InputFromUser inputFromUser)
        {
            this.data = data;
            this.dataManager = dataManager;
            this.inputFromUser = inputFromUser;
        }
    }
}