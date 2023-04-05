namespace DrawStar
{
    public class GameManager
    {
        public GameManager()
        {
            this._starDrawer = new StarDrawer();
            // 내부 변수로 별을 찍어주는 StarWriter 클래스의 인스턴스 생성
        }

        public void StartGame()
        {
            if (!this._starDrawer.InputMenu()) // 입력 값이 0일 경우, 종료
                return;

            int retry = 1;

            while (retry == 1)
            {
                this._starDrawer.InputLines(); // 줄 수를 입력 받음
                retry = this._starDrawer.DrawStar();
            }

            if (retry == 3)
                return;
            StartGame();
            // 정상적으로 끝났을 경우에는 다시 시작
        }

        private StarDrawer _starDrawer;
    }
}