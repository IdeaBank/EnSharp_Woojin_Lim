using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class GameManager
    {
        public GameManager()
        {

        }

        private bool IsDigitsOnly(string str)
        {
            if (str.Length == 0)
                return false;
            
            foreach (char c in str)
                if (c < '0' || c > '9')
                    return false;
            // 길이가 0이거나, 숫자가 아닌 문자가 있으면 false 반환
            
            return true;
            // 숫자로 이루어졌을 경우에는 true 반환
        }
        
        public void GetPlayerInput(int pos)
        {
            
        }

        public bool CheckGameOver()
        {
            return false;
        }

        public void StartUserVSComputer()
        {
            string tempInput = Console.ReadLine();

            while (IsDigitsOnly(tempInput) == false)
            {
                tempInput = Console.ReadLine();
            }

            
        }

        public void StartUserVSUser()
        {
            
        }

        public void ShowScoreboard()
        {
            
        }

        public void StartGame()
        {
            this.board = new List<int>(Enumerable.Range(1, 9).ToArray());

            string tempInput = Console.ReadLine();

            if (IsDigitsOnly(tempInput) == false)
            {
                StartGame();
                return;
            }

            int input = Int32.Parse(tempInput);

            switch (input)
            {
                case 1:
                    StartUserVSComputer();
                    break;
                case 2:
                    StartUserVSUser();
                    break;
                case 3:
                    ShowScoreboard();
                    break;
                case 4:
                    return;
            }

            StartGame();
        }

        private int mode;
        private List<int> board;
    }
}