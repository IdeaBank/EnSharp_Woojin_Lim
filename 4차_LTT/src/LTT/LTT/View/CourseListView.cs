using LTT.Model;
using LTT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.View
{
    public class CourseListView
    {
        private ConsoleWriter consoleWriter;

        public CourseListView(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public void ShowCourseList(List<Course> courses)
        {
            Console.Clear();

            string instruction = consoleWriter.StringPadRight("NO", 4) + consoleWriter.StringPadRight("개설학과전공", 20) +
                consoleWriter.StringPadRight("학수번호", 10) + consoleWriter.StringPadRight("분반", 5) +
                consoleWriter.StringPadRight("교과목명", 40) + consoleWriter.StringPadRight("이수구분", 15) +
                consoleWriter.StringPadRight("학년", 5) + consoleWriter.StringPadRight("학점", 5) +
                consoleWriter.StringPadRight("요일 및 강의시간", 40) + consoleWriter.StringPadRight("강의실", 20) +
                consoleWriter.StringPadRight("메인 교수명", 30) + consoleWriter.StringPadRight("강의 언어", 10);

            Console.WriteLine(new string('=', 210));
            Console.WriteLine(instruction);
            Console.WriteLine(new string('=', 210));

            foreach (Course course in courses)
            {
                string value = consoleWriter.StringPadRight(course.Number.ToString(), 4) + consoleWriter.StringPadRight(course.DepartmentString, 20) +
                consoleWriter.StringPadRight(course.CurriculumNumber, 10) + consoleWriter.StringPadRight(course.ClassNumber, 5) +
                consoleWriter.StringPadRight(course.CurriculumName, 40) + consoleWriter.StringPadRight(course.CurriculumString, 15) +
                consoleWriter.StringPadRight(course.StudentAcademicYear.ToString(), 5) + consoleWriter.StringPadRight(course.Credit.ToString(), 5) +
                consoleWriter.StringPadRight(course.LectureTimeString, 40) + consoleWriter.StringPadRight(course.Classroom, 20) +
                consoleWriter.StringPadRight(course.Professor, 30) + consoleWriter.StringPadRight(course.LanguageString, 10);

                Console.WriteLine(value);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("<ESC> 키를 눌러 뒤로가기");
            Console.ResetColor();
        }
    }
}
