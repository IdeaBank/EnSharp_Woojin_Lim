using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Library.Utility
{
    public class UserInputManager
    {
        // Singleton
        private static UserInputManager _instance;

        private UserInputManager()
        {
        }

        public static UserInputManager getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserInputManager();
                }

                return _instance;
            }
        }

        private ResultCode MatchesRegex(string expression, string str)
        {
            Regex regex = new Regex(expression);

            if (regex.IsMatch(str))
            {
                return ResultCode.SUCCESS;
            }

            return ResultCode.DO_NOT_MATCH_REGEX;
        }

        public int GetHangeulCount(string str)
        {
            int count = 0;

            foreach (char ch in str)
            {
                if ((0xac00 <= ch && ch <= 0xd7a3) || (0x3131 <= ch && ch <= 0x318e))
                {
                    count += 1;
                }
            }

            return count;
        }


        public bool IsNumber(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }

            foreach (char ch in str)
            {
                if (!IsDigit(ch))
                {
                    return false;
                }
            }

            return true;
        }

        // Return true if character is between 0 and 9
        private bool IsDigit(char ch)
        {
            return '0' <= ch && ch <= '9';
        }

        // Return true if character is a special character
        public static bool IsSpecialCharacter(char ch)
        {
            string specialChar = @"\|!$%&/()=?@{}.-;<>_, ";

            foreach (char item in specialChar)
            {
                if (ch == item)
                {
                    return true;
                }
            }

            return false;
        }

        // Return true if character is alphabet
        private bool IsAlphabet(char ch)
        {
            return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
        }

        // Return true if character is Hangeul
        private static bool IsKoreanCharacter(char ch)
        {
            return (0xac00 <= ch && ch <= 0xd7a3) || (0x3131 <= ch && ch <= 0x318e);
        }

        // Return true if character is number or character
        private bool IsNumberOrCharacter(char ch, bool canEnterKorean)
        {
            if (canEnterKorean)
            {
                return IsAlphabet(ch) || IsKoreanCharacter(ch) || IsDigit(ch) || IsSpecialCharacter(ch);
            }

            return IsAlphabet(ch) || IsDigit(ch) || IsSpecialCharacter(ch);
        }

        private bool IsAddressValid(string address)
        {
<<<<<<< Updated upstream
            JObject jObject = RestfulApiConnector.getInstance.GetResponseAsJObject(Constant.ApiUrl.KAKAO_API_ADDRESS,
                address, "Authorization", "KakaoAK 0064631b5828a014e919d63af22f8c49");

            if (jObject["meta"]["total_count"].ToString() == "0")
            {
                return false;
=======
            string encoded = System.Web.HttpUtility.UrlEncode(address);
            string url = "https://dapi.kakao.com/v2/local/search/address.json?analyze_type=exact&query=" + encoded;

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Headers["Authorization"] = "KakaoAK 0064631b5828a014e919d63af22f8c49";

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();

                JObject jObject = JObject.Parse(result);

                if (jObject["meta"]["total_count"].ToString() == "0")
                {
                    return false;
                }
>>>>>>> Stashed changes
            }

            return true;
        }

        // Get input from user and return its result.
        // Return FailCode.ESC_PRESSED if esc is pressed during input.
        public UserInput ReadInputFromUser(int cursorX, int cursorY, int maxInputLength,
            bool isPassword, bool canEnterKorean, string defaultInput = "")
        {
            // Set cursor visible
            Console.CursorVisible = true;

            // Set current input into default input
            ResultCode inputResult = ResultCode.SUCCESS;
            string currentInput = defaultInput;

            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();

            // While Enter key is not pressed
            while (keyInput.Key != ConsoleKey.Enter)
            {
                // Write down stars if input mode is password
                if (isPassword)
                {
                    ConsoleWriter.getInstance.WriteOnPosition(cursorX, cursorY, new string('*', currentInput.Length));
                }

                // Write down current input
                else
                {
                    ConsoleWriter.getInstance.WriteOnPosition(cursorX, cursorY, currentInput);
                }

                // Get key input
                keyInput = Console.ReadKey(true);

                // If Escape key is pressed
                if (keyInput.Key == ConsoleKey.Escape)
                {
                    // Set cursor invisible
                    Console.CursorVisible = false;

                    // set input result to esc_pressed and break while loop
                    inputResult = ResultCode.ESC_PRESSED;
                    break;
                }

                // If backspace is pressed and length of current input is over 0
                if (keyInput.Key == ConsoleKey.Backspace && currentInput.Length > 0)
                {
                    // Delete last character in current input
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);

                    // Remove printed input
                    ConsoleWriter.getInstance.WriteOnPosition(cursorX, cursorY,
                        new string(' ', maxInputLength + GetHangeulCount(currentInput) + 1));
                }

                else
                {
                    // If length of current input is under max input length and pressed key is number or character
                    if (currentInput.Length < maxInputLength && IsNumberOrCharacter(keyInput.KeyChar, canEnterKorean))
                    {
                        // Add pressed key character into current input
                        currentInput += keyInput.KeyChar;
                    }
                }
            }

            // Set cursor invisible
            Console.CursorVisible = false;

            // Return input result
            return new UserInput(inputResult, currentInput);
        }

        public ResultCode InputYesOrNo()
        {
            ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();

            while (consoleKeyInfo.Key != ConsoleKey.Y &&
                   consoleKeyInfo.Key != ConsoleKey.N &&
                   consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == ConsoleKey.Y)
                {
                    return ResultCode.YES;
                }

                else if (consoleKeyInfo.Key == ConsoleKey.N || consoleKeyInfo.Key == ConsoleKey.Escape)
                {
                    return ResultCode.NO;
                }
            }

            return ResultCode.NO;
        }

        private ResultCode ValidateUserInformationInput(UserInput input, int inputIndex)
        {
            ResultCode resultCode = ResultCode.SUCCESS;

            if (input.ResultCode == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            switch (inputIndex)
            {
                case Constant.Input.Type.USER_ID:
                    resultCode = MatchesRegex(RegularExpression.USER_ID, input.Input);
                    break;
                case Constant.Input.Type.USER_PASSWORD:
                    resultCode = MatchesRegex(RegularExpression.USER_PASSWORD, input.Input);
                    break;
                case Constant.Input.Type.USER_PASSWORD_CONFIRM:
                    resultCode = MatchesRegex(RegularExpression.USER_PASSWORD, input.Input);
                    break;
                case Constant.Input.Type.USER_NAME:
                    resultCode = MatchesRegex(RegularExpression.USER_NAME, input.Input);
                    break;
                case Constant.Input.Type.USER_AGE:
                    resultCode = MatchesRegex(RegularExpression.USER_AGE, input.Input);
                    break;
                case Constant.Input.Type.USER_PHONE_NUMBER:
                    resultCode = MatchesRegex(RegularExpression.USER_PHONE_NUMBER, input.Input);
                    break;
                case Constant.Input.Type.USER_ADDRESS:
                    resultCode = MatchesRegex(RegularExpression.USER_ADDRESS, input.Input);
                    break;
            }

            if (inputIndex == Constant.Input.Type.USER_ADDRESS)
            {
                if (!IsAddressValid(input.Input))
                {
                    return ResultCode.NO_ADDRESS;
                }
            }

            return resultCode;
        }

        public ResultCode GetUserInformationInput(List<UserInput> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case Constant.Input.Type.USER_ID:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.USER_PASSWORD:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.USER_PASSWORD_CONFIRM:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 2,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.USER_NAME:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 3, Constant.Input.Max.USER_NAME,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.USER_AGE:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 4, Constant.Input.Max.USER_AGE,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.USER_PHONE_NUMBER:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 5,
                        Constant.Input.Max.USER_PHONE_NUMBER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.USER_ADDRESS:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 6,
                        Constant.Input.Max.USER_ADDRESS, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
            }

            if (inputs[inputIndex].ResultCode == ResultCode.ESC_PRESSED)
            {
                return inputs[inputIndex].ResultCode;
            }

            ResultCode validateResult = ValidateUserInformationInput(inputs[inputIndex], inputIndex);

            if (validateResult == ResultCode.NO_ADDRESS)
            {
                inputs[inputIndex].ResultCode = ResultCode.DO_NOT_MATCH_REGEX;
                inputs[inputIndex].Input = "";
            }

            if (validateResult == ResultCode.DO_NOT_MATCH_REGEX)
            {
                inputs[inputIndex].ResultCode = ResultCode.DO_NOT_MATCH_REGEX;
                inputs[inputIndex].Input = "";
            }

            if (inputIndex == Constant.Input.Type.USER_ID)
            {
                if (UserDAO.getInstance.UserExists(inputs[0].Input))
                {
                    inputs[inputIndex].ResultCode = ResultCode.USER_ID_EXISTS;
                    inputs[inputIndex].Input = "";
                }
            }

            if (inputIndex == Constant.Input.Type.USER_PASSWORD_CONFIRM)
            {
                if (inputs[Constant.Input.Type.USER_PASSWORD].Input != inputs[Constant.Input.Type.USER_PASSWORD_CONFIRM].Input)
                {
                    inputs[Constant.Input.Type.USER_PASSWORD_CONFIRM].ResultCode = ResultCode.NO;
                    inputs[Constant.Input.Type.USER_PASSWORD_CONFIRM].Input = "";

                    return ResultCode.DO_NOT_MATCH_PASSWORD;
                }
            }

            return inputs[inputIndex].ResultCode;
        }

        private ResultCode ValidateBookInformationInput(UserInput input, int inputIndex)
        {
            ResultCode resultCode = ResultCode.SUCCESS;

            if (input.ResultCode == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            switch (inputIndex)
            {
                case Constant.Input.Type.BOOK_NAME:
                    resultCode = MatchesRegex(RegularExpression.BOOK_NAME, input.Input);
                    break;
                case Constant.Input.Type.BOOK_AUTHOR:
                    resultCode = MatchesRegex(RegularExpression.BOOK_AUTHOR, input.Input);
                    break;
                case Constant.Input.Type.BOOK_PUBLISHER:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PUBLISHER, input.Input);
                    break;
                case Constant.Input.Type.BOOK_QUANTITY:
                    resultCode = MatchesRegex(RegularExpression.BOOK_QUANTITY, input.Input);
                    break;
                case Constant.Input.Type.BOOK_PRICE:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PRICE, input.Input);
                    break;
                case Constant.Input.Type.BOOK_PUBLISHED_DATE:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PUBLISHED_DATE, input.Input);
                    break;
                case Constant.Input.Type.BOOK_ISBN:
                    resultCode = MatchesRegex(RegularExpression.BOOK_ISBN, input.Input);
                    break;
                case Constant.Input.Type.BOOK_DESCRIPTION:
                    resultCode = MatchesRegex(RegularExpression.BOOK_DESCRIPTION, input.Input);
                    break;
            }

            return resultCode;
        }

        public ResultCode GetBookInformationInput(List<UserInput> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case Constant.Input.Type.BOOK_NAME:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_AUTHOR:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_PUBLISHER:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 2,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_QUANTITY:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 3,
                        Constant.Input.Max.BOOK_QUANTITY, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_PRICE:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 4, Constant.Input.Max.BOOK_PRICE,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_PUBLISHED_DATE:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 5,
                        Constant.Input.Max.BOOK_PUBLISHED_DATE, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_ISBN:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 6, Constant.Input.Max.BOOK_ISBN,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case Constant.Input.Type.BOOK_DESCRIPTION:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 7,
                        Constant.Input.Max.BOOK_DESCRIPTION, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
            }

            ResultCode validateResult = ValidateBookInformationInput(inputs[inputIndex], inputIndex);

            if (validateResult == ResultCode.DO_NOT_MATCH_REGEX)
            {
                inputs[inputIndex] = new UserInput(ResultCode.DO_NOT_MATCH_REGEX, "");
            }

            return inputs[inputIndex].ResultCode;
        }

        public ResultCode GetSearchUserInput(List<UserInput> inputs, int inputIndex)
        {
            switch (inputIndex)
            {
                case Constant.Input.Type.SEARCH_USER_NAME:
                    inputs[inputIndex] = ReadInputFromUser(7, 0, Constant.Input.Max.USER_NAME,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
                case Constant.Input.Type.SEARCH_USER_ID:
                    inputs[inputIndex] = ReadInputFromUser(7, 1,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN);
                    break;
                case Constant.Input.Type.SEARCH_USER_ADDRESS:
                    inputs[inputIndex] = ReadInputFromUser(7, 2,
                        Constant.Input.Max.USER_ADDRESS, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
            }

            return inputs[inputIndex].ResultCode;
        }

        public ResultCode GetSearchBookInput(List<UserInput> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case Constant.Input.Type.SEARCH_BOOK_NAME:
                    inputs[inputIndex] = ReadInputFromUser(8,
                        0, Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
                case Constant.Input.Type.SEARCH_BOOK_AUTHOR:
                    inputs[inputIndex] = ReadInputFromUser(8,
                        1, Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
                case Constant.Input.Type.SEARCH_BOOK_PUBLISHER:
                    inputs[inputIndex] = ReadInputFromUser(8,
                        2, Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
            }

            return inputs[inputIndex].ResultCode;
        }

        public ResultCode GetUserLoginInput(List<UserInput> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case Constant.Input.Type.LOGIN_USER_ID:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");
                    break;
                case Constant.Input.Type.LOGIN_USER_PASSWORD:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");
                    break;
            }

            return inputs[inputIndex].ResultCode;
        }
        
        public ResultCode GetRequestBookInput(List<UserInput> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case Constant.Input.Type.REQUEST_BOOK_NAME:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, "");
                    break;
                case Constant.Input.Type.REQUEST_BOOK_COUNT:
                    inputs[inputIndex] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.BOOK_QUANTITY, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");
                    break;
            }

            if (inputIndex == Constant.Input.Type.REQUEST_BOOK_NAME)
            {
                if (MatchesRegex(Constant.RegularExpression.BOOK_NAME, inputs[inputIndex].Input) == ResultCode.DO_NOT_MATCH_REGEX)
                {
                    inputs[inputIndex].ResultCode = ResultCode.DO_NOT_MATCH_REGEX;
                }
            }
            
            else if (inputIndex == Constant.Input.Type.REQUEST_BOOK_COUNT)
            {
                if (MatchesRegex(Constant.RegularExpression.BOOK_REQUEST_COUNT, inputs[inputIndex].Input) == ResultCode.DO_NOT_MATCH_REGEX)
                {
                    inputs[inputIndex].ResultCode = ResultCode.DO_NOT_MATCH_REGEX;
                }
            }

            return inputs[inputIndex].ResultCode;
        }

        public UserInput GetRequestBookIsbn()
        {
            UserInput input = ReadInputFromUser(40, Console.CursorTop, Constant.Input.Max.BOOK_ISBN,
                Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");

            return input;
        }
        
        public UserInput GetRemoveLogWithId()
        {
            UserInput input = ReadInputFromUser(Console.CursorLeft, Console.CursorTop, Constant.Input.Max.LOG_ID,
                Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");

            return input;
        }

        public void ReadUntilEsc()
        {
            bool isEscPressed = false;

            while (!isEscPressed)
            {
                ConsoleKeyInfo keyInput = Console.ReadKey(true);

                if (keyInput.Key == ConsoleKey.Escape)
                {
                    isEscPressed = true;
                }
            }
        }
    }
}