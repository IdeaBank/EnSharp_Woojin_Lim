using System;
using System.Collections.Generic;
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
        public static KeyValuePair<FailCode, string> ReadInputFromUser(int cursorX, int cursorY, int maxInputLength, 
            bool canEnterKorean, string defaultInput = "")
        {
            string currentInput = defaultInput;
            FailCode inputResult = FailCode.SUCCESS;
            
            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();

            while (keyInput.Key != ConsoleKey.Enter)
            {
                ConsoleWriter.WriteOnPosition(cursorX, cursorY, currentInput);

                keyInput = Console.ReadKey(true);

                if (keyInput.Key == ConsoleKey.Escape)
                {
                    inputResult = FailCode.ESC_PRESSED;
                    break;
                }

                if (keyInput.Key == ConsoleKey.Backspace && currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);

                    ConsoleWriter.WriteOnPosition(cursorX, cursorY, new string(' ', maxInputLength));
                }

                else
                {
                    if (currentInput.Length < maxInputLength && IsNumberOrCharacter(keyInput.KeyChar, canEnterKorean))
                    {
                        currentInput += keyInput.KeyChar;
                    }
                }
            }

            return new KeyValuePair<FailCode, string>(inputResult, currentInput);
        }
    }
}