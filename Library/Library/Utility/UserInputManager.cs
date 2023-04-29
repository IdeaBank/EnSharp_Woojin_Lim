using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;

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

        public static bool MatchesRegex(string expression, string str)
        {
            Regex regex = new Regex(expression);
            return regex.IsMatch(str);
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
                    ConsoleWriter.WriteOnPosition(cursorX, cursorY, new string(' ', maxInputLength));
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
        
        public static GetInput()
    }
}