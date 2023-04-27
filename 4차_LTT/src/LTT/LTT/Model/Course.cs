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
        private string lectureTimeString;
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

        public string DepartmentString
        {
            get
            {
                switch (this.department)
                {
                    case Department.SOFTWARE:
                        return "����Ʈ�����а�";

                    case Department.COMPUTER_SCIENCE:
                        return "��ǻ�Ͱ��а�";

                    case Department.INTELLIGENT_MECHATRONICS_ENGINEERING:
                        return "���ɱ������к�";

                    case Department.MACHINE_AEROSPACE_ENGINEERING:
                        return "����װ����ְ��к�";
                }

                return "";
            }
        }

        public string CurriculumNumber
        {
            get => this.curriculumNumber;
            set => this.curriculumNumber = value;
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

        public string CurriculumString
        {
            get
            {
                switch (this.curriculumType)
                {
                    case CurriculumType.MAJOR_SELECTIVE:
                        return "��������";
                    case CurriculumType.MAJOR_ESSENTIAL:
                        return "�����ʼ�";
                    case CurriculumType.COMMON_GENERAL_ELECTIVE_ESSENTIAL:
                        return "���뱳���ʼ�";
                }

                return "";
            }
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

        public string LectureTimeString
        {
            get => this.lectureTimeString;
            set => this.lectureTimeString = value;
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

        public string LanguageString
        {
            get
            {
                switch (this.language)
                {
                    case Language.ENGLISH:
                        return "����";

                    case Language.KOREAN:
                        return "�ѱ���";

                    case Language.ENGLISH_AND_KOREAN:
                        return "����/�ѱ���";
                }

                return "";
            }
        }


        public Course()
        {
            this.lectureTimes = new List<LectureTime>();
        }
    }
}