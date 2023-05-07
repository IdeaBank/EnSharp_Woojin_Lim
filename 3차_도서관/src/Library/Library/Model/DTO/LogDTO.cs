namespace Library.Model.DTO
{
    public class LogDTO
    {
        private int logId;
        private string logTime;
        private string userName;
        private string logContents;
        private string logAction;

        public LogDTO()
        {
            
        }

        public LogDTO(int logId, string logTime, string userName, string logContents, string logAction)
        {
            this.logId = logId;
            this.logTime = logTime;
            this.userName = userName;
            this.logContents = logContents;
            this.logAction = logAction;
        }

        public int LogId
        {
            get => this.logId;
            set => this.logId = value;
        }

        public string LogTime
        {
            get => this.logTime;
            set => this.logTime = value;
        }

        public string UserName
        {
            get => this.userName;
            set => this.userName = value;
        }

        public string LogContents
        {
            get => this.logContents;
            set => this.logContents = value;
        }

        public string LogAction
        {
            get => this.logAction;
            set => this.logAction = value;
        }
    }
}