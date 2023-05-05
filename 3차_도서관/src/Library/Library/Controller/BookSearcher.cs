using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Model.DTO;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.Controller
{
    public class BookSearcher
    {
        private static BookSearcher _instance;

        private BookSearcher()
        {
        }

        public static BookSearcher getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BookSearcher();
                }

                return _instance;
            }
        }

        // 책 검색 기능
        public ResultCode SearchBook()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            List<BookDTO> books = BookDAO.getInstance.GetAllBooks();

            // UI를 띄워줌
            Console.Clear();
            View.SearchResultView.getInstance.SearchBook();
            View.SearchResultView.getInstance.ViewSearchBookResult(books);

            List<UserInput> inputs = new List<UserInput>();

            for (int i = 0; i < Constant.Input.Count.SEARCH_BOOK; ++i)
            {
                inputs.Add(new UserInput(ResultCode.NO, ""));

                UserInputManager.getInstance.GetSearchBookInput(inputs, i);

                if (inputs[i].ResultCode == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }
            }

            // 책 검색 결과를 저장
            List<BookDTO> searchBookResult = BookDAO.getInstance.SearchBook(inputs[0].Input, inputs[1].Input, inputs[2].Input);

            // 책 검색 결과를 출력
            Console.Clear();
            View.SearchResultView.getInstance.ViewSearchBookResult(searchBookResult);

            // 키를 입력 받을때까지 출력 유지
            Console.ReadKey(true);

            return ResultCode.SUCCESS;
        }
    }
}