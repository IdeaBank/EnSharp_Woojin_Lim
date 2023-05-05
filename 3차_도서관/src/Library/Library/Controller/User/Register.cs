using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Model.DTO;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.Controller.User
{
    public class Register
    {
        public Register()
        {
        }

        public void TryRegister()
        {
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[7];
            string[] warning_message = { "8~15글자 영어, 숫자포함", "8~15글자 영어, 숫자포함", "8~15글자 영어, 숫자포함", "영어, 한글 1개 이상", "1-200사이의 자연수", "01x-xxxx-xxxx", "XX도 XX시" };
            bool allRegexPassed = false;

            // 각 입력 값을 저장하기 위한 변수 선언
            List<UserInput> inputs = new List<UserInput>();

            // 빈 값을 inputs에 넣어줌
            for (int i = 0; i < Constant.Input.Count.ADD_MEMBER; ++i)
            {
                inputs.Add(new UserInput(ResultCode.NO, ""));
            }

            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 일시 정지
                View.User.LoginOrRegisterView.getInstance.PrintRegister(warning, inputs);
                 Console.ReadKey();

                // 이후 경고 내용을 없앰
                warning = new string[7];
                View.User.LoginOrRegisterView.getInstance.PrintRegister(warning, inputs);

                bool isInputValid = true;

                for (int i = 0; i < Constant.Input.Count.ADD_MEMBER && isInputValid; ++i)
                {
                    if (inputs[i].ResultCode == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    // 유저 정보를 입력 받음
                    ResultCode inputResult = UserInputManager.getInstance.GetUserInformationInput(inputs, i);

                    switch (inputResult)
                    {
                        // ESC키가 눌렸으면 이를 반환
                        case ResultCode.ESC_PRESSED:
                            return;

                        // 정규식에 맞지 않으면 경고 메세지 출력
                        case ResultCode.DO_NOT_MATCH_REGEX:
                            warning[i] = warning_message[i];
                            isInputValid = false;
                            break;

                        case ResultCode.USER_ID_EXISTS:
                            warning[0] = "USER ID EXISTS!!";
                            isInputValid = false;
                            break;

                        // 패스워드와 패스워드 확인이 다르면 경고 메세지 출력
                        case ResultCode.DO_NOT_MATCH_PASSWORD:
                            warning[1] = warning[2] = "PASSWORD DO NOT MATCH!";
                            isInputValid = false;
                            break;

                        case ResultCode.SUCCESS:
                            break;
                    }
                }

                if (!isInputValid)
                {
                    continue;
                }

                allRegexPassed = true;
            }

            UserDTO user = new UserDTO();

            user.Id = inputs[0].Input;
            user.Password = inputs[1].Input;
            user.Name = inputs[3].Input;
            user.BirthYear = DateTime.Now.Year - int.Parse(inputs[4].Input) + 1;
            user.PhoneNumber = inputs[5].Input;
            user.Address = inputs[6].Input;

            // 등록을 시도하고 결과값을 저장
            ResultCode registerResult = UserDAO.getInstance.AddUser(user);

            // 동일 아이디가 중복되었을 시 결과 출력
            if (registerResult == ResultCode.USER_ID_EXISTS)
            {
                View.User.LoginOrRegisterView.getInstance.PrintRegisterResult("SAME ID EXISTS!", ConsoleColor.Red);
                Console.ReadKey(true);
            }

            // 성공 결과 출력
            else
            {
                View.User.LoginOrRegisterView.getInstance.PrintRegisterResult("REGISTER SUCCESS!");
                Console.ReadKey(true);
            }
        }
    }
}