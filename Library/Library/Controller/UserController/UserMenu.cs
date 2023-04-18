using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.View.UserView;

namespace Library.Controller.UserController
{
    public class UserMenu : ControllerInterface
    {
        private int currentSelectionIndex;
        private int currentUserIndex;

        public UserMenu(TotalData data, CombinedManager combinedManager, int currentSelectionIndex, int currentUserIndex) : base(data, combinedManager)
        {
            this.currentSelectionIndex = currentSelectionIndex;
            this.currentUserIndex = currentUserIndex;
        }

        public void SelectUserMenu()
        {
            Console.Clear();
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED)
            {
                UserMenuView.PrintUserMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.USER, MenuType.USER);

                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                this.currentSelectionIndex = result.Value;

                if (EnterNextMenu())
                {
                    return;
                }
                
                Console.Clear();
            }
        }

        private bool EnterNextMenu()
        {
            BookSearcher bookSearcher = new BookSearcher(this.data, this.combinedManager);
            switch (this.currentSelectionIndex)
            {
                case 0:
                    bookSearcher.SearchBook();
                    break;
                case 1:
                    bookSearcher.SearchBook();
                    BorrowBook();
                    break;
                case 2:
                    CheckBorrowedBook();
                    break;
                case 3:
                    CheckBorrowedBook();
                    ReturnBook();
                    break;
                case 4:
                    CheckReturnedBook();
                    break;
                case 5:
                    EditUserInformation();
                    break;
                case 6:
                    if (Withdraw() == ResultCode.SUCCESS)
                        return true;
                    break;
            }

            return false;
        }

        private void BorrowBook()
        {
            UserMenuView.PrintBorrowOrReturnBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, InputMax.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            if (bookIdInputResult.Value.Length > 0 && ('0' <= bookIdInputResult.Value[0] && bookIdInputResult.Value[0] <= '9'))
            {
                ResultCode borrowBookResult =
                    combinedManager.BookManager.BorrowBook(currentUserIndex, bookIdInputResult.Value[0] - '0');
                
                if (borrowBookResult == ResultCode.SUCCESS)
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책을 성공적으로 빌렸습니다.");
                }

                else if(borrowBookResult == ResultCode.BOOK_NOT_ENOUGH)
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책이 부족합니다.");
                }

                else
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책이 존재하지 않습니다.");
                }
                
                Console.ReadKey(true);
            }
        }

        private void CheckBorrowedBook()
        {
            SearchBookOrUserView.PrintBorrowedOrReturnedBooks(data.Users[currentUserIndex].Name, data.Users[currentUserIndex].BorrowedBooks);
            Console.ReadKey(true);
        }

        private void ReturnBook()
        {
            UserMenuView.PrintBorrowOrReturnBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, InputMax.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED || bookIdInputResult.Value.Length == 0)
            {
                return;
            }

            if ('0' <= bookIdInputResult.Value[0] && bookIdInputResult.Value[0] <= '9')
            {
                ResultCode returnBookResult =
                    combinedManager.BookManager.ReturnBook(currentUserIndex, bookIdInputResult.Value[0] - '0');
                
                if (returnBookResult == ResultCode.SUCCESS)
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책을 성공적으로 반납했습니다.");
                }

                else
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책이 존재하지 않습니다.");
                }
                
                Console.ReadKey(true);
            }
        }

        private void CheckReturnedBook()
        {
            SearchBookOrUserView.PrintBorrowedOrReturnedBooks(data.Users[this.currentUserIndex].Name, 
                data.Users[this.currentUserIndex].ReturnedBooks);
            Console.ReadKey(true);
        }

        private void EditUserInformation()
        {
            
        }

        private ResultCode Withdraw()
        {
            UserSelectionView.PrintYesOrNO("Are you sure to exit?");
            
            if (UserInputManager.InputYesOrNo() == ResultCode.YES)
            {
                if (combinedManager.UserManager.DeleteUser(data.Users[this.currentUserIndex].Number) == ResultCode.SUCCESS)
                {
                    UserSelectionView.PrintYesOrNO("Withdraw success!");
                    return ResultCode.SUCCESS;
                }

                
                UserSelectionView.PrintYesOrNO("You must return all books!");
                return ResultCode.MUST_RETURN_BOOK;
            }

            return ResultCode.NO;
        }
    }
}