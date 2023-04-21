using System;
using System.Collections.Generic;
using LTT.Constant;
using LTT.Model;
using LTT.Utility;
using LTT.View;

namespace LTT.Controller
{
    public class LectureTimeTableStart
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private StudentLoginView studentLoginView;

        public LectureTimeTableStart()
        {
            this.totalData = new TotalData();
            this.dataManipulator = new DataManipulator();
            this.consoleWriter = new ConsoleWriter();
            this.userInputManager = new UserInputManager();
            this.studentLoginView = new StudentLoginView(consoleWriter);
        }
        
        public void StartProgram()
        {
            AddSampleStudent();
            Console.Clear();

            ResultCode finish = ResultCode.SUCCESS;
            
            while (finish != ResultCode.Y_PRESSED)
            {
                ResultCode loginResult = TryLogin();
                if (loginResult == ResultCode.ESC_PRESSED)
                {
                    finish = userInputManager.InputYesOrNo();
                }
            }

            Console.CursorVisible = true;
        }

        private void AddSampleStudent()
        {
            this.totalData.Students.Add(new Student("20011787", "dnltjd01!"));
        }

        private ResultCode TryLogin()
        {
            studentLoginView.PrintLogin("", "");
            
            int targetLeft = Console.WindowWidth / 4 * 3;
            int targetTop = Console.WindowHeight / 4 * 3;
            
            // id, password의 일치 여부를 저장하는 변수 
            bool[] isLoggedIn = new bool[2] { false, false };
            // 로그인 결과의 힌트를 보여주기 위한 변수 (ex. ID가 틀렸습니다. Password가 틀렸습니다)
            string[] loginHint = new string[2] { "", "" };
            
            // 아이디와 비번 둘 중 하나라도 일치하지 않으면 반복
            while (!isLoggedIn[0] || !isLoggedIn[1])
            {
                // 로그인 창 출력
                studentLoginView.PrintLogin(loginHint[0], loginHint[1]);
                
                // 아이디 입력 받기
                KeyValuePair<ResultCode, string> inputId = userInputManager.ReadInputFromUser(consoleWriter, 
                targetLeft + 3, targetTop, 8, false, false, "");

                // esc가 눌렸으면 함수를 끝냄
                if (inputId.Key == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }

                // 비밀번호 입력 받기
                KeyValuePair<ResultCode, string> inputPassword = userInputManager.ReadInputFromUser(consoleWriter, targetLeft + 3, 
                    targetTop + 1, 15, true, false, "");
                
                // esc가 눌렸으면 함수를 끝냄
                if (inputPassword.Key == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }

                // 로그인 결과 값 얻어오기
                KeyValuePair<ResultCode, int> loginResult = dataManipulator.TryLogin(this.totalData, inputId.Value, inputPassword.Value);

                // 로그인이 성공했으면 isLoggedIn에 true를 저장
                if (loginResult.Key == ResultCode.SUCCESS)
                {
                    isLoggedIn[0] = isLoggedIn[1] = true;
                }
                
                // 로그인에 실패했고, 결과가 NO_ID면 ID가 없다고 표시
                if (loginResult.Key == ResultCode.ID_DO_NO_EXIST)
                {
                    loginHint[0] = "No ID";
                }

                // 로그인에 실패했고, 결과가 WRONG_PASSWORD면 비밀번호가 틀렸다고 표시
                if (loginResult.Key == ResultCode.PASSWORD_DO_NOT_MATCH)
                {
                    loginHint[1] = "Wrong Password";
                }

                // 만약 로그인에 실패했다면 무슨 오류가 있었는지 표시
                if (!isLoggedIn[0] || !isLoggedIn[1])
                {
                    studentLoginView.PrintLogin(loginHint[0], loginHint[1]);
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Console.CursorVisible = true;
                    loginHint[0] = loginHint[1] = "";
                }
            }
            
            return ResultCode.SUCCESS;
        }
    }
}