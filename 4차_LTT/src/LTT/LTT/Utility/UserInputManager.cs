using System;
using System.Collections.Generic;
using LTT.Constant;

namespace LTT.Utility
{
    public class UserInputManager
    {
        public bool IsDigit(char ch)
        {
            return '0' <= ch && ch <= '9';
        }

        public bool IsNumber(string str)
        {
            foreach(char ch in str)
            {
                if(!IsDigit(ch))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsSpecialCharacter(char ch)
        {
            char[] availableSpecialCharacter =
            {
                '?', '!', '+', '=', '-', ' ', '_'
            };

            // 사용 가능한 특수문자 리스트에 해당 문자가 있는 지 점검
            foreach (char specialCharacter in availableSpecialCharacter)
            {
                if (ch == specialCharacter)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsAlphabet(char ch)
        {
            return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
        }

        private bool IsHangeul(char ch)
        {
            return (0xac00 <= ch && ch <= 0xd7a3) || (0x3131 <= ch && ch <= 0x318e);
        }

        private bool IsDigitOrCharacter(char ch, bool canEnterKorean)
        {
            if (canEnterKorean)
            {
                return IsAlphabet(ch) || IsHangeul(ch) || IsDigit(ch) || IsSpecialCharacter(ch);
            }

            return IsAlphabet(ch) || IsDigit(ch) || IsSpecialCharacter(ch);
        }

        public KeyValuePair<ResultCode, string> ReadInputFromUser(ConsoleWriter consoleWriter, int cursorX,
            int cursorY, int maxInputLength,
            bool isPassword, bool canEnterKorean, string defaultInput = "")
        {
            // 입력 단계에서는 커서 보이게 설정
            Console.CursorVisible = true;

            // 현재 입력 값을 기존 입력 값으로 설정
            string currentInput = defaultInput;
            ResultCode inputResult = ResultCode.ENTER_PRESSED;

            ConsoleKeyInfo keyInput = new ConsoleKeyInfo();

            // 엔터 키가 안 눌렸을 때까지 반복
            while (keyInput.Key != ConsoleKey.Enter)
            {
                // 비밀번호를 입력 받는 상황이면 *로 출력
                if (isPassword)
                {
                    consoleWriter.PrintOnPosition(cursorX, cursorY, new string('*', currentInput.Length), Align.LEFT);
                }

                // 비밀번호를 입력 받는 상황이 아니면 현재 입력한 값 출력
                else
                {
                    consoleWriter.PrintOnPosition(cursorX, cursorY, currentInput, Align.LEFT);
                }

                // 입력 단계에서는 커서 보이게 설정
                Console.CursorVisible = true;

                // 키를 입력 받음
                keyInput = Console.ReadKey(true);

                // 입력 받았으면 다시 커서 안 보이게 설정
                Console.CursorVisible = false;

                // ESC키를 눌렀으면 종료
                if (keyInput.Key == ConsoleKey.Escape)
                {
                    // 입력이 끝났으므로 커서가 안 보이게 설정
                    Console.CursorVisible = false;

                    inputResult = ResultCode.ESC_PRESSED;
                    break;
                }

                // Backspace를 눌렀고 현재 입력한 값이 있다면
                if (keyInput.Key == ConsoleKey.Backspace && currentInput.Length > 0)
                {
                    // 문자열의 마지막 문자를 지움
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);

                    // 출력한 문자열 다 지우기
                    consoleWriter.PrintOnPosition(cursorX, cursorY, new string(' ', maxInputLength), Align.LEFT);
                }

                else
                {
                    // 최대 길이보다 현재 입력이 짧고 문자를 입력했다면
                    if (currentInput.Length < maxInputLength && IsDigitOrCharacter(keyInput.KeyChar, canEnterKorean))
                    {
                        // 입력한 키를 현재 입력에 넣기
                        currentInput += keyInput.KeyChar;
                    }
                }
            }

            // 입력이 끝났으므로 커서를 다시 안 보이게 설정
            Console.CursorVisible = false;

            // 입력한 결과 반환
            return new KeyValuePair<ResultCode, string>(inputResult, currentInput);
        }

        // Y 키 혹은 N 키를 입력 받는 함수
        public ResultCode InputYesOrNo()
        {
            ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();

            while (consoleKeyInfo.Key != ConsoleKey.Y &&
                   consoleKeyInfo.Key != ConsoleKey.N &&
                   consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                consoleKeyInfo = Console.ReadKey(true);

                // 키에 따라 결과 값 반환
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Y:
                        return ResultCode.Y_PRESSED;

                    case ConsoleKey.N:
                    case ConsoleKey.Escape:
                        return ResultCode.N_PRESSED;
                }

                return ResultCode.N_PRESSED;
            }

            return ResultCode.N_PRESSED;
        }


        public void ReadUntilESC()
        {
            bool isESCPressed = false;

            while(!isESCPressed)
            {
                ConsoleKeyInfo keyInput = Console.ReadKey(true);
                
                if(keyInput.Key == ConsoleKey.Escape)
                {
                    isESCPressed = true;
                }
            }
        }
    }
}