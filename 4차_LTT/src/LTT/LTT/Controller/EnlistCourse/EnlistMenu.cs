using LTT.Constant;
using LTT.Model;
using LTT.Utility;
using LTT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.Controller.EnlistCourse
{
    public class EnlistMenu
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private ViewList viewList;
        private MenuSelector menuSelector;
        private int currentSelectionIndex;
        private int userIndex;

        public EnlistMenu(TotalData totalData, DataManipulator dataManipulator, ConsoleWriter consoleWriter, UserInputManager userInputManager, ViewList viewList, MenuSelector menuSelector, int userIndex)
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

            viewList.EnlistMenuView.MakeView();

            KeyValuePair<ResultCode, int> selectResult = new KeyValuePair<ResultCode, int>();

            while (selectResult.Key != ResultCode.ESC_PRESSED)
            {
                selectResult = menuSelector.ChooseMenu(0, MenuCount.ENLIST_MENU, MenuType.ENLIST_COURSE);

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
                            ReservedOrAllCourse reservedOrAllCourse = new ReservedOrAllCourse(totalData, dataManipulator, consoleWriter, userInputManager, viewList, menuSelector, userIndex);
                            reservedOrAllCourse.Start();
                            break;
                        case 1:
                            viewList.CourseListView.ShowCourseList(totalData.Students[userIndex].EnlistedCourses);
                            Console.ReadKey(true);
                            break;
                        case 2:
                            Console.Clear();
                            viewList.TimeTableView.ShowTimeTable(totalData.Students[userIndex].EnlistedCourses);
                            Console.ReadKey(true);
                            break;
                        case 3:
                            Console.WriteLine("4");
                            break;
                    }

                    viewList.EnlistMenuView.MakeView();
                }
            }
        }
    }
}
