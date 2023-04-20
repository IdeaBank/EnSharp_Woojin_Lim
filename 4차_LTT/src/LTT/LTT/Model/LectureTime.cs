namespace LTT.Model
{
    public class LectureTime
    {
        private int day;
        private int startTime;
        private int endTime;

        public int Day
        {
            get => this.day;
            set => this.day = value;
        }

        public int StartTime
        {
            get => this.startTime;
            set => this.startTime = value;
        }

        public int EndTime
        {
            get => this.endTime;
            set => this.endTime = value;
        }
    }
}