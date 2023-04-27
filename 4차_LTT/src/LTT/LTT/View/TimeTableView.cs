using LTT.Model;
using LTT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.View
{
    public class TimeTableView
    {
        private ConsoleWriter consoleWriter;

        public TimeTableView(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public void ShowTimeTable(List<Course> courses)
        {
            for (int i = 1; i < 6; ++i)
            {
                Console.SetCursorPosition(i * 30, 4);

                switch (i)
                {
                    case 1:
                        Console.Write("월");
                        break;
                    case 2:
                        Console.Write("화");
                        break;
                    case 3:
                        Console.Write("수");
                        break;
                    case 4:
                        Console.Write("목");
                        break;
                    case 5:
                        Console.Write("금");
                        break;
                }
            }

            for (int i = 480; i < 1260; i += 30)
            {
                Console.SetCursorPosition(1, ((i - 480) / 30 * 2) + 10);
                Console.Write((i / 60).ToString("00") + ":" + (i % 60).ToString("00") + "~" + ((i + 30) / 60).ToString("00") + ":" + ((i + 30) % 60).ToString("00"));
            }

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(new string('-', 50));

            foreach (Course course in courses)
            {
                foreach (LectureTime lectureTime in course.LectureTimes)
                {
                    for (int i = 1; i < 6; ++i)
                    {
                        for (int j = 480; j < 1260; j += 30)
                        {
                            if (lectureTime.StartTime <= j && j < lectureTime.EndTime && (int)lectureTime.Day == i)
                            {
                                int cursorLeft = i * 30;
                                int cursorTop = ((j - 480) / 30 * 2) + 10;

                                Console.SetCursorPosition(cursorLeft, cursorTop);

                                Console.Write(course.CurriculumName);

                                Console.SetCursorPosition(cursorLeft, cursorTop + 1);

                                Console.Write(course.Classroom);
                            }
                        }
                    }
                }
            }

            int courseCount = 0;
            foreach (Course course in courses)
            {
                if (course.LectureTimeString == "")
                {
                    Console.SetCursorPosition(0, 61 + courseCount);
                    courseCount += 1;

                    Console.WriteLine(course.CurriculumName);
                }
            }
        }
    }
}
