using Library.Model;
using Library.Utility;

namespace Library.Controller
{
    public class ControllerInterface
    {
        protected TotalData data;
        protected CombinedManager combinedManager;

        public ControllerInterface(TotalData data, CombinedManager combinedManager)
        {
            this.data = data;
            this.combinedManager = combinedManager;
        }
    }
}