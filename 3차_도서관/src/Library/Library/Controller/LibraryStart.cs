using Library.Constants;
using Library.Controller.AdminController;
using Library.Controller.UserController;
using Library.Utility;
using Library.View;
using System;
using System.Collections.Generic;

namespace Library.Controller
{
    public class LibraryStart
    {
        // 책과 유저의 정보를 다룰 때 사용하는 클래스 선언
        private BookManager bookManager;
        private UserManager userManager;
        private SqlManager sqlManager;

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
            this.bookManager = new BookManager();
            this.userManager = new UserManager();
            this.combinedManager = new CombinedManager(bookManager, userManager);
            this.bookSearcher = new BookSearcher(combinedManager);

            this.userLoginOrRegister = new UserLoginOrRegister(combinedManager);
            this.adminLogin = new AdminLogin(combinedManager);

            this.currentSelectionIndex = 0;
        }

        public void StartLibrary()
        {
            Console.Clear();
            Console.SetWindowSize(200, 50);

            // 커서 안 보이게 설정 (입력 하는 단계에서만 커서를 보이게 함)
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
                result = MenuSelector.getInstance.ChooseMenu(0, MenuCount.MAIN, MenuType.USER_OR_ADMIN);

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