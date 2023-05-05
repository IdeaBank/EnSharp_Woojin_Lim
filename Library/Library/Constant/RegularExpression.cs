namespace Library.Constant
{
    // 필요한 정규표현식을 정의해 놓은 클래스
    public class RegularExpression
    {
        public const string USER_ID = @"^[0-9a-zA-Z]{8,15}$";
        public const string USER_PASSWORD = @"^[0-9a-zA-Z!@#$%^&*]{8,15}$";

        public const string USER_NAME = @"^[a-zA-Zㄱ-힇]{1,}";
        public const string USER_AGE = @"^200$|^(1[0-9]{2})$|^([1-9]{1}[0-9]{1})$|^([1-9]{1})$";
        public const string USER_PHONE_NUMBER = @"^01[0-9]{1}-[0-9]{3,4}-[0-9]{4}$";
        public const string USER_ADDRESS = @"^[ㄱ-힇]+[시|도] [ㄱ-힇]+[시|군|구]";

        public const string BOOK_NAME = @"^[0-9a-zA-Zㄱ-힇?!+=]+";
        public const string BOOK_AUTHOR = @"^[a-zA-Zㄱ-힇]+";
        public const string BOOK_PUBLISHER = @"^[a-zA-Zㄱ-힇]+";
        public const string BOOK_QUANTITY = @"^[1-9]{1}[0-9]{0,2}";
        public const string BOOK_PRICE = @"^[1-9]{1}[0-9]{0,6}";
        public const string BOOK_PUBLISHED_DATE = @"(19|20)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])";
        public const string BOOK_ISBN = @"[0-9]{13}$";
        public const string BOOK_DESCRIPTION = @"[0-9a-zA-Zㄱ-힇\b]+";
    }
}