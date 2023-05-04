using System;
using System.Collections.Generic;
using System.Data;
using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Model.DTO;
using Library.Utility;

namespace Library.Controller.Admin
{
    public class Menu
    {
        private int currentSelectionIndex;

        public Menu()
        {
            this.currentSelectionIndex = 0;
        }

        public void SelectAdminMenu()
        {
            Console.Clear();
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);

            // ESC 입력 받을 때까지 반복
            while (result.ResultCode != ResultCode.ESC_PRESSED)
            {
                // UI 출력
                AdminMenuView.PrintAdminMenuContour();
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
                    bookSearcher.SearchBook();
                    break;

                case Constant.Menu.Selection.ADD_BOOK:
                    AddBook();
                    break;

                // 책을 삭제하기 전 책 검색
                case Constant.Menu.Selection.DELETE_BOOK:
                    if (bookSearcher.SearchBook() != ResultCode.ESC_PRESSED)
                    {
                        DeleteBook();
                    }

                    break;

                // 책을 수정하기 전 책 검색
                case Constant.Menu.Selection.EDIT_BOOK:
                    if (bookSearcher.SearchBook() != ResultCode.ESC_PRESSED)
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
            }
        }

        private void AddBook()
        {
            // 각 입력 값을 저장하기 위한 변수 선언
            List<UserInput> inputs = new List<UserInput>();

            // 책 정보를 저장하기 위해 8칸을 빈 값으로 새로 채워줌
            for (int i = 0; i < 8; ++i)
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
            UserLoginOrRegisterView.PrintRegisterResult("BOOK ADDED!");
            Console.ReadKey(true);
        }

        private void EditBook()
        {
            // 책 정보를 수정하는 UI를 출력해 줌
            AdminMenuView.PrintEditBook();

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
                if (!BookDAO.getInstance.BookExists(int.Parse(bookIdInputResult.Input)))
                {
                    UserLoginOrRegisterView.PrintRegisterResult("BOOK DOES NOT EXIST!");
                    Console.ReadKey(true);
                    return;
                }

                // 책 정보를 얻어옴
                BookDTO originalBook = BookDAO.getInstance.GetBookInfo(int.Parse(bookIdInputResult.Input)));

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
                UserLoginOrRegisterView.PrintRegisterResult("BOOK EDITED!");
                Console.ReadKey(true);
            }
        }

        private ResultCode InsertBookInfo(List<UserInput> inputs)
        {
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[8];
            string[] warningMessage =
            {
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
                            warning[i] = warningMessage[i];
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
            UserInput userIdInputResult = UserInputManager.getInstance.ReadInputFromUser(
                windowWidthHalf,
                windowHeightHalf, Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                Constant.Input.Parameter.CANNOT_ENTER_KOREAN);

            // ESC키가 눌려지면 반환
            if (userIdInputResult.ResultCode == ResultCode.ESC_PRESSED)
            {
                return;
            }

            UserSelectionView.PrintYesOrNO("Are you sure to delete user " + userIdInputResult.Input + "?");

            // 회원 삭제 여부를 물어보고 Y키가 입력되었으면
            if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
            {
                // 바로 엔터를 누른 것이 아니면
                if (userIdInputResult.Input.Length > 0)
                {
                    // 삭제를 시도
                    ResultCode deleteResult = UserDAO.getInstance.DeleteUser(userIdInputResult.Input);

                    // 성공했으면 결과 출력
                    if (deleteResult == ResultCode.SUCCESS)
                    {
                        AdminMenuView.PrintDeleteResult("사용자를 성공적으로 제거했습니다.");
                    }

                    // 실패했으면 그 이유를 출력
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

            // 유저 정보를 모두 불러옴
            List<UserDTO> users = UserDAO.getInstance.GetAllUsers();

            // 유저 아이디에 해당하는 값을 Borrowed_Book에서 찾아보고 그 결과를 출력
            foreach (UserDTO user in users)
            {
                List<BorrowedBookDTO> bookData =
                    BookDAO.getInstance.GetBorrowedBooks(user.Id);
                SearchBookOrUserView.PrintBorrowedBooks(user.Id, bookData);
            }

            Console.ReadKey();
        }

        private ResultCode SearchMember()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            List<UserInput> inputs = new List<UserInput>();

            for (int i = 0; i < 3; ++i)
            {
                inputs.Add(new UserInput(ResultCode.NO, ""));
            }
            
            SearchBookOrUserView.PrintSearchUser();

            for (int i = 0; i < 3; ++i)
            {
                UserInputManager.getInstance.GetSearchUserInput(inputs, i);

                if (inputs[i].ResultCode == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }
            }

            // 유저 검색 결과를 저장
            DataSet searchUserResult = UserDAO.getInstance.SearchUser(inputs[0].Input,
                inputs[1].Input, inputs[2].Input);

            // 유저 검색 결과를 출력
            SearchBookOrUserView.ViewSearchUserResult(searchUserResult);

            // 키를 입력 받을때까지 출력 유지
            Console.ReadKey(true);

            return ResultCode.SUCCESS;
        }
    }
}