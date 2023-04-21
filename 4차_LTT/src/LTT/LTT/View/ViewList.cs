using LTT.Utility;
using LTT.View.EnlistCourse;
using LTT.View.ReserveCourse;

namespace LTT.View
{
    public class ViewList
    {
        private MainMenuView mainMenuView;
        private EnlistMenuView enlistMenuView;
        private ReserveMenuView reserveMenuView;
        
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

        public ViewList(ConsoleWriter consoleWriter)
        {
            this.mainMenuView = new MainMenuView(consoleWriter);
            this.enlistMenuView = new EnlistMenuView(consoleWriter);
            this.reserveMenuView = new ReserveMenuView(consoleWriter);
        }
    }
}