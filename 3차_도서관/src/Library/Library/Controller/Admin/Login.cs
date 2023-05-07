using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Utility;
using System;
using System.Collections.Generic;
using Library.Model.DTO;

namespace Library.Controller.Admin
{
    public class Login
    {
        public Login()
        {
        }

        public void TryLogin()
        {
            // 현재 윈도우 창의 너비/높이의 반절을 변수에 저장함
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            Console.Clear();

            // id, password의 일치 여부를 저장하는 변수
            bool[] isLoggedIn = new bool[2] { false, false };

            // 로그인 결과의 힌트를 보여주기 위한 변수 (ex. ID가 틀렸습니다. Password가 틀렸습니다)
            string[] loginHint = new string[2] { "", "" };

            // 아이디와 비번 둘 중 하나라도 일치하지 않으면 반복
            while (!isLoggedIn[0] || !isLoggedIn[1])
            {
                // 로그인 창 출력
                View.User.LoginOrRegisterView.getInstance.PrintLogin(loginHint[0], loginHint[1]);

                List<UserInput> inputs = new List<UserInput>();

                for (int i = 0; i < Constant.Input.Count.LOGIN_INPUT; ++i)
                {
                    inputs.Add(new UserInput(ResultCode.NO, ""));

                    if (UserInputManager.getInstance.GetUserLoginInput(inputs, i) == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }
                }

                // 로그인 결과 값 얻어오기
                ResultCode loginResult = UserDAO.getInstance.LoginAsAdministrator(inputs[0].Input, inputs[1].Input);

                switch (loginResult)
                {
                    // 로그인이 성공했으면 isLoggedIn에 true를 저장
                    case ResultCode.SUCCESS:
                        isLoggedIn[0] = isLoggedIn[1] = true;
                        LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), inputs[0].Input, "성공", "로그인"));
                        break;

                    // 로그인에 실패했고, 결과가 NO_ID면 ID가 없다고 표시
                    case ResultCode.NO_ID:
                        loginHint[0] = "No ID";
                        LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), inputs[0].Input, "아이디 틀림", "로그인 시도"));
                        break;

                    // 로그인에 실패했고, 결과가 WRONG_PASSWORD면 비밀번호가 틀렸다고 표시
                    case ResultCode.WRONG_PASSWORD:
                        loginHint[1] = "Wrong Password";
                        LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), inputs[0].Input, "비밀번호 틀림", "로그인 시도"));
                        break;
                }

                // 만약 로그인에 실패했다면 무슨 오류가 있었는지 표시
                if (!isLoggedIn[0] || !isLoggedIn[1])
                {
                    View.User.LoginOrRegisterView.getInstance.PrintLogin(loginHint[0], loginHint[1]);
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Console.CursorVisible = true;
                    loginHint[0] = loginHint[1] = "";
                }
            }

            // 로그인에 성공했다면 관리자 메뉴 표시 및 다음 메뉴 입력 받기
            Menu adminMenu = new Menu();
            adminMenu.SelectAdminMenu();
        }
    }
}