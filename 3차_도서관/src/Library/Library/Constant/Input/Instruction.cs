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
        public const string USER_ADDRESS = "주소 ( 도로명 주소 ): ";

        public static string[] BOOK_INPUT_INSTRUCTION =
        {
            "영어, 한글, 숫자, ?!+= 1개 이상",
            "영어, 한글 1개 이상",
            "영어, 한글 1개 이상",
            "1-999 사이의 자연수",
            "1-9999999 사이의 자연수",
            "19xx or 20xx-xx-xx",
            "정수 13개",
            "최소 1개의 문자(공백포함)"
        };

        public static string[] USER_INPUT_INSTRUCTION =
        {
            "ID ( 8~15글자 영어, 숫자포함 ): ",
            "패스워드 ( 8~15글자 영어, 숫자포함 ): ",
            "패스워드 확인: ",
            "이름 ( 영어, 한글 1개 이상 ): ",
            "나이 ( 1-200사이의 자연수 ): ",
            "전화번호 ( 01x-xxxx-xxxx ): ",
            "주소 ( 도로명 주소  ): "
        };

        public static string[] USER_REQUEST_BOOK_INSTRUCTION =
        {
            "영어, 한글, 숫자, ?!+= 1개 이상",
            "1-100 사이의 자연수"
        };
    }
}