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

        public ViewList(ConsoleWriter consoleWriter)
        {
            this.studentLoginView = new StudentLoginView(consoleWriter);
            this.mainMenuView = new MainMenuView(consoleWriter);
            this.enlistMenuView = new EnlistMenuView(consoleWriter);
            this.reserveMenuView = new ReserveMenuView(consoleWriter);
            this.courseListView = new CourseListView(consoleWriter);
        }
    }
}