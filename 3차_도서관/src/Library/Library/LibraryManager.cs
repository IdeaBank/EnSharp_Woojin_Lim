using System;
using System.Collections.Generic;
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
        public Data data { get; set; }
        private int currentViewPosition;
        private int currentIndex;
        private User currentUser;
        private UserManagement userManagement;
        private BookManagement bookManagement;

        public LibraryManager()
        {
            data = new Data();
            currentViewPosition = 1;
            currentIndex = 0;
            userManagement = new UserManagement();
            bookManagement = new BookManagement();
        }

        public void start()
        {
            MainMenuView.Print(currentIndex);

            while (currentViewPosition != VIEW.EXIT_VIEW)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
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

                                Console.SetCursorPosition(20, 0);
                                string adminId = Console.ReadLine();
                                Console.SetCursorPosition(20, 1);
                                string adminPassword = Console.ReadLine();

                                if (userManagement.LoginAsAdministrator(data, adminId, adminPassword))
                                {
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.ADMIN_MENU_VIEW;
                                    AdminMenuView.Print(currentIndex);
                                }

                                else
                                {
                                    currentIndex = 0;
                                    Console.ReadKey();
                                    currentViewPosition = VIEW.MAIN_MENU_VIEW;
                                    MainMenuView.Print(currentIndex);
                                }
                            }
                        }

                        else if (currentViewPosition == VIEW.USER_START_VIEW)
                        {
                            if (currentIndex == 0)
                            {
                                currentIndex = 0;
                                currentViewPosition = VIEW.USER_LOGIN_VIEW;
                                UserLoginView.Print();

                                Console.SetCursorPosition(20, 0);
                                string userId = Console.ReadLine();
                                Console.SetCursorPosition(20, 1);
                                string userPassword = Console.ReadLine();
                                KeyValuePair<bool, User> loginResult =
                                    userManagement.LoginAsUser(data, userId, userPassword);

                                if (loginResult.Key)
                                {
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_MENU_VIEW;
                                    UserMenuView.Print(currentIndex);
                                    currentUser = loginResult.Value;
                                }

                                else
                                {
                                    currentIndex = 0;
                                    Console.ReadKey();
                                    currentViewPosition = VIEW.USER_START_VIEW;
                                    UserStartView.Print(currentIndex);
                                }
                            }

                            else
                            {
                                currentIndex = 0;
                                currentViewPosition = VIEW.USER_REGISTER_VIEW;
                                UserRegisterView.Print();
                            }
                        }

                        else if (currentViewPosition == VIEW.USER_MENU_VIEW)
                        {
                            switch (currentIndex)
                            {
                                case 0:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_SEARCH_BOOK_VIEW;
                                    UserSearchBookView.Print();
                                    Console.SetCursorPosition(11, 0);
                                    string name = Console.ReadLine();
                                    Console.SetCursorPosition(11, 1);
                                    string author = Console.ReadLine();
                                    Console.SetCursorPosition(11, 2);
                                    string publisher = Console.ReadLine();

                                    List<Book> books = bookManagement.SearchBook(data, name, author, publisher);
                                    UserSearchBookView.Print(books);

                                    currentViewPosition = VIEW.USER_MENU_VIEW;
                                    UserMenuView.Print(currentIndex);
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
                                    UserReturnBookView.Print();
                                    break;

                                case 4:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_RETURNED_BOOK_LIST_VIEW;
                                    UserReturnedBookListView.Print(currentUser);
                                    break;

                                case 5:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_MODIFY_MEMBER_VIEW;
                                    UserModifyMemberView.Print(currentUser);
                                    break;

                                case 6:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_WITHDRAW_VIEW;
                                    UserWithdrawView.Print();
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
                                    AdminSearchBookView.Print();

                                    Console.SetCursorPosition(15, 0);
                                    string name = Console.ReadLine();
                                    Console.SetCursorPosition(15, 1);
                                    string author = Console.ReadLine();
                                    Console.SetCursorPosition(15, 2);
                                    string publisher = Console.ReadLine();

                                    List<Book> books = bookManagement.SearchBook(data, name, author, publisher);
                                    AdminSearchBookView.Print(books);
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
                                    AdminModifyBookView.Print();

                                    int targetBookId = 1;
                                    AdminModifyBookView.Print(data, targetBookId);

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
                        if (currentViewPosition / 10 == 20)
                        {
                            currentIndex = 0;
                            currentViewPosition = VIEW.USER_MENU_VIEW;
                            UserMenuView.Print(currentIndex);
                        }

                        else if (currentViewPosition / 10 == 21)
                        {
                            currentIndex = 0;
                            currentViewPosition = VIEW.ADMIN_MENU_VIEW;
                            AdminMenuView.Print(currentIndex);
                        }

                        else
                        {
                            switch (currentViewPosition)
                            {
                                case 1:
                                    return;
                                case 10:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.MAIN_MENU_VIEW;
                                    MainMenuView.Print(currentIndex);
                                    break;
                                case 11:
                                case 12:
                                case 20:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.USER_START_VIEW;
                                    UserStartView.Print(currentIndex);
                                    break;
                                case 13:
                                case 21:
                                    currentIndex = 0;
                                    currentViewPosition = VIEW.MAIN_MENU_VIEW;
                                    MainMenuView.Print(currentIndex);
                                    break;
                                default:
                                    return;
                            }
                        } 
                        break;
                }
            }
        }
    }
}