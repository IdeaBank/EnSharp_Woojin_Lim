using System;

namespace LTT.Model
{
    public class LectureTime
    {
        private DayOfWeek day;
        private int startTime;
        private int endTime;

        public DayOfWeek Day
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