using Library.Constant;

namespace Library.Model
{
    public class UserInput
    {
        private ResultCode resultCode;
        private string input;

        public UserInput(ResultCode resultCode, string input)
        {
            this.resultCode = resultCode;
            this.input = input;
        }

        public ResultCode ResultCode
        {
            get => this.resultCode;
            set => this.resultCode = value;
        }

        public string Input
        {
            get => this.input;
            set => this.input = value;
        }
    }
}