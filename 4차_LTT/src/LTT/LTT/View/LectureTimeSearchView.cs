using LTT.Constant;
using LTT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT.View
{
    public class LectureTimeSearchView
    {
        ConsoleWriter consoleWriter;

        public LectureTimeSearchView(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public void MakeView()
        {
            Console.Clear();

            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf - 10, "<강의 검색>");
            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf - 9, "↑ ↓ 방향키로 이동 후 ENTER키를 눌러 입력");
            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf - 8, "← → 방향키로 이동 후 ENTER키를 눌러 선택");
            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf - 7, "입력 중 ESC키를 눌러 취소");
            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf - 6, "입력을 완료한 뒤에 <검색하기>에서 ENTER를 눌러 검색");
        }

        public void UpdateView(int selectionIndex, int[] selectedItems)
        {
            int windowWidthHalf = Console.WindowWidth / 2;
            int windowHeightHalf = Console.WindowHeight / 2;

            string[] mainMenuList =
            {
                "개설학과전공",
                "이수구분",
                "교과목명",
                "교수명",
                "학년",
                "학수번호",
                "<검색하기>"
            };

            string[] departmentList =
            {
                "전체",
                "컴퓨터공학과",
                "소프트웨어학과",
                "지능기전공학부",
                "기계항공우주공학부"
            };

            string[] curriculumType =
            {
                "전체",
                "공통교양필수",
                "전공필수",
                "전공선택"
            };

            string[] academicYear =
            {
                "전체",
                "1학년",
                "2학년",
                "3학년"
            };

            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf + 3, new string(' ', 120), Align.LEFT);
            consoleWriter.PrintOnPosition(windowWidthHalf, windowHeightHalf + 4, new string(' ', 120), Align.LEFT);

            for (int i = 0; i < mainMenuList.Length; ++i)
            {
                if (i == selectionIndex)
                {
                    consoleWriter.PrintOnPosition(windowWidthHalf - 4, windowHeightHalf + i + 3, mainMenuList[i], Align.RIGHT, ConsoleColor.Green);
                }

                else
                {
                    consoleWriter.PrintOnPosition(windowWidthHalf - 4, windowHeightHalf + i + 3, mainMenuList[i], Align.RIGHT, ConsoleColor.White);
                }
            }

            if (selectedItems[0] != -1)
            {
                for(int i = 0; i < departmentList.Length; ++i)
                {
                    if (i == selectedItems[0])
                    {
                        consoleWriter.PrintOnPosition(windowWidthHalf + i * 20, windowHeightHalf + 3, departmentList[i], Align.LEFT, ConsoleColor.Green);
                    }

                    else
                    {
                        consoleWriter.PrintOnPosition(windowWidthHalf + i * 20, windowHeightHalf + 3, departmentList[i], Align.LEFT, ConsoleColor.White);
                    }
                }
            }

            if (selectedItems[1] != -1)
            {
                for (int i = 0; i < curriculumType.Length; ++i)
                {
                    if (i == selectedItems[1])
                    {
                        consoleWriter.PrintOnPosition(windowWidthHalf + i * 15, windowHeightHalf + 4, curriculumType[i], Align.LEFT, ConsoleColor.Green);
                    }

                    else
                    {
                        consoleWriter.PrintOnPosition(windowWidthHalf + i * 15, windowHeightHalf + 4, curriculumType[i], Align.LEFT, ConsoleColor.White);
                    }
                }
            }

            if (selectedItems[2] != -1)
            {
                for (int i = 0; i < curriculumType.Length; ++i)
                {
                    if (i == selectedItems[2])
                    {
                        consoleWriter.PrintOnPosition(windowWidthHalf + i * 15, windowHeightHalf + 7, academicYear[i], Align.LEFT, ConsoleColor.Green);
                    }

                    else
                    {
                        consoleWriter.PrintOnPosition(windowWidthHalf + i * 15, windowHeightHalf + 7, academicYear[i], Align.LEFT, ConsoleColor.White);
                    }
                }
            }
        }
    }
}
