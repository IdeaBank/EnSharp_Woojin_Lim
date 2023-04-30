using Library.Constants;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.View.UserView
{
    public class UserLoginOrRegisterView
    {
        private UserLoginOrRegisterView _instance;

        private UserLoginOrRegisterView()
        {

        }

        public UserLoginOrRegisterView getInstance
        {
            get
            {
                if (this._instance == null)
                {
                    _instance = new UserLoginOrRegisterView();
                }

                return _instance;
            }
        }

        public static void PrintLoginOrRegisterContour()
        {
            ConsoleWriter.DrawContour(50, 8);
        }

        public static void PrintRegisterContour()
        {
            ConsoleWriter.DrawContour(150, 18);
        }

        public static void PrintLoginOrRegister(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "Login", "Register" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < MenuCount.USER_LOGIN_OR_REGISTER; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER, ConsoleColor.White);
                }
            }
        }

        public static void PrintLogin(string idHint, string passwordHint)
        {
            PrintLoginOrRegisterContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf - 10, windowHeightHalf, "ID: ".PadLeft(10, ' '),
                AlignType.RIGHT);

            if (idHint != "")
            {
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, idHint, AlignType.RIGHT,
                    ConsoleColor.Red);
            }

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf - 10, windowHeightHalf + 1,
                "PASSWORD: ".PadLeft(10, ' '), AlignType.RIGHT);

            if (passwordHint != "")
            {
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 1, passwordHint,
                    AlignType.RIGHT, ConsoleColor.Red);
            }
        }

        public static void PrintRegister(string[] warnings, List<KeyValuePair<ResultCode, string>> inputs)
        {
            PrintRegisterContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            string[] instructions = new string[]
            {
                "Enter ID: ",
                "Enter Password: ",
                "Confirm Password: ",
                "Enter Name: ",
                "Enter Age: ",
                "Enter Phone Number: ",
                "Enter User Address: "
            };

            for (int i = 0; i < instructions.Length; ++i)
            {
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, instructions[i],
                    AlignType.LEFT);
                ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, warnings[i],
                    AlignType.RIGHT, ConsoleColor.Red);
                
                if (i == 1 || i == 2)
                {
                    ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, new String('*', inputs[i].Value.Length),
                    AlignType.RIGHT);
                }

                else
                {
                    ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, inputs[i].Value,
                        AlignType.RIGHT);
                }
            }
        }

        public static void PrintRegisterResult(string resultString, ConsoleColor color = ConsoleColor.White)
        {
            Console.Clear();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.DrawContour(30, 5);

            ConsoleWriter.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, resultString,
                AlignType.CENTER, color);
        }
    }
}