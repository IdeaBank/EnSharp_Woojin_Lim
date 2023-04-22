using LTT.Utility;
using LTT.View.EnlistCourse;
using LTT.View.ReserveCourse;

namespace LTT.View
{
    public class ViewList
    {
        private StudentLoginView studentLoginView;
        private MainMenuView mainMenuView;
        private EnlistMenuView enlistMenuView;
        private ReserveMenuView reserveMenuView;
        private CourseListView courseListView;
        private LectureTimeSearchView lectureTimeSearchView;
        private TimeTableView timeTableView;
        private ReservedOrAllCourseView reservedOrAllCourseView;

        public StudentLoginView StudentLoginView
        {
            get => this.studentLoginView;
        }

        public MainMenuView MainMenuView
        {
            get => this.mainMenuView;
        }

        public EnlistMenuView EnlistMenuView
        {
            get => this.enlistMenuView;
        }

        public ReserveMenuView ReserveMenuView
        {
            get => this.reserveMenuView;
        }

        public CourseListView CourseListView
        {
            get => this.courseListView;
        }

        public LectureTimeSearchView LectureTimeSearchView
        {
            get => this.lectureTimeSearchView;
        }

        public TimeTableView TimeTableView
        {
            get => this.timeTableView;
        }

        public ReservedOrAllCourseView ReservedOrAllCourseView
        {
            get => this.reservedOrAllCourseView;
        }

        public ViewList(ConsoleWriter consoleWriter)
        {
            this.studentLoginView = new StudentLoginView(consoleWriter);
            this.mainMenuView = new MainMenuView(consoleWriter);
            this.enlistMenuView = new EnlistMenuView(consoleWriter);
            this.reserveMenuView = new ReserveMenuView(consoleWriter);
            this.courseListView = new CourseListView(consoleWriter);
            this.lectureTimeSearchView = new LectureTimeSearchView(consoleWriter);
            this.timeTableView = new TimeTableView(consoleWriter);
            this.reservedOrAllCourseView = new ReservedOrAllCourseView(consoleWriter);
        }
    }
}