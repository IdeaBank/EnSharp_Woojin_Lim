using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Model.DTO;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.Controller.Admin
{
    public class Menu
    {
        private int currentSelectionIndex;
        private LogController logController;

        public Menu()
        {
            this.currentSelectionIndex = 0;
            this.logController = new LogController();
        }

        public void SelectAdminMenu()
        {
            Console.Clear();
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);

            // ESC 입력 받을 때까지 반복
            while (result.ResultCode != ResultCode.ESC_PRESSED)
            {
                // UI 출력
                View.Admin.MenuView.getInstance.PrintAdminMenuContour();
                result = MenuSelector.getInstance.ChooseMenu(0, Constant.Menu.Count.ADMIN, Constant.Menu.Type.ADMIN);

                // ESC키가 눌렸으면 반환
                if (result.ResultCode == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 커서 위치 저장
                this.currentSelectionIndex = result.ReturnedInt;

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
                case Constant.Menu.Selection.SEARCH_BOOK:
                    BookSearcher.getInstance.SearchBook();
                    break;

                case Constant.Menu.Selection.ADD_BOOK:
                    AddBook();
                    break;

                // 책을 삭제하기 전 책 검색
                case Constant.Menu.Selection.DELETE_BOOK:
                    if (BookSearcher.getInstance.SearchBook() != ResultCode.ESC_PRESSED)
                    {
                        DeleteBook();
                    }

                    break;

                // 책을 수정하기 전 책 검색
                case Constant.Menu.Selection.EDIT_BOOK:
                    if (BookSearcher.getInstance.SearchBook() != ResultCode.ESC_PRESSED)
                    {
                        EditBook();
                    }

                    break;

                // 유저를 없애기 전에 유저 검색
                case Constant.Menu.Selection.DELETE_MEMBER:
                    if (SearchMember() != ResultCode.ESC_PRESSED)
                    {
                        DeleteMember();
                    }

                    break;

                case Constant.Menu.Selection.VIEW_BORROWED_BOOKS:
                    ViewBorrowedBooks();
                    break;
                
                case Constant.Menu.Selection.ADD_FROM_REQUESTED_BOOK:
                    AddRequestedBook();
                    break;
                
                case Constant.Menu.Selection.CONTROL_LOG:
                    logController.SelectLogMenu();
                    break;
                    
            }
        }

        private void AddBook()
        {
            // 각 입력 값을 저장하기 위한 변수 선언
            List<UserInput> inputs = new List<UserInput>();

            // 책 정보를 저장하기 위해 8칸을 빈 값으로 새로 채워줌
            for (int i = 0; i < Constant.Input.Count.ADD_BOOK; ++i)
            {
                inputs.Add(new UserInput(ResultCode.NO, ""));
            }

            // 책 정보를 입력 받음. ESC키가 눌렸으면 return
            if (InsertBookInfo(inputs) == ResultCode.ESC_PRESSED)
            {
                return;
            }

            BookDTO newBook = new BookDTO(inputs[0].Input, inputs[1].Input, inputs[2].Input, int.Parse(inputs[3].Input),
                int.Parse(inputs[4].Input), inputs[5].Input, inputs[6].Input, inputs[7].Input);

            // 책 정보를 데이터베이스에 넣어줌
            BookDAO.getInstance.AddBook(newBook);

            // 결과를 출력하고 유지시키기 위해 키를 입력 받음
            View.User.LoginOrRegisterView.getInstance.PrintRegisterResult("BOOK ADDED!");
            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "성공", "책 추가"));
            Console.ReadKey(true);
        }

        private void EditBook()
        {
            // 책 정보를 수정하는 UI를 출력해 줌
            View.Admin.MenuView.getInstance.PrintEditBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 입력 받음
            UserInput bookIdInputResult = UserInputManager.getInstance.ReadInputFromUser(
                windowWidthHalf,
                windowHeightHalf, Constant.Input.Max.BOOK_ID, Constant.Input.Parameter.IS_NOT_PASSWORD,
                Constant.Input.Parameter.CANNOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (bookIdInputResult.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 숫자가 입력되었을 시
            if (UserInputManager.getInstance.IsNumber(bookIdInputResult.Input))
            {
                // 책이 존재하지 않으면 경고창을 띄워줌
                if (!BookDAO.getInstance.IsBookExists(int.Parse(bookIdInputResult.Input)))
                {
                    View.User.LoginOrRegisterView.getInstance.PrintRegisterResult("BOOK DOES NOT EXIST!");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "책이 존재하지 않음", "책 수정"));
                    Console.ReadKey(true);
                    return;
                }

                // 책 정보를 얻어옴
                BookDTO originalBook = BookDAO.getInstance.GetBookInfo(int.Parse(bookIdInputResult.Input));

                // 각 입력 값을 저장하기 위한 변수 선언
                List<UserInput> inputs = new List<UserInput>
                {
                    // 기존 책 정보를 넣어줌
                    new UserInput(ResultCode.NO, originalBook.Name),
                    new UserInput(ResultCode.NO, originalBook.Author),
                    new UserInput(ResultCode.NO, originalBook.Publisher),
                    new UserInput(ResultCode.NO, originalBook.Quantity.ToString()),
                    new UserInput(ResultCode.NO, originalBook.Price.ToString()),
                    new UserInput(ResultCode.NO, originalBook.PublishedDate),
                    new UserInput(ResultCode.NO, originalBook.Isbn),
                    new UserInput(ResultCode.NO, originalBook.Description)
                };

                // 책 정보를 입력 받음
                if (InsertBookInfo(inputs) == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                BookDTO updatedBook = new BookDTO
                {
                    Name = inputs[0].Input,
                    Author = inputs[1].Input,
                    Publisher = inputs[2].Input,
                    Quantity = int.Parse(inputs[3].Input),
                    Price = int.Parse(inputs[4].Input),
                    PublishedDate = inputs[5].Input,
                    Isbn = inputs[6].Input,
                    Description = inputs[7].Input
                };

                // 데이터베이스에 수정된 결과를 넣어줌
                BookDAO.getInstance.EditBook(updatedBook);

                // 결과를 출력하고 유지시키기 위해 키를 입력 받음
                View.User.LoginOrRegisterView.getInstance.PrintRegisterResult("BOOK EDITED!");
                LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "성공", "책 수정"));
                Console.ReadKey(true);
            }
        }

        private ResultCode InsertBookInfo(List<UserInput> inputs)
        {
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[8];

            bool allRegexPassed = false;

            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 일시 정지
                View.Admin.MenuView.getInstance.PrintAddBook(warning, inputs);
                Console.ReadKey();

                // 이후 경고 내용을 없앰
                warning = new string[8];
                View.Admin.MenuView.getInstance.PrintAddBook(warning, inputs);

                bool isInputValid = true;

                for (int i = 0; i < 8 && isInputValid; ++i)
                {
                    // 제대로 입력 받은 값들은 넘어감
                    if (inputs[i].ResultCode == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    // 책 정보를 입력 받음
                    ResultCode inputResult = UserInputManager.getInstance.GetBookInformationInput(inputs, i);

                    switch (inputResult)
                    {
                        // ESC키가 눌렸으면 이를 return함
                        case ResultCode.ESC_PRESSED:
                            return ResultCode.ESC_PRESSED;

                        // 정규표현식에 맞지 않으면 경고 메세지를 띄워줌
                        case ResultCode.DO_NOT_MATCH_REGEX:
                            warning[i] = Constant.Input.Instruction.BOOK_INPUT_INSTRUCTION[i];
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
            View.Admin.MenuView.getInstance.PrintDeleteBook();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 책 아이디를 입력 받음
            UserInput bookIdInputResult = UserInputManager.getInstance.ReadInputFromUser(
                windowWidthHalf,
                windowHeightHalf, Constant.Input.Max.BOOK_ID, Constant.Input.Parameter.IS_NOT_PASSWORD,
                Constant.Input.Parameter.CANNOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (bookIdInputResult.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }

            // 숫자가 입력되었을 시
            if (UserInputManager.getInstance.IsNumber(bookIdInputResult.Input))
            {
                // 숫자에 해당하는 아이디 값의 책 삭제를 시도하고 결과 출력
                if (BookDAO.getInstance.RemoveBook(int.Parse(bookIdInputResult.Input)) == ResultCode.SUCCESS)
                {
                    View.Admin.MenuView.getInstance.PrintDeleteResult("책을 성공적으로 제거했습니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "성공", "책 제거"));
                }

                else
                {
                    View.Admin.MenuView.getInstance.PrintDeleteResult("책이 존재하지 않습니다.");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "책이 존재하지 않음", "책 제거"));
                }

                // 일시 정지
                Console.ReadKey(true);
            }
        }

        private void DeleteMember()
        {
            Console.Clear();
            View.SearchResultView.getInstance.PrintDeleteUser();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // 유저 번호를 입력 받음
            UserInput userIdInputResult = UserInputManager.getInstance.ReadInputFromUser(
                windowWidthHalf,
                windowHeightHalf, Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                Constant.Input.Parameter.CANNOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (userIdInputResult.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }

            View.UserSelectionView.getInstance.PrintYesOrNO("유저 : " + userIdInputResult.Input + "를 삭제하시겠습니까?");

            // 회원 삭제 여부를 물어보고 Y키가 입력되었으면
            if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
            {
                // 바로 엔터를 누른 것이 아니면
                if (userIdInputResult.Input.Length > 0)
                {
                    // 삭제를 시도
                    ResultCode deleteResult = UserDAO.getInstance.DeleteUser(userIdInputResult.Input);

                    switch (deleteResult)
                    {
                        // 성공했으면 결과 출력
                        case ResultCode.SUCCESS:
                            View.Admin.MenuView.getInstance.PrintDeleteResult("사용자를 성공적으로 제거했습니다.");
                            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "성공, " + userIdInputResult.Input, "유저 제거"));
                            break;
                        // 실패했으면 그 이유를 출력
                        case ResultCode.MUST_RETURN_BOOK:
                            View.Admin.MenuView.getInstance.PrintDeleteResult("책을 모두 반납해야 합니다!");
                            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "책을 반납해야 함", "유저 제거"));
                            break;
                        default:
                            View.Admin.MenuView.getInstance.PrintDeleteResult("사용자가 존재하지 않습니다.");
                            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "사용자가 존재하지 않음", "유저 제거"));
                            break;
                    }

                    // 일시 정지
                    Console.ReadKey(true);
                }
            }
        }

        private void ViewBorrowedBooks()
        {
            Console.Clear();

            // 유저 정보를 모두 불러옴
            List<UserDTO> users = UserDAO.getInstance.GetAllUsers();

            // 유저 아이디에 해당하는 값을 Borrowed_Book에서 찾아보고 그 결과를 출력
            foreach (UserDTO user in users)
            {
                List<BorrowedBookDTO> bookData =
                    BookDAO.getInstance.GetBorrowedBooks(user.Id);
                View.SearchResultView.getInstance.PrintBorrowedBooks(user.Id, bookData);
            }
            
            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "성공", "빌린 책 조회"));

            Console.ReadKey();
        }

        private ResultCode SearchMember()
        {
            Console.Clear();

            View.SearchResultView.getInstance.PrintSearchUser();
            View.SearchResultView.getInstance.ViewSearchUserResult(UserDAO.getInstance.GetAllUsers());

            List<UserInput> inputs = new List<UserInput>();

            for (int i = 0; i < Constant.Input.Count.SEARCH_MEMBER; ++i)
            {
                inputs.Add(new UserInput(ResultCode.NO, ""));
                UserInputManager.getInstance.GetSearchUserInput(inputs, i);

                if (inputs[i].ResultCode == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }
            }

            // 유저 검색 결과를 저장
            List<UserDTO> searchUserResult = UserDAO.getInstance.SearchUser(inputs[0].Input,
                inputs[1].Input, inputs[2].Input);

            // 유저 검색 결과를 출력
            Console.Clear();
            View.SearchResultView.getInstance.ViewSearchUserResult(searchUserResult);

            // 키를 입력 받을때까지 출력 유지
            Console.ReadKey(true);

            return ResultCode.SUCCESS;
        }

        private void AddRequestedBook()
        {
            List<RequestedBookDTO> requestedBooks = BookDAO.getInstance.GetAllRequestedBooks();
            
            View.Admin.MenuView.getInstance.PrintRequestedBooks(requestedBooks);
            
            UserInput input = UserInputManager.getInstance.GetRequestBookIsbn();

            if(input.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }
            
            Console.Clear();
            Console.WriteLine(new string('=', 25) + "등록 결과" + new string('=', 25));
            Console.WriteLine();

            foreach(RequestedBookDTO requestedBook in requestedBooks)
            {
                if(requestedBook.Isbn.Contains(input.Input))
                {
                    switch(BookDAO.getInstance.TryAddRequestedBook(requestedBook))
                    {
                        case ResultCode.SUCCESS:
                            Console.WriteLine(requestedBook.Isbn + "가 ISBN인 책을 성공적으로 등록하였습니다!");
                            break;
                    }
                }
            }

            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "책 등록", "NAVER 책 등록"));
            Console.ReadKey(true);
        }
    }
}