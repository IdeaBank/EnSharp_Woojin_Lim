using System;
using System.Collections.Generic;
using Library.Constant;
using Library.Model;
using Library.Model.DTO;
using Library.Utility;

namespace Library.View.Admin
{
    public class LogView
    {
        private static LogView _instance;

        private LogView()
        {
            
        }

        public static LogView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogView();
                }

                return _instance;
            }
        }
        
        public void PrintLogMenuContour()
        {
            ConsoleWriter.getInstance.DrawContour(100, 24);
            ConsoleWriter.getInstance.DrawLogo(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 - 8);
        }

        public void PrintLogConfirmContour()
        {
            ConsoleWriter.getInstance.DrawContour(70, 4);
        }

        public void PrintLogResult(string result, bool isWarning = false)
        {
            Console.Clear();
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            PrintLogConfirmContour();
            Console.SetCursorPosition(windowWidthHalf - UserInputManager.getInstance.GetHangeulCount(result.Substring(0, result.Length / 2)) - result.Length / 2, windowHeightHalf);
            
            if(isWarning)
            {
                ConsoleWriter.getInstance.PrintWarning(result);
                return;
            }

            Console.Write(result);
        }

        public void PrintLogs(List<LogDTO> logs)
        {
            Console.WriteLine(new string('=', 36));

            if (logs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("로그 결과가 없습니다!");
                Console.ResetColor();
            }

            foreach (LogDTO log in logs)
            {
                Console.Write("로그 ID: ".PadLeft(15, ' '));
                Console.WriteLine(log.LogId);

                Console.Write("로그 시간: ".PadLeft(15, ' '));
                Console.WriteLine(log.LogTime);

                Console.Write("로그 사용자: ".PadLeft(15, ' '));
                Console.WriteLine(log.UserName);
                
                Console.Write("로그 정보: ".PadLeft(15, ' '));
                Console.WriteLine(log.LogContents);

                Console.Write("로그 행동: ".PadLeft(15, ' '));
                Console.WriteLine(log.LogAction);
                Console.WriteLine();
            }
            
            Console.WriteLine(new string('=', 36));
        }
        
        public void PrintLogMenu(int currentSelectionIndex)
        {
            Console.Clear();
            PrintLogMenuContour();
            
            string[] loginOrRegister = new[] { "로그 수정", "로그 저장", "로그 삭제", "로그 초기화"};
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < Constant.Menu.Count.LOG; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf - 4, consoleWindowHeightHalf + 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf - 4, consoleWindowHeightHalf + 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.White);
                }
            }
        }
    }
}