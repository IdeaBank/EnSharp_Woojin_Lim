using Library.Constant;
using Library.Model;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.View.Admin
{
    public class MenuView
    {
        private static MenuView _instance;

        private MenuView()
        {

        }

        public static MenuView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MenuView();
                }

                return _instance;
            }
        }

        public void PrintAdminMenuContour()
        {
            ConsoleWriter.getInstance.DrawContour(100, 24);
            ConsoleWriter.getInstance.DrawLogo(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 - 8);
        }

        private void PrintAddBookContour()
        {
            Console.Clear();
            ConsoleWriter.getInstance.DrawContour(120, 20);
        }

        private void PrintChooseBookContour()
        {
            Console.Clear();
            ConsoleWriter.getInstance.DrawContour(60, 4);
        }

        public void PrintAdminMenu(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "회원 관리", "대여 상황" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < Constant.Menu.Count.ADMIN; ++i)
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

        public void PrintAddBook(string[] warnings, List<UserInput> inputs)
        {
            Console.Clear();
            PrintAddBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            string[] instructions = new string[]
            {
                "이름 ( 영어, 한글, 숫자, ?!+= 1개 이상 ): ",
                "작가 ( 영어, 한글 1개 이상 ): ",
                "출판사 ( 영어, 한글 1개 이상 ): ",
                "수량 ( 1-999 사이의 자연수 ): ",
                "가격 ( 1-9999999 사이의 자연수: ",
                "출판 날짜 ( 19xx or 20xx-xx-xx ): ",
                "ISBN ( 정수 13개 ): ",
                "설명 ( 최소 1개의 문자(공백포함) ): "
            };

            for (int i = 0; i < instructions.Length; ++i)
            {
                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, instructions[i],
                    AlignType.LEFT);
                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, warnings[i],
                    AlignType.RIGHT, ConsoleColor.Red);
                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, inputs[i].Input,
                    AlignType.RIGHT);
            }
        }

        public void PrintDeleteBook()
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of book to remove: ", AlignType.LEFT);
        }

        public void PrintEditBook()
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of book to edit: ", AlignType.LEFT);
        }

        public void PrintDeleteResult(string str)
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, str, AlignType.CENTER);
        }
    }
}