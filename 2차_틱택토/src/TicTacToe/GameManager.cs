using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace TicTacToe
{
    public class GameManager
    {
        private TicTacToeBoard ticTacToeBoard;
        private int playOrder;
        private int playMode;
        
        public void StartGame()
        {
            // 보드 생성
            this.ticTacToeBoard = new TicTacToeBoard();
            
            this.ticTacToeBoard.DrawBoard();

            int a = InputOneDigit(1, 1);

            // int a = InputOneDigit();
        }

        public void PlayAgainstPlayer()
        {
            
        }

        public void PlayAgainstComputer()
        {
            
        }
        
        private int InputOneDigit(int x, int y)
        {
            char current_input = '\0';
            
            // return 될 때까지 계속 반복
            while (true)
            {
                Console.SetCursorPosition(x, y);
                
                // 우선 한 줄 입력 받기
                ConsoleKeyInfo keyInput = Console.ReadKey();
                
                if (keyInput.Key != ConsoleKey.Enter)
                {
                    current_input = keyInput.KeyChar;
                }

                else
                {
                    if ('0' <= current_input && current_input <= '9')
                    {
                        return current_input - '0';
                    }
                }
            }
        }

        // 게임이 끝났는 지 판별하는 함수 
        public KeyValuePair<bool, int> HasGameEnded(List<int> board)
        {
            // 모든 승리 조건
            int [,] combination = 
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };
            
            // 0: First player, 1: Second player
            for (int i = 0; i < 2; ++i)
            {
                // combination 크기 (8)만큼 반복
                for (int j = 0; j < combination.Length; ++j)
                {
                    // 누군가가 이겼다면, 해당 사람의 번호 반환
                    if (board[combination[j, 0]] == board[combination[j, 1]] &&
                        board[combination[j, 1]] == board[combination[j, 2]])
                    {
                        return new KeyValuePair<bool, int>(true, i);
                    }
                }
            }

            // 이긴 사람이 없다면, false 반환
            return new KeyValuePair<bool, int>(false, 0);
        }
    }
}