using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class GameManager
    {
        private TicTacToeBoard ticTacToeBoard;
        
        public void StartGame()
        {
            // 보드 생성
            this.ticTacToeBoard = new TicTacToeBoard();
            
            
        }

        private int InputOneDigit()
        {
            // return 될 때까지 계속 반복
            while (true)
            {
                // 우선 한 줄 입력 받기
                string input = Console.ReadLine();

                // 1개의 문자일 때만
                if (input.Length == 1)
                {
                    // 해당 문자가 숫자이면, 0~9 반환
                    if ('0' <= input[0] && input[0] <= '9')
                        return input[0] - '0';
                }
            }
        }
    }
}