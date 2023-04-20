using System.Collections.Generic;
using LTT.Constant;
using LTT.Model;

namespace LTT.Utility
{
    public class DataManipulator
    {
        public bool CheckTimeOverlap(List<Course> previousCourses, Course newCourse)
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

        public bool CheckCurriculumNumberDuplicate(List<Course> previousCourses, Course newCourse)
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

        public bool IsClassCompatible(List<Course> previousCourses, Course newCourse)
        {
            return !CheckTimeOverlap(previousCourses, newCourse) &&
                   !CheckCurriculumNumberDuplicate(previousCourses, newCourse);
        }

        public ResultCode TryLogin(TotalData totalData, string id, string password)
        {
            foreach (Student student in totalData.Students)
            {
                if (student.StudentNumber == id)
                {
                    if (student.Password == password)
                    {
                        return ResultCode.LOGIN_SUCCESS;
                    }

                    return ResultCode.PASSWORD_DO_NOT_MATCH;
                }
            }

            return ResultCode.ID_DO_NO_EXIST;
        }
    }
}