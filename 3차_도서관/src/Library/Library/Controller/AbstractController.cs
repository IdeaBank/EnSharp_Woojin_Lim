using Library.Utility;

namespace Library.Controller
{
    // Controller의 원형 선언 (모든 컨트롤러는 이 클래스를 상속 받기 때문에, 내부에 TotalData와 CombinedManager를 가져야 함.)
    public abstract class AbstractController
    {
        protected CombinedManager combinedManager;

        public CombinedManager CombinedManager
        {
            get => this.combinedManager;
            set => this.combinedManager = value;
        }

        protected AbstractController(CombinedManager combinedManager)
        {
            this.combinedManager = combinedManager;
        }
    }
}