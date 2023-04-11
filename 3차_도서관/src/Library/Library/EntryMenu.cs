using System;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.View;

namespace Library
{
    public class EntryMenu
    {
        private int selectionIndex;
        private const int MAX_INDEX = 1;
        private Data data;
        private InputFromUser inputFromUser;
        private DataManager dataManager;

        public EntryMenu(Data data, InputFromUser inputFromUser, DataManager dataManager)
        {
            this.selectionIndex = 0;
            this.data = data;
            this.inputFromUser = inputFromUser;
            this.dataManager = dataManager;
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
                    Console.WriteLine("USER MENU");
                    Console.ReadKey();
                    break;
                
                case 1:
                    AdminLoginViewer adminLoginViewer = new AdminLoginViewer(data, inputFromUser);
                    adminLoginViewer.TryLogin();
                    
                    // Console.WriteLine("ADMIN MENU");
                    // Console.ReadKey();
                    break;
            }
        }
    }
}