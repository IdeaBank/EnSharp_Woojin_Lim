using Library.Constant;

namespace Library.Model
{
    public class ReturnedValue
    {
        private ResultCode resultCode;
        private int returnedInt;
        private string returnedString;

        public ReturnedValue(ResultCode resultCode, int returnedInt)
        {
            this.resultCode = resultCode;
            this.returnedInt = returnedInt;
        }

        public ReturnedValue(ResultCode resultCode, string returnedString)
        {
            this.resultCode = resultCode;
            this.returnedString = returnedString;
        }

        public ResultCode ResultCode
        {
            get => this.resultCode;
        }

        public int ReturnedInt
        {
            get => this.returnedInt;
        }

        public string ReturnedString
        {
            get => this.returnedString;
        }
    }
}