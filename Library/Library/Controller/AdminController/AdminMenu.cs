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

        // 생성자에서 현재 커서 위치를 0으로 초기화
        public AdminMenu(TotalData data, CombinedManager combinedManager): base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
        }
        
        public void SelectAdminMenu()
        {
            Console.Clear();
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            // ESC 입력 받을 때까지 반복
            while (result.Key != ResultCode.ESC_PRESSED)
            {
                // UI 출력
                AdminMenuView.PrintAdminMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.ADMIN, MenuType.ADMIN);

                // ESC키가 눌렸으면 반환
                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 커서 위치 저장
                this.currentSelectionIndex = result.Value;

                // 다음 메뉴 진입
                EnterNextMenu();
                Console.Clear();
            }
        }

        private void EnterNextMenu()
        {
            // 책 검색을 위한 클래스의 인스턴스 생성
            BookSearcher bookSearcher = new BookSearcher(this.data, this.combinedManager);
            
            // 현재 커서 위치에 따라 메뉴 진입
            switch (this.currentSelectionIndex)
            {
                case MenuSelection.SEARCH_BOOK:
                    bookSearcher.SearchBook();
                    break;
                case MenuSelection.ADD_BOOK:
                    AddBook();
                    break;
                // 책을 삭제하기 전 책 검색
                case MenuSelection.DELETE_BOOK:
                    bookSearcher.SearchBook();
                    DeleteBook();
                    break;
                case MenuSelection.EDIT_BOOK:
                    bookSearcher.SearchBook();
                    EditBook();
                    break;
                // 유저를 없애기 전에 유저 검색
                case MenuSelection.DELETE_MEMBER:
                    SearchMember();
                    DeleteMember();
                    break;
                case MenuSelection.VIEW_BORROWED_BOOKS:
                    ViewBorrowedBooks();
                    break;
            }
        }

        private void AddBook()
        {
            // 화면의 너비와 높이를 받아 옴
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[8];
            bool[] inputValid = { false, false, false, false, false, false, false, false };
            string[] previousInput = new string[8];
            bool allRegexPassed = false;
            
            // 각 입력 값을 저장하기 위한 변수 선언
            KeyValuePair<ResultCode, string> 
                nameInputResult = new KeyValuePair<ResultCode, string>(),
                authorInputResult = new KeyValuePair<ResultCode, string>(),
                publisherInputResult = new KeyValuePair<ResultCode, string>(),
                quantityInputResult = new KeyValuePair<ResultCode, string>(),
                priceInputResult = new KeyValuePair<ResultCode, string>(),
                publishedDateInputResult = new KeyValuePair<ResultCode, string>(),
                isbnInputResult = new KeyValuePair<ResultCode, string>(),
                descriptionInputResult = new KeyValuePair<ResultCode, string>();

            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 아무 키나 입력 받음
                Console.Clear();
                AdminMenuView.PrintAddBook(warning, previousInput);
                Console.ReadKey();
                Console.Clear();
                warning = new string[8];
                AdminMenuView.PrintAddBook(warning, previousInput);

                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 이름을 입력 받음
                if (!inputValid[0])
                {
                    nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (nameInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_NAME, nameInputResult.Value))
                    {
                        warning[0] = "영어, 한글, 숫자, ?!+= 1개 이상";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[0] = nameInputResult.Value;
                    inputValid[0] = true;
                }

                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 작가를 입력 받음
                if (!inputValid[1])
                {
                    authorInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 1, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (authorInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }
                    
                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_AUTHOR,
                            authorInputResult.Value))
                    {
                        warning[1] = "영어, 한글 1개 이상";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[1] = authorInputResult.Value;
                    inputValid[1] = true;
                }
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 출판사를 입력 받음
                if (!inputValid[2])
                {
                    publisherInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 2, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (publisherInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_PUBLISHER, publisherInputResult.Value))
                    {
                        warning[2] = "영어, 한글 1개 이상";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[2] = publisherInputResult.Value;
                    inputValid[2] = true;
                }
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 수량을 입력 받음
                if (!inputValid[3])
                {
                    quantityInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 3, InputMax.BOOK_QUANTITY, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (quantityInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_QUANTITY, quantityInputResult.Value))
                    {
                        warning[3] = "1-999사이의 자연수";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[3] = quantityInputResult.Value;
                    inputValid[3] = true;
                }

                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 가격을 입력 받음
                if (!inputValid[4])
                {
                    priceInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 4, InputMax.BOOK_PRICE, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (priceInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_PRICE, priceInputResult.Value))
                    {
                        warning[4] = "1-9999999사이의 자연수";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[4] = priceInputResult.Value;
                    inputValid[4] = true;
                }

                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 출판 날짜를 입력 받음
                if (!inputValid[5])
                {
                    publishedDateInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 5, InputMax.BOOK_PUBLISHED_DATE, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (publishedDateInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_PUBLISHED_DATE,
                            publishedDateInputResult.Value))
                    {
                        warning[5] = "19xx or 20xx-xx-xx";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[5] = publishedDateInputResult.Value;
                    inputValid[5] = true;
                }

                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 ISBN을 입력 받음
                if (!inputValid[6])
                {
                    isbnInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 6, InputMax.BOOK_ISBN, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (isbnInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_ISBN, isbnInputResult.Value))
                    {
                        warning[6] = "정수9개 + 영어1개 + 공백 + 정수13개";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[6] = isbnInputResult.Value;
                    inputValid[6] = true;
                }
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 설명을 입력 받음
                if (!inputValid[7])
                {
                    descriptionInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf + 7, InputMax.BOOK_DESCRIPTION, InputParameter.IS_NOT_PASSWORD,
                        InputParameter.ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (descriptionInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.BOOK_DESCRIPTION, descriptionInputResult.Value))
                    {
                        warning[7] = "최소1개의 문자(공백포함)";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[7] = descriptionInputResult.Value;
                    inputValid[7] = true;
                }

                // 모든 정규식에 부합하면 allRegexPassed에 true 값 저장
                allRegexPassed = true;
            }
            
            // 모든 정규식에 부합하면 책 추가
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
            
            // 결과를 출력하고 유지시키기 위해 키를 입력 받음
            UserLoginOrRegisterView.PrintRegisterResult("BOOK ADDED!");
            Console.ReadKey(true);
        }

        private void DeleteBook()
        {
            AdminMenuView.PrintDeleteBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 입력 받음
            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, InputMax.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 숫자가 입력되었을 시
            if ('0' <= bookIdInputResult.Value[0] && bookIdInputResult.Value[0] <= '9')
            {
                // 숫자에 해당하는 아이디 값의 책 삭제를 시도하고 결과 출력
                if (combinedManager.BookManager.RemoveBook(bookIdInputResult.Value[0] - '0') == ResultCode.SUCCESS)
                {
                    AdminMenuView.PrintDeleteBookResult("책을 성공적으로 제거했습니다.");
                }

                else
                {
                    AdminMenuView.PrintDeleteBookResult("책이 존재하지 않습니다.");
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void EditBook()
        {
            
        }

        private void DeleteMember()
        {
            
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