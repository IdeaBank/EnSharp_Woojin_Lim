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
            DataManager dataManager = new DataManager();

            InputFromUser inputFromUser = new InputFromUser();

            InsertSampleData(data, dataManager);
            
            EntryMenuViewerClass mv = new EntryMenuViewerClass(data, inputFromUser, dataManager);
            mv.ViewEntryMenu();
        }

        public static void InsertSampleData(Data data, DataManager dataManager)
        {
            data.admins.Add(new Administrator("admin123", "admin123", "woojin", 12, "ASDF", "ASDF"));
            dataManager.userManager.Register(data, new User("user1234", "pass1234", "asdf", 12, "asdf", "asdf"));
            dataManager.userManager.Register(data, new User("user4321", "pass4321", "asdf", 12, "asdf", "asdf"));
            dataManager.userManager.Register(data, new User("sosquadron", "dnltjd01!", "asdf", 12, "asdf", "asdf"));
            
            dataManager.bookManager.AddBook(data, new Book("이클립스", "민트향", "무설탕",
                3, 1000, "2001-09-11", "123123123a 1231231231231", "입안이 상쾌해집니다"));
            dataManager.bookManager.AddBook(data, new Book("티즐", "복숭아맛", "제로",
                1, 1000, "2001-09-12", "123123123b 1231231231231", "맛있습니다"));
            dataManager.bookManager.AddBook(data, new Book("애플워치", "팀쿡", "애플",
                1, 1000, "TestDate", "2023-02-09", "손목에 채울 수 있습니다"));
            dataManager.bookManager.AddBook(data, new Book("삼다수", "생수", "수원지",
                1, 1000, "2023-04-14", "123123123c 1231231231", "갈증을 해소하세요"));
            dataManager.bookManager.AddBook(data, new Book("충전기", "LG", "구광모",
                1, 1000, "2023-05-10", "123123123d 1231231231", "데이터베이스 실습실에 두고 왔습니다"));
            dataManager.bookManager.AddBook(data, new Book("노트북 파우치", "섬유", "회사",
                1, 1000, "2018-01-01", "123123123e 1231231231", "노트북을 감싸줍니다"));
        }
    }
}