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
    public partial class Claims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            //System.Threading.Thread.Sleep(1000);
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("zzz_procIntranet_ClaimsServiceSearchSortable_List_Test", connection); // stored procedure’s name and connection

            cmd.CommandType = CommandType.StoredProcedure; //   choose command type stored procedures

            cmd.Parameters.Add("@ColumnList", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ColumnList"].Value = "CS.Member#,CS.Aff#"; // add parameters value



            cmd.Parameters.Add("@Membernumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Membernumber"].Value = "A0012928500"; // add parameters value

            SqlDataAdapter dp = new SqlDataAdapter(cmd);

            //System.Threading.Thread.Sleep(5000);
            dp.Fill(dt); // fill results to datatable
            connection.Close();
            using (ExcelPackage xp = new ExcelPackage())
            {

                dt.TableName = "Sheet1";


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


                Response.AddHeader("content-disposition", "attachment;filename=test.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(xp.GetAsByteArray());
                Response.End();

                //Response.Redirect("Claims.aspx");

            }
        
    }

        protected void Button2_Click(object sender, EventArgs e)
        {

            System.Threading.Thread.Sleep(2000);

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("zzz_procIntranet_ClaimsServiceSearchSortable_List_Test", connection); // stored procedure’s name and connection

            cmd.CommandType = CommandType.StoredProcedure; //   choose command type stored procedures

            cmd.Parameters.Add("@ColumnList", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ColumnList"].Value = "CS.Member#,CS.Aff#"; // add parameters value



            cmd.Parameters.Add("@Membernumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Membernumber"].Value = "A0012928500"; // add parameters value

            SqlDataAdapter dp = new SqlDataAdapter(cmd);

            //System.Threading.Thread.Sleep(5000);
            dp.Fill(dt); // fill results to datatable
            connection.Close();
            grvData.DataSource = dt;
            grvData.DataBind();
            grvData.Visible = true;

    
            //UpdatePanel1.Visible = false;

        }

        
    }
}