using Library.Constant;
using Library.Utility;
using System;
using System.Collections.Generic;
using Library.Model;

namespace Library.View.User
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

        public void PrintUserMenuContour()
        {
            ConsoleWriter.getInstance.DrawContour(100, 26);
            ConsoleWriter.getInstance.DrawLogo(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 - 8);
        }

        private void PrintChooseBookContour()
        {
            ConsoleWriter.getInstance.DrawContour(40, 4);
        }

        private void PrintRequestBookContour()
        {
            ConsoleWriter.getInstance.DrawContour(80, 6);
        }

        public void PrintUserMenu(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "ㅁ 도서 찾기", "ㅁ 도서 대여", "ㅁ 도서 대여 확인", "ㅁ 도서 반납", "ㅁ 도서 반납 내역", "ㅁ 회원 정보 수정", "ㅁ 회원 탈퇴", "ㅁ 네이버 검색, 도서 신청" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < Constant.Menu.Count.USER; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf - 6, consoleWindowHeightHalf + 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf - 6, consoleWindowHeightHalf + 3 + i,
                        loginOrRegister[i], AlignType.RIGHT, ConsoleColor.White);
                }
            }
        }

        public void PrintBorrowOrReturnBook()
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "ID of book: ", AlignType.LEFT);
        }

        public void PrintBorrowOrReturnBookResult(string str)
        {
            Console.Clear();
            PrintChooseBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, str, AlignType.LEFT);

        }

        public void PrintRequestBook(string nameWarning, string countWarning, List<UserInput> inputs)
        {
            Console.Clear();
            PrintRequestBookContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, "책 이름: ", AlignType.LEFT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 1, "찾을 책 수량: ", AlignType.LEFT);
            
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, nameWarning, AlignType.RIGHT, ConsoleColor.Red);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 1, countWarning, AlignType.RIGHT, ConsoleColor.Red);
            
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, inputs[0].Input, AlignType.RIGHT);
            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 1, inputs[1].Input, AlignType.RIGHT);
        }
    }
}