namespace Library.Constants
{
    // 함수의 리턴값을 구분해 주기 위한 enum
    public enum ResultCode
    {
        SUCCESS,
        NO_BOOK,
        BOOK_NOT_ENOUGH,
        NO_USER,
        USER_ID_EXISTS,
        MUST_RETURN_BOOK,
        NO_ID,
        WRONG_PASSWORD,
        ESC_PRESSED,
        ENTER_PRESSED,
        DO_NOT_MATCH_REGEX,
        DO_NOT_MATCH_PASSWORD,
        YES,
        NO,
        FAIL
    }
}