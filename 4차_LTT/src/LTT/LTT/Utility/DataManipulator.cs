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
            // 기존에 있던 강의들을 순회함
            foreach (Course previousCourse in previousCourses)
            {
                // 기존에 있던 강의들의 강의 시간을 순회함
                foreach (LectureTime previousLectureTime in previousCourse.LectureTimes)
                {
                    // 새로운 강의의 강의 시간을 순회함
                    foreach (LectureTime newLectureTime in newCourse.LectureTimes)
                    {
                        // 새로운 강의와 기존에 있던 강의의 시간이 겹친다면 참을 반환
                        if (previousLectureTime.Day == newLectureTime.Day &&
                            previousLectureTime.StartTime < newLectureTime.EndTime &&
                            newLectureTime.StartTime < previousLectureTime.EndTime)
                        {
                            return true;
                        }
                    }
                }
            }

            // 겹치지 않는다면 false 반환
            return false;
        }

        private bool CheckCurriculumNumberDuplicate(List<Course> previousCourses, Course newCourse)
        {
            // 기존에 있던 강의들을 순회함
            foreach (Course previousCourse in previousCourses)
            {
                // 기존에 있던 강의의 학수번호와 겹치는 것이 있다면 true 반환
                if (previousCourse.CurriculumNumber == newCourse.CurriculumNumber)
                {
                    return true;
                }
            }

            // 겹치지 않는다면 false 반환
            return false;
        }

        private bool IsClassCompatible(List<Course> previousCourses, Course newCourse)
        {
            // 시간이 겹치거나 학수번호가 겹치는 지 여부를 반환
            return !CheckTimeOverlap(previousCourses, newCourse) &&
                   !CheckCurriculumNumberDuplicate(previousCourses, newCourse);
        }

        public KeyValuePair<ResultCode, int> TryLogin(TotalData totalData, string id, string password)
        {
            // 학생 리스트를 순회함
            for (int i = 0; i < totalData.Students.Count; ++i)
            {
                // 아이디가 존재하면
                if (totalData.Students[i].StudentNumber == id)
                {
                    // 패스워드가 일치하면
                    if (totalData.Students[i].Password == password)
                    {
                        // 성공 반환
                        return new KeyValuePair<ResultCode, int>(ResultCode.SUCCESS, i);
                    }

                    // 패스워드가 일치하지 않으면 실패 반환
                    return new KeyValuePair<ResultCode, int>(ResultCode.PASSWORD_DO_NOT_MATCH, -1);
                }
            }

            // 아이디가 존재하지 않으면 실패 반환
            return new KeyValuePair<ResultCode, int>(ResultCode.ID_DO_NO_EXIST, -1);
        }

        public int GetStudentIndexByNumber(List<Student> students, string studentNumber)
        {
            // 학생 리스트에서 studentNumber을 활용해 인덱스 값 얻어오기
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
            // 강의 리스트에서 courseNumber을 활용해 인덱스 값 얻어오기
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
            // null 값들을 모두 빈 문자열로 바꿔줘 NullReferenceException 수정
            if (lectureTimes == null)
            {
                lectureTimes = "";
            }

            if (classroom == null)
            {
                classroom = "";
            }

            // 새로운 강의 추가
            Course newCourse = new Course(number, curriculumNumber, classNumber, curriculumName, studentAcademicYear, credit, classroom, professor);

            // 각 값에 따라 데이터 저장
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

            // 강의 시간 분석
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
            // 강의시간 분석
            List<LectureTime> result = new List<LectureTime>();

            // 강의시간이 비어있으면 빈 리스트 반환
            if (lectureTimes == null || lectureTimes.Length == 0)
            {
                return result;
            }

            // 콤마 기준으로 여러 개의 문자열로 나눔
            string[] splitResult = lectureTimes.Split(',');

            // 최대 2개의 요일이 있으므로 2개의 요일을 담을 변수 선언
            DayOfWeek firstDay = DayOfWeek.Monday;
            DayOfWeek secondDay = DayOfWeek.Monday;

            // 나눈 결과를 순회함
            foreach (string tempString in splitResult)
            {
                // 좌우 공백을 없애줌
                string splitString = tempString.Trim();

                // 첫 문자에 따라 첫 번째 요일에 값을 넣어줌
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

                // 요일이 문자열에 하나만 있으면 시간 분석해서 하나만 넣어줌
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

                // 요일이 문자열에 두 개 있으면 시간 분석해서 둘 다 넣어줌
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

            // 분석한 시간표를 반환함
            return result;
        }

        private int GetTotalCredit(List<Course> courseList)
        {
            int totalCredit = 0;

            // 선택한 강의 리스트의 학점의 합을 구함
            foreach(Course course in courseList)
            {
                totalCredit += course.Credit;
            }

            // 학점의 합 반환
            return totalCredit;
        }

        private ResultCode AddCourse(List<Course> courseList, Course newCourse, int courseIndex, int studentIndex, bool isEnlistedCourse)
        {
            // 기존 강의 리스트에 새로운 강의를 더했을 때 총 학점 수가 24점이 넘으면 실패 반환
            if(GetTotalCredit(courseList) + newCourse.Credit > 24)
            {
                return ResultCode.OVER_MAX_CREDIT;
            }

            // 만약 수강신청 중이면
            if (isEnlistedCourse)
            {
                // 만약 기존 강의 리스트에 새로운 강의를 더할 수 없으면
                if (!IsClassCompatible(courseList, newCourse))
                {
                    // 실패 반환
                    return ResultCode.FAIL;
                }
            }

            // 아무 이상 없으면 리스트에 추가
            courseList.Add(newCourse);

            // 성공 반환
            return ResultCode.SUCCESS;
        }

        public ResultCode AddReservedCourse(TotalData totalData, List<Course> courseList, int courseNumber, int studentIndex)
        {
            int courseIndex = GetCourseIndexByNumber(courseList, courseNumber);

            // 강의가 없으면 NO_COURSE 반환
            if (courseIndex == -1)
            {
                return ResultCode.NO_COURSE;
            }

            return AddCourse(totalData.Students[studentIndex].ReservedCourses, courseList[courseIndex], courseIndex, studentIndex, false);
        }

        public ResultCode AddEnlistedCourse(TotalData totalData, List<Course> courseList, int courseNumber, int studentIndex)
        {
            int courseIndex = GetCourseIndexByNumber(courseList, courseNumber);

            // 강의가 없으면 NO_COURSE 반환
            if (courseIndex == -1)
            {
                return ResultCode.NO_COURSE;
            }

            return AddCourse(totalData.Students[studentIndex].EnlistedCourses, courseList[courseIndex], courseIndex, studentIndex, true);
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

        public ResultCode RemoveReservedCourse(List<Course> courseList, int courseNumber, int userIndex)
        {
            return RemoveCourse(courseList, courseNumber);
        }

        public ResultCode RemoveEnlistedCourse(List<Course> courseList, int courseNumber, int userIndex)
        {
            return RemoveCourse(courseList, courseNumber);
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