using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7.Ajax
{
    public partial class AjaxPageLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "You have clicked the button";
        }
    }
}