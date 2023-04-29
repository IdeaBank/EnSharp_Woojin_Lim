using Library.Constants;
using Library.Controller.AdminController;
using Library.Controller.UserController;
using Library.Model;
using Library.Utility;
using Library.View;
using System;
using System.Collections.Generic;

namespace Library.Controller
{
    public class LibraryStart
    {
        // 모든 유저 / 책 데이터를 저장할 totalData 선언
        private TotalData totalData;

        // 책과 유저의 정보를 다룰 때 사용하는 클래스 선언
        private BookManager bookManager;
        private UserManager userManager;

        // 통합해서 관리할 수 있게 해주는 클래스 선언
        private CombinedManager combinedManager;
        private BookSearcher bookSearcher;

        // 현재 커서 위치 저장할 변수 선언
        private int currentSelectionIndex;


        private UserLoginOrRegister userLoginOrRegister;
        private AdminLogin adminLogin;

        public LibraryStart()
        {
            // 생성자에서는 각 멤버 변수의 인스턴스 생성 및 커서 위치 0으로 초기화

            this.totalData = new TotalData();
            this.bookManager = new BookManager(totalData);
            this.userManager = new UserManager(totalData);
            this.combinedManager = new CombinedManager(bookManager, userManager);
            this.bookSearcher = new BookSearcher(totalData, combinedManager);

            this.userLoginOrRegister = new UserLoginOrRegister(totalData, combinedManager);
            this.adminLogin = new AdminLogin(totalData, combinedManager);

            this.currentSelectionIndex = 0;
        }

        private void AddSampleData()
        {
            // 관리자 추가
            User administrator = new User
            {
                Id = "admin123",
                Password = "admin123"
            };

            this.totalData.Administrators.Add(administrator);

            // 유저 예시 추가
            this.combinedManager.UserManager.AddUser("userid12", "userpw12", "User 1",
                2001, "010-1234-1234", "서울특별시 종로구 청와대로 1");
            this.combinedManager.UserManager.AddUser("userid09", "userpw11", "User 2",
                2001, "010-9876-5432", "경기도 고양시 일산동구");

            // 책 예시 추가
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
            Console.Clear();
            AddSampleData();

            // 커서 안 보이게 설정
            Console.CursorVisible = false;

            // 메뉴 선택을 저장하기 위한 변수
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            // 프로그램을 종료할 것인지 여부를 묻는 변수
            bool endProgram = false;

            // 프로그램을 종료하지 않았을 경우 계속 반복
            while (!endProgram)
            {
                // UI 출력 및 메뉴 선택
                UserOrAdminView.PrintUserOrAdminContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.MAIN, MenuType.USER_OR_ADMIN);

                // esc키가 눌렸을 경우 종료 여부를 물어보고 Y키를 눌렸을 경우 종료, N키를 눌렀을 경우 취소
                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    UserSelectionView.PrintYesOrNO("Are you sure to exit?");

                    if (UserInputManager.InputYesOrNo() == ResultCode.YES)
                    {
                        endProgram = true;
                        break;
                    }

                    continue;
                }

                // 현재 커서 위치 저장
                this.currentSelectionIndex = result.Value;

                // 선택한 메뉴 진입
                EnterNextMenu();
                Console.Clear();
            }
        }

        private void EnterNextMenu()
        {
            // 현재 커서 위치에 따라 메뉴 선택
            switch (this.currentSelectionIndex)
            {
                case MenuSelection.SELECT_LOGIN_OR_REGISTER:
                    userLoginOrRegister.SelectLoginOrRegister();
                    break;

                case MenuSelection.ADMIN_LOGIN:
                    adminLogin.TryLogin();
                    break;
            }
        }
    }
}