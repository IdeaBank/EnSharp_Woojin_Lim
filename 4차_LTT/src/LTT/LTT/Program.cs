using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTT.Controller;
using LTT.Model;
using LTT.Utility;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTT
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LectureTimeTableStart lectureTimeTableStart = new LectureTimeTableStart();
            lectureTimeTableStart.StartProgram();
        }
    }
}