using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebReports
{
    public partial class listboxclientside : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //string ggg = Request.Form["MyListbox"];
            TextBox1.Text = "You selected:";

            for (int i = 0; i <= MyListbox.Items.Count - 1; i++)
            {
                                  TextBox1.Text += "- " + MyListbox.Items[i].Text;
            }

           
        }
    }
}