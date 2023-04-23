using LTT.Constant;
using LTT.Model;
using LTT.Utility;
using LTT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.Controller
{
    public class CourseRemover
    {
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private ViewList viewList;
        private MenuSelector menuSelector;

        public CourseRemover(DataManipulator dataManipulator, ConsoleWriter consoleWriter, UserInputManager userInputManager, ViewList viewList, MenuSelector menuSelector)
        {
            this.dataManipulator = dataManipulator;
            this.consoleWriter = consoleWriter;
            this.userInputManager = userInputManager;
            this.viewList = viewList;
            this.menuSelector = menuSelector;
        }

        public void RemoveCourseFromList(List<Course> courseList, int userIndex, MenuType menuType)
        {

            KeyValuePair<ResultCode, string> result = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, "");

            while (result.Key != ResultCode.ESC_PRESSED)
            {

                viewList.CourseListView.ShowCourseList(courseList);

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', 200));
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("삭제할 과목의 번호를 입력하세요: ");

                result = userInputManager.ReadInputFromUser(consoleWriter, Console.CursorLeft, Console.CursorTop, 3, false, false);

                if (result.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                if (!userInputManager.IsNumber(result.Value) || result.Value == "")
                {
                    consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "숫자를 입력해주세요", Align.LEFT, ConsoleColor.Red);
                    Console.ReadKey();
                    continue;
                }

                ResultCode removeCourseFromListResult = ResultCode.SUCCESS;

                switch (menuType)
                {
                    case MenuType.RESERVE_COURSE:
                        removeCourseFromListResult = dataManipulator.RemoveReservedCourse(courseList, Int32.Parse(result.Value), userIndex);
                        break;
                    case MenuType.ENLIST_COURSE:
                        removeCourseFromListResult = dataManipulator.RemoveEnlistedCourse(courseList, Int32.Parse(result.Value), userIndex);
                        break;
                }

                switch (removeCourseFromListResult)
                {
                    case ResultCode.SUCCESS:
                        consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "삭제 성공", Align.LEFT, ConsoleColor.Green);
                        break;

                    case ResultCode.NO_COURSE:
                        consoleWriter.PrintOnPosition(Console.CursorLeft, Console.CursorTop, "해당 번호의 강의가 없습니다!", Align.LEFT, ConsoleColor.Red);
                        break;
                }

                Console.ReadKey();
            }
        }

    }
}
