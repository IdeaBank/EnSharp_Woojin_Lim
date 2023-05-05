using Library.Constant;
using Library.Model;
using Library.Utility;
using System;

namespace Library.Controller.User
{
    public class LoginOrRegister
    {
        private int currentSelectionIndex;

        public LoginOrRegister()
        {
            this.currentSelectionIndex = 0;
        }

        public void SelectLoginOrRegister()
        {
            Console.Clear();

            // 메뉴 선택 결과를 저장하기 위한 변수 선언
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);

            // ESC키가 눌릴 때까지 반복
            while (result.ResultCode != ResultCode.ESC_PRESSED)
            {
                // UI를 출력하고 메뉴를 선택함
                View.User.LoginOrRegisterView.getInstance.PrintLoginOrRegisterContour();
                result = MenuSelector.getInstance.ChooseMenu(0, Constant.Menu.Count.USER_LOGIN_OR_REGISTER, Constant.Menu.Type.USER_LOGIN_OR_REGISTER);

                // ESC키가 눌리면 반환
                if (result.ResultCode == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 현재 인덱스 값을 currentSelectionIndex에 저장
                this.currentSelectionIndex = result.ReturnedInt;

                // 해당 인덱스의 메뉴로 이동
                EnterNextMenu();
                Console.Clear();
            }
        }

        private void EnterNextMenu()
        {
            // 인덱스에 따라 다음 메뉴로 이동
            switch (this.currentSelectionIndex)
            {
                case 0:
                    Login userLogin = new Login();
                    userLogin.TryLogin();
                    break;
                case 1:
                    Register userRegister = new Register();
                    userRegister.TryRegister();
                    break;
            }
        }
    }
}