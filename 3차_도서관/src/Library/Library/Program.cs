using System;
using System.Text.RegularExpressions;

namespace Library
{
    class Program
    {
        public static void Main(string[] args)
        {
            Data totalData = new Data();
            BookManagement bookManager = new BookManagement();

            bookManager.AddBook(totalData, new Book(1, "TestBook", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.AddBook(totalData, new Book(2, "TestBook2", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.AddBook(totalData, new Book(2, "TestBook3", "TestAuthor", "TestPublisher",
                1, 1000, "TestDate", "TestIsbn", "TestDescription"));
            bookManager.DeleteBook(totalData, 3);

            /*
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