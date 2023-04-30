using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;
using Library.View.AdminView;
using Library.View.UserView;
using System;
using System.Collections.Generic;
using System.Data;

namespace Library.Controller.AdminController
{
    public class AdminMenu : ControllerInterface
    {
        private int currentSelectionIndex;
        private BookSearcher bookSearcher;

        // 생성자에서 현재 커서 위치를 0으로 초기화
        public AdminMenu(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
            this.bookSearcher = new BookSearcher(data, combinedManager);
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
                    if (bookSearcher.SearchBook() != ResultCode.ESC_PRESSED)
                    {
                        DeleteBook();
                    }
                    break;

                // 책을 수정하기 전 책 검색
                case MenuSelection.EDIT_BOOK:
                    if (bookSearcher.SearchBook() != ResultCode.ESC_PRESSED)
                    {
                        EditBook();
                    }
                    break;

                // 유저를 없애기 전에 유저 검색
                case MenuSelection.DELETE_MEMBER:
                    if (SearchMember() != ResultCode.ESC_PRESSED)
                    {
                        DeleteMember();
                    }
                    break;

                case MenuSelection.VIEW_BORROWED_BOOKS:
                    ViewBorrowedBooks();
                    break;
            }
        }

        private void AddBook()
        {
            // 각 입력 값을 저장하기 위한 변수 선언
            List<KeyValuePair<ResultCode, string>> inputs = new List<KeyValuePair<ResultCode, string>>();

            for (int i = 0; i < 8; ++i)
            {
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, ""));
            }

            if (InsertBookInfo(inputs) == ResultCode.ESC_PRESSED)
            {
                return;
            }
            
            combinedManager.BookManager.AddBook(inputs[0].Value, inputs[1].Value, inputs[2].Value, inputs[3].Value, inputs[4].Value, 
                inputs[5].Value, inputs[6].Value, inputs[7].Value);

            // 결과를 출력하고 유지시키기 위해 키를 입력 받음
            UserLoginOrRegisterView.PrintRegisterResult("BOOK ADDED!");
            Console.ReadKey(true);
        }

        private void EditBook()
        {
            AdminMenuView.PrintEditBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 입력 받음
            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, MaxInputLength.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 숫자가 입력되었을 시
            if (UserInputManager.IsNumber(bookIdInputResult.Value))
            {
                if (!combinedManager.BookManager.IsBookExist(bookIdInputResult.Value))
                {
                    UserLoginOrRegisterView.PrintRegisterResult("BOOK DOES NOT EXIST!");
                    Console.ReadKey(true);
                    return;
                }

                DataSet dataSet = combinedManager.BookManager.GetBook(bookIdInputResult.Value);

                // 각 입력 값을 저장하기 위한 변수 선언
                List<KeyValuePair<ResultCode, string>> inputs = new List<KeyValuePair<ResultCode, string>>();

                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["name"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["author"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["publisher"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["quantity"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["price"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["published_date"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["isbn"].ToString()));
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, dataSet.Tables["Book"].Rows[0]["description"].ToString()));

                if (InsertBookInfo(inputs) == ResultCode.ESC_PRESSED)
                {
                    return;
                }
            
                combinedManager.BookManager.EditBook(bookIdInputResult.Value, inputs[0].Value, inputs[1].Value, inputs[2].Value, inputs[3].Value, inputs[4].Value, inputs[5].Value, inputs[6].Value, inputs[7].Value);

                // 결과를 출력하고 유지시키기 위해 키를 입력 받음
                UserLoginOrRegisterView.PrintRegisterResult("BOOK EDITED!");
                Console.ReadKey(true);
            }
        }
        
        private ResultCode InsertBookInfo(List<KeyValuePair<ResultCode, string>> inputs)
        {
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[8];

            string[] warning_message = {
                "영어, 한글, 숫자, ?!+= 1개 이상", "영어, 한글 1개 이상", "영어, 한글 1개 이상", "1-999사이의 자연수",
                "1-9999999사이의 자연수", "19xx or 20xx-xx-xx", "정수9개 + 영어1개 + 공백 + 정수13개", "최소1개의 문자(공백포함)"
            };

            bool allRegexPassed = false;
            
            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 일시 정지
                AdminMenuView.PrintAddBook(warning, inputs);
                Console.ReadKey();

                // 이후 경고 내용을 없앰
                warning = new string[8];
                AdminMenuView.PrintAddBook(warning, inputs);

                bool isInputValid = true;

                for (int i = 0; i < 8 && isInputValid; ++i)
                {
                    if (inputs[i].Key == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    ResultCode inputResult = UserInputManager.GetBookInformationInput(inputs, i);

                    switch (inputResult)
                    {
                        case ResultCode.ESC_PRESSED:
                            return ResultCode.ESC_PRESSED;

                        case ResultCode.DO_NOT_MATCH_REGEX:
                            warning[i] = warning_message[i];
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

            return ResultCode.SUCCESS;
        }

        private void DeleteBook()
        {
            AdminMenuView.PrintDeleteBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 입력 받음
            KeyValuePair<ResultCode, string> bookIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, MaxInputLength.BOOK_ID, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (bookIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 숫자가 입력되었을 시
            if (UserInputManager.IsNumber(bookIdInputResult.Value))
            {
                // 숫자에 해당하는 아이디 값의 책 삭제를 시도하고 결과 출력
                if (combinedManager.BookManager.RemoveBook(bookIdInputResult.Value) == ResultCode.SUCCESS)
                {
                    AdminMenuView.PrintDeleteResult("책을 성공적으로 제거했습니다.");
                }

                else
                {
                    AdminMenuView.PrintDeleteResult("책이 존재하지 않습니다.");
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void DeleteMember()
        {
            SearchBookOrUserView.PrintDeleteUser();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 유저 번호를 입력 받음
            KeyValuePair<ResultCode, string> userIdInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, MaxInputLength.USER_ID_PASSWORD, InputParameter.IS_NOT_PASSWORD,
                InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (userIdInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            UserSelectionView.PrintYesOrNO("Are you sure to delete user "+ userIdInputResult.Value + "?");

            // 회원탈퇴 여부를 물어보고 Y키가 입력되었으면
            if (UserInputManager.InputYesOrNo() == ResultCode.YES)
            {
                if (userIdInputResult.Value.Length > 0)
                {
                    ResultCode deleteResult = combinedManager.UserManager.DeleteUser(userIdInputResult.Value);

                    // 성공했으면 결과 출력
                    if (deleteResult == ResultCode.SUCCESS)
                    {
                        AdminMenuView.PrintDeleteResult("사용자를 성공적으로 제거했습니다.");
                    }

                    else if (deleteResult == ResultCode.MUST_RETURN_BOOK)
                    {
                        AdminMenuView.PrintDeleteResult("책을 모두 반납해야 합니다!");
                    }

                    else
                    {
                        AdminMenuView.PrintDeleteResult("사용자가 존재하지 않습니다.");
                    }

                    // 일시 정지
                    Console.ReadKey(true);
                }
            }
        }

        private void ViewBorrowedBooks()
        {
            Console.Clear();

            DataSet dataSet = combinedManager.SqlManager.ExecuteSql("select * from User", "User");

            foreach (DataRow row in dataSet.Tables["User"].Rows)
            {
                DataSet bookData = combinedManager.SqlManager.ExecuteSql("select * from Borrowed_Book where user_id=\'" + row["id"] + "\'", "Borrowed_Book");
                SearchBookOrUserView.PrintBorrowedBooks(row["id"].ToString(), bookData);
            }

            Console.ReadKey();
        }

        private ResultCode SearchMember()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            SearchBookOrUserView.PrintSearchUser();

            // 유저 이름을 입력 받음
            KeyValuePair<ResultCode, string> nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf - 2, MaxInputLength.USER_NAME, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (nameInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            // 유저 아이디를 입력 받음
            KeyValuePair<ResultCode, string> idInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf - 1, MaxInputLength.USER_ID_PASSWORD, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (idInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            // 유저 주소를 입력 받음
            KeyValuePair<ResultCode, string> addressInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf - 0, MaxInputLength.USER_ADDRESS, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (idInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            // 유저 검색 결과를 저장
            DataSet searchUserResult = combinedManager.UserManager.SearchUser(nameInputResult.Value,
                idInputResult.Value, addressInputResult.Value);

            // 유저 검색 결과를 출력
            SearchBookOrUserView.ViewSearchUserResult(searchUserResult);

            // 키를 입력 받을때까지 출력 유지
            Console.ReadKey(true);

            return ResultCode.SUCCESS;
        }
    }
}