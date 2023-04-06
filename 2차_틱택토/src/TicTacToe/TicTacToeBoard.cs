using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class TicTacToeBoard
    {
        private List<int> board;

        public TicTacToeBoard()
        {
            // 보드 초기화
            this.board = new List<int>();

            for (int i = 0; i < 9; ++i)
            {
                this.board.Add(0);
            }
        }

        public TicTacToeBoard(List<int> board)
        {
            this.board = board;
        }

        public List<int> GetBoard()
        {
            return this.board;
        }

        public void PrintOnPosition(int x, int y, string str, int align)
        {
            int lastCursorX = Console.CursorLeft;
            int lastCursorY = Console.CursorTop;

            switch (align)
            {
                case ALIGN.LEFT:
                    Console.SetCursorPosition(x, y);
                    break;
                case ALIGN.CENTER:
                    Console.SetCursorPosition(x - str.Length / 2, y);
                    break;
                case ALIGN.RIGHT:
                    Console.SetCursorPosition(x - str.Length, y);
                    break;
                default:
                    return;
            }
            
            // 원하는 문자열 출력
            Console.Write(str);
            
            // 커서 복귀
            Console.SetCursorPosition(lastCursorX, lastCursorY);
        }

        public void DrawBoard()
        {
            Console.Clear();

            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            int[,] position =
            {
                { consoleWidth / 2 - 6, consoleHeight / 2 - 3 },
                { consoleWidth / 2 + 0, consoleHeight / 2 - 3 },
                { consoleWidth / 2 + 6, consoleHeight / 2 - 3 },
                { consoleWidth / 2 - 6, consoleHeight / 2 + 0 },
                { consoleWidth / 2 + 0, consoleHeight / 2 + 0 },
                { consoleWidth / 2 + 6, consoleHeight / 2 + 0 },
                { consoleWidth / 2 - 6, consoleHeight / 2 + 3 },
                { consoleWidth / 2 + 0, consoleHeight / 2 + 3 },
                { consoleWidth / 2 + 6, consoleHeight / 2 + 3 },
            };

            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 - 4, "     |     |     ", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 - 3, "     |     |     ", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 - 2, "_____|_____|_____", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 - 1, "     |     |     ", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 0, "     |     |     ", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 1, "_____|_____|_____", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 2, "     |     |     ", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 3, "     |     |     ", ALIGN.CENTER);
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 4, "     |     |     ", ALIGN.CENTER);

            for (int i = 0; i < 9; ++i)
            {
                switch (this.board[i])
                {
                    case 0:
                        PrintOnPosition(position[i, 0], position[i, 1], (i + 1).ToString(), ALIGN.CENTER);
                        break;
                    case 1:
                        PrintOnPosition(position[i, 0], position[i, 1], "X", ALIGN.CENTER);
                        break;
                    case 2:
                        PrintOnPosition(position[i, 0], position[i, 1], "O", ALIGN.CENTER);
                        break;
                }
            }
        }

        public void DrawBoardWithInput()
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;
            
            this.DrawBoard();
            
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 14, "_______", ALIGN.CENTER);
        }

        public void DrawBlankBoardWithInput()
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            this.board = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            this.DrawBoard();
            
            PrintOnPosition(consoleWidth / 2, consoleHeight / 2 + 14, "_______", ALIGN.CENTER);
        }
        
        public bool IsCellEmpty(List<int> targetBoard, int pos)
        {
            return targetBoard[pos] == 0;
        }
        
        public bool SetBoardWithPosition(int index, int value)
        {
            if (IsCellEmpty(this.board, index))
            {
                this.board[index] = value;
                return true;
            }

            return false;
        }
    }
}