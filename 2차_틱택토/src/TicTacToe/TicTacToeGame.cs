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
            // ctrl+c 막기
            Console.CancelKeyPress += console_CancelKeyPress;
            
            // 게임 시작
            GameManager gameManager = new GameManager();
            gameManager.Start();
        }
    }
}