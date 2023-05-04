using Library.Constant;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Model;

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

        private int GetHangeulCount(string str)
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
        private bool IsSpecialCharacter(char ch)
        {
            char[] availableSpecialCharacter =
            {
                '?', '!', '+', '=', '-', ' ', '_'
            };

            // See if character is in special character list
            foreach (char specialCharacter in availableSpecialCharacter)
            {
                if (ch == specialCharacter)
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
                case 0:
                    resultCode = MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, input.Input);
                    break;
                case 1:
                    resultCode = MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, input.Input);
                    break;
                case 2:
                    resultCode = MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, input.Input);
                    break;
                case 3:
                    resultCode = MatchesRegex(RegularExpression.USER_NAME, input.Input);
                    break;
                case 4:
                    resultCode = MatchesRegex(RegularExpression.USER_AGE, input.Input);
                    break;
                case 5:
                    resultCode = MatchesRegex(RegularExpression.USER_PHONE_NUMBER, input.Input);
                    break;
                case 6:
                    resultCode = MatchesRegex(RegularExpression.USER_ADDRESS, input.Input);
                    break;
            }

            return resultCode;
        }

        public ResultCode GetUserInformationInput(List<UserInput> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 2:
                    inputs[2] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 2,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 3:
                    inputs[3] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 3, Constant.Input.Max.USER_NAME,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case 4:
                    inputs[4] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 4, Constant.Input.Max.USER_AGE,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case 5:
                    inputs[5] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 5,
                        Constant.Input.Max.USER_PHONE_NUMBER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 6:
                    inputs[6] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 6,
                        Constant.Input.Max.USER_ADDRESS, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
            }

            ResultCode validateResult = ValidateUserInformationInput(inputs[inputIndex], inputIndex);

            if (validateResult == ResultCode.DO_NOT_MATCH_REGEX)
            {
                inputs[inputIndex] = new UserInput(ResultCode.DO_NOT_MATCH_REGEX, "");
            }

            if (inputIndex == 2)
            {
                if (inputs[1].Input != inputs[2].Input)
                {
                    inputs[2] = new UserInput(ResultCode.NO, "");

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
                case 0:
                    resultCode = MatchesRegex(RegularExpression.BOOK_NAME, input.Input);
                    break;
                case 1:
                    resultCode = MatchesRegex(RegularExpression.BOOK_AUTHOR, input.Input);
                    break;
                case 2:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PUBLISHER, input.Input);
                    break;
                case 3:
                    resultCode = MatchesRegex(RegularExpression.BOOK_QUANTITY, input.Input);
                    break;
                case 4:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PRICE, input.Input);
                    break;
                case 5:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PUBLISHED_DATE, input.Input);
                    break;
                case 6:
                    resultCode = MatchesRegex(RegularExpression.BOOK_ISBN, input.Input);
                    break;
                case 7:
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
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 2:
                    inputs[2] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 2,
                        Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CAN_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 3:
                    inputs[3] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 3,
                        Constant.Input.Max.BOOK_QUANTITY, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 4:
                    inputs[4] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 4, Constant.Input.Max.BOOK_PRICE,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case 5:
                    inputs[5] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 5,
                        Constant.Input.Max.BOOK_PUBLISHED_DATE, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, inputs[inputIndex].Input);
                    break;
                case 6:
                    inputs[6] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 6, Constant.Input.Max.BOOK_ISBN,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CANNOT_ENTER_KOREAN,
                        inputs[inputIndex].Input);
                    break;
                case 7:
                    inputs[7] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 7,
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
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf, windowHeightHalf - 2, Constant.Input.Max.USER_NAME,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf, windowHeightHalf - 1,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN);
                    break;
                case 2:
                    inputs[2] = ReadInputFromUser(windowWidthHalf, windowHeightHalf - 0,
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
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf - 2, Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf - 1, Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER,
                        Constant.Input.Parameter.IS_NOT_PASSWORD, Constant.Input.Parameter.CAN_ENTER_KOREAN);
                    break;
                case 2:
                    inputs[2] = ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf - 0, Constant.Input.Max.BOOK_NAME_AUTHOR_PUBLISHER,
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
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf, windowHeightHalf,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_NOT_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1,
                        Constant.Input.Max.USER_ID_PASSWORD, Constant.Input.Parameter.IS_PASSWORD,
                        Constant.Input.Parameter.CANNOT_ENTER_KOREAN, "");
                    break;
            }

            return inputs[inputIndex].ResultCode;
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