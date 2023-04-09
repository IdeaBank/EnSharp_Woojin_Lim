using System;
using System.Text.RegularExpressions;
using Library.Model;
using Library.View;
using Library.View.Admin;

namespace Library
{
    class Program
    {
        public static void Main(string[] args)
        {
            LibraryManager libraryManager = new LibraryManager();
            libraryManager.start();
            
            /*
            int currentMenuLocation = 1;
            
            Data totalData = new Data();
            BookManagement bookManager = new BookManagement();
            UserManagement userManager = new UserManagement();

            bookManager.AddBook(totalData, new Book(1, "TestBook", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.AddBook(totalData, new Book(2, "TestBook2", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.AddBook(totalData, new Book(2, "TestBook3", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.DeleteBook(totalData, 3);
            userManager.Register(totalData, new User(1, "woojin", "woojin1234", "Woojin Lim", 10,
                "010-8302-3090", "경기도 고양시 일산동구 위시티 1로 7 505동 1501호"));
            userManager.Withdraw(totalData, 1);
            Console.WriteLine(userManager.LoginAsUser(totalData, "woojin", "woojin1234"));

            
            AdminSearchBookView.Print(totalData);
            
            while (currentMenuLocation > 0)
            {
                ConsoleKeyInfo keyInput = Console.ReadKey(true);

                switch (keyInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        
                        break;
                    
                    case ConsoleKey.DownArrow:
                        break;
                    
                    case ConsoleKey.Enter:
                        break;
                    
                    case ConsoleKey.Escape:
                        currentMenuLocation /= 10;
                        break;
                }
            }
            ViewFrame menuView = new ViewFrame();
            
            //menuView.SelectLoginType(0);
            
            Console.Title = "콘솔 테스트";                          //타이틀변경

            Console.BackgroundColor = ConsoleColor.Gray; 		//배경색상변경

            Console.ForegroundColor = ConsoleColor.Red;   		//글씨색상변경

            Console.Clear();                                        //화면지우기

            Console.Beep();                                         //삑!! 소리

            Console.WriteLine("색상을 변경했습니다.");		//출력후 줄바꿈

            Console.ReadLine();                                     //입력받음

            Console.ResetColor();                             
            //콘솔배경 전경색 기본값설정

            Console.SetCursorPosition(10, 10);                      //커서의 위치 설정
            */
        }
    }
}