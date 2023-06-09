package constant;

public class RegularExpression {
    public final static String NAME_EXPRESSION = "^[a-zA-Z]{2,10}$|^[ㄱ-힇]{2,4}$";
    public final static String ID_EXPRESSION = "^[a-zA-Z0-9]{7,14}$";
    public final static String PASSWORD_EXPRESSION = "^[a-zA-Z0-9]{7,14}$";
    public final static String BIRTHDATE_EXPRESSION = "^[0-9]{2}((0[1-9]{1})|1[0-2]{1}){1}(0[1-9]{1}|[1-2]{1}[0-9]{1}|3[1-2]{1})$";
    public final static String PHONE_NUMBER_MIDDLE_EXPRESSION = "^[0-9]{3,4}";
    public final static String PHONE_NUMBER_LAST_EXPRESSION = "^[0-9]{4}";
    public final static String ADDRESS_NUMBER_EXPRESSION = "^[0-9]{5}";
}
