using LTT.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTT.Utility
{
    public class ExcelFileSaver
    {
        public ExcelFileSaver()
        {

        }

        public void AskSaveFile(Student student)
        {
            ConsoleKeyInfo consoleKey = new ConsoleKeyInfo();

            Console.WriteLine("<ESC> 뒤로가기 <ENTER> 파일로 저장");

            while (consoleKey.Key != ConsoleKey.Escape && consoleKey.Key != ConsoleKey.Enter)
            {
                consoleKey = Console.ReadKey(true);

                if (consoleKey.Key == ConsoleKey.Enter)
                {
                    SaveToFile(student);
                }

                else if (consoleKey.Key == ConsoleKey.Escape)
                {
                    break;
                }

            }

            return;
        }

        private void WriteDefaultData(Worksheet worksheet)
        {
            worksheet.Cells[1, 1] = "수강신청내역";
            worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 12]].Merge();
            worksheet.Cells[2, 1] = "NO";
            worksheet.Cells[2, 2] = "개설학과전공";
            worksheet.Cells[2, 3] = "학수번호";
            worksheet.Cells[2, 4] = "분반";
            worksheet.Cells[2, 5] = "교과목명";
            worksheet.Cells[2, 6] = "이수구분";
            worksheet.Cells[2, 7] = "학년";
            worksheet.Cells[2, 8] = "학점";
            worksheet.Cells[2, 9] = "요일 및 강의시간";
            worksheet.Cells[2, 10] = "강의실";
            worksheet.Cells[2, 11] = "메인교수명";
            worksheet.Cells[2, 12] = "강의언어";
            
            worksheet.Cells[1, 14] = "시간표";
            worksheet.Range[worksheet.Cells[1, 14], worksheet.Cells[1, 20]].Merge();
            worksheet.Cells[2, 15] = "월";
            worksheet.Cells[2, 16] = "화";
            worksheet.Cells[2, 17] = "수";
            worksheet.Cells[2, 18] = "목";
            worksheet.Cells[2, 19] = "금";

            worksheet.Range[worksheet.Cells[1, 14], worksheet.Cells[2, 19]].Font.Bold = true;

        }
        
        private void SaveToFile(Student student)
        {
            try
            {
                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = workbook.Worksheets[1] as Excel.Worksheet;
                
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[2, 12]].Font.Bold = true;

                for (int i = 0; i < student.EnlistedCourses.Count; ++i)
                {
                    worksheet.Cells[3 + i, 1] = student.EnlistedCourses[i].Number;
                    worksheet.Cells[3 + i, 2] = student.EnlistedCourses[i].Department;
                    worksheet.Cells[3 + i, 3] = student.EnlistedCourses[i].CurriculumNumber;
                    worksheet.Cells[3 + i, 4] = student.EnlistedCourses[i].ClassNumber;
                    worksheet.Cells[3 + i, 5] = student.EnlistedCourses[i].CurriculumName;
                    worksheet.Cells[3 + i, 6] = student.EnlistedCourses[i].CurriculumType;
                    worksheet.Cells[3 + i, 7] = student.EnlistedCourses[i].StudentAcademicYear;
                    worksheet.Cells[3 + i, 8] = student.EnlistedCourses[i].Credit;
                    worksheet.Cells[3 + i, 9] = student.EnlistedCourses[i].LectureTimeString;
                    worksheet.Cells[3 + i, 10] = student.EnlistedCourses[i].Classroom;
                    worksheet.Cells[3 + i, 11] = student.EnlistedCourses[i].Professor;
                    worksheet.Cells[3 + i, 12] = student.EnlistedCourses[i].LanguageString;
                }
                
                for (int i = 480; i < 1260; i += 30)
                {
                    worksheet.Cells[(i - 480) / 30 * 2 + 3, 14] = ((i / 60).ToString("00") + ":" + (i % 60).ToString("00") + "~" + ((i + 30) / 60).ToString("00") + ":" + ((i + 30) % 60).ToString("00"));
                }

                foreach (Course course in student.EnlistedCourses)
                {
                    foreach (LectureTime lectureTime in course.LectureTimes)
                    {
                        for (int i = 1; i < 6; ++i)
                        {
                            for (int j = 480; j < 1260; j += 30)
                            {
                                if (lectureTime.StartTime <= j && j < lectureTime.EndTime && (int)lectureTime.Day == i)
                                {
                                    int x = ((j - 480) / 30 * 2) + 3;
                                    int y = i + 14;

                                    worksheet.Cells[x, y] = course.CurriculumName;
                                    worksheet.Cells[x + 1, y] = course.Classroom;
                                }
                            }
                        }
                    }
                }

                int courseCount = 0;
                foreach (Course course in student.EnlistedCourses)
                {
                    if (course.LectureTimeString == "")
                    {
                        worksheet.Range[worksheet.Cells[50 + courseCount, 15], worksheet.Cells[50 + courseCount, 20]].Merge();
                        worksheet.Cells[55 + courseCount, 15] = course.CurriculumName;
                        courseCount += 1;
                    }
                }

                worksheet.Columns.AutoFit();

                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[60, 21]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                workbook.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + student.StudentNumber.ToString() + ".xlsx");
                workbook.Close();
            }

            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
