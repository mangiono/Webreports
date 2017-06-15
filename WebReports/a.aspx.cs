using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OfficeOpenXml;

namespace WebReports
{
    public partial class a : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            CreateExcelFile();
        }

        DataSet GetData()
        {
            DataTable dt = new DataTable("Students");
            dt.Columns.Add("StudentID", typeof(int));
            dt.Columns.Add("StudentName", typeof(string));
            dt.Columns.Add("RollNumber", typeof(int));
            dt.Columns.Add("TotalMarks", typeof(int));
            dt.Rows.Add(1, "Jame's", 101, 900);
            dt.Rows.Add(2, "Steave, Smith", 105, 820);
            dt.Rows.Add(3, "Mark\"Waugh", 109, 850);
            dt.Rows.Add(4, "Steave,\"Waugh", 110, 950);
            dt.Rows.Add(5, "Smith", 111, 910);
            dt.Rows.Add(6, "Williams", 115, 864);
            DataSet ds = new DataSet("Example-DotnetLearners");
            ds.Tables.Add(dt);

            dt = new DataTable("Prodcuts");
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            for (int i = 1; i <= 100; i++)
                dt.Rows.Add(i, "Product - " + i.ToString(), i * 1.123);
            ds.Tables.Add(dt);
            return ds;
        }

        void CreateExcelFile()
        {
            try
            {
                using (DataSet ds = GetData())
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);

                                int rowstart = 2;
                                int colstart = 2;
                                int rowend = rowstart;
                                int colend = colstart + dt.Columns.Count;

                                ws.Cells[rowstart, colstart, rowend, colend].Merge = true;
                                ws.Cells[rowstart, colstart, rowend, colend].Value = dt.TableName;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Bold = true;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                                rowstart += 2;
                                rowend = rowstart + dt.Rows.Count;
                                ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                                int i = 1;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    i++;
                                    if (dc.DataType == typeof(decimal))
                                        ws.Column(i).Style.Numberformat.Format = "#0.00";
                                }
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();



                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                            }
                            Response.AddHeader("content-disposition", "attachment;filename=" + ds.DataSetName + ".xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}