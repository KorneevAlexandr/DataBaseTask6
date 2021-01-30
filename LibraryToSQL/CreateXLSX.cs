using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Excel = Microsoft.Office.Interop.Excel;

namespace LibraryToSQL
{

    /// <summary>
    /// Class for create xlsx files with tables
    /// </summary>
    public class CreateXLSX
    {
        /// <summary>
        /// Object for get list data
        /// </summary>
        private static CreateList createList;

        /// <summary>
        /// Inizialize object
        /// </summary>
        public CreateXLSX()
        {
            createList = new CreateList();
        }

        /// <summary>
        /// General method for creating xlsx file with data table.
        /// Uses already prepared arrays of strings
        /// </summary>
        /// <param name="mas">Arrays of strings</param>
        /// <param name="path">Path and file name</param>
        private void CreateExcel(string[] mas, string path)
        {
            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("Excel is not properly installed!!");
                return;
            }

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //Вывод в ячейки используя номер строки и столбца Cells[строка, столбец]
            string[] row;
            for (int i = 1; i <= mas.Length; i++)
            {
                row = mas[i - 1].Split(' ');
                for (int j = 1; j <= row.Length; j++)
                {
                    var excelcells = (Excel.Range)xlWorkSheet.Cells[i, j];
                    excelcells.Value2 = row[j - 1];
                }
            }

            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);


            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            Console.WriteLine("Excel file created , you can find the file d:\\csharp-Excel.xlsx");
        }

        /// <summary>
        /// Create xlsx file with list student expulsion
        /// </summary>
        /// <param name="path">Path and file name</param>
        public void CreateListOutStudents(string path = "ListOutStudents.xlsx")
        {
            CreateExcel(createList.GetOutStudents(), path);
        }

        /// <summary>
        /// Creating an xlsx file with a list of average / minimum / maximum grades for each group per semester
        /// </summary>
        /// <param name="path">Path and file name</param>
        public void CreateListMarks(string path = "ListMarks.xlsx")
        {
            CreateExcel(createList.GetResultMark(), path);
        }

        /// <summary>
        /// Creation of xlsx file with a list of all students and session results for each group
        /// </summary>
        /// <param name="path">Path and file name</param>
        public void CreateListResultSession(string path = "ListResultSession.xlsx")
        {
            StringBuilder[] sb = createList.GetResultSession();

            int size = 0;
            for (int i = 0; i < sb.Length; i++)
            {
                size += sb[i].ToString().Split('\n').Length;
            }
            string[] mas = new string[size];

            int index = 0;
            for (int i = 0; i < sb.Length; i++)
            {
                for (int j = 0; j < sb[i].ToString().Split('\n').Length; j++)
                {
                    mas[index] = sb[i].ToString().Split('\n')[j];
                    index++;
                }
            }

            CreateExcel(mas, path);
        }


	}
}
