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
    public partial class ImportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {


            FileUpload1 = (FileUpload)Session["FileUpload1"];
            SqlConnection connection;
            //Check if column names are duplicated
            string file_name = FileUpload1.FileName.ToString();

            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            //GridView1.DataSource = package.ToDataTable();


            //put logic here to check for duplicate column names

            DataTable dtdup = package.ToDataTableDup(lslBoxSheets.SelectedValue.ToString());

            var duplicates = dtdup.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1);

            if (duplicates.Any())
            {
                string display = "Duplicate columns found in spreadsheet - import aborted!";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                //pnlMain.Visible = false;
                //pnlResults.Visible = false;
                //lblmsgdone.Text = "Duplicate column found - import aborted!";
                //lblmsgdone.ForeColor = System.Drawing.Color.Red;
                //btnViewData.Visible = false;
                return;

            }


            string tblnamet = txtTableName.Text;

            //if (tblnamet.Any(Char.IsWhiteSpace))
            //{
            //    string display1 = "Table name cannot have spaces.";
            //    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
            //    txtTableName.Focus();
            //    return;
            //}


            //string selectedValue = rbtntblList.SelectedValue;


            DataTable dt = new DataTable();
            //string query = "select count(*) as Total from [Intranet2012].[dbo].[ClaimsAdhocsavedReports] where ReportName =" + "'" + txtboxReportName.Text + "'";

            if (ddlDbase.Text == "Claims_Reporting")
            {

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Claims_ReportingConnectionString"].ConnectionString);
            }

            else
            {

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
            }//string query = @"select count(*) as Total from @db.INFORMATION_SCHEMA.TABLES where TABLE_NAME = @tn";


            string query = @"IF EXISTS(SELECT * FROM " + ddlDbase.Text + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME= '" + txtTableName.Text + "') SELECT 1 ELSE SELECT 0";

            //return;
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@db", ddlDbase.Text.Trim());
            //cmd.Parameters.AddWithValue("@tn", txtTableName.Text.Trim());
            //cmd.Parameters["@rn"].Value = ddlSavedReport.SelectedValue;
            //return;
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            int x = Convert.ToInt32(cmd.ExecuteScalar());
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);
            connection.Close();
            //da.Dispose();

            if (x == 1)
            {

                Label1.Text = "Table '" + txtTableName.Text + "' already exists - would you like to drop and re-create it or cancel the import?";
                Label1.ForeColor = System.Drawing.Color.Black;
                Label1.Font.Bold = true;
                mp1.Show();

                //string display1 = "Table name exists - file not imported!";
                //////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
                ////FileUpload1 = (FileUpload)Session["FileUpload1"];
                return;
            }

            else
            {
                importfile();

                pnlMain.Visible = false;
                pnlResults.Visible = true;
                txtTableName.Text = "";

                txtSQL.Text = "Select * from " + ddlDbase.Text + ".dbo." + tblnamet;
                txtSQL.Focus();
                txtSQL.Attributes.Add("onfocus", "this.select()");

                btnViewData.Visible = true;

            }


            //importfile();



            //pnlMain.Visible = false;
            //pnlResults.Visible = true;
            //txtTableName.Text = "";

            //txtSQL.Text = "select * from " + ddlDbase.Text + ".dbo." + tblnamet;
            //txtSQL.Focus();
            //txtSQL.Attributes.Add("onfocus", "this.select()");

            //btnViewData.Visible = true;


            Session.Clear();

        }
        protected void importfile()

        {

            FileUpload1 = (FileUpload)Session["FileUpload1"];

            SqlConnection SqlConnectionObj;

            if (ddlDbase.Text == "Claims_Reporting")
            {

                SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Claims_ReportingConnectionString"].ConnectionString);
            }

            else
            {

                SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
            }

            ////check columns
            //using (ExcelPackage package = new ExcelPackage(FileUpload1.FileContent))
            //{

            //    string file = FileUpload1.FileContent.ToString();
            //    ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

            //    string test = "";
            //    foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            //    {
            //        test = test + firstRowCell.Text;
            //        string display = test;
            //        ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
            //    }
            //}


            //return;
            //string display1 = Path.GetExtension(FileUpload1.FileName);
            //////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
            //return;
            if (FileUpload1.HasFile)
            {



                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                {

                    string sname = lslBoxSheets.SelectedValue.ToString();
                    string file_name = FileUpload1.FileName.ToString();

                    ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                    //GridView1.DataSource = package.ToDataTable();

                    DataTable dt1 = package.ToDataTableSN(sname);



                    dt1.TableName = ddlDbase.Text + ".dbo." + txtTableName.Text;//txtTableName.Text;
                    string servtbl = ddlDbase.Text + ".dbo." + txtTableName.Text;
                    //GridView1.DataBind();

                    //string alter = "alter table [Intranet2012].[dbo].[zzz_ADHD]   add   [Mem Term Date] [varchar](255) NULL, 	[PCP] [varchar](255) NULL, 	[PCP Address1] [varchar](255) NULL, 	[PCP Address2] [varchar](255) NULL, 	[PCP City] [varchar](255) NULL, 	[PCP State] [varchar](255) NULL,     [PCP Zip] [varchar](255) NULL";

                    //string result = "IF OBJECT_ID(" + "'dbo." + dt1.TableName + "', 'U') IS NOT NULL " + "DROP TABLE dbo." + dt1.TableName + " " + BuildCreateTableScript(dt1);
                    string result = "IF OBJECT_ID('" + servtbl + "', 'U') IS NOT NULL " + "DROP TABLE " + servtbl + " " + BuildCreateTableScript(dt1);
                    //SqlConnection GetSQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);


                    //return;
                    //SqlConnection SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
                    //SqlCommand commanda = new SqlCommand(alter, SqlConnectionObj);
                    SqlCommand command = new SqlCommand(result, SqlConnectionObj);
                    SqlConnectionObj.Open();
                    command.ExecuteNonQuery();
                    //commanda.ExecuteNonQuery();

                    SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlConnectionObj, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                    bulkCopy.DestinationTableName = servtbl;
                    bulkCopy.WriteToServer(package.ToDataTableSN(sname));
                    SqlConnectionObj.Close();


                    int columnsimported = dt1.Columns.Count;
                    int rowsimported = dt1.Rows.Count;

                    lblmsgdone.Text = "Excel file '" + file_name + "' has been imported to table '" + txtTableName.Text + "' - " + columnsimported.ToString() + " columns and " + rowsimported.ToString() + " rows imported";


                    //option to show msg box
                    //string display = "Excel file (" + file_name + ") has been imported to table (" + txtTableName.Text + ")"  + "\\n" + columnsimported.ToString() + " columns and " + rowsimported.ToString() + " rows imported";
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                }

                else
                {
                    string display = "Please supply an xlsx file.";
                    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
                }

            }


        }

        protected void importfileFirst()

        {

            FileUpload1 = (FileUpload)Session["FileUpload1"];

            SqlConnection SqlConnectionObj;

            if (ddlDbase.Text == "Claims_Reporting")
            {

                SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Claims_ReportingConnectionString"].ConnectionString);
            }

            else
            {

                SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
            }

            ////check columns
            //using (ExcelPackage package = new ExcelPackage(FileUpload1.FileContent))
            //{

            //    string file = FileUpload1.FileContent.ToString();
            //    ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

            //    string test = "";
            //    foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            //    {
            //        test = test + firstRowCell.Text;
            //        string display = test;
            //        ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
            //    }
            //}


            //return;
            //string display1 = Path.GetExtension(FileUpload1.FileName);
            //////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
            //return;
            if (FileUpload1.HasFile)
            {



                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                {

                    string sname = lslBoxSheets.SelectedValue.ToString();
                    string file_name = FileUpload1.FileName.ToString();

                    ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                    //GridView1.DataSource = package.ToDataTable();

                    DataTable dt1 = package.ToDataTable();



                    dt1.TableName = ddlDbase.Text + ".dbo." + txtTableName.Text;//txtTableName.Text;
                    string servtbl = ddlDbase.Text + ".dbo." + txtTableName.Text;
                    //GridView1.DataBind();

                    //string alter = "alter table [Intranet2012].[dbo].[zzz_ADHD]   add   [Mem Term Date] [varchar](255) NULL, 	[PCP] [varchar](255) NULL, 	[PCP Address1] [varchar](255) NULL, 	[PCP Address2] [varchar](255) NULL, 	[PCP City] [varchar](255) NULL, 	[PCP State] [varchar](255) NULL,     [PCP Zip] [varchar](255) NULL";

                    //string result = "IF OBJECT_ID(" + "'dbo." + dt1.TableName + "', 'U') IS NOT NULL " + "DROP TABLE dbo." + dt1.TableName + " " + BuildCreateTableScript(dt1);
                    string result = "IF OBJECT_ID('" + servtbl + "', 'U') IS NOT NULL " + "DROP TABLE " + servtbl + " " + BuildCreateTableScript(dt1);
                    //SqlConnection GetSQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);


                    //return;
                    //SqlConnection SqlConnectionObj = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
                    //SqlCommand commanda = new SqlCommand(alter, SqlConnectionObj);
                    SqlCommand command = new SqlCommand(result, SqlConnectionObj);
                    SqlConnectionObj.Open();
                    command.ExecuteNonQuery();
                    //commanda.ExecuteNonQuery();

                    SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlConnectionObj, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                    bulkCopy.DestinationTableName = servtbl;
                    bulkCopy.WriteToServer(package.ToDataTable());
                    SqlConnectionObj.Close();


                    int columnsimported = dt1.Columns.Count;
                    int rowsimported = dt1.Rows.Count;

                    lblmsgdone.Text = "Excel file '" + file_name + "' has been imported to table '" + txtTableName.Text + "' - " + columnsimported.ToString() + " columns and " + rowsimported.ToString() + " rows imported";


                    //option to show msg box
                    //string display = "Excel file (" + file_name + ") has been imported to table (" + txtTableName.Text + ")"  + "\\n" + columnsimported.ToString() + " columns and " + rowsimported.ToString() + " rows imported";
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                }

                else
                {
                    string display = "Please supply an xlsx file.";
                    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
                }

            }


        }


        protected void btndownload_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)Session["dt"];


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


                Response.AddHeader("content-disposition", "attachment;filename=DataExcel.xlsx");
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
            result.AppendFormat("CREATE TABLE {1} ({0}   ", Environment.NewLine, Table.TableName);

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
                case "String": return "[nvarchar](4000)";
                default: return "[nvarchar](MAX)";
            }
        }

        protected void btnViewData_Click(object sender, EventArgs e)
        {


            string constr = ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(txtSQL.Text))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Visible = true;
                        }
                    }
                }
            }




            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
            ////SqlConnection con = new SqlConnection(strConnection);
            //con.Open();

            //SqlCommand sqlCmd = new SqlCommand();
            //sqlCmd.Connection = con;
            //sqlCmd.CommandType = CommandType.Text;
            //sqlCmd.CommandText = "select * from intranet2012.dbo.zzzzzxzxzx";//lblSQL.Text;
            //SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

            //DataTable dtRecord = new DataTable();
            //sqlDataAdap.Fill(dtRecord);
            //GridView1.DataSource = dtRecord;
            //GridView1.Visible = true;

            //con.Close();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportData.aspx");
        }



        protected void rbtntblList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["FileUpload1"] = FileUpload1;
            //FileUpload1 = (FileUpload)(Session["FileUpload1"]);
        }

        protected void lslBoxSheets_SelectedIndexChanged(object sender, EventArgs e)
        {

            //FileUpload1 = (FileUpload)(Session["FileUpload1"]);
            FileUpload1 = (FileUpload)Session["FileUpload1"];

            string file_name = FileUpload1.FileName.ToString();

            string sname = lslBoxSheets.SelectedValue.ToString();
            //check if no data in A1
            ExcelPackage package1 = new ExcelPackage(FileUpload1.FileContent);
            ExcelWorksheet workSheet = package1.Workbook.Worksheets[sname];
            if (workSheet.Cells["A1"].Value == null)
            {

                string display = "No value found in cell A1 - import aborted!";
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + display + "');window.location ='ImportData.aspx';", true);

                //return;
            }



            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            //GridView1.DataSource = package.ToDataTable();

            
            //put logic here to check for duplicate column names

            DataTable dtdup = package.ToDataTableDup(sname);

            var duplicates = dtdup.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1);

            if (duplicates.Any())
            {
                string display = "Duplicate columns found in spreadsheet - import aborted!";
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + display + "');window.location ='ImportData.aspx';", true);

                //Response.Redirect("ImportData.aspx");
                //pnlMain.Visible = false;
                //pnlResults.Visible = false;
                //lblmsgdone.Text = "Duplicate column found - import aborted!";
                //lblmsgdone.ForeColor = System.Drawing.Color.Red;
                //btnViewData.Visible = false;
                return;

            }

            else
            {
                //lslBoxSheets.GetItemText(listBox1.SelectedValue);
                btnImportExcel.Text = "Import sheet: " + "'" + lslBoxSheets.SelectedValue + "'";
                btnImportExcel.Visible = true;

            }
            //string path = FileUpload1.FileContent.ToString();

        }

        protected void btnChooseSheet_Click(object sender, EventArgs e)
        {

            lblOR.Visible = false;
            btnFirstSheet.Visible = false;
            btnChooseSheet.Visible = false;

            Session["FileUpload1"] = FileUpload1;

            string path = Convert.ToString(FileUpload1.PostedFile.FileName);
            


            //display = display + excelWorksheet.ToString();
            //ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + path + "');", true);
            lslBoxSheets.Items.Clear();
            var existingFile = new FileInfo(path);
            //var sheets = new List<string>();

            //ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            using (ExcelPackage package = new ExcelPackage(FileUpload1.FileContent))
            {
                // var worksheet = package.Workbook.Worksheets[1];

                // OR
                //string display = "";

                foreach (var excelWorksheet in package.Workbook.Worksheets)

                {
                    lslBoxSheets.Items.Add(new ListItem(excelWorksheet.ToString(), excelWorksheet.ToString()));

                    //display = display + excelWorksheet.ToString();
                    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);


                }
                lblChooseSheet.Visible = true;
                lslBoxSheets.Visible = true;
                //ddlAge.Items.Add(new ListItem(i.ToString(), i.ToString()));'
            }

            //FileUpload1 = (FileUpload)Session["FileUpload1"];
            
        }

        protected void btnFirstSheet_Click(object sender, EventArgs e)
        {




            Session["FileUpload1"] = FileUpload1;
            //check if no data in A1
            ExcelPackage package1 = new ExcelPackage(FileUpload1.FileContent);
            ExcelWorksheet workSheet = package1.Workbook.Worksheets.First();


            if (workSheet.Cells["A1"].Value == null)
            {

                string display = "No value found in cell A1 - import aborted!";
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + display + "');window.location ='ImportData.aspx';", true);
                
                return;
            }



            //FileUpload1 = (FileUpload)Session["FileUpload1"];
            SqlConnection connection;
            //Check if column names are duplicated
            string file_name = FileUpload1.FileName.ToString();

            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            //GridView1.DataSource = package.ToDataTable();


            //put logic here to check for duplicate column names

            DataTable dtdup = package.ToDataTableDupFirst();
            string msg1 = "Blank column found in excel header - aborting import!";
            foreach (DataRow row in dtdup.Rows)
            {
                //TextBox1.Text = row["ImagePath"].ToString();
                if (row["Test"].ToString().Trim() == "")
                   

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + msg1 + "');window.location ='ImportData.aspx';", true);
                //return;
            }




            var duplicates = dtdup.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1);

            if (duplicates.Any())
            {
                //string display = "Duplicate columns found in spreadsheet - import aborted!";
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");


                string display = "Duplicate columns found in spreadsheet - import aborted!";
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + display + "');window.location ='ImportData.aspx';", true);

                //pnlMain.Visible = false;
                //pnlResults.Visible = false;
                //lblmsgdone.Text = "Duplicate column found - import aborted!";
                //lblmsgdone.ForeColor = System.Drawing.Color.Red;
                //btnViewData.Visible = false;
                return;

            }


            string tblnamet = txtTableName.Text;

            //if (tblnamet.Any(Char.IsWhiteSpace))
            //{
            //    string display1 = "Table name cannot have spaces.";
            //    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
            //    txtTableName.Focus();
            //    return;
            //}


            //string selectedValue = rbtntblList.SelectedValue;

            
                DataTable dt = new DataTable();
                //string query = "select count(*) as Total from [Intranet2012].[dbo].[ClaimsAdhocsavedReports] where ReportName =" + "'" + txtboxReportName.Text + "'";

                if (ddlDbase.Text == "Claims_Reporting")
                {

                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Claims_ReportingConnectionString"].ConnectionString);
                }

                else
                {

                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
                }//string query = @"select count(*) as Total from @db.INFORMATION_SCHEMA.TABLES where TABLE_NAME = @tn";


                string query = @"IF EXISTS(SELECT * FROM " + ddlDbase.Text + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME= '" + txtTableName.Text + "') SELECT 1 ELSE SELECT 0";

                //return;
                //SqlCommand cmd = new SqlCommand(query, connection);
                //cmd.Parameters.AddWithValue("@db", ddlDbase.Text.Trim());
                //cmd.Parameters.AddWithValue("@tn", txtTableName.Text.Trim());
                //cmd.Parameters["@rn"].Value = ddlSavedReport.SelectedValue;
                //return;
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                int x = Convert.ToInt32(cmd.ExecuteScalar());
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);
                connection.Close();
            //da.Dispose();

            if (x == 1)
            {


                Label1.Text = "Table '" + txtTableName.Text + "' already exists - would you like to drop and re-create it or cancel the import?";
                Label1.ForeColor = System.Drawing.Color.Black;
                Label1.Font.Bold = true;
                mp1.Show();
                //string display1 = "Table name exists - file not imported!";
                //////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
                ////FileUpload1 = (FileUpload)Session["FileUpload1"];
                return;

            }

            else

            {
                importfileFirst();

                pnlMain.Visible = false;
                pnlResults.Visible = true;


                txtSQL.Text = "Select * from " + ddlDbase.Text + ".dbo." + txtTableName.Text;
                txtSQL.Focus();
                txtSQL.Attributes.Add("onfocus", "this.select()");

                btnViewData.Visible = true;

            }
              
                //    importfileFirst();

                //    pnlMain.Visible = false;
                //    pnlResults.Visible = true;
                //    txtTableName.Text = "";

                //    txtSQL.Text = "Select * from " + ddlDbase.Text + ".dbo." + tblnamet;
                //    txtSQL.Focus();
                //    txtSQL.Attributes.Add("onfocus", "this.select()");

                //    btnViewData.Visible = true;
              
            
            
            

                //importfileFirst();



                //pnlMain.Visible = false;
                //pnlResults.Visible = true;
                //txtTableName.Text = "";

                //txtSQL.Text = "select * from " + ddlDbase.Text + ".dbo." + tblnamet;
                //txtSQL.Focus();
                //txtSQL.Attributes.Add("onfocus", "this.select()");

                //btnViewData.Visible = true;
            

            //Session.Clear();
        }

        protected void btnResetpage_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportData.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

            if (btnImportExcel.Visible == true)
            {
                importfile();
            }

            else

            {
                importfileFirst();
            }
            

            pnlMain.Visible = false;
            pnlResults.Visible = true;
            

            txtSQL.Text = "Select * from " + ddlDbase.Text + ".dbo." + txtTableName.Text;
            txtSQL.Focus();
            txtSQL.Attributes.Add("onfocus", "this.select()");

            btnViewData.Visible = true;
            //Session.Clear();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //string display1 = "You pressed Cancel";
            //////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display1 + "')</script>");
            ////FileUpload1 = (FileUpload)Session["FileUpload1"];
            Response.Redirect("ImportData.aspx");
            //return;

        }
    }
}