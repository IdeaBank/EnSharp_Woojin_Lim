using System;
using Library.Constants;
using Library.Model;
using Library.Utility;

namespace Library.Controller
{
    public class LibraryStart
    {
        private TotalData totalData;
        private BookManager bookManager;
        private UserManager userManager;
        private CombinedManager combinedManager;
        
        private int currentSelectionIndex;
        
        public LibraryStart()
        {
            this.totalData = new TotalData();
            this.bookManager = new BookManager(totalData);
            this.userManager = new UserManager(totalData);
            this.combinedManager = new CombinedManager(this.bookManager, this.userManager);

            this.currentSelectionIndex = 0;
        }

        private void AddSampleData()
        {
            // Add administrator
            User administrator = new User();
            administrator.Id = "admin123";
            administrator.Password = "admin123";
            this.totalData.Administrators.Add(administrator);

            // Add sample user
            this.combinedManager.UserManager.AddUser("userid12", "userpw12", "User 1",
                2001, "010-1234-1234", "서울특별시 종로구 청와대로 1");
            this.combinedManager.UserManager.AddUser("userid09", "userpw11", "User 2",
                2001, "010-9876-5432", "");

            // Add sample books
            this.combinedManager.BookManager.AddBook("세이노의 가르침", "세이노", "데이윈", 10, 7200, "2023-03-02",
                "979116847S 9791168473690", "베스트 셀러입니다.");
            this.combinedManager.BookManager.AddBook("내일을 바꾸는 인생 공부", "신진상", "미디어 숲", 5, 17800, "2023-05-10",
                "979115874N 9791158741884", "예약 판매중입니다.");
            this.combinedManager.BookManager.AddBook("사장학개론", "김승호", "스노우폭스북스", 15, 22500, "2023-04-19",
                "979118833S 9791188331888", "신간 도서입니다.");
            this.combinedManager.BookManager.AddBook("메리골드 마음 세탁소", "윤정은", "북로망스", 15, 13500, "2023-03-06",
                "979119189M 9791191891287", "마음을 세탁하세요.");
            this.combinedManager.BookManager.AddBook("돌연한 출발", "프란츠 카프카", "민음사", 15, 16000, "2023-04-07",
                "978893742D 9788937427831", "갑작스러운 출발.");
        }

        public void StartLibrary()
        {
            AddSampleData();
            ChooseMenu();
        }

        private void MoveCursorInMenu(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.UP:
                    this.currentSelectionIndex =
                        (this.currentSelectionIndex + MenuCount.MAIN_MENU - 1) % MenuCount.MAIN_MENU;
                    break;
                case MoveDirection.DOWN:
                    this.currentSelectionIndex =
                        (this.currentSelectionIndex + 1) % MenuCount.MAIN_MENU;
                    break;
            }
        }

        private void EnterNextMenu()
        {
            switch (currentSelectionIndex)
            {
                case 0:
                    
                    break;
                case 1:
                    
                    break;
            }
        }
        
        private void ChooseMenu()
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey(true);
                
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveCursorInMenu(MoveDirection.UP);
                        break;
                    
                    case ConsoleKey.DownArrow:
                        MoveCursorInMenu(MoveDirection.DOWN);
                        break;
                    
                    case ConsoleKey.Enter:
                        EnterNextMenu();
                        break;
                    
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}