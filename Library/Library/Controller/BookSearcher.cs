using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View;

namespace Library.Controller
{
    public class BookSearcher: ControllerInterface
    {
        public BookSearcher(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {
            
        }
        
        public void SearchBook()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            SearchBookOrUserView.SearchBook();
            
            KeyValuePair<ResultCode, string> nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                windowHeightHalf - 2, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            if (nameInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }
            
            KeyValuePair<ResultCode, string> authorInputResult =  UserInputManager.ReadInputFromUser(windowWidthHalf, 
                windowHeightHalf - 1, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            if (authorInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            KeyValuePair<ResultCode, string> publisherInputResult =  UserInputManager.ReadInputFromUser(windowWidthHalf, 
                windowHeightHalf - 0, InputMax.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

            if (publisherInputResult.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            List<Book> searchBookResult = combinedManager.BookManager.SearchBook(nameInputResult.Value,
                authorInputResult.Value, publisherInputResult.Value);
            
            SearchBookOrUserView.ViewSearchBookResult(searchBookResult);

            Console.ReadKey(true);
        }
    }
}