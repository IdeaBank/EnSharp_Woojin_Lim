using LTT.Constant;
using LTT.Model;
using LTT.Utility;
using LTT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.Controller.EnlistCourse
{
    public class EnlistMenu
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private ViewList viewList;
        private MenuSelector menuSelector;
        private int currentSelectionIndex;
        private int userIndex;

        public EnlistMenu(TotalData totalData, DataManipulator dataManipulator, ConsoleWriter consoleWriter, UserInputManager userInputManager, ViewList viewList, MenuSelector menuSelector, int userIndex)
        {
            this.totalData = totalData;
            this.dataManipulator = dataManipulator;
            this.consoleWriter = consoleWriter;
            this.userInputManager = userInputManager;
            this.viewList = viewList;
            this.menuSelector = menuSelector;
            this.userIndex = userIndex;
        }

        public void Start()
        {
            this.currentSelectionIndex = 0;

            viewList.EnlistMenuView.MakeView();

            KeyValuePair<ResultCode, int> selectResult = new KeyValuePair<ResultCode, int>();

            while (selectResult.Key != ResultCode.ESC_PRESSED)
            {
                selectResult = menuSelector.ChooseMenu(0, MenuCount.ENLIST_MENU, MenuType.ENLIST_COURSE);

                if (selectResult.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                else
                {
                    this.currentSelectionIndex = selectResult.Value;

                    switch (currentSelectionIndex)
                    {
                        case 0:
                            AddEnlistedCourse();
                            break;
                        case 1:
                            viewList.CourseListView.ShowCourseList(totalData.Students[userIndex].EnListedCourses);
                            Console.ReadKey(true);
                            break;
                        case 2:
                            Console.Clear();
                            viewList.TimeTableView.ShowTimeTable(totalData.Students[userIndex].EnListedCourses);
                            Console.ReadKey(true);
                            break;
                        case 3:
                            Console.WriteLine("4");
                            break;
                    }

                    viewList.EnlistMenuView.MakeView();
                }
            }
        }

        private void AddEnlistedCourse()
        {
            LectureTimeSearcher lectureTimeSearcher = new LectureTimeSearcher(totalData, dataManipulator, consoleWriter, userInputManager, viewList, menuSelector);
            viewList.LectureTimeSearchView.MakeView();

            KeyValuePair<ResultCode, List<Course>> searchResultList = lectureTimeSearcher.LectureTimeSearch(totalData.Students[userIndex].EnListedCourses);

            if (searchResultList.Key == ResultCode.ESC_PRESSED)
            {
                return;
            }

            KeyValuePair<ResultCode, string> result = new KeyValuePair<ResultCode, string>();

            while (result.Key != ResultCode.ESC_PRESSED)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', 200));
                Console.SetCursorPosition(0, Console.CursorTop);

                int studentTotalEnlistedCourse = 0;

                foreach (Course course in totalData.Students[userIndex].EnListedCourses)
                {
                    studentTotalEnlistedCourse += course.Credit;
                }

                viewList.EnlistMenuView.MakeEnlistAddingView(24 - studentTotalEnlistedCourse, studentTotalEnlistedCourse);

                result = userInputManager.ReadInputFromUser(consoleWriter, Console.CursorLeft, Console.CursorTop, 3, false, false);

                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                if (!userInputManager.IsNumber(result.Value) || result.Value == "")
                {
                    consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "숫자를 입력해주세요", Align.LEFT, ConsoleColor.Green);
                    Console.ReadKey();
                    continue;
                }

                ResultCode addEnlistedCourseResult = dataManipulator.AddEnlistedCourse(totalData, searchResultList.Value, Int32.Parse(result.Value), userIndex);

                switch (addEnlistedCourseResult)
                {
                    case ResultCode.SUCCESS:
                        consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "등록 성공", Align.LEFT, ConsoleColor.Green);
                        break;

                    case ResultCode.NO_COURSE:
                        consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "이미 등록되어 있거나 없는 강의입니다!", Align.LEFT, ConsoleColor.Red);
                        break;

                    case ResultCode.FAIL:
                        consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "해당 시간에 이미 강의가 있습니다!", Align.LEFT, ConsoleColor.Red);
                        break;

                    case ResultCode.OVER_MAX:
                        consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "최대 등록 가능한 학점 수를 초과했습니다!", Align.LEFT, ConsoleColor.Red);
                        break;
                }

                Console.ReadKey();
            }
        }
    }
}
