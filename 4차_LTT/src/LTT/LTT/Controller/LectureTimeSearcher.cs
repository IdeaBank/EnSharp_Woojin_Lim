﻿using LTT.Constant;
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
            int[] currentSelectionColumnIndex = new int[2] { -1, -1 };

            KeyValuePair<ResultCode, string> name = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, ""),
                professor = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, ""),
                studentAcademicYear = new KeyValuePair<ResultCode, string>(ResultCode.SUCCESS, ""),
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
                    case 0:
                        currentSelectionColumnIndex[0] = menuSelector.ChooseColumn(currentSelectionRowIndex, 5, Constant.MenuType.SEARCH_TIME_TABLE, currentSelectionColumnIndex).Value;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        break;

                    case 1:
                        currentSelectionColumnIndex[1] = menuSelector.ChooseColumn(currentSelectionRowIndex, 4, Constant.MenuType.SEARCH_TIME_TABLE, currentSelectionColumnIndex).Value;
                        viewList.LectureTimeSearchView.UpdateView(currentSelectionRowIndex, currentSelectionColumnIndex);
                        break;

                    case 2:
                        name = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 5, 20, false, true, name.Value);
                        break;

                    case 3:
                        professor = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 6, 20, false, true, professor.Value);
                        break;

                    case 4:
                        studentAcademicYear = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 7, 20, false, true, studentAcademicYear.Value);
                        break;

                    case 5:
                        curriculumNumber = userInputManager.ReadInputFromUser(consoleWriter, Console.WindowWidth / 2, Console.WindowHeight / 2 + 8, 20, false, true, curriculumNumber.Value);
                        break;

                    case 6:
                        Console.Clear();
                        List<Course> searchResultList = dataManipulator.SearchCourseList(this.totalData, coursesToIgnore, currentSelectionColumnIndex[0], currentSelectionColumnIndex[1], name.Value, professor.Value, studentAcademicYear.Value, curriculumNumber.Value);
                        viewList.CourseListView.ShowCourseList(searchResultList);
                        return new KeyValuePair<ResultCode, List<Course>>(ResultCode.SUCCESS, searchResultList);
                }
            }

            return new KeyValuePair<ResultCode, List<Course>>(ResultCode.SUCCESS, null);
        }
    }
}
