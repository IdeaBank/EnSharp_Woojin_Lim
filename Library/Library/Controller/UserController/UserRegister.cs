using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;
using System;
using System.Collections.Generic;

namespace Library.Controller.UserController
{
    public class UserRegister : ControllerInterface
    {
        public UserRegister(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {
        }

        public void Register()
        {
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = new string[7];
            string[] warning_message = { "8~15글자 영어, 숫자포함", "8~15글자 영어, 숫자포함", "8~15글자 영어, 숫자포함", "영어, 한글 1개 이상", "1-200사이의 자연수", "01x-xxxx-xxxx", "[a]" };
            bool allRegexPassed = false;

            // 각 입력 값을 저장하기 위한 변수 선언
            List<KeyValuePair<ResultCode, string>> inputs = new List<KeyValuePair<ResultCode, string>>();

            for (int i = 0; i < 7; ++i)
            {
                inputs.Add(new KeyValuePair<ResultCode, string>(ResultCode.NO, ""));
            }

            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 일시 정지
                UserLoginOrRegisterView.PrintRegister(warning, inputs);
                Console.ReadKey();

                // 이후 경고 내용을 없앰
                warning = new string[7];
                UserLoginOrRegisterView.PrintRegister(warning, inputs);

                bool isInputValid = true;

                for (int i = 0; i < 7 && isInputValid; ++i)
                {
                    if (inputs[i].Key == ResultCode.SUCCESS)
                    {
                        continue;
                    }

                    ResultCode inputResult = UserInputManager.GetUserInformationInput(inputs, i);

                    switch (inputResult)
                    {
                        case ResultCode.ESC_PRESSED:
                            return;

                        case ResultCode.DO_NOT_MATCH_REGEX:
                            warning[i] = warning_message[i];
                            isInputValid = false;
                            break;

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

            // 등록을 시도하고 결과값을 저장
            ResultCode registerResult = combinedManager.UserManager.AddUser(inputs[0].Value, inputs[1].Value, inputs[3].Value, DateTime.Now.Year - Int32.Parse(inputs[4].Value) + 1,
                inputs[5].Value, inputs[6].Value);

            // 동일 아이디가 중복되었을 시 결과 출력
            if (registerResult == ResultCode.USER_ID_EXISTS)
            {
                UserLoginOrRegisterView.PrintRegisterResult("SAME ID EXISTS!", ConsoleColor.Red);
                Console.ReadKey(true);
            }

            // 성공 결과 출력
            else
            {
                UserLoginOrRegisterView.PrintRegisterResult("REGISTER SUCCESS!");
                Console.ReadKey(true);
            }
        }
    }
}