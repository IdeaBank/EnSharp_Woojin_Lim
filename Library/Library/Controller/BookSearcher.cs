using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;
using System;
using System.Collections.Generic;

namespace Library.Controller
{
    public class BookSearcher : ControllerInterface
    {
        public BookSearcher(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {

        }

        // 책 검색 기능
        public ResultCode SearchBook()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            // UI를 띄워줌
            SearchBookOrUserView.SearchBook();

            // 책 이름을 입력 받음
            KeyValuePair<ResultCode, string> nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf - 2, MaxInputLength.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (nameInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            // 책 저자를 입력 받음
            KeyValuePair<ResultCode, string> authorInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf - 1, MaxInputLength.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (authorInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            // 책 출판사를 입력 받음
            KeyValuePair<ResultCode, string> publisherInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                windowHeightHalf - 0, MaxInputLength.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            // ESC키가 눌렸으면 반환
            if (publisherInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            // 책 검색 결과를 저장
            List<Book> searchBookResult = combinedManager.BookManager.SearchBook(nameInputResult.Value,
                authorInputResult.Value, publisherInputResult.Value);

            // 책 검색 결과를 출력
            SearchBookOrUserView.ViewSearchBookResult(searchBookResult);

            // 키를 입력 받을때까지 출력 유지
            Console.ReadKey(true);

            return ResultCode.SUCCESS;
        }
    }
}