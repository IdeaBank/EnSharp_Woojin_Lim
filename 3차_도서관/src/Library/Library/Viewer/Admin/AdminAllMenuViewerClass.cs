using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Exception;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.View.Admin;

namespace Library
{
    public class AdminAllMenuViewerClass: Viewer.ViewerClass
    {
        private int selectionIndex;
        
        public AdminAllMenuViewerClass(Data data, DataManager dataManager, InputFromUser inputFromUser): base(data, dataManager, inputFromUser)
        {
            this.selectionIndex = 0;
        }

        public void ShowAdminMenu()
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
                        Console.Clear();
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
                if (this.selectionIndex == ViewMaxIndex.ADMIN_MENU_MAX_INDEX)
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
                    SearchBookViewerClass searchBookViewerClass = new SearchBookViewerClass(data, dataManager, inputFromUser);
                    searchBookViewerClass.SearchBook();
                    break;
                
                case 1:
                    AdminAddBookViewerClass adminAddBookViewerClass = new AdminAddBookViewerClass(data, dataManager, inputFromUser);
                    adminAddBookViewerClass.AddBook();
                    break;
                
                case 2:
                    AdminRemoveBookViewerClass adminRemoveBookViewerClass =
                        new AdminRemoveBookViewerClass(data, dataManager, inputFromUser);
                    adminRemoveBookViewerClass.RemoveBookWithInput();
                    break;
                
                case 3:
                    AdminEditBookViewerClass adminEditBookViewerClass = 
                        new AdminEditBookViewerClass(data, dataManager, inputFromUser);
                    adminEditBookViewerClass.ShowEditBookView();
                    break;
                
                case 4:
                    AdminRemoveUserViewerClass adminRemoveUserViewerClass =
                        new AdminRemoveUserViewerClass(data, dataManager, inputFromUser);
                    adminRemoveUserViewerClass.RemoveUser();
                    break;
                
                case 5:
                    AdminAllBorrowedBooksViewer.ShowAllBorrowedBooks(data, dataManager);
                    Console.Clear();
                    break;
            }
        }
    }
}