using System.Collections.Generic;

namespace LTT.Model
{
    public class TotalData
    {
        private List<Student> students;
        private List<Course> courses;

        public List<Student> Students
        {
            get => this.students;
            set => this.students = value;
        }

        public List<Course> Courses
        {
            get => this.courses;
            set => this.courses = value;
        }
        
        public TotalData()
        {
            this.students = new List<Student>();
            this.courses = new List<Course>();
        }
    }
}