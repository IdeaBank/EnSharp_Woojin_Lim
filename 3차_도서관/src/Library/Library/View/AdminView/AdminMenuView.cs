using Library.Constants;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.View.AdminView
{
    public class AdminMenuView
    {
        private AdminMenuView _instance;

        private AdminMenuView()
        {

        }

        public AdminMenuView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new AdminMenuView();
                }

                return _instance;
            }
        }

        public static void PrintAdminMenuContour()
        {
            ConsoleWriter.DrawContour(40, 13);
        }

        private static void PrintAddBookContour()
        {
            ConsoleWriter.DrawContour(80, 20);
        }

        private static void PrintChooseBookContour()
        {
            ConsoleWriter.DrawContour(60, 4);
        }

        public static void PrintAdminMenu(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "회원 관리", "대여 상황" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < MenuCount.ADMIN; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf - 4, consoleWindowHeightHalf - 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf- 4, consoleWindowHeightHalf - 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.White);
                }
            }
        }

        public static void PrintAddBook(string[] warnings, List<KeyValuePair<ResultCode, string>> inputs)
        {
            PrintAddBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            string[] instructions = new string[]
            {
                "Enter name: ",
                "Enter author: ",
                "Enter publisher: ",
                "Enter quantity: ",
                "Enter price: ",
                "Enter published date: ",
                "Enter isbn: ",
                "Enter desription: "
            };

            for (int i = 0; i < instructions.Length; ++i)
            {
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, instructions[i],
                    AlignType.LEFT);
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, warnings[i],
                    AlignType.RIGHT, ConsoleColor.Red);
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, inputs[i].Value,
                    AlignType.RIGHT);
            }
        }

        public static void PrintDeleteBook()
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of book to remove: ", AlignType.LEFT);
        }
        
        public static void PrintEditBook()
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of book to edit: ", AlignType.LEFT);
        }

        public static void PrintDeleteResult(string str)
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, str, AlignType.CENTER);
        }
    }
}