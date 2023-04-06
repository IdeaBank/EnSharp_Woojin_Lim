using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class GameManager
    {
        private TicTacToeBoard ticTacToeBoard;
        private int playMode;
        private int playOrder;
        private int[,] gameResult;

        public GameManager()
        {
            this.ticTacToeBoard = new TicTacToeBoard();
            this.gameResult = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            this.playOrder = 1;
        }
        
        public void Start()
        {
            //gameResult = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            
            ShowMainMenu();
            this.playMode = InputOneDigitBetween(0, 3);

            if(this.playMode != 0)
                StartGame();
        }
        
        public void ShowMainMenu()
        {
            this.ticTacToeBoard.DrawBlankBoardWithInput();

            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 8,
                "0: Exit program", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 9,
                "1: Play against computer", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 10,
                "2: Play against player", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 11,
                "3: Show scoreboard", ALIGN.CENTER);
        }

        public void ShowOrderInput()
        {
            this.ticTacToeBoard.DrawBlankBoardWithInput();

            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 11,
                "1: First, 2: Second", ALIGN.CENTER);
        }

        public void ShowRetry()
        {
            this.ticTacToeBoard.DrawBoardWithInput();
            
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 11,
                "1: Retry, 2: Back to menu", ALIGN.CENTER);
        }

        public void ShowScoreboard()
        {
            Console.Clear();
            
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 3,
                "|  W  |  D  |  L  |", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 2,
                "|-----|-----|-----|", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 1,
                "|     |     |     |", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 1,
                "VS Computer", ALIGN.RIGHT);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 0,
                "|-----|-----|-----|", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 1,
                "|     |     |     |", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 + 1,
                "VS Player", ALIGN.RIGHT);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 2,
                "|-----|-----|-----|", ALIGN.CENTER);
            
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2 - 1,
                this.gameResult[0, 0].ToString(), ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 1,
                this.gameResult[0, 1].ToString(), ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2 + 6, Console.WindowHeight / 2 - 1,
                this.gameResult[0, 2].ToString(), ALIGN.CENTER);
            
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2 + 1,
                this.gameResult[1, 0].ToString(), ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 1,
                this.gameResult[1, 1].ToString(), ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2 + 6, Console.WindowHeight / 2 + 1,
                this.gameResult[1, 2].ToString(), ALIGN.CENTER);
        }
        
        public int InputOneDigitBetween(int from, int to)
        {
            int a = InputOneDigit(Console.WindowWidth / 2, Console.WindowHeight / 2 + 13);

            while (!(from <= a && a <= to))
            {
                a = InputOneDigit(Console.WindowWidth / 2, Console.WindowHeight / 2 + 13);
            }

            return a;
        }
        
        public void StartGame()
        {
            if (this.playMode == 3)
            {
                ShowScoreboard();
                
                Console.ReadKey();
            }

            else
            {
                while (true)
                {
                    ShowOrderInput();
                    this.playOrder = InputOneDigitBetween(1, 2);

                    switch (playMode)
                    {
                        case 1:

                            break;
                        case 2:
                            PlayAgainstPlayer();
                            break;
                    }
                    ShowRetry();

                    if (InputOneDigitBetween(1, 2) == 2)
                    {
                        break;
                    }
                }
            }
            
            Start();
        }

        public void PlayAgainstPlayer()
        {
            int i = 0;
            
            while(true)
            {
                this.ticTacToeBoard.DrawBoardWithInput();
                KeyValuePair<bool, int> result = HasGameEnded(this.ticTacToeBoard.GetBoard());

                if (result.Key)
                {
                    if (result.Value == 0)
                    {
                        this.gameResult[1, 1] += 1;
                    }
                    
                    else if (result.Value == playOrder)
                    {
                        this.gameResult[1, 0] += 1;
                    }

                    else
                    {
                        this.gameResult[1, 2] += 1;
                    }
                    
                    this.ticTacToeBoard.DrawBoard();
                    
                    break;
                }

                int positionInput = InputOneDigitBetween(1, 9);

                while (!ticTacToeBoard.IsCellEmpty(this.ticTacToeBoard.GetBoard(), positionInput - 1))
                {
                    positionInput = InputOneDigitBetween(1, 9);
                }

                this.ticTacToeBoard.SetBoardWithPosition(positionInput - 1, i % 2 + 1);

                ++i;
                
                this.ticTacToeBoard.DrawBoardWithInput();
            }
        }

        public void PlayAgainstComputer()
        {
            this.ticTacToeBoard.SetBoardWithPosition(0, 2);
            Console.WriteLine(CalculateBestMove(1));
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

                if (keyInput.Key == ConsoleKey.Backspace)
                {
                    currentInput = ' ';
                    Console.Write("  ");
                }
                
                else if (keyInput.Key != ConsoleKey.Enter)
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
                for (int j = 0; j < combination.Length / 3; ++j)
                {
                    // 누군가가 이겼다면, 해당 사람의 번호 반환 (1 or 2)
                    if (board[combination[j, 0]] == board[combination[j, 1]] &&
                        board[combination[j, 1]] == board[combination[j, 2]] &&
                        board[combination[j, 0]] != 0 && board[combination[j, 0]] == i)
                    {
                        return new KeyValuePair<bool, int>(true, i);
                    }
                }
            }

            // 채워진 칸의 개수 구하기
            int count = 0;

            for (int i = 0; i < 9; ++i)
            {
                if (board[i] != 0)
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

        public int CalculateBestMove(int botOrder)
        {
            int bestScore = -1000;
            int bestIndex = -1;
                
            for (int i = 0; i < 9; ++i)
            {
                if (this.ticTacToeBoard.IsCellEmpty(this.ticTacToeBoard.GetBoard(), i))
                {
                    List<int> tempBoard = new List<int>(ticTacToeBoard.GetBoard());
                    tempBoard[i] = botOrder;
                    
                    int miniMaxResult = MiniMax(tempBoard, false, botOrder);
                    
                    if (bestScore < miniMaxResult)
                    {
                        bestIndex = i;
                        bestScore = miniMaxResult;
                    }
                }
            }

            return bestIndex;
        }

        public int MiniMax(List<int> board, bool isMaximizing, int botOrder)
        {
            KeyValuePair<bool, int> result = HasGameEnded(board);

            if (result.Key)
            {
                if (result.Value == 0)
                {
                    return 0;
                }
                
                else if (result.Value == botOrder)
                {
                    return 100;
                }

                else
                {
                    return -100;
                }
            }

            if (isMaximizing)
            {
                int bestScore = -1000;
                
                for (int i = 0; i < 9; ++i)
                {
                    if (this.ticTacToeBoard.IsCellEmpty(board, i))
                    {
                        List<int> tempBoard = new List<int>(board);
                        tempBoard[i] = botOrder;

                        int minimaxResult = MiniMax(tempBoard, false, botOrder);

                        if (minimaxResult > bestScore)
                        {
                            bestScore = minimaxResult;
                        }
                    }
                }
                return bestScore;
            }

            else
            {
                int bestScore =  1000;
                
                for (int i = 0; i < 9; ++i)
                {
                    if (this.ticTacToeBoard.IsCellEmpty(board, i))
                    {
                        List<int> tempBoard = new List<int>(board);
                        tempBoard[i] = botOrder % 2 + 1;

                        int minimaxResult = MiniMax(tempBoard, true, botOrder);

                        if (minimaxResult < bestScore)
                        {
                            bestScore = minimaxResult;
                        }
                    }
                }
                return bestScore;
            }
        }
    }
}