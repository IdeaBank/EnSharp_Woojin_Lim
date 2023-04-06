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
            //gameManager.ShowMainMenu();
            // Console.ReadLine();

            List<int> asd = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            
            Console.WriteLine(gameManager.MiniMax(asd, 5, 2));
        }
    }
}