using System;
using Library.Constant;
using Library.Model;
using Library.Utility;
using Library.View;

namespace Library.Controller
{
    public class LibraryStart
    {
        // 현재 커서 위치 저장할 변수 선언
        private int currentSelectionIndex;
        private User.LoginOrRegister loginOrRegister;
        private Admin.Login adminLogin;
        
        public LibraryStart()
        {
            this.currentSelectionIndex = 0;
            this.loginOrRegister = new User.LoginOrRegister();
            this.adminLogin = new Admin.Login();
        }

        public void StartLibrary()
        {
            Console.Clear();
            Console.SetWindowSize(200, 50);

            // 커서 안 보이게 설정 (입력 하는 단계에서만 커서를 보이게 함)
            Console.CursorVisible = false;

            // 메뉴 선택을 저장하기 위한 변수
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);

            // 프로그램을 종료할 것인지 여부를 묻는 변수
            bool endProgram = false;

            // 프로그램을 종료하지 않았을 경우 계속 반복
            while (!endProgram)
            {
                // UI 출력 및 메뉴 선택
                UserOrAdminView.getInstance.PrintUserOrAdminContour();
                result = MenuSelector.getInstance.ChooseMenu(0, Constant.Menu.Count.MAIN, Constant.Menu.Type.USER_OR_ADMIN);

                // esc키가 눌렸을 경우 종료 여부를 물어보고 Y키를 눌렸을 경우 종료, N키를 눌렀을 경우 취소
                if (result.ResultCode == ResultCode.ESC_PRESSED)
                {
                    UserSelectionView.getInstance.PrintYesOrNO("Are you sure to exit?");

                    if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
                    {
                        endProgram = true;
                        break;
                    }

                    continue;
                }

                // 현재 커서 위치 저장
                this.currentSelectionIndex = result.ReturnedInt;

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
                case Constant.Menu.Selection.SELECT_LOGIN_OR_REGISTER:
                    loginOrRegister.SelectLoginOrRegister();
                    break;

                case Constant.Menu.Selection.ADMIN_LOGIN:
                    adminLogin.TryLogin();
                    break;
            }
        }
    }
}