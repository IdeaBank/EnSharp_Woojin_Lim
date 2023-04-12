using Library.Exception;
using Library.Model;

namespace Library
{
    public class Viewer
    {
        protected Data data;
        protected DataManager dataManager;
        protected InputFromUser inputFromUser;

        public Viewer()
        {
            this.data = new Data();
            this.dataManager = new DataManager();
            this.inputFromUser = new InputFromUser();
        }
        
        public Viewer(Data data, DataManager dataManager, InputFromUser inputFromUser)
        {
            this.data = data;
            this.dataManager = dataManager;
            this.inputFromUser = inputFromUser;
        }
    }
}