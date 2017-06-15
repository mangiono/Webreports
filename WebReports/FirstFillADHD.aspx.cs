using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace WebReports
{
    public partial class FirstFillADHD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                {


                    string file_name = FileUpload1.FileName.ToString();
                    Session.Add("file_name", file_name);


                    ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                    //GridView1.DataSource = package.ToDataTable();
                    DataTable dt1 = package.ToDataTable();
                    dt1.TableName = "zzz_ADHD";
                    //GridView1.DataBind();

                    string alter = "alter table [Intranet2012].[dbo].[zzz_ADHD]   add   [Mem Term Date] [varchar](255) NULL, 	[PCP] [varchar](255) NULL, 	[PCP Address1] [varchar](255) NULL, 	[PCP Address2] [varchar](255) NULL, 	[PCP City] [varchar](255) NULL, 	[PCP State] [varchar](255) NULL,     [PCP Zip] [varchar](255) NULL";

                    string result = "IF OBJECT_ID(" + "'dbo." + dt1.TableName + "', 'U') IS NOT NULL " + "DROP TABLE dbo." + dt1.TableName + " " + BuildCreateTableScript(dt1);
                    //SqlConnection GetSQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);



                    SqlConnection SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
                    SqlCommand commanda = new SqlCommand(alter, SqlConnectionObj);
                    SqlCommand command = new SqlCommand(result, SqlConnectionObj);
                    SqlConnectionObj.Open();
                    command.ExecuteNonQuery();
                    commanda.ExecuteNonQuery();

                    SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlConnectionObj, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                    bulkCopy.DestinationTableName = "dbo.zzz_ADHD";
                    bulkCopy.WriteToServer(package.ToDataTable());
                    //isSuccuss = true;
                    SqlCommand cmd = new SqlCommand("dbo.zzz_ADHP_GetData", SqlConnectionObj);

                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = "dbo.zzz_ADHP_GetData";

                    DataTable dt = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    //cmd.ExecuteNonQuery();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    SqlConnectionObj.Close();
                    Session.Add("dt", dt);
                    btndownload.Visible = true;
                    btnImportExcel.Visible = false;
                    btnNewQuery.Visible = true;
                    lblOr.Visible = true;
                    FileUpload1.Visible = false;
                    lblTitle.Visible = false;
                    lblmsgdone.Text = "Provider data has been added to the file, please click 'Export to excel' to download the file.";
                    lblmsgdone.Visible = true;
                }

                else
                {
                    string display = "Please supply an xlsx file.";
                    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
                }



            }
            else
            {
                string display = "Please choose a file.";
                ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
            }

            
        }

        protected void btndownload_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)Session["dt"];

            string fn = (string)Session["file_name"];

            using (ExcelPackage xp = new ExcelPackage())
            {


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
                //int i = 1;
                //foreach (DataColumn dc in dt.Columns)
                //{
                //    i++;
                //    if (dc.DataType == typeof(decimal))
                //        ws.Column(i).Style.Numberformat.Format = "#0.00";
                //}
                ws.Cells[ws.Dimension.Address].AutoFitColumns();



                //ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                //   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                //Response.AddHeader("content-disposition", "attachment;filename=DataExcel.xlsx");
                Response.AddHeader("content-disposition", "attachment;filename=" + fn);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(xp.GetAsByteArray());
                Response.End();

            }


        }

        public static string BuildCreateTableScript(DataTable Table)
        {
            //if (!Helper.IsValidDatatable(Table, IgnoreZeroRows: true))
            //    return string.Empty;

            StringBuilder result = new StringBuilder();
            result.AppendFormat("CREATE TABLE dbo.[{1}] ({0}   ", Environment.NewLine, Table.TableName);

            bool FirstTime = true;
            foreach (DataColumn column in Table.Columns.OfType<DataColumn>())
            {
                if (FirstTime) FirstTime = false;
                else
                    result.Append("   ,");

                result.AppendFormat("[{0}] {1} {2} {3}",
                    column.ColumnName, // 0
                    GetSQLTypeAsString(column.DataType), // 1
                    column.AllowDBNull ? "NULL" : "NOT NULL", // 2
                    Environment.NewLine // 3
                );
            }
            result.AppendFormat(") ON [PRIMARY]{0}{0}{0}", Environment.NewLine);

            // Build an ALTER TABLE script that adds keys to a table that already exists.
            if (Table.PrimaryKey.Length > 0)
                result.Append(BuildKeysScript(Table));

            return result.ToString();
        }

        private static string BuildKeysScript(DataTable Table)
        {
            // Already checked by public method CreateTable. Un-comment if making the method public
            // if (Helper.IsValidDatatable(Table, IgnoreZeroRows: true)) return string.Empty;
            if (Table.PrimaryKey.Length < 1) return string.Empty;

            StringBuilder result = new StringBuilder();

            if (Table.PrimaryKey.Length == 1)
                result.AppendFormat("ALTER TABLE {1}{0}   ADD PRIMARY KEY ({2}){0}GO{0}{0}", Environment.NewLine, Table.TableName, Table.PrimaryKey[0].ColumnName);
            else
            {
                List<string> compositeKeys = Table.PrimaryKey.OfType<DataColumn>().Select(dc => dc.ColumnName).ToList();
                string keyName = compositeKeys.Aggregate((a, b) => a + b);
                string keys = compositeKeys.Aggregate((a, b) => string.Format("{0}, {1}", a, b));
                result.AppendFormat("ALTER TABLE {1}{0}ADD CONSTRAINT pk_{3} PRIMARY KEY ({2}){0}GO{0}{0}", Environment.NewLine, Table.TableName, keys, keyName);
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns the SQL data type equivalent, as a string for use in SQL script generation methods.
        /// </summary>
        private static string GetSQLTypeAsString(Type DataType)
        {
            switch (DataType.Name)
            {
                case "Boolean": return "[bit]";
                case "Char": return "[char]";
                case "SByte": return "[tinyint]";
                case "Int16": return "[smallint]";
                case "Int32": return "[int]";
                case "Int64": return "[bigint]";
                case "Byte": return "[tinyint] UNSIGNED";
                case "UInt16": return "[smallint] UNSIGNED";
                case "UInt32": return "[int] UNSIGNED";
                case "UInt64": return "[bigint] UNSIGNED";
                case "Single": return "[float]";
                case "Double": return "[double]";
                case "Decimal": return "[decimal]";
                case "DateTime": return "[datetime]";
                case "Guid": return "[uniqueidentifier]";
                case "Object": return "[variant]";
                case "String": return "[nvarchar](500)";
                default: return "[nvarchar](MAX)";
            }
        }

        protected void btnNewQuery_Click(object sender, EventArgs e)
        {
            Response.Redirect("FirstFillADHD.aspx");
        }
    }
}