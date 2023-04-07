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
            // 변수 초기화
            this.ticTacToeBoard = new TicTacToeBoard();
            this.gameResult = new int[2, 3] { { 0, 0, 0 }, { 0, 0, 0 } };
            this.playOrder = 1;
        }
        
        // 처음 시작
        public void Start()
        {
            // 메인 메뉴 표시 후 0 ~ 3을 입력 받음
            ShowMainMenu();
            this.playMode = InputOneDigitBetween(0, 3);

            // 종료가 아니면, 게임 시작
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
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 11,
                "1: Retry, 2: Back to menu", ALIGN.CENTER);
            this.ticTacToeBoard.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 14,
                "_______", ALIGN.CENTER);
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
        
        // 범위를 정하여 한 개의 숫자를 입력 받는 함수 (from에서 to까지)
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
            int consoleWidthHalf = Console.WindowWidth / 2;
            int consoleHeightHalf = Console.WindowHeight / 2;
            
            // 3 (점수판 출력) 입력 시 점수판 출력 후 아무 키나 입력할 때까지 대기
            if (this.playMode == 3)
            {
                ShowScoreboard();
                
                Console.ReadKey();
            }

            // 1 혹은 2일 경우
            else
            {
                while (true)
                {
                    int result = 0;
                    // 순서를 입력 받음
                    ShowOrderInput();
                    this.playOrder = InputOneDigitBetween(1, 2);

                    // 보드 리셋
                    this.ticTacToeBoard.ResetBoard();
                    
                    // playMode에 따라 PVC 혹은 PVP 진행
                    switch (playMode)
                    {
                        case PLAYMODE.PVC:
                            result = PlayAgainstComputer();
                            break;
                        case PLAYMODE.PVP:
                            result = PlayAgainstPlayer();
                            break;
                    }
                    
                    // 게임이 끝난 후, 결과 판 공개
                    ShowScoreboard();
                    
                    // 결과에 따라 PLAYER1 WON, DRAW, PLAYER1 LOST 출력
                    if (result == playOrder)
                    {
                        this.ticTacToeBoard.PrintOnPosition(consoleWidthHalf, consoleHeightHalf + 8, 
                            "PLAYER1 WON", ALIGN.CENTER);
                    }
                    
                    else if (result == 0)
                    {
                        this.ticTacToeBoard.PrintOnPosition(consoleWidthHalf, consoleHeightHalf + 8, 
                            "DRAW", ALIGN.CENTER);
                    }

                    else
                    {
                        this.ticTacToeBoard.PrintOnPosition(consoleWidthHalf, consoleHeightHalf + 8, 
                            "PLAYER1 LOST", ALIGN.CENTER);
                    }
                    
                    // 다시 할 것인지 입력 받음
                    ShowRetry();

                    if (InputOneDigitBetween(1, 2) == 2)
                    {
                        break;
                    }
                }
            }
            
            // 다시 하지 않을 경우, 처음으로 돌아감
            Start();
        }
        
        public int PlayAgainstPlayer()
        {
            // 현재 턴 수 및 게임 결과를 저장하는 변수 currentTurn, result 선언
            int currentTurn = 0;
            KeyValuePair<bool, int> result;
            
            while(true)
            {
                this.ticTacToeBoard.DrawBoardWithInput();
                // 게임 결과를 계속 받아 옴
                result = HasGameEnded(this.ticTacToeBoard.GetBoard());
                
                // result.Key는 게임의 종료 여부를 나타냄. (true: 종료, false: 아직 더 수가 남음)
                if (result.Key)
                {
                    // 비겼을 시
                    if (result.Value == 0)
                    {
                        this.gameResult[1, 1] += 1;
                    }
                    
                    // 플레이어가 승리했을 시
                    else if (result.Value == playOrder)
                    {
                        this.gameResult[1, 0] += 1;
                    }

                    // 상대가 승리했을 시
                    else
                    {
                        this.gameResult[1, 2] += 1;
                    }
                    
                    break;
                }

                // 아직 게임이 종료되지 않았을 경우, 1에서 9까지 수를 입력 받음
                int positionInput = InputOneDigitBetween(1, 9);

                // 보드판에서의 해당 숫자가 채워지지 않았을 때까지 반복해서 입력 받음
                while (!ticTacToeBoard.IsCellEmpty(this.ticTacToeBoard.GetBoard(), positionInput - 1))
                {
                    positionInput = InputOneDigitBetween(1, 9);
                }

                // 보드판에서 해당 숫자를 채워 넣음
                this.ticTacToeBoard.SetBoardWithPosition(positionInput - 1, currentTurn % 2 + 1);

                // 턴 수를 1 증가시킴
                ++currentTurn;
                
                // 현재 보드판을 그림
                this.ticTacToeBoard.DrawBoardWithInput();
            }

            // 게임이 끝났을 경우, 이긴 사람 반환
            return result.Value;
        }
        
        public int PlayAgainstComputer()
        {
            // 현재 턴 수 및 게임 결과를 저장하는 변수 currentTurn, result 선언
            int currentTurn = 0;
            KeyValuePair<bool, int> result; 
            
            while(true)
            {
                this.ticTacToeBoard.DrawBoardWithInput();
                // 게임 결과를 계속 받아옴
                result = HasGameEnded(this.ticTacToeBoard.GetBoard());

                // 게임이 끝났을 경우
                if (result.Key)
                {
                    // 비겼을 경우
                    if (result.Value == 0)
                    {
                        this.gameResult[0, 1] += 1;
                    }
                    
                    // 이겼을 경우
                    else if (result.Value == playOrder)
                    {
                        this.gameResult[0, 0] += 1;
                    }

                    // 졌을 경우
                    else
                    {
                        this.gameResult[0, 2] += 1;
                    }
                    
                    this.ticTacToeBoard.DrawBoard();
                    
                    break;
                }

                // 플레이어의 턴일 경우
                if (currentTurn % 2 + 1 == playOrder)
                {
                    int positionInput = InputOneDigitBetween(1, 9);
                    
                    while (!ticTacToeBoard.IsCellEmpty(this.ticTacToeBoard.GetBoard(), positionInput - 1))
                    {
                        positionInput = InputOneDigitBetween(1, 9);
                    }
                
                    this.ticTacToeBoard.SetBoardWithPosition(positionInput - 1, currentTurn % 2 + 1);
                }

                // 컴퓨터의 턴일 경우
                else
                {
                    // 가장 좋은 수를 계산해서 그 곳에 둠
                    int bestMove = CalculateBestMove(playOrder % 2 + 1);
                    this.ticTacToeBoard.SetBoardWithPosition(bestMove, playOrder % 2 + 1);
                }
                
                // 턴 수를 1 증가시킴
                ++currentTurn;
                
                this.ticTacToeBoard.DrawBoardWithInput();
            }

            // 게임의 결과를 반환
            return result.Value;
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

                // 백스페이스를 입력했을 시 입력 칸을 지움
                if (keyInput.Key == ConsoleKey.Backspace)
                {
                    currentInput = ' ';
                    Console.Write("  ");
                }
                
                // 입력한 키가 엔터키가 아닐 경우, currentInput의 값을 입력한 키로 설정
                else if (keyInput.Key != ConsoleKey.Enter)
                {
                    currentInput = keyInput.KeyChar;
                }

                // 입력한 키가 엔터키일 경우, currentInput이 '0' 이상 '9' 이하이면 해당 값을 숫자로 변환하여 반환
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

            // 모든 칸이 채워졌을 경우, 무승부 반환
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
            
            // 모든 칸 탐색
            for (int i = 0; i < 9; ++i)
            {
                // 해당 칸이 비어있으면
                if (this.ticTacToeBoard.IsCellEmpty(this.ticTacToeBoard.GetBoard(), i))
                {
                    // 해당 칸에 수를 뒀다고 가정하고 결과를 계산
                    List<int> tempBoard = new List<int>(ticTacToeBoard.GetBoard());
                    tempBoard[i] = botOrder;
                    
                    // 결과를 계산한 값을 miniMaxResult에 저장
                    int miniMaxResult = MiniMax(tempBoard, false, botOrder, 0);
                    
                    // 가장 높은 점수를 bestScore에 저장하고 그 index값 또한 저장
                    if (bestScore < miniMaxResult)
                    {
                        bestIndex = i;
                        bestScore = miniMaxResult;
                    }
                }
            }

            // 가장 좋은 수를 반환
            return bestIndex;
        }

        public int MiniMax(List<int> board, bool isMaximizing, int botOrder, int depth)
        {
            // 게임이 끝났는지 판단
            KeyValuePair<bool, int> result = HasGameEnded(board);

            // 끝났다면
            if (result.Key)
            {
                // 비겼을 경우 0 반환
                if (result.Value == 0)
                {
                    return 0;
                }
                
                // 이겼을 경우 100 반환
                else if (result.Value == botOrder)
                {
                    return 10 - depth;
                }

                // 졌을 경우 -100 반환
                else
                {
                    return depth -10;
                }
            }

            // 현재 컴퓨터의 차례라면, 가장 좋은 곳을 찾음
            if (isMaximizing)
            {
                int bestScore = -1000;
                
                for (int i = 0; i < 9; ++i)
                {
                    if (this.ticTacToeBoard.IsCellEmpty(board, i))
                    {
                        List<int> tempBoard = new List<int>(board);
                        tempBoard[i] = botOrder;

                        // 다음 수를 계산
                        int minimaxResult = MiniMax(tempBoard, false, botOrder, depth + 1);

                        if (minimaxResult > bestScore)
                        {
                            bestScore = minimaxResult;
                        }
                    }
                }
                return bestScore;
            }

            // 현재 플레이어의 차례라면, 가장 안 좋은 곳을 찾음 (컴퓨터의 입장에서는 플레이어에게 안 좋은 수가 곧 좋은 수임)
            else
            {
                int bestScore = 1000;
                
                for (int i = 0; i < 9; ++i)
                {
                    if (this.ticTacToeBoard.IsCellEmpty(board, i))
                    {
                        List<int> tempBoard = new List<int>(board);
                        tempBoard[i] = botOrder % 2 + 1;

                        // 다음 수를 계산
                        int minimaxResult = MiniMax(tempBoard, true, botOrder, depth + 1);

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