using System;

namespace DrawStar
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (Console.WindowHeight < Constants.MAX_HEIGHT + 1)
            {
                Console.WriteLine("화면을 키워주세요");
                return;
            }
            
            var gameManager = new GameManager();
            // 전체적인 게임을 관리해 줄 게임 매니저
            gameManager.StartGame();
            // 게임 시작
        }
    }
}