using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Model.DTO;
using Library.Utility;
using Library.View;
using System;
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;

namespace Library.Controller.User
{
    public class Menu
    {
        private int currentSelectionIndex;
        private string currentUserId;

        public Menu(int currentSelectionIndex, string currentUserId)
        {
            this.currentSelectionIndex = currentSelectionIndex;
            this.currentUserId = currentUserId;
        }

        public void SelectUserMenu()
        {
            Console.Clear();
            // 메뉴 선택 결과를 저장하기 위한 변수 선언
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);

            // ESC키가 눌릴 때까지 반복
            while (result.ResultCode != ResultCode.ESC_PRESSED)
            {
                // UI를 출력하고 메뉴를 선택함
                View.User.MenuView.getInstance.PrintUserMenuContour();
                result = MenuSelector.getInstance.ChooseMenu(0, Constant.Menu.Count.USER, Constant.Menu.Type.USER);

                // ESC키가 눌리면 반환
                if (result.ResultCode == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 현재 인덱스 값을 currentSelectionIndex에 저장
                this.currentSelectionIndex = result.ReturnedInt;

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
            // 인덱스에 따라 다음 메뉴로 이동
            switch (this.currentSelectionIndex)
            {
                case Constant.Menu.Selection.SEARCH_BOOK:
                    BookSearcher.getInstance.SearchBook();
                    break;

                case Constant.Menu.Selection.BORROW_BOOK:
                    // 책을 빌리기 전 검색창 출력
                    if (BookSearcher.getInstance.SearchBook() == ResultCode.SUCCESS)
                    {
                        BorrowBook();
                    }

                    break;

                case Constant.Menu.Selection.CHECK_BORROWED_BOOK:
                    CheckBorrowedBook();
                    break;

                case Constant.Menu.Selection.RETURN_BOOK:
                    // 책을 반납하기 전 빌린 책 리스트 표시
                    CheckBorrowedBook();
                    ReturnBook();
                    break;

                case Constant.Menu.Selection.CHECK_RETURNED_BOOK:
                    CheckReturnedBook();
                    break;

                case Constant.Menu.Selection.EDIT_USER_INFORMATION:
                    EditUserInformation();
                    break;

                case Constant.Menu.Selection.WITHDRAW:
                    if (Withdraw() == ResultCode.SUCCESS)
                    {
                        return true;
                    }
                    break;

                case Constant.Menu.Selection.REQUEST_BOOK:
                    SearchRequestBook();
                    break;
            }

            return false;
        }

        private void BorrowBook()
        {
            View.User.MenuView.getInstance.PrintBorrowOrReturnBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 입력하기 위한 변수 선언
            UserInput bookIdInputResult = UserInputManager.getInstance.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, Constant.Input.Max.BOOK_ID, Constant.Input.Parameter.IS_NOT_PASSWORD,
                Constant.Input.Parameter.CANNOT_ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (bookIdInputResult.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 아이디가 입력되었고 숫자면
            if (bookIdInputResult.Input.Length > 0 && (UserInputManager.getInstance.IsNumber(bookIdInputResult.Input)))
            {
                // 책을 빌리는 것을 시도하고 결과값 저장
                ResultCode borrowBookResult =
                    BookDAO.getInstance.BorrowBook(currentUserId, int.Parse(bookIdInputResult.Input));

                // 성공 여부에 따라 결과 출력
                if (borrowBookResult == ResultCode.SUCCESS)
                {
                    View.User.MenuView.getInstance.PrintBorrowOrReturnBookResult("책을 성공적으로 빌렸습니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "책 대출"));
                }

                else if (borrowBookResult == ResultCode.BOOK_NOT_ENOUGH)
                {
                    View.User.MenuView.getInstance.PrintBorrowOrReturnBookResult("책이 부족합니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "책이 부족함", "책 대출 시도"));
                }

                else
                {
                    View.User.MenuView.getInstance.PrintBorrowOrReturnBookResult("책이 존재하지 않습니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "책이 존재하지 않음", "책 대출 시도"));
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void CheckBorrowedBook()
        {
            Console.Clear();

            // 현재 유저가 빌린 책 리스트를 출력
            View.SearchResultView.getInstance.PrintBorrowedBooks(currentUserId,
                BookDAO.getInstance.GetBorrowedBooks(currentUserId));

            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "빌린 도서 조회"));
            
            Console.ReadKey(true);
        }

        private void ReturnBook()
        {
            View.User.MenuView.getInstance.PrintBorrowOrReturnBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 저장하기 위한 변수 선언
            UserInput bookIdInputResult = UserInputManager.getInstance.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf, Constant.Input.Max.BOOK_ID, Constant.Input.Parameter.IS_NOT_PASSWORD,
                Constant.Input.Parameter.CANNOT_ENTER_KOREAN);

            // ESC키를 입력 받았으면 반환
            if (bookIdInputResult.ResultCode == ResultCode.ESC_PRESSED || bookIdInputResult.Input.Length == 0)
            {
                return;
            }

            // 아이디가 입력되었고 숫자면
            if (bookIdInputResult.Input.Length > 0 && UserInputManager.getInstance.IsNumber(bookIdInputResult.Input))
            {
                // 책 반납을 시도하고 결과값 저장
                ResultCode returnBookResult =
                    BookDAO.getInstance.ReturnBook(currentUserId, int.Parse(bookIdInputResult.Input));

                // 책 반납 결과에 따라 값 출력
                if (returnBookResult == ResultCode.SUCCESS)
                {
                    View.User.MenuView.getInstance.PrintBorrowOrReturnBookResult("책을 성공적으로 반납했습니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "책 반납"));
                }

                else
                {
                    View.User.MenuView.getInstance.PrintBorrowOrReturnBookResult("책이 존재하지 않습니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "책이 존재하지 않음", "책 반납 시도"));
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void CheckReturnedBook()
        {
            Console.Clear();

            // 반납한 책 리스트 출력
            View.SearchResultView.getInstance.PrintReturnedBooks(currentUserId,
                BookDAO.getInstance.GetReturnedBooks(currentUserId));

            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "반납 도서 조회"));
            
            Console.ReadKey(true);
        }

        private void EditUserInformation()
        {
            Console.Clear();
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[7];

            bool allRegexPassed = false;

            UserDTO originalUser = UserDAO.getInstance.GetUserInfo(currentUserId);

            // 각 입력 값을 저장하기 위한 변수 선언
            List<UserInput> inputs = new List<UserInput>
            {
                // 기존 유저 정보를 넣어줌
                new UserInput(ResultCode.NO, originalUser.Id),
                new UserInput(ResultCode.NO, originalUser.Password),
                new UserInput(ResultCode.NO, originalUser.Password),
                new UserInput(ResultCode.NO, originalUser.Name),
                new UserInput(ResultCode.NO, (DateTime.Now.Year - originalUser.BirthYear + 1).ToString()),
                new UserInput(ResultCode.NO, originalUser.PhoneNumber),
                new UserInput(ResultCode.NO, originalUser.Address)
            };

            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 일시 정지
                View.User.LoginOrRegisterView.getInstance.PrintRegister(warning, inputs);
                Console.ReadKey();

                // 이후 경고 내용을 없앰
                warning = new string[7];
                View.User.LoginOrRegisterView.getInstance.PrintRegister(warning, inputs);

                bool isInputValid = true;

                for (int i = 1; i < 7 && isInputValid; ++i)
                {
                    // 제대로 입력 받은 값은 넘어감
                    if (inputs[i].ResultCode == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    ResultCode inputResult = UserInputManager.getInstance.GetUserInformationInput(inputs, i);

                    switch (inputResult)
                    {
                        // ESC키가 눌렸으면 이를 반환
                        case ResultCode.ESC_PRESSED:
                            return;

                        // 정규식에 맞지 않으면 경고 메세지 출력
                        case ResultCode.DO_NOT_MATCH_REGEX:
                            warning[i] = Constant.Input.Instruction.BOOK_INPUT_INSTRUCTION[i];
                            isInputValid = false;
                            break;

                        // 패스워드가 패스워드 확인과 다르면 경고 메세지 출력
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

            UserDTO user = new UserDTO();

            user.Id = inputs[0].Input;
            user.Password = inputs[1].Input;
            user.Name = inputs[3].Input;
            user.BirthYear = DateTime.Now.Year - int.Parse(inputs[4].Input) + 1;
            user.PhoneNumber = inputs[5].Input;
            user.Address = inputs[6].Input;

            // 등록을 시도
            UserDAO.getInstance.EditUser(user);

            View.User.LoginOrRegisterView.getInstance.PrintRegisterResult("성공적으로 수정했습니다!");
            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "유저 정보 수정"));
            Console.ReadKey(true);
        }

        private ResultCode Withdraw()
        {
            UserSelectionView.getInstance.PrintYesOrNO("정말 탈퇴하겠습니까?");

            // 회원탈퇴 여부를 물어보고 Y키가 입력되었으면
            if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
            {
                // 탈퇴를 시도함. 그 결과가 성공이면
                if (UserDAO.getInstance.DeleteUser(currentUserId) == ResultCode.SUCCESS)
                {
                    // 결과 출력 후 결과 반환
                    UserSelectionView.getInstance.PrintYesOrNO("성공적으로 탈퇴했습니다!");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "회원 탈퇴"));
                    Console.ReadKey();
                    return ResultCode.SUCCESS;
                }

                // 탈퇴하지 못했다면 책을 반납하라고 출력 후 결과 반환
                UserSelectionView.getInstance.PrintYesOrNO("책을 모두 반납해야합니다!");
                LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "책을 반납해야 함", "회원 탈퇴 시도"));
                Console.ReadKey();
                return ResultCode.MUST_RETURN_BOOK;
            }

            // 탈퇴하지 못했다면 결과 반환
            return ResultCode.NO;
        }

        private void SearchRequestBook()
        {
            bool isSearchInputValid = false;
            bool isInputValid = true;
            string[] searchHint = new string[2] { "", "" };

            List<UserInput> inputs = new List<UserInput>();

            for (int i = 0; i < Constant.Input.Count.REQUEST_BOOK; ++i)
            {
                inputs.Add(new UserInput(ResultCode.NO, ""));
            }


            while (!isSearchInputValid)
            {
                isInputValid = true;

                View.User.MenuView.getInstance.PrintRequestBook(searchHint[0], searchHint[1], inputs);

                for (int i = 0; i < Constant.Input.Count.REQUEST_BOOK; ++i)
                {
                    if (inputs[i].ResultCode == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    ResultCode inputResult = UserInputManager.getInstance.GetRequestBookInput(inputs, i);

                    if (inputResult == ResultCode.ESC_PRESSED)
                    {
                        ;
                        return;
                    }

                    if (inputResult == ResultCode.DO_NOT_MATCH_REGEX)
                    {
                        isInputValid = false;
                        searchHint[i] = Constant.Input.Instruction.USER_REQUEST_BOOK_INSTRUCTION[i];
                        break;
                    }
                }

                if (isInputValid)
                {
                    isSearchInputValid = true;
                }

                else
                {
                    View.User.MenuView.getInstance.PrintRequestBook(searchHint[0], searchHint[1], inputs);
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Console.CursorVisible = true;
                    searchHint[0] = searchHint[1] = "";
                }
            }

            RequestBook(inputs[0].Input, inputs[1].Input);

            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), currentUserId, "성공", "NAVER API 책 신청"));
        }

        public void RequestBook(string bookName, string bookCount)
        {
            JObject responseJSON = RestfulApiConnector.getInstance.GetResponseAsJObject(
                string.Format(Constant.ApiUrl.NAVER_API_ADDRESS, bookCount), bookName, "X-Naver-Client-Id",
                Constant.ApiUrl.NAVER_API_ID, "X-Naver-Client-Secret", Constant.ApiUrl.NAVER_API_PASSWORD);

            List<RequestedBookDTO> searchedBookList = new List<RequestedBookDTO>();

            foreach (JObject bookInfo in responseJSON["items"])
            {
                searchedBookList.Add(new RequestedBookDTO(bookInfo["isbn"].ToString(), 
                    bookInfo["title"].ToString(), bookInfo["author"].ToString(), bookInfo["publisher"].ToString(), 
                    (int)bookInfo["discount"], bookInfo["pubdate"].ToString(), bookInfo["description"].ToString()));
            }

            View.User.MenuView.getInstance.PrintSearchBookFromNaver(searchedBookList, bookName, bookCount);

            UserInput input = UserInputManager.getInstance.GetRequestBookIsbn();

            if(input.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine(new string('=', 25) + "등록 결과" + new string('=', 25));
            Console.WriteLine();

            foreach(RequestedBookDTO searchedBook in searchedBookList)
            {
                if(searchedBook.Isbn.Contains(input.Input))
                {
                    switch(BookDAO.getInstance.TryRequestBook(searchedBook))
                    {
                        case ResultCode.ALREADY_IN_DATABASE:
                            ConsoleWriter.getInstance.PrintWarning(searchedBook.Isbn + "가 ISBN인 책이 이미 도서관에 있습니다!");
                            break;
                        case ResultCode.ALREADY_REQUESTED:
                            ConsoleWriter.getInstance.PrintWarning(searchedBook.Isbn + "가 ISBN인 책이 이미 신청되어 있습니다!");
                            break;
                        case ResultCode.SUCCESS:
                            Console.WriteLine(searchedBook.Isbn + "가 ISBN인 책을 성공적으로 등록하였습니다!");
                            break;
                    }
                }
            }

            Console.ReadKey(true);
        }
    }
}