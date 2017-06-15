using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebReports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            System.Threading.Thread.Sleep(1000);
            //Bind courses
            // GridView1.DataSource = tblCourse;
            GridView1.DataBind();
            GridView1.Visible = true;


        }

        protected void btnAddAff_Click(object sender, EventArgs e)
        {
            if (txtBoxAff.Text.Trim() == "")
            {
                txtBoxAff.Text = "";
            }
            else
            {
                lstBoxAff.Items.Add(txtBoxAff.Text.ToUpper());
                txtBoxAff.Text = "";
            }
        }

        protected void lstBoxAff_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}