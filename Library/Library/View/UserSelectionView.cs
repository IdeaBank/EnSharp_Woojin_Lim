using System;
using Library.Constants;
using Library.Utility;

namespace Library.View
{
    public class UserSelectionView
    {
        private UserSelectionView _instance;

        private UserSelectionView()
        {
            
        }

        public UserSelectionView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserSelectionView();
                }

                return _instance;
            }
        }

        public static void PrintYesOrNO(string instruction)
        {
            int currentWindowWidthHalf = Console.WindowWidth / 2;
            int currentWindowHeightHalf = Console.WindowHeight / 2;

            Console.Clear();
            ConsoleWriter.DrawContour(30, 6);

            ConsoleWriter.WriteOnPositionWithAlign(currentWindowWidthHalf, currentWindowHeightHalf - 1, 
                instruction, AlignType.CENTER);
            
            ConsoleWriter.WriteOnPositionWithAlign(currentWindowWidthHalf, currentWindowHeightHalf + 1,
                "Y: yes, N: no", AlignType.CENTER);
        }
    }
}