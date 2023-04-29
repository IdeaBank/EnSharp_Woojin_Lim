using Library.Constants;
using System;
using System.Collections.Generic;
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

        public static ResultCode MatchesRegex(string expression, string str)
        {
            Regex regex = new Regex(expression);

            if (regex.IsMatch(str))
            {
                return ResultCode.SUCCESS;
            }

            return ResultCode.DO_NOT_MATCH_REGEX;
        }

        private static int GetHangeulCount(string str)
        {
            int count = 0;

            foreach(char ch in str)
            {
                if((0xac00 <= ch && ch <= 0xd7a3) || (0x3131 <= ch && ch <= 0x318e))
                {
                    count += 1;
                }
            }

            return count;
        }

        // Return true if character is between 0 and 9
        private static bool IsDigit(char ch)
        {
            return '0' <= ch && ch <= '9';
        }

        // Return true if character is a special character
        private static bool IsSpecialCharacter(char ch)
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
        private static bool IsAlphabet(char ch)
        {
            return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
        }

        // Return true if character is Hangeul
        private static bool IsKoreanCharacter(char ch)
        {
            return (0xac00 <= ch && ch <= 0xd7a3) || (0x3131 <= ch && ch <= 0x318e);
        }

        // Return true if character is number or character
        private static bool IsNumberOrCharacter(char ch, bool canEnterKorean)
        {
            if (canEnterKorean)
            {
                return IsAlphabet(ch) || IsKoreanCharacter(ch) || IsDigit(ch) || IsSpecialCharacter(ch);
            }

            return IsAlphabet(ch) || IsDigit(ch) || IsSpecialCharacter(ch);
        }

        // Get input from user and return its result.
        // Return FailCode.ESC_PRESSED if esc is pressed during input.
        public static KeyValuePair<ResultCode, string> ReadInputFromUser(int cursorX, int cursorY, int maxInputLength,
            bool isPassword, bool canEnterKorean, string defaultInput = "")
        {
            // Set cursor visible
            Console.CursorVisible = true;

            // Set current input into default input
            string currentInput = defaultInput;
            ResultCode inputResult = ResultCode.SUCCESS;

            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();

            // While Enter key is not pressed
            while (keyInput.Key != ConsoleKey.Enter)
            {
                // Write down stars if input mode is password
                if (isPassword)
                {
                    ConsoleWriter.WriteOnPosition(cursorX, cursorY, new string('*', currentInput.Length));
                }

                // Write down current input
                else
                {
                    ConsoleWriter.WriteOnPosition(cursorX, cursorY, currentInput);
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
                    ConsoleWriter.WriteOnPosition(cursorX, cursorY, new string(' ', maxInputLength + GetHangeulCount(currentInput) + 1));
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
            return new KeyValuePair<ResultCode, string>(inputResult, currentInput);
        }

        public static ResultCode InputYesOrNo()
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

        public static ResultCode ValidateUserInformationInput(KeyValuePair<ResultCode, string> input, int inputIndex)
        {
            ResultCode resultCode = ResultCode.SUCCESS;

            if (input.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            switch (inputIndex)
            {
                case 0:
                    resultCode = MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, input.Value);
                    break;
                case 1:
                    resultCode = MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, input.Value);
                    break;
                case 2:
                    resultCode = MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, input.Value);
                    break;
                case 3:
                    resultCode = MatchesRegex(RegularExpression.USER_NAME, input.Value);
                    break;
                case 4:
                    resultCode = MatchesRegex(RegularExpression.USER_AGE, input.Value);
                    break;
                case 5:
                    resultCode = MatchesRegex(RegularExpression.USER_PHONE_NUMBER, input.Value);
                    break;
                case 6:
                    resultCode = MatchesRegex(RegularExpression.USER_ADDRESS, input.Value);
                    break;
            }

            return resultCode;
        }

        public static ResultCode GetUserInformationInput(List<KeyValuePair<ResultCode, string>> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf, windowHeightHalf, MaxInputLength.USER_ID_PASSWORD, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1, MaxInputLength.USER_ID_PASSWORD, InputParameter.IS_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 2:
                    inputs[2] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 2, MaxInputLength.USER_ID_PASSWORD, InputParameter.IS_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 3:
                    inputs[3] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 3, MaxInputLength.USER_NAME, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);
                    break;
                case 4:
                    inputs[4] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 4, MaxInputLength.USER_AGE, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 5:
                    inputs[5] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 5, MaxInputLength.USER_PHONE_NUMBER, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 6:
                    inputs[6] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 6, MaxInputLength.USER_ADDRESS, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);
                    break;
            }

            ResultCode validateResult = ValidateUserInformationInput(inputs[inputIndex], inputIndex);

            if (validateResult == ResultCode.DO_NOT_MATCH_REGEX)
            {
                inputs[inputIndex] = new KeyValuePair<ResultCode, string>(ResultCode.DO_NOT_MATCH_REGEX, "");
            }

            if (inputIndex == 2)
            {
                if (inputs[1].Value != inputs[2].Value)
                {
                    inputs[1] = new KeyValuePair<ResultCode, string>(ResultCode.NO, "");
                    inputs[2] = new KeyValuePair<ResultCode, string>(ResultCode.NO, "");

                    return ResultCode.DO_NOT_MATCH_PASSWORD;
                }
            }

            return inputs[inputIndex].Key;
        }

        public static ResultCode ValidateBookInformationInput(KeyValuePair<ResultCode, string> input, int inputIndex)
        {
            ResultCode resultCode = ResultCode.SUCCESS;

            if (input.Key == ResultCode.ESC_PRESSED)
            {
                return ResultCode.ESC_PRESSED;
            }

            switch (inputIndex)
            {
                case 0:
                    resultCode = MatchesRegex(RegularExpression.BOOK_NAME, input.Value);
                    break;
                case 1:
                    resultCode = MatchesRegex(RegularExpression.BOOK_AUTHOR, input.Value);
                    break;
                case 2:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PUBLISHER, input.Value);
                    break;
                case 3:
                    resultCode = MatchesRegex(RegularExpression.BOOK_QUANTITY, input.Value);
                    break;
                case 4:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PRICE, input.Value);
                    break;
                case 5:
                    resultCode = MatchesRegex(RegularExpression.BOOK_PUBLISHED_DATE, input.Value);
                    break;
                case 6:
                    resultCode = MatchesRegex(RegularExpression.BOOK_ISBN, input.Value);
                    break;
                case 7:
                    resultCode = MatchesRegex(RegularExpression.BOOK_DESCRIPTION, input.Value);
                    break;
            }

            return resultCode;
        }

        public static ResultCode GetBookInformationInput(List<KeyValuePair<ResultCode, string>> inputs, int inputIndex)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            switch (inputIndex)
            {
                case 0:
                    inputs[0] = ReadInputFromUser(windowWidthHalf, windowHeightHalf, MaxInputLength.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);
                    break;
                case 1:
                    inputs[1] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 1, MaxInputLength.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);
                    break;
                case 2:
                    inputs[2] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 2, MaxInputLength.BOOK_NAME_AUTHOR_PUBLISHER, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);
                    break;
                case 3:
                    inputs[3] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 3, MaxInputLength.BOOK_QUANTITY, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 4:
                    inputs[4] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 4, MaxInputLength.BOOK_PRICE, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 5:
                    inputs[5] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 5, MaxInputLength.BOOK_PUBLISHED_DATE, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 6:
                    inputs[6] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 6, MaxInputLength.BOOK_ISBN, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    break;
                case 7:
                    inputs[7] = ReadInputFromUser(windowWidthHalf, windowHeightHalf + 7, MaxInputLength.BOOK_DESCRIPTION, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);
                    break;
            }

            ResultCode validateResult = ValidateBookInformationInput(inputs[inputIndex], inputIndex);

            if (validateResult == ResultCode.DO_NOT_MATCH_REGEX)
            {
                inputs[inputIndex] = new KeyValuePair<ResultCode, string>(ResultCode.DO_NOT_MATCH_REGEX, "");
            }

            return inputs[inputIndex].Key;
        }

        public static void ReadUntilESC()
        {
            bool isESCPressed = false;

            while (!isESCPressed)
            {
                ConsoleKeyInfo keyInput = Console.ReadKey(true);

                if (keyInput.Key == ConsoleKey.Escape)
                {
                    isESCPressed = true;
                }
            }
        }
    }
}