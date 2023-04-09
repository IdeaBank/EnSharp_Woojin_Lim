using System;
using System.Diagnostics;
using Library.Constants;
using Library.Model;
using Library.View;
using Library.View.Admin;
using Library.View.User;

namespace Library
{
    public class LibraryManager
    {
        private Data data;
        private int currentViewPosition;
        private int currentIndex;
        private User currentUser;

        public LibraryManager()
        {
            data = new Data();
            currentViewPosition = 1;
            currentIndex = 0;
        }

        public void start()
        {
            MainMenuView.Print(currentIndex);
            
            while (currentViewPosition != VIEW.EXIT_VIEW)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                
                switch(keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        switch (currentViewPosition)
                        {
                            case VIEW.MAIN_MENU_VIEW:
                                if (currentIndex < 1)
                                {
                                    currentIndex += 1;
                                    MainMenuView.Print(currentIndex);
                                }
                                break;
                            
                            case VIEW.USER_START_VIEW:
                                if (currentIndex < 1)
                                {
                                    currentIndex += 1;
                                    UserStartView.Print(currentIndex);
                                }
                                break;
                            
                            case VIEW.USER_MENU_VIEW:
                                if (currentIndex < 5)
                                {
                                    currentIndex += 1;
                                    UserMenuView.Print(currentIndex);
                                }
                                break;
                            
                            case VIEW.ADMIN_MENU_VIEW:
                                if (currentIndex < 5)
                                {
                                    currentIndex += 1;
                                    AdminMenuView.Print(currentIndex);
                                }
                                break;
                        }
                        break;
                    
                    case ConsoleKey.UpArrow:
                        switch (currentViewPosition)
                        {
                            case VIEW.MAIN_MENU_VIEW:
                                if (currentIndex > 0)
                                {
                                    currentIndex -= 1;
                                    MainMenuView.Print(currentIndex);
                                }
                                break;
                            
                            case VIEW.USER_START_VIEW:
                                if (currentIndex > 0)
                                {
                                    currentIndex -= 1;
                                    UserStartView.Print(currentIndex);
                                }
                                break;
                            
                            case VIEW.USER_MENU_VIEW:
                                if (currentIndex > 0)
                                {
                                    currentIndex -= 1;
                                    UserMenuView.Print(currentIndex);
                                }
                                break;
                            
                            case VIEW.ADMIN_MENU_VIEW:
                                if (currentIndex > 0)
                                {
                                    currentIndex -= 1;
                                    AdminMenuView.Print(currentIndex);
                                }
                                break;
                        }
                        break;
                    
                    case ConsoleKey.Enter:
                        
                        if (currentViewPosition == VIEW.MAIN_MENU_VIEW)
                        {
                            if (currentIndex == 0)
                            {
                                currentIndex = 0;
                                currentViewPosition = VIEW.USER_START_VIEW;
                                UserStartView.Print(currentIndex);
                            }

                            else
                            {
                                currentIndex = 0;
                                currentViewPosition = VIEW.ADMIN_LOGIN_VIEW;
                                AdminLoginView.Print();
                            }
                        }
                        
                        else if (currentViewPosition == VIEW.USER_START_VIEW)
                        {
                            if (currentIndex == 0)
                            {
                                currentIndex = 0;
                                currentViewPosition = VIEW.USER_LOGIN_VIEW;
                                UserLoginView.Print();
                            }
                            
                            else
                            {
                                currentIndex = 0;
                                currentViewPosition = VIEW.USER_REGISTER_VIEW;
                                //UserRegisterView.Print();
                            }
                        }
                        
                        else if (currentViewPosition == VIEW.USER_MENU_VIEW)
                        {
                            switch (currentIndex)
                            {
                                case 0:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_SEARCH_BOOK_VIEW;
                                    UserSearchBookView.Print(data);
                                    break;
                                
                                case 1:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_BORROW_BOOK_VIEW;
                                    UserBorrowBookView.Print();
                                    break;
                                
                                case 2:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_BORROWED_BOOK_LIST_VIEW;
                                    UserBorrowedBookListView.Print(currentUser);
                                    break;
                                
                                case 3:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_RETURN_BOOK_VIEW;
                                    //UserReturnBookView.Print(currentUser);
                                    break;
                                
                                case 4:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_RETURNED_BOOK_LIST_VIEW;
                                    //UserReturnedBookListView.Print(currentUser);
                                    break;
                                
                                case 5:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_MODIFY_MEMBER_VIEW;
                                    UserModifyMemberView.Print(currentUser);
                                    break;
                                
                                case 6:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_WITHDRAW_VIEW;
                                    //UserWithdrawView.Print();
                                    break;
                            }
                        }

                        else if (currentViewPosition == VIEW.ADMIN_MENU_VIEW)
                        {
                            switch (currentIndex)
                            {
                                case 0:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_SEARCH_BOOK_VIEW;
                                    AdminSearchBookView.Print(data);
                                    break;
                                
                                case 1:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_ADD_BOOK_VIEW;
                                    AdminAddBookView.Print();
                                    break;
                                
                                case 2:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_DELETE_BOOK_VIEW;
                                    AdminDeleteBookView.Print();
                                    break;
                                
                                case 3:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_MODIFY_BOOK_VIEW;
                                    //AdminModifyBookView.Print(data, currentBook);
                                    break;
                                
                                case 4:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_DELETE_MEMBER_VIEW;
                                    AdminDeleteMemberView.Print();
                                    break;
                                
                                case 5:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_ALL_BORROWED_BOOK_LIST_VIEW;
                                    AdminAllBorrowedBookListView.Print(data);
                                    break;
                            }
                        }
                        break;
                    
                    case ConsoleKey.Escape:
                        break;
                }
            }
        }
    }
}