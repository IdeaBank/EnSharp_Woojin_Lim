using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.View;

namespace Library.Exception
{
    public class InputFromUser
    {
        private bool IsNumberOrCharacter(char ch)
        {
            return ('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
        }

        public KeyValuePair<bool, string> ReadInputFromUser(int cursorX, int cursorY, int maxInputLength, bool isPassword)
        {
            bool isValidInput = true;
            string currentInput = "";
            
            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();
            
            while (keyInput.Key != ConsoleKey.Enter)
            {
                if (!isPassword)
                {
                    GeneralOutputWriter.WriteOnPosition(cursorX, cursorY, currentInput);
                }

                else
                {
                    GeneralOutputWriter.WriteOnPosition(cursorX, cursorY, new string('*', currentInput.Length));
                }

                keyInput = Console.ReadKey(true);

                if (keyInput.Key == ConsoleKey.Escape)
                {
                    isValidInput = false;
                    break;
                }

                else if (keyInput.Key == ConsoleKey.Backspace && currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    
                    // 입력 칸 비워주기
                    GeneralOutputWriter.WriteOnPosition(cursorX, cursorY, new string(' ', maxInputLength));
                }

                else
                {
                    if (currentInput.Length < maxInputLength && IsNumberOrCharacter(keyInput.KeyChar))
                    {
                        currentInput += keyInput.KeyChar;
                    }
                }
            }
            
            return new KeyValuePair<bool, string>(isValidInput, currentInput);
        }
    }
}