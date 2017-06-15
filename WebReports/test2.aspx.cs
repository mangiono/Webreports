using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebReports
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //ExecuteReader("Data Source=cfis2k8;Initial Catalog=Intranet2012;Persist Security Info=True;User ID=Intranet;Password=cfhp!@#as" , "Select top 1 * from [Intranet2012].[dbo].[ClaimsAdhocsavedReports]", CommandType.Text);

            GridView1.DataSource = ExecuteReader("Data Source=cfis2k8;Initial Catalog=Intranet2012;Persist Security Info=True;User ID=Intranet;Password=cfhp!@#as", "Select  count(*) from [Intranet2012].[dbo].[tbl_PCPMedReview]", CommandType.Text);
            GridView1.DataBind();
            string test = "sdfsdfsd";

            Response.Write(test);
        }


        public static SqlDataReader ExecuteReader(String connectionString, String commandText,
      CommandType commandType)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                //cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the 
                // IDataReader is closed.
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }
    }
}