using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class GameManager
    {
        private TicTacToeBoard ticTacToeBoard;
        private int playOrder;
        private int playMode;

        public void ShowMainMenu()
        {
            this.ticTacToeBoard = new TicTacToeBoard(new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1 });
            this.ticTacToeBoard.DrawBoard();
        }
        
        public void StartGame()
        {
            // 보드 생성
            this.ticTacToeBoard = new TicTacToeBoard();
            
            this.ticTacToeBoard.DrawBoard();

            // getInputFromUser();
        }

        public void PlayAgainstPlayer()
        {
            
        }

        public void PlayAgainstComputer()
        {
            
        }

        public bool IsCellEmpty(List<int> board, int pos)
        {
            return this.ticTacToeBoard.GetBoard()[pos] == 0;
        }
        
        private int InputOneDigit(int x, int y)
        {
            char currentInput = '\0';
            
            // return 될 때까지 계속 반복
            while (true)
            {
                Console.SetCursorPosition(x, y);
                
                // 우선 한 줄 입력 받기
                ConsoleKeyInfo keyInput = Console.ReadKey();
                
                if (keyInput.Key != ConsoleKey.Enter)
                {
                    currentInput = keyInput.KeyChar;
                }

                else
                {
                    if ('0' <= currentInput && currentInput <= '9')
                    {
                        return currentInput - '0';
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
            
            // 1: First player, 2: Second player
            for (int i = 1; i <= 2; ++i)
            {
                // combination 크기 (8)만큼 반복
                for (int j = 0; j < combination.Length; ++j)
                {
                    // 누군가가 이겼다면, 해당 사람의 번호 반환 (1 or 2)
                    if (board[combination[j, 0]] == board[combination[j, 1]] &&
                        board[combination[j, 1]] == board[combination[j, 2]])
                    {
                        return new KeyValuePair<bool, int>(true, i);
                    }
                }
            }

            // 채워진 칸의 개수 구하기
            int count = 0;

            for (int i = 0; i < 9; ++i)
            {
                if (board[i] == 0)
                {
                    count += 1;
                }
            }

            // 모든 칸이 채워졌을 때 무승부 반환
            if (count == 9)
            {
                return new KeyValuePair<bool, int>(true, 0);
            }

            // 끝나지 않았다면, false 반환
            return new KeyValuePair<bool, int>(false, 0);
        }

        public int CalculateBestMove(int turn)
        {
            int bestIndex = -1;
            int bestPercentage = -1;
                
            for (int i = 0; i < 9; ++i)
            {
                if (IsCellEmpty(this.ticTacToeBoard.GetBoard(), i))
                {
                    int miniMaxResult = MiniMax(ticTacToeBoard.GetBoard(), i, turn);
                    
                    if (bestPercentage < miniMaxResult)
                    {
                        bestIndex = i;
                        bestPercentage = miniMaxResult;
                    }
                }
            }

            return bestIndex;
        }

        public int MiniMax(List<int> board, int index, int turn)
        {
            List<int> tempBoard = new List<int>(board);

            tempBoard[index] = turn;

            KeyValuePair<bool, int> result = HasGameEnded(tempBoard);

            if (result.Key)
            {
                if (result.Value == 0)
                    return 0;

                else if (result.Value == turn)
                    return 1;

                else
                    return -1;
            }

            else
            {
                int minimaxResult = 0;
                for (int i = 0; i < 9; ++i)
                {
                    if (IsCellEmpty(tempBoard, i))
                    {
                        minimaxResult += MiniMax(tempBoard, i, turn);
                    }
                }

                return minimaxResult;
            }

            return 0;
        }
    }
}