using System;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class TicTacToeGame
    {
        private static bool _keepRunning = true;
        
        private static void console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;

            _keepRunning = false;
        }

        public static void Main(string[] args)
        {
            if (Console.WindowHeight < 30)
            {
                Console.WriteLine("화면을 키워주세요");
                Console.ReadKey();
                return;
            }
            // ctrl+c 막기
            Console.CancelKeyPress += console_CancelKeyPress;
            
            // 게임 시작
            GameManager gameManager = new GameManager();
            gameManager.Start();
        }
    }
}