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

        public Course(int number, string curriculumNumber, string classNumber, string curriculumName,
            int studentAcademicYear, int credit, string classroom, string professor)
        {
            this.number = number;
            this.curriculumNumber = curriculumNumber;
            this.classNumber = classNumber;
            this.curriculumName = curriculumName;
            this.studentAcademicYear = studentAcademicYear;
            this.credit = credit;
            this.classroom = classroom;
            this.professor = professor;
        }
        
        public int Number
        {
            get => this.number;
            set => this.number = value;
        }

        public Department Department
        {
            get => this.department;
            set => this.department = value;
        }

        public string CurriculumNumber
        {
            get => this.curriculumName;
            set => this.curriculumName = value;
        }

        public string ClassNumber
        {
            get => this.classNumber;
            set => this.classNumber = value;
        }

        public string CurriculumName
        {
            get => this.curriculumName;
            set => this.curriculumName = value;
        }

        public CurriculumType CurriculumType
        {
            get => this.curriculumType;
            set => this.curriculumType = value;
        }

        public int StudentAcademicYear
        {
            get => this.studentAcademicYear;
            set => this.studentAcademicYear = value;
        }

        public int Credit
        {
            get => this.credit;
            set => this.credit = value;
        }

        public List<LectureTime> LectureTimes
        {
            get => this.lectureTimes;
            set => this.lectureTimes = value;
        }

        public string Classroom
        {
            get => this.classroom;
            set => this.classroom = value;
        }

        public string Professor
        {
            get => this.professor;
            set => this.professor = value;
        }

        public Language Language
        {
            get => this.language;
            set => this.language = value;
        }

        public Course()
        {
            this.lectureTimes = new List<LectureTime>();
        }
    }
}