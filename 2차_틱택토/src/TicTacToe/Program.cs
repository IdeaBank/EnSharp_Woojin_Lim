using System;

namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
                Console.WriteLine();
            };
            
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}