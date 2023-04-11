using System;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.View;
using Library.View.Admin;

namespace Library
{
    public class AdminMenuViewer
    {
        private Data data;
        private InputFromUser inputFromUser;
        private int selectionIndex;
        private const int MAX_INDEX = 5;

        public AdminMenuViewer(Data data, InputFromUser inputFromUser)
        {
            this.data = data;
            this.inputFromUser = inputFromUser;
            this.selectionIndex = 0;
        }

        public void ShowAdminMenu()
        {
            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();
            AdminMenuView.Print(selectionIndex);

            while (keyInput.Key != ConsoleKey.Escape)
            {
                AdminMenuView.Print(selectionIndex);
                keyInput = Console.ReadKey();
                
                switch (keyInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveSelection(Direction.UP);
                        break;

                    case ConsoleKey.DownArrow:
                        MoveSelection(Direction.DOWN);
                        break;

                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.Enter:
                        EnterNextMenu();
                        break;
                }
            }
        }
        
        private void MoveSelection(Direction direction)
        {
            if (direction == Direction.UP)
            {
                if (this.selectionIndex < MAX_INDEX)
                {
                    return;
                }

                this.selectionIndex -= 1;
            }
            
            else if (direction == Direction.DOWN)
            {
                if (this.selectionIndex == MAX_INDEX)
                {
                    return;
                }

                this.selectionIndex += 1;
            }
        }

        private void EnterNextMenu()
        {
            switch (selectionIndex)
            {
                case 0:
                    Console.WriteLine("1");
                    Console.ReadKey();
                    break;
                
                case 1:
                    Console.WriteLine("2");
                    Console.ReadKey();
                    break;
                
                case 2:
                    Console.WriteLine("3");
                    Console.ReadKey();
                    break;
                
                case 3:
                    Console.WriteLine("4");
                    Console.ReadKey();
                    break;
                
                case 4:
                    Console.WriteLine("5");
                    Console.ReadKey();
                    break;
                
                case 5:
                    Console.WriteLine("6");
                    Console.ReadKey();
                    break;
            }
        }
    }
}