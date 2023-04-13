using System;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View.Admin;

namespace Library.Viewer.User
{
    public class UserLoginOrRegisterViewerClass: ViewerClass
    {
        private int selectionIndex;

        public UserLoginOrRegisterViewerClass(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
            this.selectionIndex = 0;
        }
        
        public void LoginOrRegister()
        {
            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();
            AdminMenuView.PrintMenu(selectionIndex);

            while (keyInput.Key != ConsoleKey.Escape)
            {
                AdminMenuView.PrintMenu(selectionIndex);
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
                if (this.selectionIndex == ViewMaxIndex.USER_LOGIN_MENU_MAX_INDEX)
                {
                    return;
                }

                this.selectionIndex += 1;
            }
        }
        
        private void EnterNextMenu()
        {
            AdminAllMenuViewerClass adminAllMenuViewerClass = 
                new AdminAllMenuViewerClass(this.data, this.dataManager, this.inputFromUser);
            
            switch (selectionIndex)
            {
                case 0:
                    SearchBookViewerClass searchBookViewerClass = new SearchBookViewerClass(data, dataManager, inputFromUser);
                    searchBookViewerClass.SearchBook();
                    break;
                
                case 1:
                    AdminAddBookViewerClass adminAddBookViewerClass = new AdminAddBookViewerClass(data, dataManager, inputFromUser);
                    adminAddBookViewerClass.AddBook();
                    break;
            }
        }
    }
}