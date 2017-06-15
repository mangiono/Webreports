using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.Data;

namespace WebReports
{
    public static class ExcelCheckDupColumns
    {
        public static DataTable ToDataTableDup(this ExcelPackage package, System.String args)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets[args.ToString()];
            DataTable table = new DataTable();
            table.Columns.Add("Test");
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {

                
                table.Rows.Add(firstRowCell.Text);

            }


            return table;
        }

        public static DataTable ToDataTableDupFirst(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            table.Columns.Add("Test");
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {


                table.Rows.Add(firstRowCell.Text);

            }


            return table;
        }

    }
}