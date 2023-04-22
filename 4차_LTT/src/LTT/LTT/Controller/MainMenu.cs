using LTT.Model;
using LTT.Utility;
using LTT.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTT.Constant;

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

        public MainMenu(TotalData totalData, DataManipulator dataManipulator, ConsoleWriter consoleWriter, UserInputManager userInputManager, ViewList viewList, MenuSelector menuSelector) 
        {
            this.totalData = totalData;
            this.dataManipulator = dataManipulator;
            this.consoleWriter = consoleWriter;
            this.userInputManager = userInputManager;
            this.viewList = viewList;
            this.menuSelector = menuSelector;
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
                            lectureTimeSearcher.LectureTimeSearch();
                            break;
                        case 1:
                            Console.WriteLine("2");
                            break;
                        case 2:
                            Console.WriteLine("3");
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
