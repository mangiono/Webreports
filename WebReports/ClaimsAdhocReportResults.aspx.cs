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
    public partial class ClaimsAdhocReportResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string type = Session["Type"].ToString();

            string crt = Session["CriteriaInfo"].ToString();

            if (type == "Screen")
            {
                DataTable dt = (DataTable)Session["dt"];


                grvData.DataSource = dt;
                grvData.DataBind();
                grvData.Visible = true;
                lblMessage.Text = dt.Rows.Count.ToString() + " record(s) found.";

                txtCriteriaResults.Text = crt;
                if (dt.Rows.Count == 0)

                {
                    btnExportExcel.Visible = false;
                }

                else
                {
                    btnExportExcel.Visible = true;
                }


            }


        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToString("dd, MMM yyyy");
            //}
        }


        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dt"];

            using (ExcelPackage xp = new ExcelPackage())
            {


                dt.TableName = "ClaimReportAff";

                if (dt.Rows.Count == 0)

                {
                    string display = "No records found";
                    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
                }

                else
                {
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
                    //int i = 1;
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    //    i++;
                    //    if (dc.DataType == typeof(decimal))
                    //        ws.Column(i).Style.Numberformat.Format = "#0.00";
                    //}
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();



                    ExcelWorksheet ws2 = xp.Workbook.Worksheets.Add("Criteria");

                    //int rowstart2 = 1;
                    //int colstart2 = 1;
                    //int rowend2 = rowstart2;
                    //int colend2 = colstart2;
                    ////ws2.Cells[ws2.Dimension.Address].AutoFitColumns();

                    //rowstart2 += 0;
                    //rowend2 = rowstart2;
                    //ws2.Cells[rowstart2, colstart2].LoadFromText("test");
                    ws2.Cells[1, 1].LoadFromText("Criteria Chosen");
                    ws2.Cells[2, 1].LoadFromText(txtCriteriaResults.Text);
                    ws2.Cells[ws2.Dimension.Address].AutoFitColumns();
                    //ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                    //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                    //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                    //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                    Response.AddHeader("content-disposition", "attachment;filename=ClaimReportbyAffiliation.xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(xp.GetAsByteArray());
                    Response.End();
                }
            }

        }
    }
}