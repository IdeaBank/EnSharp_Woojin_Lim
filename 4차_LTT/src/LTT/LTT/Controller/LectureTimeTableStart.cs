using LTT.Model;
using LTT.Utility;

namespace LTT.Controller
{
    public class LectureTimeTableStart
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;

        public LectureTimeTableStart()
        {
            this.totalData = new TotalData();
            this.dataManipulator = new DataManipulator();
            this.consoleWriter = new ConsoleWriter();
            this.userInputManager = new UserInputManager();
        }
        
        public void StartProgram()
        {
            AddSampleStudent();
        }

        private void AddSampleStudent()
        {
            this.totalData.Students.Add(new Student("20011787", "dnltjd01!"));
        }
    }
}