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
            data.admins.Add(new Administrator("admin", "admin123", "woojin", 12, "ASDF", "ASDF"));
            dataManager.userManager.Register(data, new User("userid12", "asdf", "asdf", 12, "asdf", "asdf"));
            dataManager.userManager.Register(data, new User("asdf1", "asdf", "asdf", 12, "asdf", "asdf"));
            dataManager.userManager.Register(data, new User("asdf2", "asdf", "asdf", 12, "asdf", "asdf")); 
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
        }
    }
}