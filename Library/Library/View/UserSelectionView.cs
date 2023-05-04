using System;
using Library.Constant;
using Library.Utility;

namespace Library.View
{
    public class UserSelectionView
    {
        private static UserSelectionView _instance;

        private UserSelectionView()
        {

        }

        public static UserSelectionView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSelectionView();
                }

                return _instance;
            }
        }

        public void PrintYesOrNO(string instruction)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            Console.Clear();
            ConsoleWriter.getInstance.DrawContour(30, 6);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf - 1,
                instruction, AlignType.CENTER);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 1,
                "Y: yes, N: no", AlignType.CENTER);
        }
    }
}