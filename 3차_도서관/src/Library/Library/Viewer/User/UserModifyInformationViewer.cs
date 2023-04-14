using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.User;

namespace Library.Viewer.User
{
    public class UserModifyInformationViewer: UserViewer
    {
        public UserModifyInformationViewer(Data data, DataManager dataManager, InputFromUser inputFromUser,
            int currentUserNumber) : base(data, dataManager, inputFromUser, currentUserNumber)
        {
             
        }
        
        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }

        public void PrintModifyUserInformation()
        {
            UserRegisterView.Print();
            KeyValuePair<bool, string> userId,
                userPassword,
                userName,
                userAge,
                userPhoneNumber,
                userAddress;
            
            while (true)
            {
                userId = inputFromUser.ReadInputFromUser(26, 1, 15, false, false);

                if (!userId.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(userId.Value, @"^[0-9a-zA-Z]{8,15}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 1, "8~15글자 영어, 숫자포함", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 1);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                userPassword = inputFromUser.ReadInputFromUser(26, 2, 15, false, false);

                if (!userPassword.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(userPassword.Value, @"^[0-9a-zA-Z]{8,15}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 2, "8~15글자 영어, 숫자포함", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 2);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                userName = inputFromUser.ReadInputFromUser(26, 3, 20, false, true);

                if (!userName.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(userName.Value, @"^[a-zA-Zㄱ-힇]{2,}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 3, "영어, 한글 1개 이상", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 3);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                userAge = inputFromUser.ReadInputFromUser(26, 4, 3, false, false);

                if (!userAge.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(userAge.Value, @"^200$|^(1[0-9]{2})$|^([1-9]{1}[0-9]{1})$|^([1-9]{1})$"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 4, "1-200사이의 자연수", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 4);
                Console.Write(new string(' ', 50));
            }

            while (true)
            {
                userPhoneNumber = inputFromUser.ReadInputFromUser(26, 5, 13, false, false);

                if (!userPhoneNumber.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(userPhoneNumber.Value, @"^01[0-9]{1}-[0-9]{4}-[0-9]{4}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 5, "01x-xxxx-xxxx", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 5);
                Console.Write(new string(' ', 50));
            }

            userAddress = inputFromUser.ReadInputFromUser(26, 6, 30, false, false);

            if (!userAddress.Key)
            {
                Console.Clear();
                return;
            }

            dataManager.userManager.ModifyMemberInfo(data, currentUserNumber, userId.Value, userPassword.Value, userName.Value, Int32.Parse(userAge.Value), userPhoneNumber.Value, userAddress.Value);

            Console.Clear();
        }
    }
}