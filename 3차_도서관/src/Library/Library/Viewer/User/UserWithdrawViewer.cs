using System;
using Library.Constants;
using Library.Model;
using Library.Utility;

namespace Library.Viewer.User
{
    public class UserWithdrawViewer: UserViewer
    {
        public UserWithdrawViewer(Data data, DataManager dataManager, InputFromUser inputFromUser, int currentUserNumber)
            : base(data, dataManager, inputFromUser, currentUserNumber)
        {
            
        }

        public bool PrintWithdraw()
        {
            Console.Clear();
            FramePrinter.PrintFrame(1);
            FramePrinter.PrintLibrary(1);
            
            FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 1, "1: Withdraw, 2: Cancel", AlignType.CENTER, ConsoleColor.White);

            ConsoleKeyInfo confirm = new ConsoleKeyInfo();

            while (confirm.Key != ConsoleKey.D1 && confirm.Key != ConsoleKey.D2 && confirm.Key != ConsoleKey.Escape)
            {
                confirm = Console.ReadKey(true);

                if (confirm.Key == ConsoleKey.D1)
                {
                    if (dataManager.userManager.DeleteMember(data, currentUserNumber))
                    {
                        FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 2, "Withdraw success", AlignType.CENTER, ConsoleColor.White);
                        Console.ReadKey(true);
                        Console.Clear();
                        return true;
                    }

                    FramePrinter.PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 2, "You must return all borrowed books to withdraw!", AlignType.CENTER, ConsoleColor.White);
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                }
            }

            return false;
        }
    }
}