using System;

namespace WriteStar
{
    public class GameManager
    {
        public GameManager()
        {
            this.starWriter = new StarWriter();
            // 내부 변수로 별을 찍어주는 StarWriter 클래스의 인스턴스 생성
        }

        public void StartGame()
        {
            if (this.starWriter.InputType() == false)
                return;

            if (this.starWriter.InputLines() == true) // 1 이상을 입력 받았을 때만 출력.
                this.starWriter.PrintStar();

            StartGame();
            // 정상적으로 끝났을 경우에는 다시 시작
        }

        private StarWriter starWriter;
    }
}