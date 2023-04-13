using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Constants;
using Library.Model;
using Library.Utility;
using Library.View.User;

namespace Library.Viewer.User
{
    public class UserRegisterViewer : ViewerClass
    {
        public bool IsInputValid(string str, string regularExpression)
        {
            Regex regex = new Regex(regularExpression);

            return regex.IsMatch(str);
        }
        
        public UserRegisterViewer(Data data, DataManager dataManager, InputFromUser inputFromUser) : base(data,
            dataManager, inputFromUser)
        {
        }

        public void TryRegister()
        {
            UserRegisterView.Print();
            KeyValuePair<bool, string> userID,
                userPassword,
                userName,
                UserAge,
                UserPhoneNumber,
                UserAddress;

            while (true)
            {
                userID = inputFromUser.ReadInputFromUser(26, 1, 15, false, false);

                if (!userID.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(userID.Value, @"^[0-9a-zA-Z]{8,15}"))
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
                UserAge = inputFromUser.ReadInputFromUser(26, 4, 3, false, false);

                if (!UserAge.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(UserAge.Value, @"^200$|^(1[0-9]{2})$|^([1-9]{1}[0-9]{1})$|^([1-9]{1})$"))
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
                UserPhoneNumber = inputFromUser.ReadInputFromUser(26, 5, 13, false, false);

                if (!UserPhoneNumber.Key)
                {
                    Console.Clear();
                    return;
                }

                if (IsInputValid(UserPhoneNumber.Value, @"^01[0-9]{1}-[0-9]{4}-[0-9]{4}"))
                {
                    break;
                }

                FramePrinter.PrintOnPosition(26, 5, "01x-xxxx-xxxx", AlignType.LEFT, ConsoleColor.Red);
                Console.ReadKey(true);
                Console.SetCursorPosition(26, 5);
                Console.Write(new string(' ', 50));
            }

            //while (true)
            //{
            UserAddress = inputFromUser.ReadInputFromUser(26, 6, 30, false, false);

            if (!UserAddress.Key)
            {
                Console.Clear();
                return;
            }

                
            //    break;

                // FramePrinter.PrintOnPosition(26, 6, "도로명 주소 오류", AlignType.LEFT, ConsoleColor.Red);
                // Console.ReadKey(true);
                // Console.SetCursorPosition(26, 6);
                // Console.Write(new string(' ', 50));
            //}

            dataManager.userManager.Register(data,
                new Model.User(userID.Value, userPassword.Value, userName.Value,
                    Int32.Parse(UserAge.Value), UserPhoneNumber.Value,
                    UserAddress.Value));

            Console.Clear();
        }
    } 
}