using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebReports
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            String strAllValues = hfSelectedValues.Value; if (strAllValues != null)
            {

                if (!strAllValues.Equals(String.Empty))
                {

                    //Remember 1 extra (,) Comma appended at last, ignore it

                    String[] strRoleIds = strAllValues.Split(','); for (int i = 0; i < strRoleIds.Length - 1; i++)
                    {

                        //Label1.Text += strRoleIds[i].ToString();

                    }

                }

            }

        }
    }
}