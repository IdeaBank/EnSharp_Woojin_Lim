using System.Collections.Generic;
using LTT.Constant;

namespace LTT.Model
{
    public class Course
    {
        private int number;
        private Department department;
        private string curriculumNumber;
        private string classNumber;
        private string curriculumName;
        private CurriculumType curriculumType;
        private int studentAcademicYear;
        private int credit;
        private List<LectureTime> lectureTimes;
        private string classroom;
        private string professor;
        private Language language;
    }
}