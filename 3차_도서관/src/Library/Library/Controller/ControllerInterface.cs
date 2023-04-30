using Library.Model;
using Library.Utility;

namespace Library.Controller
{
    // Controller의 원형 선언 (모든 컨트롤러는 이 클래스를 상속 받기 때문에, 내부에 TotalData와 CombinedManager를 가져야 함.)
    public class ControllerInterface
    {
        protected TotalData data;
        protected CombinedManager combinedManager;

        protected ControllerInterface(TotalData data, CombinedManager combinedManager)
        {
            this.data = data;
            this.combinedManager = combinedManager;
        }
    }
}