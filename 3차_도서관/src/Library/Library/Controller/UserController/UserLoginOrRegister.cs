using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;
using System;
using System.Collections.Generic;

namespace Library.Controller.UserController
{
    public class UserLoginOrRegister : ControllerInterface
    {
        private int currentSelectionIndex;

        public UserLoginOrRegister(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {
            this.currentSelectionIndex = 0;
        }

        public void SelectLoginOrRegister()
        {
            // 메뉴 선택 결과를 저장하기 위한 변수 선언
            KeyValuePair<ResultCode, int> result = new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, -1);

            // ESC키가 눌릴 때까지 반복
            while (result.Key != ResultCode.ESC_PRESSED)
            {
                // UI를 출력하고 메뉴를 선택함
                UserLoginOrRegisterView.PrintLoginOrRegisterContour();
                result = MenuSelector.ChooseMenu(0, MenuCount.USER_LOGIN_OR_REGISTER, MenuType.USER_LOGIN_OR_REGISTER);

                // ESC키가 눌리면 반환
                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 현재 인덱스 값을 currentSelectionIndex에 저장
                this.currentSelectionIndex = result.Value;

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
                    UserLogin userLogin = new UserLogin(data, combinedManager);
                    userLogin.TryLogin();
                    break;
                case 1:
                    UserRegister userRegister = new UserRegister(data, combinedManager);
                    userRegister.Register();
                    break;
            }
        }
    }
}