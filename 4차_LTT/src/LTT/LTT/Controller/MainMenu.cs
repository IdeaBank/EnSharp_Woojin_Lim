using LTT.Model;
using LTT.Utility;
using LTT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTT.Constant;
using LTT.Controller.ReserveCourse;
using LTT.Controller.EnlistCourse;

namespace LTT.Controller
{
    class MainMenu
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private ViewList viewList;
        private MenuSelector menuSelector;
        private int currentSelectionIndex;
        private int userIndex;

        public MainMenu(TotalData totalData, DataManipulator dataManipulator, ConsoleWriter consoleWriter, UserInputManager userInputManager, ViewList viewList, MenuSelector menuSelector, int userIndex) 
        {
            this.totalData = totalData;
            this.dataManipulator = dataManipulator;
            this.consoleWriter = consoleWriter;
            this.userInputManager = userInputManager;
            this.viewList = viewList;
            this.menuSelector = menuSelector;
            this.userIndex = userIndex;
        }

        public void Start()
        {
            this.currentSelectionIndex = 0;

            viewList.MainMenuView.MakeView();

            KeyValuePair<ResultCode, int> selectResult = new KeyValuePair<ResultCode, int>();
            
            while (selectResult.Key != ResultCode.ESC_PRESSED)
            {
                selectResult = menuSelector.ChooseMenu(0, MenuCount.MAIN_MENU, MenuType.MAIN_MENU);

                if (selectResult.Key == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                else
                {
                    this.currentSelectionIndex = selectResult.Value;

                    switch (currentSelectionIndex)
                    {
                        case 0:
                            LectureTimeSearcher lectureTimeSearcher = new LectureTimeSearcher(totalData, dataManipulator, consoleWriter, userInputManager, viewList, menuSelector);
                            viewList.LectureTimeSearchView.MakeView();
                            lectureTimeSearcher.LectureTimeSearch(new List<Course>());
                            Console.ReadKey();
                            break;
                        case 1:
                            ReserveMenu reserveMenu = new ReserveMenu(totalData, dataManipulator, consoleWriter, userInputManager, viewList, menuSelector, userIndex);
                            reserveMenu.Start();
                            break;
                        case 2:
                            EnlistMenu enlistMenu = new EnlistMenu(totalData, dataManipulator, consoleWriter, userInputManager, viewList, menuSelector, userIndex);
                            enlistMenu.Start();
                            break;
                        case 3:
                            Console.WriteLine("4");
                            break;
                    }

                    viewList.MainMenuView.MakeView();
                }
            }
        }
    }
}
