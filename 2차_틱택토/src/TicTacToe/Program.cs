using System;

namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            TicTacToeBoard test = new TicTacToeBoard();
            
            test.DrawBoard();
            Console.ReadKey();
        }
    }
}