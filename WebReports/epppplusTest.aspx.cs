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
    public partial class epppplusTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo("c:\\test\\test.xlsx")))
            {
                // var worksheet = package.Workbook.Worksheets[1];

                // OR
                string display = "";

                foreach (var excelWorksheet in package.Workbook.Worksheets) 

                {
                     display  = display + excelWorksheet.ToString();
                    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                   

                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");

            }
        }
    }
}