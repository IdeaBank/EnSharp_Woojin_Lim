using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    public static class ALIGN
    {
        public const int LEFT = 0;
        public const int CENTER = 1;
        public const int RIGHT = 2;
    }
    
    public class TicTacToeBoard
    {
        private List<int> board;

        public TicTacToeBoard()
        {
            // 보드 초기화
            this.board = new List<int>();
    
            for (int i = 0; i < 9; ++i)
                this.board.Add(0);
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
            
            // 원상 복귀
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
                if (this.board[i] == 0)
                {
                    PrintOnPosition(position[i, 0], position[i, 1], (i + 1).ToString(), ALIGN.CENTER);
                }
            }
        }
        
        // 게임이 끝났는 지 판별하는 함수
        public KeyValuePair<bool, int> HasGameEnded()
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
                    if (this.board[combination[j, 0]] == this.board[combination[j, 1]]
                        && this.board[combination[j, 1]] == this.board[combination[j, 2]])
                        return new KeyValuePair<bool, int>(true, i);
                }
            }

            // 이긴 사람이 없다면, false 반환
            return new KeyValuePair<bool, int>(false, 0);
        }
    }
}