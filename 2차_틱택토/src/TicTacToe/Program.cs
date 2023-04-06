using System;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
            };
            
            GameManager gameManager = new GameManager();
            Console.ReadKey();
            gameManager.Start();
            //gameManager.ShowScoreboard();
            Console.ReadKey();
            //gameManager.PlayAgainstComputer();
        }
    }
}