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
            
            string[] warning = { "", "", "", "", "", "", "" };
            bool[] inputValid = { false, false, false, false, false, false, false };
            string[] previousInput = new string[7];
            bool allRegexPassed = false;
            KeyValuePair<FailCode, string> idInputResult = new KeyValuePair<FailCode, string>(),
                passwordInputResult = new KeyValuePair<FailCode, string>(),
                passwordConfirmResult = new KeyValuePair<FailCode, string>(),
                nameInputResult = new KeyValuePair<FailCode, string>(),
                userAgeInputResult = new KeyValuePair<FailCode, string>(),
                phoneNumberInputResult = new KeyValuePair<FailCode, string>(),
                addressInputResult = new KeyValuePair<FailCode, string>();
                

            while (!allRegexPassed)
            {
                Console.Clear();
                UserLoginOrRegisterView.PrintRegister(warning, previousInput);
                Console.ReadKey();
                Console.Clear();
                warning = new string[7];
                UserLoginOrRegisterView.PrintRegister(warning, previousInput);
                
                if (!inputValid[0])
                {
                    idInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf,
                        windowHeightHalf, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DONT_ENTER_KOREAN);

                    if (idInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, idInputResult.Value))
                    {
                        warning[0] = "8~15글자 영어, 숫자포함";
                        continue;
                    }

                    previousInput[0] = idInputResult.Value;
                    inputValid[0] = true;
                }

                if (!inputValid[1])
                {
                    passwordInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 1, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_PASSWORD, InputParameter.DONT_ENTER_KOREAN);

                    if (passwordInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_ID_AND_PASSWORD, passwordInputResult.Value))
                    {
                        warning[1] = "8~15글자 영어, 숫자포함";
                        continue;
                    }

                    previousInput[1] = new string('*', passwordInputResult.Value.Length);
                    inputValid[1] = true;
                    
                }
                
                if (!inputValid[2])
                {
                    passwordConfirmResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 2, InputMax.USER_ID_PASSWORD_LENGTH, InputParameter.IS_PASSWORD, InputParameter.DONT_ENTER_KOREAN);

                    if (passwordConfirmResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (passwordInputResult.Value != passwordConfirmResult.Value)
                    {
                        warning[1] = warning[2] = "비밀번호는 서로 같아야 합니다!";
                        previousInput[1] = previousInput[2] = "";
                        inputValid[1] = inputValid[2] = false;
                        continue;
                    }
                    
                    previousInput[2] = new string('*', passwordConfirmResult.Value.Length);
                    inputValid[2] = true;
                }
                
                if (!inputValid[3])
                {
                    nameInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 3, InputMax.USER_NAME_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.ENTER_KOREAN);

                    if (nameInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_NAME, nameInputResult.Value))
                    {
                        warning[3] = "영어, 한글 1개 이상";
                        continue;
                    }

                    previousInput[3] = nameInputResult.Value;
                    inputValid[3] = true;
                }
                
                
                if (!inputValid[4])
                {
                    userAgeInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 4, InputMax.USER_AGE_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DONT_ENTER_KOREAN);

                    if (userAgeInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_AGE, userAgeInputResult.Value))
                    {
                        warning[4] = "1-200사이의 자연수";
                        continue;
                    }

                    previousInput[4] = userAgeInputResult.Value;
                    inputValid[4] = true;
                }
                
                if (!inputValid[5])
                {
                    phoneNumberInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 5, InputMax.USER_PHONE_NUMBER_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DONT_ENTER_KOREAN);

                    if (phoneNumberInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_PHONE_NUMBER, phoneNumberInputResult.Value))
                    {
                        warning[5] = "01x-xxxx-xxxx";
                        continue;
                    }

                    previousInput[5] = phoneNumberInputResult.Value;
                    inputValid[5] = true;
                }
                
                if (!inputValid[6])
                {
                    addressInputResult = UserInputManager.ReadInputFromUser(windowWidthHalf, 
                        windowHeightHalf + 6, InputMax.USER_ADDRESS_LENGTH, InputParameter.IS_NOT_PASSWORD, InputParameter.DONT_ENTER_KOREAN);
                    
                    if (addressInputResult.Key == FailCode.ESC_PRESSED)
                    {
                        return;
                    }

                    if (!UserInputManager.MatchesRegex(RegularExpression.USER_ADDRESS, addressInputResult.Value))
                    {
                        warning[6] = "[a]";
                        continue;
                    }

                    previousInput[6] = addressInputResult.Value;
                    inputValid[6] = true;
                }

                allRegexPassed = true;
            }

            combinedManager.UserManager.AddUser(idInputResult.Value, passwordInputResult.Value, nameInputResult.Value, DateTime.Now.Year - Int32.Parse(userAgeInputResult.Value) + 1, phoneNumberInputResult.Value, addressInputResult.Value);
        }
    }
}