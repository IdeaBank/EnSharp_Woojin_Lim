using Library.Constant;
using Library.Model;
using Library.Utility;
using System;
using System.Collections.Generic;

namespace Library.View.User
{
    public class LoginOrRegisterView
    {
        private static LoginOrRegisterView _instance;

        private LoginOrRegisterView()
        {

        }

        public static LoginOrRegisterView getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoginOrRegisterView();
                }

                return _instance;
            }
        }

        public void PrintLoginOrRegisterContour()
        {
            ConsoleWriter.getInstance.DrawContour(50, 8);
        }

        public void PrintRegisterContour()
        {
            ConsoleWriter.getInstance.DrawContour(150, 18);
        }

        public void PrintLoginOrRegister(int currentSelectionIndex)
        {
            string[] loginOrRegister = new[] { "Login", "Register" };
            int consoleWindowWidthHalf = Console.WindowWidth / 2;
            int consoleWindowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < Constant.Menu.Count.USER_LOGIN_OR_REGISTER; ++i)
            {
                if (i == currentSelectionIndex)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER, ConsoleColor.Green);
                }

                else
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(consoleWindowWidthHalf, consoleWindowHeightHalf - 1 + i,
                        loginOrRegister[i], AlignType.CENTER, ConsoleColor.White);
                }
            }
        }

        public void PrintLogin(string idHint, string passwordHint)
        {
            PrintLoginOrRegisterContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf - 10, windowHeightHalf, "ID: ".PadLeft(10, ' '),
                AlignType.RIGHT);

            if (idHint != "")
            {
                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, idHint, AlignType.RIGHT,
                    ConsoleColor.Red);
            }

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf - 10, windowHeightHalf + 1,
                "PASSWORD: ".PadLeft(10, ' '), AlignType.RIGHT);

            if (passwordHint != "")
            {
                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + 1, passwordHint,
                    AlignType.RIGHT, ConsoleColor.Red);
            }
        }

        public void PrintRegister(string[] warnings, List<UserInput> inputs)
        {
            PrintRegisterContour();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            for (int i = 0; i < Constant.Input.Instruction.USER_INPUT_INSTRUCTION.Length; ++i)
            {
                if (inputs[i].ResultCode == ResultCode.SUCCESS)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, Constant.Input.Instruction.USER_INPUT_INSTRUCTION[i],
                        AlignType.LEFT, ConsoleColor.Green);
                }

                if (i == 1 || i == 2)
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, new String('*', inputs[i].Input.Length),
                    AlignType.RIGHT);
                }

                else
                {
                    ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, inputs[i].Input,
                        AlignType.RIGHT);
                }

                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, Constant.Input.Instruction.USER_INPUT_INSTRUCTION[i],
                    AlignType.LEFT);
                ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf + i, warnings[i],
                    AlignType.RIGHT, ConsoleColor.Red);

            }
        }

        public void PrintRegisterResult(string resultString, ConsoleColor color = ConsoleColor.White)
        {
            Console.Clear();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            ConsoleWriter.getInstance.DrawContour(30, 5);

            ConsoleWriter.getInstance.WriteOnPositionWithAlign(windowWidthHalf, windowHeightHalf, resultString,
                AlignType.CENTER, color);
        }
    }
}