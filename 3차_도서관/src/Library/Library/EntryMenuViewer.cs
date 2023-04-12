using System;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.View;

namespace Library
{
    public class EntryMenuViewer: Viewer
    {
        private int selectionIndex;
        private const int MAX_INDEX = 1;

        public EntryMenuViewer(Data data, InputFromUser inputFromUser, DataManager dataManager): base(data, dataManager, inputFromUser)
        {
            this.selectionIndex = 0;
        }
        
        public void ViewEntryMenu()
        {
            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();
            MainMenuView.PrintMenu(selectionIndex);

            while (keyInput.Key != ConsoleKey.Escape)
            {
                MainMenuView.PrintMenu(selectionIndex);
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
                if (this.selectionIndex == 0)
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
                    Console.WriteLine("USER MENU");
                    Console.ReadKey();
                    break;
                
                case 1:
                    AdminLoginViewer adminLoginViewer = new AdminLoginViewer(data, dataManager, inputFromUser);
                    adminLoginViewer.TryLogin();
                    
                    // Console.WriteLine("ADMIN MENU");
                    // Console.ReadKey();
                    break;
            }
        }
    }
}