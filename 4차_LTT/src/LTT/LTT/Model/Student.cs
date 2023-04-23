using System.Collections.Generic;

namespace LTT.Model
{
    public class Student
    {
        private string studentNumber;
        private string password;
        private List<Course> reservedCourses;
        private List<Course> enlistedCourses;

        public string StudentNumber
        {
            get => this.studentNumber;
            set => this.studentNumber = value;
        }

        public string Password
        {
            get => this.password;
            set => this.password = value;
        }

        public List<Course> ReservedCourses
        {
            get => this.reservedCourses;
            set => this.reservedCourses = value;
        }

        public List<Course> EnlistedCourses
        {
            get => this.enlistedCourses;
            set => this.enlistedCourses = value;
        }
        
        public Student(string studentNumber, string password)
        {
            this.studentNumber = studentNumber;
            this.password = password;
            
            this.reservedCourses = new List<Course>();
            this.enlistedCourses = new List<Course>();
        }
    }
}