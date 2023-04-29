using LTT.Constant;
using LTT.Model;
using LTT.Utility;
using LTT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.Controller
{
    public class LectureTimeSearcher
    {
        private TotalData totalData;
        private DataManipulator dataManipulator;
        private ConsoleWriter consoleWriter;
        private UserInputManager userInputManager;
        private ViewList viewList;
        private MenuSelector menuSelector;

        public LectureTimeSearcher(TotalData totalData, DataManipulator dataManipulator, ConsoleWriter consoleWriter, UserInputManager userInputManager, ViewList viewList, MenuSelector menuSelector)
        {
            this.totalData = totalData;
            this.dataManipulator = dataManipulator;
            this.consoleWriter = consoleWriter;
            this.userInputManager = userInputManager;
            this.viewList = viewList;
            this.menuSelector = menuSelector;
        }

        public KeyValuePair<ResultCode, List<Course>> LectureTimeSearch(List<Course> coursesToIgnore)
        {
            int currentSelectionRowIndex = 0;
            int[] currentSelectionColumnIndex = new int[3] { -1, -1, -1 };

            KeyValuePair<ResultCode, string> name = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, ""),
                professor = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, ""),
                curriculumNumber = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, "");

            while (currentSelectionRowIndex != 6)
            {
                KeyValuePair<ResultCode, int> selectRowResult = menuSelector.ChooseMenu(currentSelectionRowIndex, 7, Constant.MenuType.SEARCH_TIME_TABLE, currentSelectionColumnIndex);
                currentSelectionRowIndex = selectRowResult.Value;

                if (selectRowResult.Key == ResultCode.ESC_PRESSED)
                {
                    return new KeyValuePair<ResultCode, List<Course>>(ResultCode.ESC_PRESSED, null);
                }

                switch (currentSelectionRowIndex)
                {
                    case SearchMenu.DEPARTMENT:
                        currentSelectionColumnIndex[0] = 0;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        currentSelectionColumnIndex[0] = menuSelector.ChooseColumn(currentSelectionRowIndex, 5, Constant.MenuType.SEARCH_TIME_TABLE, currentSelectionColumnIndex).Value;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        break;

                    case SearchMenu.CURRICULUM_TYPE:
                        currentSelectionColumnIndex[1] = 0;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        currentSelectionColumnIndex[1] = menuSelector.ChooseColumn(currentSelectionRowIndex, 4, Constant.MenuType.SEARCH_TIME_TABLE, currentSelectionColumnIndex).Value;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        break;

                    case SearchMenu.ACADEMIC_YEAR:
                        currentSelectionColumnIndex[2] = 0;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        currentSelectionColumnIndex[2] = menuSelector.ChooseColumn(2, 4, Constant.MenuType.SEARCH_TIME_TABLE, currentSelectionColumnIndex).Value;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        break;
                    
                    case SearchMenu.COURSE_NAME:
                        name = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 5, 20, false, true, name.Value);
                        break;

                    case SearchMenu.PROFESSOR:
                        professor = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 6, 20, false, true, professor.Value);
                        break;

                    case SearchMenu.CURRICULUM_NUMBER:
                        curriculumNumber = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 8, 20, false, true, curriculumNumber.Value);
                        break;

                    case SearchMenu.SEARCH:
                        Console.Clear();
                        List<Course> searchResultList = dataManipulator.SearchCourseList(this.totalData, coursesToIgnore, currentSelectionColumnIndex[0], currentSelectionColumnIndex[1], name.Value, professor.Value, currentSelectionColumnIndex[2], curriculumNumber.Value);
                        viewList.CourseListView.ShowCourseList(searchResultList);
                        return new KeyValuePair<ResultCode, List<Course>>(ResultCode.SUCCESS, searchResultList);
                }
            }

            return new KeyValuePair<ResultCode, List<Course>>(ResultCode.SUCCESS, null);
        }
    }
}
