using System;
using System.Collections.Generic;
using LTT.Constant;
using LTT.Model;
using LTT.Utility;
using LTT.View;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTT.Controller
{
    public class LectureTimeTableStart
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private ViewList viewList;
        private MenuSelector menuSelector;

        public LectureTimeTableStart()
        {
            this.totalData = new TotalData();
            this.dataManipulator = new DataManipulator();
            this.consoleWriter = new ConsoleWriter();
            this.userInputManager = new UserInputManager();
            this.viewList = new ViewList(consoleWriter);
            this.menuSelector = new MenuSelector(viewList);
        }

        public void StartProgram()
        {
            AddSampleStudent();
            GetLectureTimeData();

            Console.Clear();

            ResultCode finish = ResultCode.SUCCESS;

            while (finish != ResultCode.Y_PRESSED)
            {
                ResultCode loginResult = TryLogin();

                if (loginResult == ResultCode.ESC_PRESSED)
                {
                    viewList.StudentLoginView.PrintQuitConfirm();
                    finish = userInputManager.InputYesOrNo();
                }

                else
                {
                    MainMenu mainMenu = new MainMenu(totalData, dataManipulator, consoleWriter, userInputManager, viewList, menuSelector);
                    mainMenu.Start();
                }
            }

            Console.CursorVisible = true;
        }

        private void AddSampleStudent()
        {
            this.totalData.Students.Add(new Student("20011787", "dnltjd01!"));
        }

        private void GetLectureTimeData()
        {
            try
            {
                // Excel Application 객체 생성
                Excel.Application application = new Excel.Application();

                // Workbook 객체 생성 및 파일 오픈 (바탕화면에 있는 excelStudy.xlsx 가져옴)
                Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\2023년도 1학기 강의시간표.xlsx");;

                // sheets에 읽어온 엑셀값을 넣기 (한 workbook 내의 모든 sheet 가져옴)
                Excel.Sheets sheets = workbook.Sheets;

                // 특정 sheet의 값 가져오기
                Excel.Worksheet worksheet = sheets["Sheet1"] as Excel.Worksheet;

                // 범위 설정 (좌측 상단, 우측 하단)
                Excel.Range cellRange = worksheet.get_Range("A2", "L185") as Excel.Range;

                // 설정한 범위만큼 데이터 담기 (Value2 -셀의 기본 값 제공)
                Array data = (Array)cellRange.Cells.Value2;

                for (int i = 1; i < 185; ++i)
                {
                    int number = Int32.Parse(data.GetValue(i, 1)?.ToString());
                    string department = data.GetValue(i, 2)?.ToString();
                    string curriculumNumber = data.GetValue(i, 3)?.ToString();
                    string classNumber = data.GetValue(i, 4)?.ToString();
                    string curriculumName = data.GetValue(i, 5)?.ToString();
                    string curriculumType = data.GetValue(i, 6)?.ToString();
                    int studentAcademicYear = Int32.Parse(data.GetValue(i, 7)?.ToString());
                    int credit = Int32.Parse(data.GetValue(i, 8)?.ToString());
                    string lectureTimes = data.GetValue(i, 9)?.ToString();
                    string classroom = data.GetValue(i, 10)?.ToString();
                    string professor = data.GetValue(i, 11)?.ToString();
                    string language = data.GetValue(i, 12)?.ToString();

                    totalData.Courses.Add(dataManipulator.MakeCourse(number, department, curriculumNumber,
                        classNumber, curriculumName, curriculumType, studentAcademicYear, credit, lectureTimes,
                        classroom, professor, language));
                }

                // 모든 워크북 닫기
                application.Workbooks.Close();

                // application 종료
                application.Quit();
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private ResultCode TryLogin()
        {
            viewList.StudentLoginView.PrintLogin("", "");

            // id, password의 일치 여부를 저장하는 변수 
            KeyValuePair<ResultCode, int> loginResult = new KeyValuePair<ResultCode, int>(ResultCode.FAIL, -1);
            // 로그인 결과의 힌트를 보여주기 위한 변수 (ex. ID가 틀렸습니다. Password가 틀렸습니다)
            string[] loginHint = new string[2] { "", "" };

            // 아이디와 비번 둘 중 하나라도 일치하지 않으면 반복
            while (loginResult.Key != ResultCode.SUCCESS)
            {
                // 콘솔창의 크기 얻어오기
                int targetLeft = Console.WindowWidth / 4 * 3;
                int targetTop = Console.WindowHeight / 4 * 3;

                // 로그인 창 출력
                viewList.StudentLoginView.PrintLogin(loginHint[0], loginHint[1]);

                // 아이디 입력 받기
                KeyValuePair<ResultCode, string> inputId = userInputManager.ReadInputFromUser(consoleWriter, targetLeft + 4, targetTop, 8, false, false, "");

                // esc가 눌렸으면 함수를 끝냄
                if (inputId.Key == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }

                // 비밀번호 입력 받기
                KeyValuePair<ResultCode, string> inputPassword = userInputManager.ReadInputFromUser(consoleWriter, targetLeft + 4, targetTop + 1, 15, true, false, "");

                // esc가 눌렸으면 함수를 끝냄
                if (inputPassword.Key == ResultCode.ESC_PRESSED)
                {
                    return ResultCode.ESC_PRESSED;
                }

                // 로그인 결과 값 얻어오기
                loginResult = dataManipulator.TryLogin(this.totalData, inputId.Value, inputPassword.Value);

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
                if (loginResult.Key != ResultCode.SUCCESS)
                {
                    // 로그인 힌트 표시
                    viewList.StudentLoginView.PrintLogin(loginHint[0], loginHint[1]);

                    // 일시 정지
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Console.CursorVisible = true;

                    // 띄워준 힌트는 없앰
                    loginHint[0] = loginHint[1] = "";
                }
            }

            return ResultCode.SUCCESS;
        }
    }
}