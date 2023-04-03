namespace WriteStar
{
    public class GameManager
    {
        public GameManager()
        {
            this._starWriter = new StarWriter();
            // 내부 변수로 별을 찍어주는 StarWriter 클래스의 인스턴스 생성
        }

        public void StartGame()
        {
            if (this._starWriter.InputType() == false) // 입력 값이 0일 경우, 종료
                return;

            if (this._starWriter.InputLines()) // 1 이상을 입력 받았을 때만 출력.
                this._starWriter.PrintStar();

            StartGame();
            // 정상적으로 끝났을 경우에는 다시 시작
        }

        private StarWriter _starWriter;
    }
}