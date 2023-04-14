using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Exception;
using Library.Model;
using Library.View;
using Library.View.Admin;
using Library.Utility;

namespace Library
{
    class Program
    {
        public static void Main(string[] args)
        {
            Data data = new Data();
            data.admins.Add(new Administrator("admin", "admin123", "woojin", 12, "ASDF", "ASDF"));
            DataManager dataManager = new DataManager();
            
            dataManager.userManager.Register(data, new User("asdf", "asdf", "asdf", 12, "asdf", "asdf"));
            dataManager.userManager.Register(data, new User("asdf1", "asdf", "asdf", 12, "asdf", "asdf"));
            dataManager.userManager.Register(data, new User("asdf2", "asdf", "asdf", 12, "asdf", "asdf"));            // dataManager.bookManager.AddBook(data, new Book("TestBook1", "TestAuthor", "TestPublisher",
            //    1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            // dataManager.bookManager.AddBook(data, new Book("TestBook2", "TestAuthor", "TestPublisher",
            //    1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            // dataManager.bookManager.AddBook(data, new Book("TestBook3", "TestAuthor", "TestPublisher",
            //    1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            // dataManager.bookManager.AddBook(data, new Book("TestBook4", "TestAuthor", "TestPublisher",
            //    1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook5", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook6", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook7", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook8", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook9", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook10", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook11", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            dataManager.bookManager.AddBook(data, new Book("TestBook12", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            
            InputFromUser inputFromUser = new InputFromUser();
            
            EntryMenuViewerClass mv = new EntryMenuViewerClass(data, inputFromUser, dataManager);
            mv.ViewEntryMenu();
            
            // InputFromUser ifs = new InputFromUser();

            // KeyValuePair<bool, string> test = ifs.ReadInputFromUser(1, 1, 10, true);

            // Console.Write(test.Value);
            /*
            LibraryManager libraryManager = new LibraryManager();
            BookManager bookManager = new BookManager();
            UserManager userManager = new UserManager();
            
            bookManager.AddBook(libraryManager.data, new Book(1, "TestBook1", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.AddBook(libraryManager.data, new Book(2, "TestBook2", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.AddBook(libraryManager.data, new Book(3, "TestBook3", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            // bookManager.DeleteBook(libraryManager.data, 3);
            userManager.Register(libraryManager.data, new User(1, "woojin", "woojin123", "Woojin Lim", 10,
                "010-8302-3090", "경기도 고양시 일산동구 위시티 1로 7 505동 1501호"));
            libraryManager.data.admins.Add(new Administrator(1, "admin", "qhdks01!", "Administrator", 23,
                "010-8302-3090", "경기도 고양시 위시티 1로7 505동 1501호"));
            libraryManager.data.users[0].BorrowBook(libraryManager.data, 1);
            
            
            libraryManager.start();
            */
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