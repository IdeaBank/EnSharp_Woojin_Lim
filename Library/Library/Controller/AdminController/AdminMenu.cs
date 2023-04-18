using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.View.AdminView;
using Library.View.UserView;

namespace Library.Controller.AdminController
{
    public class AdminMenu: ControllerInterface
    {
        private int currentSelectionIndex;

        public AdminMenu(TotalData data, CombinedManager combinedManager): base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
        }
        
        public void SelectAdminMenu()
        {
            Console.Clear();
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            while (result.Key != ResultCode.ESC_PRESSED)
            {
                AdminMenuView.PrintAdminMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.ADMIN, MenuType.ADMIN);

                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                this.currentSelectionIndex = result.Value;

                EnterNextMenu();
                Console.Clear();
            }
        }

        private void EnterNextMenu()
        {
            BookSearcher bookSearcher = new BookSearcher(this.data, this.combinedManager);
            
            switch (this.currentSelectionIndex)
            {
                case 0:
                    bookSearcher.SearchBook();
                    break;
                case 1:
                    AddBook();
                    break;
                case 2:
                    bookSearcher.SearchBook();
                    DeleteBook();
                    break;
                case 3:
                    bookSearcher.SearchBook();
                    EditBook();
                    break;
                case 4:
                    SearchMember();
                    DeleteMember();
                    break;
                case 5:
                    ViewBorrowedBooks();
                    break;
            }
        }

        private void AddBook()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            string[] warning = new string[8];
            bool[] inputValid = { false, false, false, false, false, false, false, false };
            string[] previousInput = new string[8];
            bool allRegexPassed = false;
            
            KeyValuePair<ResultCode, string> 
                nameInputResult = new KeyValuePair<ResultCode, string>(),
                authorInputResult = new KeyValuePair<ResultCode, string>(),
                publisherInputResult = new KeyValuePair<ResultCode, string>(),
                quantityInputResult = new KeyValuePair<ResultCode, string>(),
                priceInputResult = new KeyValuePair<ResultCode, string>(),
                publishedDateInputResult = new KeyValuePair<ResultCode, string>(),
                isbnInputResult = new KeyValuePair<ResultCode, string>(),
                descriptionInputResult = new KeyValuePair<ResultCode, string>();

            while (!allRegexPassed)
            {
                Console.Clear();
                AdminMenuView.PrintAddBook(warning, previousInput);
                Console.ReadKey();
                Console.Clear();
                warning = new string[8];
                AdminMenuView.PrintAddBook(warning, previousInput);

                if (!inputValid[0])
                {
                    nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    if (nameInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_NAME, nameInputResult.Value))
                    {
                        warning[0] = "영어, 한글, 숫자, ?!+= 1개 이상";
                        continue;
                    }

                    previousInput[0] = nameInputResult.Value;
                    inputValid[0] = true;
                }

                if (!inputValid[1])
                {
                    authorInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 1, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    if (authorInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_AUTHOR,
                            authorInputResult.Value))
                    {
                        warning[1] = "영어, 한글 1개 이상";
                        continue;
                    }

                    previousInput[1] = authorInputResult.Value;
                    inputValid[1] = true;

                }
                
                if (!inputValid[2])
                {
                    publisherInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 2, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    if (publisherInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_PUBLISHER, publisherInputResult.Value))
                    {
                        warning[2] = "영어, 한글 1개 이상";
                        continue;
                    }

                    previousInput[2] = publisherInputResult.Value;
                    inputValid[2] = true;
                }
                
                if (!inputValid[3])
                {
                    quantityInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 3, InputMax.BOOK_QUANTITY, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    if (quantityInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_QUANTITY, quantityInputResult.Value))
                    {
                        warning[3] = "1-999사이의 자연수";
                        continue;
                    }

                    previousInput[3] = quantityInputResult.Value;
                    inputValid[3] = true;
                }


                if (!inputValid[4])
                {
                    priceInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 4, InputMax.BOOK_PRICE, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    if (priceInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_PRICE, priceInputResult.Value))
                    {
                        warning[4] = "1-9999999사이의 자연수";
                        continue;
                    }

                    previousInput[4] = priceInputResult.Value;
                    inputValid[4] = true;
                }

                if (!inputValid[5])
                {
                    publishedDateInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 5, InputMax.BOOK_PUBLISHED_DATE, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    if (publishedDateInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_PUBLISHED_DATE,
                            publishedDateInputResult.Value))
                    {
                        warning[5] = "19xx or 20xx-xx-xx";
                        continue;
                    }

                    previousInput[5] = publishedDateInputResult.Value;
                    inputValid[5] = true;
                }

                if (!inputValid[6])
                {
                    isbnInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 6, InputMax.BOOK_ISBN, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    if (isbnInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_ISBN, isbnInputResult.Value))
                    {
                        warning[6] = "정수9개 + 영어1개 + 공백 + 정수13개";
                        continue;
                    }

                    previousInput[6] = isbnInputResult.Value;
                    inputValid[6] = true;
                }
                
                if (!inputValid[7])
                {
                    descriptionInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 7, InputMax.BOOK_DESCRIPTION, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    if (descriptionInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_DESCRIPTION, descriptionInputResult.Value))
                    {
                        warning[7] = "최소1개의 문자(공백포함)";
                        continue;
                    }

                    previousInput[7] = descriptionInputResult.Value;
                    inputValid[7] = true;
                }

                allRegexPassed = true;
            }
            
            combinedManager.BookManager.AddBook(
                nameInputResult.Value, 
                authorInputResult.Value,
                publisherInputResult.Value,
                Int32.Parse(quantityInputResult.Value), 
                Int32.Parse(priceInputResult.Value), 
                publishedDateInputResult.Value, 
                isbnInputResult.Value, 
                descriptionInputResult.Value
                );
            
            UserLoginOrRegisterView.PrintRegisterResult("BOOK ADDED!");
            Console.ReadKey(true);
        }

        private void DeleteBook()
        {
            AdminMenuView.PrintDeleteBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            bool isInputValid = false;

            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, InputMax.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            if ('0' <= bookIdInputResult.Value[0] && bookIdInputResult.Value[0] <= '9')
            {
                if (combinedManager.BookManager.RemoveBook(bookIdInputResult.Value[0] - '0') == ResultCode.SUCCESS)
                {
                    AdminMenuView.PrintDeleteBookResult("책을 성공적으로 제거했습니다.");
                }

                else
                {
                    AdminMenuView.PrintDeleteBookResult("책이 존재하지 않습니다.");
                }

                Console.ReadKey(true);
            }
        }

        private void EditBook()
        {
            
        }

        private void DeleteMember()
        {
            SearchMember();
        }

        private void ViewBorrowedBooks()
        {
            
        }

        private void SearchMember()
        {
            SearchBookOrUserView.SearchUser();
            Console.ReadKey();
        }
    }
}