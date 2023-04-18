using System;
using System.Collections.Generic;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.UserView;

namespace Library.Controller.UserController
{
    public class UserRegister: ControllerInterface
    {
        public UserRegister(TotalData data, CombinedManager combinedManager) : base(data, combinedManager)
        {
        }

        public void Register()
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;
            
            // 경고 메세지, 각 속성이 제대로 입력되었는지 여부, 이전 입력, 모든 정규식에 부합하는지 여부를 저장하는 변수 선언
            string[] warning = { "", "", "", "", "", "", "" };
            bool[] inputValid = { false, false, false, false, false, false, false };
            string[] previousInput = new string[7];
            bool allRegexPassed = false;
            
            // 각 입력 값을 저장하기 위한 변수 선언
            KeyValuePair<ResultCode, string> 
                idInputResult = new KeyValuePair<ResultCode, string>(),
                passwordInputResult = new KeyValuePair<ResultCode, string>(),
                passwordConfirmResult = new KeyValuePair<ResultCode, string>(),
                nameInputResult = new KeyValuePair<ResultCode, string>(),
                userAgeInputResult = new KeyValuePair<ResultCode, string>(),
                phoneNumberInputResult = new KeyValuePair<ResultCode, string>(),
                addressInputResult = new KeyValuePair<ResultCode, string>();
            
            // 모든 정규식에 부합할 때까지 반복
            while (!allRegexPassed)
            {
                // UI 출력 후 아무 키나 입력 받음
                Console.Clear();
                UserLoginOrRegisterView.PrintRegister(warning, previousInput);
                Console.ReadKey();
                Console.Clear();
                warning = new string[7];
                UserLoginOrRegisterView.PrintRegister(warning, previousInput);
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 아이디를 입력 받음
                if (!inputValid[0])
                {
                    idInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (idInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, idInputResult.Value))
                    {
                        warning[0] = "8~15글자 영어, 숫자포함";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[0] = idInputResult.Value;
                    inputValid[0] = true;
                }

                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 비밀번호를 입력 받음
                if (!inputValid[1])
                {
                    passwordInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 1, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (passwordInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, passwordInputResult.Value))
                    {
                        warning[1] = "8~15글자 영어, 숫자포함";
                        continue;
                    }

                    // 정규표현식에 부합하면 비밀번호의 길이만큼 *을 저장
                    previousInput[1] = new string('*', passwordInputResult.Value.Length);
                    inputValid[1] = true;
                    
                }
                
                // 처음이거나 이전에 입력한 값이 비밀번호와 다를 시 다시 입력 받음
                if (!inputValid[2])
                {
                    passwordConfirmResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 2, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (passwordConfirmResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 비밀번호와 비밀번호 확인이 다를 시 다시 입력 받음
                    if (passwordInputResult.Value != passwordConfirmResult.Value)
                    {
                        warning[1] = warning[2] = "비밀번호는 서로 같아야 합니다!";
                        previousInput[1] = previousInput[2] = "";
                        inputValid[1] = inputValid[2] = false;
                        continue;
                    }
                    
                    // 비밀번호와 일치할 시 비밀번호의 길이만큼 *을 저장
                    previousInput[2] = new string('*', passwordConfirmResult.Value.Length);
                    inputValid[2] = true;
                }
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 이름을 입력 받음
                if (!inputValid[3])
                {
                    nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 3, InputMax.USER_NAME_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (nameInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_NAME, nameInputResult.Value))
                    {
                        warning[3] = "영어, 한글 1개 이상";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[3] = nameInputResult.Value;
                    inputValid[3] = true;
                }
                
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 나이를 입력 받음
                if (!inputValid[4])
                {
                    userAgeInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 4, InputMax.USER_AGE_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (userAgeInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_AGE, userAgeInputResult.Value))
                    {
                        warning[4] = "1-200사이의 자연수";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[4] = userAgeInputResult.Value;
                    inputValid[4] = true;
                }
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 전화번호를 입력 받음
                if (!inputValid[5])
                {
                    phoneNumberInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 5, InputMax.USER_PHONE_NUMBER_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);

                    // ESC키를 입력 받을 시 반환
                    if (phoneNumberInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_PHONE_NUMBER, phoneNumberInputResult.Value))
                    {
                        warning[5] = "01x-xxxx-xxxx";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[5] = phoneNumberInputResult.Value;
                    inputValid[5] = true;
                }
                
                // 처음이거나 이전에 입력한 값이 정규표현식에 부합하지 않는다면 주소를 입력 받음
                if (!inputValid[6])
                {
                    addressInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 6, InputMax.USER_ADDRESS_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DO_NOT_ENTER_KOREAN);
                    
                    // ESC키를 입력 받을 시 반환
                    if (addressInputResult.Key == ResultCode.ESC_PRESSED)
                    {
                        return;
                    }

                    // 정규표현식에 부합하지 않으면 경고 메세지 출력 후 다시 입력 받음
                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_ADDRESS, addressInputResult.Value))
                    {
                        warning[6] = "[a]";
                        continue;
                    }

                    // 정규표현식에 부합하면 입력 값을 저장
                    previousInput[6] = addressInputResult.Value;
                    inputValid[6] = true;
                }

                allRegexPassed = true;
            }

            // 등록을 시도하고 결과값을 저장
            ResultCode registerResult = 
                combinedManager.UserManager.AddUser(idInputResult.Value, passwordInputResult.Value, nameInputResult.Value, 
                    DateTime.Now.Year - Int32.Parse(userAgeInputResult.Value) + 1, phoneNumberInputResult.Value,
                    addressInputResult.Value);
            
            // 동일 아이디가 중복되었을 시 결과 출력
            if (registerResult == ResultCode.USER_ID_EXISTS)
            {
                UserLoginOrRegisterView.PrintRegisterResult("SAME ID EXISTS!");
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