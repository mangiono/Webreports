using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Data.SqlTypes;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;


namespace WebReports
{
    public partial class ExcelDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            using (ExcelPackage xp = new ExcelPackage())
            {

                DataTable dt = Session["DTSes"] as DataTable;
                dt.TableName = "Sheet1";


                ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);

                int rowstart = 1;
                int colstart = 1;
                int rowend = rowstart;
                int colend = colstart + dt.Columns.Count;

                //ws.Cells[rowstart, colstart, rowend, colend].Merge = true;
                //ws.Cells[rowstart, colstart, rowend, colend].Value = dt.TableName;
                //ws.Cells[rowstart, colstart, rowend, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Bold = true;
                //ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                rowstart += 0;
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



                //ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                Response.AddHeader("content-disposition", "attachment;filename=test.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(xp.GetAsByteArray());
                Response.End();

               
            }
        }
    }
}