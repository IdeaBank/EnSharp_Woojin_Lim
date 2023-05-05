namespace Library.Constant.Input
{
    public class Instruction
    {
        public const string USER_ID = "ID ( 8~15글자 영어, 숫자포함 ): ";
        public const string USER_PASSWORD = "패스워드 ( 8~15글자 영어, 숫자포함 ): ";
        public const string USER_PASSWORD_CONFIRM = "패스워드 확인: ";
        public const string USER_NAME = "이름 ( 영어, 한글 1개 이상 ): ";
        public const string USER_AGE = "나이 ( 1-200사이의 자연수 ): ";
        public const string USER_PHONE_NUMBER = "전화번호 ( 01x-xxxx-xxxx ): ";
        public const string USER_ADDRESS = "주소 ( XX도 XX시 ): ";
        
        public static string[] WARNING_MESSAGE =
        {
            "영어, 한글, 숫자, ?!+= 1개 이상", "영어, 한글 1개 이상", "영어, 한글 1개 이상", "1-999사이의 자연수",
            "1-9999999사이의 자연수", "19xx or 20xx-xx-xx", "정수9개 + 영어1개 + 공백 + 정수13개", "최소1개의 문자(공백포함)"
        };
    }
}