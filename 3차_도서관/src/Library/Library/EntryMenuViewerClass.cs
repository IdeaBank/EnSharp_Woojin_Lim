using System;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.Utility;
using Library.Viewer.User;

namespace Library
{
    public class EntryMenuViewerClass: Viewer.ViewerClass
    {
        private int selectionIndex;

        public EntryMenuViewerClass(Data data, InputFromUser inputFromUser, DataManager dataManager): base(data, dataManager, inputFromUser)
        {
            this.selectionIndex = 0;
        }
        
        public void ViewEntryMenu()
        {
            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();

            while (keyInput.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                MainMenuView.PrintMenu(selectionIndex);
                
                keyInput = Console.ReadKey(true);
                
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
                if (this.selectionIndex == ViewMaxIndex.ENTRY_MENU_MAX_INDEX)
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
                    UserLoginOrRegisterViewerClass userLoginOrRegisterViewerClass =
                        new UserLoginOrRegisterViewerClass(data, dataManager, inputFromUser);
                    userLoginOrRegisterViewerClass.LoginOrRegister();
                    break;
                
                case 1:
                    AdminLoginViewer adminLoginViewer = new AdminLoginViewer(data, dataManager, inputFromUser);
                    adminLoginViewer.TryAdminLogin();
                    break;
            }
        }
    }
}