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

        private StarWriter starWriter;
    }
}