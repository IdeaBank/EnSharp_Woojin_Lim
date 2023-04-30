using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.View.UserView;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Controller.UserController
{
    public class UserMenu : ControllerInterface
    {
        private int currentSelectionIndex;
        private string currentUserId;

        public UserMenu(TotalData data, CombinedManager combinedManager, int currentSelectionIndex, string currentUserId) : base(data, combinedManager)
        {
            this.currentSelectionIndex = currentSelectionIndex;
            this.currentUserId = currentUserId;
        }

        public void SelectUserMenu()
        {
            Console.Clear();
            // 메뉴 선택 결과를 저장하기 위한 변수 선언
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            // ESC키가 눌릴 때까지 반복
            while (result.Key != ResultCode.ESC_PRESSED)
            {
                // UI를 출력하고 메뉴를 선택함
                UserMenuView.PrintUserMenuContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.USER, MenuType.USER);

                // ESC키가 눌리면 반환
                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 현재 인덱스 값을 currentSelectionIndex에 저장
                this.currentSelectionIndex = result.Value;

                // 해당 인덱스의 메뉴로 이동
                if (EnterNextMenu())
                {
                    return;
                }

                Console.Clear();
            }
        }

        private bool EnterNextMenu()
        {
            // 책을 검색하기 위한 클래스의 인스턴스 생성
            BookSearcher bookSearcher = new BookSearcher(this.data, this.combinedManager);

            // 인덱스에 따라 다음 메뉴로 이동
            switch (this.currentSelectionIndex)
            {
                case MenuSelection.SEARCH_BOOK:
                    bookSearcher.SearchBook();
                    break;
                
                case MenuSelection.BORROW_BOOK:
                    // 책을 빌리기 전 검색창 출력
                    if (bookSearcher.SearchBook() == ResultCode.SUCCESS)
                    {
                        BorrowBook();
                    }
                    break;
                
                case MenuSelection.CHECK_BORROWED_BOOK:
                    CheckBorrowedBook();
                    break;
                
                case MenuSelection.RETURN_BOOK:
                    // 책을 반납하기 전 빌린 책 리스트 표시
                    CheckBorrowedBook();
                    ReturnBook();
                    break;
                
                case MenuSelection.CHECK_RETURNED_BOOK:
                    CheckReturnedBook();
                    break;
                
                case MenuSelection.EDIT_USER_INFORMATION:
                    EditUserInformation();
                    break;
                
                case MenuSelection.WITHDRAW:
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

            // 책 아이디를 입력하기 위한 변수 선언
            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, MaxInputLength.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 아이디가 입력되었고 숫자면
            if (bookIdInputResult.Value.Length > 0 && (UserInputManager.IsNumber(bookIdInputResult.Value)))
            {
                // 책을 빌리는 것을 시도하고 결과값 저장
                ResultCode borrowBookResult =
                    combinedManager.BookManager.BorrowBook(currentUserId, bookIdInputResult.Value);

                // 성공 여부에 따라 결과 출력
                if (borrowBookResult == ResultCode.SUCCESS)
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책을 성공적으로 빌렸습니다.");
                }

                else if (borrowBookResult == ResultCode.BOOK_NOT_ENOUGH)
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책이 부족합니다.");
                }

                else
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책이 존재하지 않습니다.");
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void CheckBorrowedBook()
        {
            Console.Clear();

            // 현재 유저가 빌린 책 리스트를 출력
            SearchBookOrUserView.PrintBorrowedBooks(currentUserId, combinedManager.BookManager.GetBorrowedBooks(currentUserId));
            
            Console.ReadKey(true);
        }

        private void ReturnBook()
        {
            UserMenuView.PrintBorrowOrReturnBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 저장하기 위한 변수 선언
            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, MaxInputLength.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키를 입력 받았으면 반환
            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED || bookIdInputResult.Value.Length == 0)
            {
                return;
            }

            // 아이디가 입력되었고 숫자면
            if (bookIdInputResult.Value.Length > 0 && UserInputManager.IsNumber(bookIdInputResult.Value))
            {
                // 책 반납을 시도하고 결과값 저장
                ResultCode returnBookResult =
                    combinedManager.BookManager.ReturnBook(bookIdInputResult.Value, currentUserId);

                // 책 반납 결과에 따라 값 출력
                if (returnBookResult == ResultCode.SUCCESS)
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책을 성공적으로 반납했습니다.");
                }

                else
                {
                    UserMenuView.PrintBorrowOrReturnBookResult("책이 존재하지 않습니다.");
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void CheckReturnedBook()
        {
            Console.Clear();
            
            // 반납한 책 리스트 출력
            SearchBookOrUserView.PrintReturnedBooks(currentUserId, combinedManager.BookManager.GetReturnedBooks(currentUserId));
            
            Console.ReadKey(true);
        }

        private void EditUserInformation()
        {
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[7];
            string[] warning_message = { "8~15글자 영어, 숫자포함", "8~15글자 영어, 숫자포함", "8~15글자 영어, 숫자포함", "영어, 한글 1개 이상", "1-200사이의 자연수", "01x-xxxx-xxxx", "[a]" };
            bool allRegexPassed = false;

            DataSet dataSet = combinedManager.UserManager.GetUser(currentUserId);

            // 각 입력 값을 저장하기 위한 변수 선언
            List<KeyValuePair<ResultCode, string>> inputs = new List<KeyValuePair<ResultCode, string>>();

            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["User"].Rows[0]["id"].ToString()));
            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["User"].Rows[0]["password"].ToString()));
            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["User"].Rows[0]["password"].ToString()));
            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["User"].Rows[0]["name"].ToString()));
            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, (DateTime.Now.Year - (int)dataSet.Tables["User"].Rows[0]["birth_year"] + 1).ToString()));
            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["User"].Rows[0]["phone_number"].ToString()));
            inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["User"].Rows[0]["address"].ToString()));
            
            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 일시 정지
                UserLoginOrRegisterView.PrintRegister(warning, inputs);
                Console.ReadKey();

                // 이후 경고 내용을 없앰
                warning = new string[7];
                UserLoginOrRegisterView.PrintRegister(warning, inputs);

                bool isInputValid = true;

                for (int i = 1; i < 7 && isInputValid; ++i)
                {
                    if (inputs[i].Key == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    ResultCode inputResult = UserInputManager.GetUserInformationInput(inputs, i);

                    switch (inputResult)
                    {
                        case ResultCode.ESC_PRESSED:
                            return;

                        case ResultCode.DO_NOT_MATCH_REGEX:
                            warning[i] = warning_message[i];
                            isInputValid = false;
                            break;

                        case ResultCode.DO_NOT_MATCH_PASSWORD:
                            warning[1] = warning[2] = "PASSWORD DO NOT MATCH!";
                            isInputValid = false;
                            break;

                        case ResultCode.SUCCESS:
                            break;
                    }
                }

                if (!isInputValid)
                {
                    continue;
                }

                allRegexPassed = true;
            }

            // 등록을 시도하고 결과값을 저장
            combinedManager.UserManager.EditUser(inputs[0].Value, inputs[1].Value, 
                inputs[3].Value, (DateTime.Now.Year - Int32.Parse(inputs[4].Value) + 1).ToString(),
                inputs[5].Value, inputs[6].Value);
            
            UserLoginOrRegisterView.PrintRegisterResult("EDIT SUCCESS!");
            Console.ReadKey(true);
        }

        private ResultCode Withdraw()
        {
            UserSelectionView.PrintYesOrNO("Are you sure to exit?");

            // 회원탈퇴 여부를 물어보고 Y키가 입력되었으면
            if (UserInputManager.InputYesOrNo() == ResultCode.YES)
            {
                // 탈퇴를 시도함. 그 결과가 성공이면
                if (combinedManager.UserManager.DeleteUser(currentUserId) == ResultCode.SUCCESS)
                {
                    // 결과 출력 후 결과 반환
                    UserSelectionView.PrintYesOrNO("Withdraw success!");
                    return ResultCode.SUCCESS;
                }

                // 탈퇴하지 못했다면 책을 반납하라고 출력 후 결과 반환
                UserSelectionView.PrintYesOrNO("You must return all books!");
                return ResultCode.MUST_RETURN_BOOK;
            }

            // 탈퇴하지 못했다면 결과 반환
            return ResultCode.NO;
        }
    }
}