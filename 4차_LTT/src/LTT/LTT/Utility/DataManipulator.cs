using System;
using System.Collections.Generic;
using System.Linq;
using LTT.Constant;
using LTT.Model;

namespace LTT.Utility
{
    public class DataManipulator
    {
        private bool CheckTimeOverlap(List<Course> previousCourses, Course newCourse)
        {
            foreach (Course previousCourse in previousCourses)
            {
                foreach (LectureTime previousLectureTime in previousCourse.LectureTimes)
                {
                    foreach (LectureTime newLectureTime in newCourse.LectureTimes)
                    {
                        if (previousLectureTime.Day == newLectureTime.Day &&
                            previousLectureTime.StartTime < newLectureTime.EndTime &&
                            newLectureTime.StartTime < previousLectureTime.EndTime)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckCurriculumNumberDuplicate(List<Course> previousCourses, Course newCourse)
        {
            foreach (Course previousCourse in previousCourses)
            {
                if (previousCourse.CurriculumNumber == newCourse.CurriculumNumber)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsClassCompatible(List<Course> previousCourses, Course newCourse)
        {
            return !CheckTimeOverlap(previousCourses, newCourse) &&
                   !CheckCurriculumNumberDuplicate(previousCourses, newCourse);
        }

        public KeyValuePair<ResultCode, int> TryLogin(TotalData totalData, string id, string password)
        {
            for (int i = 0; i < totalData.Students.Count; ++i)
            {
                if (totalData.Students[i].StudentNumber == id)
                {
                    if (totalData.Students[i].Password == password)
                    {
                        return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, i);
                    }

                    return new KeyValuePair<ResultCode, int>(ResultCode.PASSWORD_DO_NOT_MATCH, -1);
                }
            }

            return new KeyValuePair<ResultCode, int>(ResultCode.ID_DO_NO_EXIST, -1);
        }

        public int GetStudentIndexByNumber(List<Student> students, string studentNumber)
        {
            for (int i = 0; i < students.Count; ++i)
            {
                if (students[i].StudentNumber == studentNumber)
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetCourseIndexByNumber(List<Course> courses, int courseNumber)
        {
            for (int i = 0; i < courses.Count; ++i)
            {
                if (courses[i].Number == courseNumber)
                {
                    return i;
                }
            }

            return -1;
        }

        public Course MakeCourse(int number, string department, string curriculumNumber,
            string classNumber, string curriculumName, string curriculumType,
            int studentAcademicYear, int credit, string lectureTimes,
            string classroom, string professor, string language)
        {
            if (lectureTimes == null)
            {
                lectureTimes = "";
            }

            if (classroom == null)
            {
                classroom = "";
            }

            Course newCourse = new Course(number, curriculumNumber, classNumber, curriculumName, studentAcademicYear, credit, classroom, professor);

            switch (department)
            {
                case "컴퓨터공학과":
                    newCourse.Department = Department.COMPUTER_SCIENCE;
                    break;

                case "소프트웨어학과":
                    newCourse.Department = Department.SOFTWARE;
                    break;

                case "지능기전공학부":
                    newCourse.Department = Department.INTELLIGENT_MECHATRONICS_ENGINEERING;
                    break;

                case "기계항공우주공학부":
                    newCourse.Department = Department.MACHINE_AEROSPACE_ENGINEERING;
                    break;
            }

            switch (curriculumType)
            {
                case "공통교양필수":
                    newCourse.CurriculumType = CurriculumType.COMMON_GENERAL_ELECTIVE_ESSENTIAL;
                    break;

                case "전공필수":
                    newCourse.CurriculumType = CurriculumType.MAJOR_ESSENTIAL;
                    break;

                case "전공선택":
                    newCourse.CurriculumType = CurriculumType.MAJOR_SELECTIVE;
                    break;
            }

            newCourse.LectureTimes = GetLectureTime(lectureTimes);
            newCourse.LectureTimeString = lectureTimes;

            switch (language)
            {
                case "한국어":
                    newCourse.Language = Language.KOREAN;
                    break;

                case "영어":
                    newCourse.Language = Language.ENGLISH;
                    break;

                case "영어/한국어":
                    newCourse.Language = Language.ENGLISH_AND_KOREAN;
                    break;
            }

            return newCourse;
        }

        private List<LectureTime> GetLectureTime(string lectureTimes)
        {
            List<LectureTime> result = new List<LectureTime>();

            if (lectureTimes == null || lectureTimes.Length == 0)
            {
                return result;
            }

            string[] splitResult = lectureTimes.Split(',');

            DayOfWeek firstDay = DayOfWeek.Monday;
            DayOfWeek secondDay = DayOfWeek.Monday;

            foreach (string tempString in splitResult)
            {
                string splitString = tempString.Trim();

                switch (splitString[0])
                {
                    case '일':
                        firstDay = DayOfWeek.Sunday;
                        break;

                    case '월':
                        firstDay = DayOfWeek.Monday;
                        break;

                    case '화':
                        firstDay = DayOfWeek.Tuesday;
                        break;

                    case '수':
                        firstDay = DayOfWeek.Wednesday;
                        break;

                    case '목':
                        firstDay = DayOfWeek.Thursday;
                        break;

                    case '금':
                        firstDay = DayOfWeek.Friday;
                        break;

                    case '토':
                        firstDay = DayOfWeek.Saturday;
                        break;
                }

                if (splitString.Length == 13)
                {
                    LectureTime lectureTime = new LectureTime();
                    lectureTime.Day = firstDay;
                    lectureTime.StartTime = Int32.Parse(splitString[2].ToString() + splitString[3]) * 60 +
                                            Int32.Parse(splitString[5].ToString() + splitString[6]);

                    lectureTime.EndTime = Int32.Parse(splitString[8].ToString() + splitString[9]) * 60 +
                                          Int32.Parse(splitString[11].ToString() + splitString[12]);

                    result.Add(lectureTime);
                }

                else
                {
                    switch (splitString[2])
                    {
                        case '일':
                            secondDay = DayOfWeek.Sunday;
                            break;

                        case '월':
                            secondDay = DayOfWeek.Monday;
                            break;

                        case '화':
                            secondDay = DayOfWeek.Tuesday;
                            break;

                        case '수':
                            secondDay = DayOfWeek.Wednesday;
                            break;

                        case '목':
                            secondDay = DayOfWeek.Thursday;
                            break;

                        case '금':
                            secondDay = DayOfWeek.Friday;
                            break;

                        case '토':
                            secondDay = DayOfWeek.Saturday;
                            break;
                    }

                    LectureTime firstLectureTime = new LectureTime();
                    firstLectureTime.Day = firstDay;
                    firstLectureTime.StartTime = Int32.Parse(splitString[4].ToString() + splitString[5]) * 60 +
                                                 Int32.Parse(splitString[7].ToString() + splitString[8]);

                    firstLectureTime.EndTime = Int32.Parse(splitString[10].ToString() + splitString[11]) * 60 +
                                               Int32.Parse(splitString[13].ToString() + splitString[14]);

                    LectureTime secondLectureTime = new LectureTime();
                    secondLectureTime.Day = secondDay;
                    secondLectureTime.StartTime = firstLectureTime.StartTime;
                    secondLectureTime.EndTime = firstLectureTime.EndTime;

                    result.Add(firstLectureTime);
                    result.Add(secondLectureTime);
                }
            }

            return result;
        }

        private ResultCode AddCourse(List<Course> courseList, Course newCourse, int courseIndex, int studentIndex)
        {
            if (IsClassCompatible(courseList, newCourse))
            {
                courseList.Add(newCourse);
                return ResultCode.SUCCESS;
            }

            return ResultCode.FAIL;
        }

        public ResultCode AddReservedCourse(TotalData totalData, List<Course> courseList, int courseNumber, int studentIndex)
        {
            int courseIndex = GetCourseIndexByNumber(courseList, courseNumber);

            // 강의가 없으면 NO_COURSE 반환
            if (courseIndex == -1)
            {
                return ResultCode.NO_COURSE;
            }

            return AddCourse(totalData.Students[studentIndex].ReservedCourses, courseList[courseIndex], courseIndex, studentIndex);
        }

        public ResultCode AddEnlistedCourse(TotalData totalData, List<Course> courseList, int courseNumber, int studentIndex)
        {
            int courseIndex = GetCourseIndexByNumber(courseList, courseNumber);

            // 강의가 없으면 NO_COURSE 반환
            if (courseIndex == -1)
            {
                return ResultCode.NO_COURSE;
            }

            return AddCourse(totalData.Students[studentIndex].EnListedCourses, courseList[courseIndex], courseIndex, studentIndex);
        }

        private ResultCode RemoveCourse(List<Course> courseList, int courseNumber)
        {
            int courseIndex = GetCourseIndexByNumber(courseList, courseNumber);

            if (courseIndex == -1)
            {
                return ResultCode.NO_COURSE;
            }

            courseList.RemoveAt(courseIndex);

            return ResultCode.SUCCESS;
        }

        public ResultCode RemoveReservedCourse(TotalData totalData, int courseNumber, int userIndex)
        {
            return RemoveCourse(totalData.Students[userIndex].ReservedCourses, courseNumber);
        }

        public ResultCode RemoveEnlistedCourse(TotalData totalData, int courseNumber, int userIndex)
        {
            return RemoveCourse(totalData.Students[userIndex].EnListedCourses, courseNumber);
        }

        public List<Course> GetCourseListExcept(TotalData totalData, List<Course> coursesToIgnore)
        {
            // 리스트 복사
            List<Course> totalCourse = totalData.Courses.ToList();

            // 총 리스트에서 필요 없는 것들 제외시키기
            foreach (Course courseToIgnore in coursesToIgnore)
            {
                foreach (Course course in totalCourse)
                {
                    if (course.Number == courseToIgnore.Number)
                    {
                        totalCourse.Remove(course);
                        break;
                    }
                }
            }

            // 결과 반환
            return totalCourse;
        }

        public List<Course> SearchCourseList(TotalData totalData, List<Course> coursesToIgnore, int departmentIndex, int curriculumTypeIndex, string name, string professor, string studentAcademicYear, string curriculumNumber)
        {
            List<Course> searchResult = new List<Course>();
            List<Course> targetCourses = GetCourseListExcept(totalData, coursesToIgnore);

            string departmentString = "", curriculumTypeString = "";

            switch(departmentIndex)
            {
                case 1:
                    departmentString = "컴퓨터공학과";
                    break;
                case 2:
                    departmentString = "소프트웨어학과";
                    break;
                case 3:
                    departmentString = "지능기전공학부";
                    break;
                case 4:
                    departmentString = "기계항공우주공학부";
                    break;
            }

            switch (curriculumTypeIndex)
            {
                case 1:
                    curriculumTypeString = "공통교양필수";
                    break;
                case 2:
                    curriculumTypeString = "전공필수";
                    break;
                case 3:
                    curriculumTypeString = "전공선택";
                    break;
            }


            foreach(Course course in targetCourses)
            {
                if ((course.DepartmentString == departmentString || departmentString == "") &&
                    (course.CurriculumString == curriculumTypeString || curriculumTypeString == "") &&
                    (course.CurriculumName.Contains(name) || name == "") &&
                    (course.Professor.Contains(professor) || professor == "") &&
                    (course.StudentAcademicYear.ToString() == studentAcademicYear || studentAcademicYear == "") &&
                    (course.CurriculumNumber == curriculumNumber || curriculumNumber == ""))
                {
                    searchResult.Add(course);
                }
            }

            return searchResult;
        }
    }
}