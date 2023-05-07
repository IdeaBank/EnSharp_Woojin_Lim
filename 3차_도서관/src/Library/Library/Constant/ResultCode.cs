namespace Library.Constant
{
    public enum ResultCode
    {
        SUCCESS,
        FAIL,

        NO_BOOK,
        BOOK_NOT_ENOUGH,
        ALREADY_BORROWED,
        ALREADY_REQUESTED,
        ALREADY_IN_DATABASE,

        NO_USER,
        USER_ID_EXISTS,
        MUST_RETURN_BOOK,
        NO_ID,
        WRONG_PASSWORD,
        NO_ADDRESS,

        ESC_PRESSED,
        ENTER_PRESSED,
        ENTERING,
        UP_PRESSED,
        DOWN_PRESSED,
        DO_NOT_MATCH_REGEX,
        DO_NOT_MATCH_PASSWORD,

        YES,
        NO,
        NO_LOG
    }
}