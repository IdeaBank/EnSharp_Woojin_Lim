namespace LTT.Constant
{
    public enum MenuType
    {
        MAIN_MENU,
        RESERVE_COURSE,
        ENLIST_COURSE,
        SEARCH_TIME_TABLE,
        RESERVED_OR_ALL_COURSE
    }

    public class SearchMenu
    {
        public const int DEPARTMENT = 0;
        public const int CURRICULUM_TYPE = 1;
        public const int ACADEMIC_YEAR = 2;
        public const int COURSE_NAME = 3;
        public const int PROFESSOR = 4;
        public const int CURRICULUM_NUMBER = 5;
        public const int SEARCH = 6;

        public const int ADD_RESERVED_COURSE = 0;
        public const int SHOW_RESERVED_COURSE = 1;
        public const int SHOW_RESERVED_TIME_TABLE = 2;
        public const int REMOVE_RESERVED_COURSE = 3;

        public const int SEARCH_LECTURE_TIME = 0;
        public const int RESERVE_MENU = 1;
        public const int ENLIST_MENU = 2;
        public const int SAVE_EXCEL = 3;

        public const int RESERVE_OR_SEARCH = 0;
        public const int SHOW_ENLISTED_COURSE = 1;
        public const int SHOW_ENLISTED_TIME_TABLE = 2;
        public const int REMOVE_ENLISTED_COURSE = 3;
    }
}