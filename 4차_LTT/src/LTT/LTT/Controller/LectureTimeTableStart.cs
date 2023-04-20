using LTT.Model;

namespace LTT.Controller
{
    public class LectureTimeTableStart
    {
        private TotalData totalData = new TotalData();

        public void StartProgram()
        {
            
        }

        private void AddSampleStudent()
        {
            totalData.Students.Add(new Student("20011787", "dnltjd01!"));
        }
    }
}