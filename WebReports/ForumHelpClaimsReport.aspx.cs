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

namespace WebReports
{
    public partial class ForumHelpClaimsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Add attributes to relevant text boxes to only allow typing in numerics
          
            txtBoxNPI.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxTIN.Attributes.Add("onkeydown", "return NumericTextBox(event)");
      

            //string display = txtAllDates.Text;
            //////    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //  Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {



            if (lstStdColumns.Items.Count == 0)
            {
                lblFieldReg.Visible = true;//Items not added in ListBox 
                lstStdColumns.Focus();
            }

            else

            {

                Panel1.Visible = false;
                Panel2.Visible = false;
                //Panel5.Visible = false;
                Panel6.Visible = false;


                System.Threading.Thread.Sleep(2000);
                DataTable dt = Getdata();
                grvData.DataSource = ExecuteReader();
                grvData.DataBind();
                Panel8.Visible = true;

                lblMessage.Text = grvData.Rows.Count.ToString() + " record(s) found.";

                txtCriteriaResults.Text = txtCriteria.Text;

                btnExportExcel.Focus();
                //Session.Add("dt", dt);
                //Session.Add("Type", "Screen");


                //PanelResults.Visible = true;
                //Response.Redirect("ClaimsAdhocReportResults.aspx", false);

            }

        }

        private DataTable Getdata()
        {


            string criteria = "";

            //Get YMDDOS start date
            String DOSStDate = "";
            if (txtBoxDOSStDate.Text == "")
            {
                DOSStDate = null;
            }
            else
            {
                DOSStDate = txtBoxDOSStDate.Text.ToString();
                criteria = "[DOS Start Date = " + DOSStDate + "]";
            }

            //Get YMDDOS end date
            String DOSEndDate = "";
            if (txtBoxDOSEndDate.Text == "")
            {
                DOSEndDate = null;
            }
            else
            {
                DOSEndDate = txtBoxDOSEndDate.Text.ToString();
                criteria = criteria + " [" + "DOS End Date = " + DOSEndDate + "]";
            }

            //Get Trans start date
            String TransStDate = "";
            if (txtBoxTransStartDate.Text == "")
            {
                TransStDate = null;
            }
            else
            {
                TransStDate = txtBoxTransStartDate.Text.ToString();
                criteria = criteria + " [" + "Trans Start Date = " + TransStDate + "]";
            }

            //Get Trans end date
            String TransEndDate = "";
            if (txtBoxTransEndDate.Text == "")
            {
                TransEndDate = null;
            }
            else
            {
                TransEndDate = txtBoxTransEndDate.Text.ToString();
                criteria = criteria + " [" + "Trans End Date = " + TransEndDate + "]";
            }

            //Get Received start date
            String RcvStDate = "";
            if (txtBoxRecievedStartDate.Text == "")
            {
                RcvStDate = null;
            }
            else
            {
                RcvStDate = txtBoxRecievedStartDate.Text.ToString();
                criteria = criteria + " [" + "Recieved Start Date = " + RcvStDate + "]";
            }

            //Get Received end date
            String RcvEndDate = "";
            if (txtBoxRecievedEndDate.Text == "")
            {
                RcvEndDate = null;
            }
            else
            {
                RcvEndDate = txtBoxRecievedEndDate.Text.ToString();
                criteria = criteria + " [" + "Recieved End Date = " + RcvEndDate + "]";
            }

            //Get Paid start date
            String PaidStDate = "";
            if (txtBoxPaidStartDate.Text == "")
            {
                PaidStDate = null;
            }
            else
            {
                PaidStDate = txtBoxPaidStartDate.Text.ToString();
                criteria = criteria + " [" + "Paid Start Date = " + PaidStDate + "]";
            }

            //Get Paid end date
            String PaidEndDate = "";
            if (txtBoxPaidEndDate.Text == "")
            {
                PaidEndDate = null;
            }
            else
            {
                PaidEndDate = txtBoxPaidEndDate.Text.ToString();
                criteria = criteria + " [" + "Paid End Date = " + PaidEndDate + "]";
            }

            //Get Due start date
            String DueStDate = "";
            if (txtBoxDueStartDate.Text == "")
            {
                DueStDate = null;
            }
            else
            {
                DueStDate = txtBoxDueStartDate.Text.ToString();
                criteria = criteria + " [" + "Due Start Date = " + DueStDate + "]";
            }

            //Get Due end date
            String DueEndDate = "";
            if (txtBoxDueEndDate.Text == "")
            {
                DueEndDate = null;
            }
            else
            {
                DueEndDate = txtBoxDueEndDate.Text.ToString();
                criteria = criteria + " [" + "Due End Date = " + DueEndDate + "]";
            }

            // Get Aff list from listbox and assign to variable
            String affnbr = "";
            for (int i = 0; i < lstBoxAff.Items.Count; i++)
            {

                {
                    affnbr = affnbr + lstBoxAff.Items[i].Value + "','";
                }
            }

            if (affnbr == "")
            {
                affnbr = null;
            }
            else
            {

                affnbr = affnbr.ToString().Substring(0, affnbr.Length - 3);
                affnbr.ToString();
                criteria = criteria + " [" + "Affliation# = " + affnbr.Replace("','", "|") + "]";
            }

            // Get Prac list from listbox and assign to variable
            String prac = "";
            for (int i = 0; i < lstBoxPrac.Items.Count; i++)
            {

                {
                    prac = prac + lstBoxPrac.Items[i].Value + "','";
                }
            }

            if (prac == "")
            {
                prac = null;
            }
            else
            {

                prac = prac.ToString().Substring(0, prac.Length - 3);
                prac.ToString();
                criteria = criteria + " [" + "Prac# = " + prac.Replace("','", "|") + "]";
            }


            //Get member num value
            String memNbr = "";
            if (txtBoxMemNbr.Text == "")
            {
                memNbr = null;
            }
            else
            {
                memNbr = txtBoxMemNbr.Text.ToString();
                criteria = criteria + " [Member# = " + memNbr + "]";
            }

            // Get ClaimNo list from listbox and assign to variable
            String claimno = "";
            for (int i = 0; i < lstBoxClaimNo.Items.Count; i++)
            {

                {
                    claimno = claimno + lstBoxClaimNo.Items[i].Value + "','";
                }
            }

            if (claimno == "")
            {
                claimno = null;
            }
            else
            {

                claimno = claimno.ToString().Substring(0, claimno.Length - 3);
                claimno.ToString();
                criteria = criteria + " [" + "Claim# = " + claimno.Replace("','", "|") + "]";
            }



            

           

        
        

            // Get tinnumber list from listbox and assign to variable
            String tinnumber = "";

            for (int i = 0; i < lstBoxTin.Items.Count; i++)
            {

                {
                    tinnumber = tinnumber + lstBoxTin.Items[i].Value + "','";
                }
            }

            if (tinnumber == "")
            {
                tinnumber = null;
            }
            else
            {

                tinnumber = tinnumber.ToString().Substring(0, tinnumber.Length - 3);
                tinnumber.ToString();
                criteria = criteria + "[TIN# " + ddlTinWhere.SelectedItem + " " + tinnumber.Replace("','", "|") + "]";
            }

            // Get NPInumber list from listbox and assign to variable
            String npinumber = "";

            for (int i = 0; i < lstBoxNPI.Items.Count; i++)
            {

                {
                    npinumber = npinumber + lstBoxNPI.Items[i].Value + "','";
                }
            }

            if (npinumber == "")
            {
                npinumber = null;
            }
            else
            {

                npinumber = npinumber.ToString().Substring(0, npinumber.Length - 3);
                npinumber.ToString();
                criteria = criteria + "[NPI# " + ddlNpiWhere.SelectedItem + " " + npinumber.Replace("','", "|") + "]";
            }

        


            //Get column list from listbox and assign to variable
            String get_valueNL = "";
            for (int i = 0; i < lstStdColumns.Items.Count; i++)
            {
                get_valueNL = get_valueNL + lstStdColumns.Items[i].Value + ",";
            }
            get_valueNL = get_valueNL.ToString().TrimEnd(',');



            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("zzz_procIntranet_ClaimsServiceSearchSortable_List_Test", connection); // stored procedure’s name and connection


            cmd.CommandType = CommandType.StoredProcedure; //   choose command type stored procedures

            cmd.Parameters.Add("@ColumnList", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ColumnList"].Value = get_valueNL.ToString(); // add parameters value

            cmd.Parameters.Add("@Startymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Startymdend"].Value = DOSStDate; // add parameters value

            cmd.Parameters.Add("@Endymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Endymdend"].Value = DOSEndDate; // add parameters value

            cmd.Parameters.Add("@Membernumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Membernumber"].Value = memNbr; // add parameters value

            cmd.Parameters.Add("@aff", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@aff"].Value = affnbr; // add parameters value

           
            cmd.Parameters.Add("@StartYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDPaid"].Value = PaidStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDPaid"].Value = PaidEndDate; // add parameters value

            cmd.Parameters.Add("@StartYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDDueDate"].Value = DueStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDDueDate"].Value = DueEndDate; // add parameters value

           
            cmd.Parameters.Add("@TinNumberSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TinNumberSearch"].Value = ddlTinWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TinNumber"].Value = tinnumber; // add parameters value

            cmd.Parameters.Add("@NPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NPISearch"].Value = ddlNpiWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@NPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NPINumber"].Value = npinumber; // add parameters value

           
            cmd.Parameters.Add("@StartYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDRcvd"].Value = RcvStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDRcvd"].Value = RcvEndDate; // add parameters value

            cmd.Parameters.Add("@StartYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDTrans"].Value = TransStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDTrans"].Value = TransEndDate; // add parameters value

           
           

           
            cmd.Parameters.Add("@claimno", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@claimno"].Value = claimno; // add parameters value


            cmd.Parameters.Add("@prac", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@prac"].Value = prac; // add parameters value



            cmd.CommandTimeout = 6000;
            SqlDataAdapter dp = new SqlDataAdapter(cmd);

            //System.Threading.Thread.Sleep(5000);
            dp.Fill(dt); // fill results to datatable
            connection.Close();

            txtCriteria.Text = criteria;

            //Session.Add("CriteriaInfo", criteria);
            //string query = (from SqlParameter p in cmd.Parameters where p != null where p.Value != null select string.Format("Param: {0} = {1},  ", p.ParameterName, p.Value.ToString())).Aggregate(cmd.CommandText, (current, parameter) => current + parameter);
            //txtCriteria.Text = query;

            //string query = cmd.CommandText;

            //foreach (SqlParameter p in cmd.Parameters)
            //{
            //    query = query.Replace(p.ParameterName, p.Value.ToString());
            //}
            //txtCriteria.Text = query;
            //txtCriteria.Visible = true;
            return dt;

        }

        private SqlDataReader ExecuteReader()
        {


            string criteria = "";

            //Get YMDDOS start date
            String DOSStDate = "";
            if (txtBoxDOSStDate.Text == "")
            {
                DOSStDate = null;
            }
            else
            {
                DOSStDate = txtBoxDOSStDate.Text.ToString();
                criteria = "[DOS Start Date = " + DOSStDate + "]";
            }

            //Get YMDDOS end date
            String DOSEndDate = "";
            if (txtBoxDOSEndDate.Text == "")
            {
                DOSEndDate = null;
            }
            else
            {
                DOSEndDate = txtBoxDOSEndDate.Text.ToString();
                criteria = criteria + " [" + "DOS End Date = " + DOSEndDate + "]";
            }

            //Get Trans start date
            String TransStDate = "";
            if (txtBoxTransStartDate.Text == "")
            {
                TransStDate = null;
            }
            else
            {
                TransStDate = txtBoxTransStartDate.Text.ToString();
                criteria = criteria + " [" + "Trans Start Date = " + TransStDate + "]";
            }

            //Get Trans end date
            String TransEndDate = "";
            if (txtBoxTransEndDate.Text == "")
            {
                TransEndDate = null;
            }
            else
            {
                TransEndDate = txtBoxTransEndDate.Text.ToString();
                criteria = criteria + " [" + "Trans End Date = " + TransEndDate + "]";
            }

            //Get Received start date
            String RcvStDate = "";
            if (txtBoxRecievedStartDate.Text == "")
            {
                RcvStDate = null;
            }
            else
            {
                RcvStDate = txtBoxRecievedStartDate.Text.ToString();
                criteria = criteria + " [" + "Recieved Start Date = " + RcvStDate + "]";
            }

            //Get Received end date
            String RcvEndDate = "";
            if (txtBoxRecievedEndDate.Text == "")
            {
                RcvEndDate = null;
            }
            else
            {
                RcvEndDate = txtBoxRecievedEndDate.Text.ToString();
                criteria = criteria + " [" + "Recieved End Date = " + RcvEndDate + "]";
            }

            //Get Paid start date
            String PaidStDate = "";
            if (txtBoxPaidStartDate.Text == "")
            {
                PaidStDate = null;
            }
            else
            {
                PaidStDate = txtBoxPaidStartDate.Text.ToString();
                criteria = criteria + " [" + "Paid Start Date = " + PaidStDate + "]";
            }

            //Get Paid end date
            String PaidEndDate = "";
            if (txtBoxPaidEndDate.Text == "")
            {
                PaidEndDate = null;
            }
            else
            {
                PaidEndDate = txtBoxPaidEndDate.Text.ToString();
                criteria = criteria + " [" + "Paid End Date = " + PaidEndDate + "]";
            }

            //Get Due start date
            String DueStDate = "";
            if (txtBoxDueStartDate.Text == "")
            {
                DueStDate = null;
            }
            else
            {
                DueStDate = txtBoxDueStartDate.Text.ToString();
                criteria = criteria + " [" + "Due Start Date = " + DueStDate + "]";
            }

            //Get Due end date
            String DueEndDate = "";
            if (txtBoxDueEndDate.Text == "")
            {
                DueEndDate = null;
            }
            else
            {
                DueEndDate = txtBoxDueEndDate.Text.ToString();
                criteria = criteria + " [" + "Due End Date = " + DueEndDate + "]";
            }

            // Get Aff list from listbox and assign to variable
            String affnbr = "";
            for (int i = 0; i < lstBoxAff.Items.Count; i++)
            {

                {
                    affnbr = affnbr + lstBoxAff.Items[i].Value + "','";
                }
            }

            if (affnbr == "")
            {
                affnbr = null;
            }
            else
            {

                affnbr = affnbr.ToString().Substring(0, affnbr.Length - 3);
                affnbr.ToString();
                criteria = criteria + " [" + "Affliation# = " + affnbr.Replace("','", "|") + "]";
            }

            // Get Prac list from listbox and assign to variable
            String prac = "";
            for (int i = 0; i < lstBoxPrac.Items.Count; i++)
            {

                {
                    prac = prac + lstBoxPrac.Items[i].Value + "','";
                }
            }

            if (prac == "")
            {
                prac = null;
            }
            else
            {

                prac = prac.ToString().Substring(0, prac.Length - 3);
                prac.ToString();
                criteria = criteria + " [" + "Prac# = " + prac.Replace("','", "|") + "]";
            }


            //Get member num value
            String memNbr = "";
            if (txtBoxMemNbr.Text == "")
            {
                memNbr = null;
            }
            else
            {
                memNbr = txtBoxMemNbr.Text.ToString();
                criteria = criteria + " [Member# = " + memNbr + "]";
            }

            // Get ClaimNo list from listbox and assign to variable
            String claimno = "";
            for (int i = 0; i < lstBoxClaimNo.Items.Count; i++)
            {

                {
                    claimno = claimno + lstBoxClaimNo.Items[i].Value + "','";
                }
            }

            if (claimno == "")
            {
                claimno = null;
            }
            else
            {

                claimno = claimno.ToString().Substring(0, claimno.Length - 3);
                claimno.ToString();
                criteria = criteria + " [" + "Claim# = " + claimno.Replace("','", "|") + "]";
            }




            // Get tinnumber list from listbox and assign to variable
            String tinnumber = "";

            for (int i = 0; i < lstBoxTin.Items.Count; i++)
            {

                {
                    tinnumber = tinnumber + lstBoxTin.Items[i].Value + "','";
                }
            }

            if (tinnumber == "")
            {
                tinnumber = null;
            }
            else
            {

                tinnumber = tinnumber.ToString().Substring(0, tinnumber.Length - 3);
                tinnumber.ToString();
                criteria = criteria + "[TIN# " + ddlTinWhere.SelectedItem + " " + tinnumber.Replace("','", "|") + "]";
            }

            // Get NPInumber list from listbox and assign to variable
            String npinumber = "";

            for (int i = 0; i < lstBoxNPI.Items.Count; i++)
            {

                {
                    npinumber = npinumber + lstBoxNPI.Items[i].Value + "','";
                }
            }

            if (npinumber == "")
            {
                npinumber = null;
            }
            else
            {

                npinumber = npinumber.ToString().Substring(0, npinumber.Length - 3);
                npinumber.ToString();
                criteria = criteria + "[NPI# " + ddlNpiWhere.SelectedItem + " " + npinumber.Replace("','", "|") + "]";
            }

           
           

            //Get column list from listbox and assign to variable
            String get_valueNL = "";
            for (int i = 0; i < lstStdColumns.Items.Count; i++)
            {
                get_valueNL = get_valueNL + lstStdColumns.Items[i].Value + ",";
            }
            get_valueNL = get_valueNL.ToString().TrimEnd(',');



            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);

            //DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("zzz_procIntranet_ClaimsServiceSearchSortable_List_Test", connection)) // stored procedure’s name and connection
            {

                cmd.CommandType = CommandType.StoredProcedure; //   choose command type stored procedures

                cmd.Parameters.Add("@ColumnList", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@ColumnList"].Value = get_valueNL.ToString(); // add parameters value

                cmd.Parameters.Add("@Startymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@Startymdend"].Value = DOSStDate; // add parameters value

                cmd.Parameters.Add("@Endymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@Endymdend"].Value = DOSEndDate; // add parameters value

                cmd.Parameters.Add("@Membernumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@Membernumber"].Value = memNbr; // add parameters value

                cmd.Parameters.Add("@aff", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@aff"].Value = affnbr; // add parameters value

               
                cmd.Parameters.Add("@StartYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@StartYMDPaid"].Value = PaidStDate; // add parameters value

                cmd.Parameters.Add("@EndYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@EndYMDPaid"].Value = PaidEndDate; // add parameters value

                cmd.Parameters.Add("@StartYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@StartYMDDueDate"].Value = DueStDate; // add parameters value

                cmd.Parameters.Add("@EndYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@EndYMDDueDate"].Value = DueEndDate; // add parameters value

               
                cmd.Parameters.Add("@TinNumberSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@TinNumberSearch"].Value = ddlTinWhere.SelectedValue; // add parameters value

                cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@TinNumber"].Value = tinnumber; // add parameters value

                cmd.Parameters.Add("@NPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@NPISearch"].Value = ddlNpiWhere.SelectedValue; // add parameters value

                cmd.Parameters.Add("@NPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@NPINumber"].Value = npinumber; // add parameters value

                
                cmd.Parameters.Add("@StartYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@StartYMDRcvd"].Value = RcvStDate; // add parameters value

                cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@EndYMDRcvd"].Value = RcvEndDate; // add parameters value

                cmd.Parameters.Add("@StartYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@StartYMDTrans"].Value = TransStDate; // add parameters value

                cmd.Parameters.Add("@EndYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@EndYMDTrans"].Value = TransEndDate; // add parameters value

               

                cmd.Parameters.Add("@claimno", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@claimno"].Value = claimno; // add parameters value

               
                cmd.Parameters.Add("@prac", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@prac"].Value = prac; // add parameters value

                connection.Open();

                cmd.CommandTimeout = 6000;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //System.Threading.Thread.Sleep(5000);
                //dp.Fill(dt); // fill results to datatable
                //connection.Close();

                txtCriteria.Text = criteria;

                //Session.Add("CriteriaInfo", criteria);
                //string query = (from SqlParameter p in cmd.Parameters where p != null where p.Value != null select string.Format("Param: {0} = {1},  ", p.ParameterName, p.Value.ToString())).Aggregate(cmd.CommandText, (current, parameter) => current + parameter);
                //txtCriteria.Text = query;

                //string query = cmd.CommandText;

                //foreach (SqlParameter p in cmd.Parameters)
                //{
                //    query = query.Replace(p.ParameterName, p.Value.ToString());
                //}
                //txtCriteria.Text = query;
                //txtCriteria.Visible = true;
                return reader;
            }
        }

        

       
        protected void lstbCSTbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem dataItem = new ListItem();
            dataItem.Text = lstbAllColumns.SelectedItem.ToString();
            dataItem.Value = lstbAllColumns.SelectedValue;
            lstStdColumns.Items.Add(dataItem);
            lstbAllColumns.Items.Remove(lstbAllColumns.SelectedItem);
            lblFieldReg.Visible = false;
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



            ListItem dataItem = new ListItem();
            dataItem.Text = lstStdColumns.SelectedItem.ToString();
            dataItem.Value = lstStdColumns.SelectedValue;
            lstbAllColumns.Items.Add(dataItem);



            lstStdColumns.Items.Remove(lstStdColumns.SelectedItem);
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        private void CreateTable(GridView grvData, ref DataTable table)
        {
            // create columns
            for (int i = 0; i < grvData.HeaderRow.Cells.Count; i++)
                table.Columns.Add(grvData.HeaderRow.Cells[i].Text);

            // fill rows
            foreach (GridViewRow row in grvData.Rows)
            {
                DataRow dr;
                dr = table.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace("&nbsp;", " ");
                }
                table.Rows.Add(dr);
            }
        }

        protected void btnAddTIN_Click(object sender, EventArgs e)
        {

            if (txtBoxTIN.Text.Trim() == "")
            {
                txtBoxTIN.Text = "";
            }
            else
            {
                lstBoxTin.Items.Add(txtBoxTIN.Text.ToUpper());
                txtBoxTIN.Text = "";
            }
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

        protected void btnAddNPI_Click(object sender, EventArgs e)
        {
            if (txtBoxNPI.Text.Trim() == "")
            {
                txtBoxNPI.Text = "";
            }
            else
            {
                lstBoxNPI.Items.Add(txtBoxNPI.Text.ToUpper());
                txtBoxNPI.Text = "";
            }
        }

      

       

        

       

        protected void lstBoxTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxTin.Items.Remove(lstBoxTin.SelectedItem);
        }

        protected void lstBoxNPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxNPI.Items.Remove(lstBoxNPI.SelectedItem);
        }

      
        protected void lstBoxAff_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxAff.Items.Remove(lstBoxAff.SelectedItem);
        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimsAdhocReport.aspx");
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {



            if (lstStdColumns.Items.Count == 0)
            {
                lblFieldReg.Visible = true;//Items not added in ListBox 
                lstbAllColumns.Focus();
                //btnSearch.Focus();
            }

            else
            {



                DataTable dt = Getdata();
                using (ExcelPackage xp = new ExcelPackage())
                {


                    dt.TableName = "Sheet1";

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
                        ws2.Cells[2, 1].LoadFromText(txtCriteria.Text);
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

        protected void txtBoxDOSStDate_TextChanged(object sender, EventArgs e)
        {

        }

       



        protected void btnLtoR_Click1(object sender, EventArgs e)
        {
            int count = lstbAllColumns.Items.Count;//assigning items in listbox it into count variable
            if (count != 0)//checking the conditions
            {
                for (int i = 0; i < count; i++)
                {
                    lstStdColumns.Items.Add(lstbAllColumns.Items[i]);
                }

            }

            lstbAllColumns.Items.Clear();//clear the listbox after transfering records.
            lblFieldReg.Visible = false;
        }

        protected void btnRtoLAll_Click(object sender, EventArgs e)
        {
            int count = lstStdColumns.Items.Count;//assigning items in listbox it into count variable
            if (count != 0)//checking the conditions
            {
                for (int i = 0; i < count; i++)
                {
                    lstbAllColumns.Items.Add(lstStdColumns.Items[i]);
                }
            }

            lstStdColumns.Items.Clear();//clear the listbox after transfering records.
        }

        protected void btnResetFields_Click(object sender, EventArgs e)
        {
            lstStdColumns.DataBind();
            lstbAllColumns.DataBind();
        }

        protected void btnSaveRpt_Click(object sender, EventArgs e)
        {





            if (txtboxReportName.Text == "")

            {
                lblReportNameValid.Visible = true;
                lblReportNameValid.Text = "Report name required.";
                txtboxReportName.Focus();
                return;
            }



            //int reportcount;

            DataTable dt = new DataTable();
            //string query = "select count(*) as Total from [Intranet2012].[dbo].[ClaimsAdhocsavedReports] where ReportName =" + "'" + txtboxReportName.Text + "'";
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
            string query = "select count(*) as Total from [Intranet2012].[dbo].[ClaimsAdhocsavedReports] where ReportName = @rn";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@rn", txtboxReportName.Text.Trim());
            //cmd.Parameters["@rn"].Value = ddlSavedReport.SelectedValue;

            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connection.Close();
            da.Dispose();

            if (dt.Rows[0]["Total"].ToString() == "0")

            {
                savereport();
                //"Report saved - choose view, excel or reset to continue.";
                lblReportSavedSucc.Text = "Report' " + txtboxReportName.Text + "' saved - choose view, excel or reset to continue.";
                //Response.Redirect("ClaimsAdhocReport.aspx");
                lblReportNameValid.Visible = false;
                //Panel1.Visible = false;
                txtboxReportName.Text = null;
                txtboxReportDescription.Text = null;
                lblReportSavedSucc.ForeColor = Color.Green;

                //"Report' " +ddlSavedReport.SelectedItem.ToString().Split('-')[0].ToString().Trim() + "' loaded - choose view, excel, reset or edit criteria to continue.";
                lblReportSavedSucc.Visible = true;
                lblReportSavedSucc0.Visible = false;
                ddlSavedReport.DataBind();
                btnReset.Focus();

            }

            else

            {


                lblReportNameValid.Visible = true;
                lblReportNameValid.Text = "Report name already exists.";
                txtboxReportName.Focus();
                return;


            }
        }

        protected void savereport()
        {



            string criteria = "";

            //Get YMDDOS start date
            String DOSStDate = "";
            if (txtBoxDOSStDate.Text == "")
            {
                DOSStDate = null;
            }
            else
            {
                DOSStDate = txtBoxDOSStDate.Text.ToString();
                criteria = txtBoxDOSStDate.ID + " " + DOSStDate + "]";
            }

            //Get YMDDOS end date
            String DOSEndDate = "";
            if (txtBoxDOSEndDate.Text == "")
            {
                DOSEndDate = null;
            }
            else
            {
                DOSEndDate = txtBoxDOSEndDate.Text.ToString();
                criteria = criteria + " [" + "DOS End Date = " + DOSEndDate + "]";
            }

            //Get Trans start date
            String TransStDate = "";
            if (txtBoxTransStartDate.Text == "")
            {
                TransStDate = null;
            }
            else
            {
                TransStDate = txtBoxTransStartDate.Text.ToString();
                criteria = criteria + " [" + "Trans Start Date = " + TransStDate + "]";
            }

            //Get Trans end date
            String TransEndDate = "";
            if (txtBoxTransEndDate.Text == "")
            {
                TransEndDate = null;
            }
            else
            {
                TransEndDate = txtBoxTransEndDate.Text.ToString();
                criteria = criteria + " [" + "Trans End Date = " + TransEndDate + "]";
            }

            //Get Received start date
            String RcvStDate = "";
            if (txtBoxRecievedStartDate.Text == "")
            {
                RcvStDate = null;
            }
            else
            {
                RcvStDate = txtBoxRecievedStartDate.Text.ToString();
                criteria = criteria + " [" + "Recieved Start Date = " + RcvStDate + "]";
            }

            //Get Received end date
            String RcvEndDate = "";
            if (txtBoxRecievedEndDate.Text == "")
            {
                RcvEndDate = null;
            }
            else
            {
                RcvEndDate = txtBoxRecievedEndDate.Text.ToString();
                criteria = criteria + " [" + "Recieved End Date = " + RcvEndDate + "]";
            }

            //Get Paid start date
            String PaidStDate = "";
            if (txtBoxPaidStartDate.Text == "")
            {
                PaidStDate = null;
            }
            else
            {
                PaidStDate = txtBoxPaidStartDate.Text.ToString();
                criteria = criteria + " [" + "Paid Start Date = " + PaidStDate + "]";
            }

            //Get Paid end date
            String PaidEndDate = "";
            if (txtBoxPaidEndDate.Text == "")
            {
                PaidEndDate = null;
            }
            else
            {
                PaidEndDate = txtBoxPaidEndDate.Text.ToString();
                criteria = criteria + " [" + "Paid End Date = " + PaidEndDate + "]";
            }

            //Get Due start date
            String DueStDate = "";
            if (txtBoxDueStartDate.Text == "")
            {
                DueStDate = null;
            }
            else
            {
                DueStDate = txtBoxDueStartDate.Text.ToString();
                criteria = criteria + " [" + "Due Start Date = " + DueStDate + "]";
            }

            //Get Due end date
            String DueEndDate = "";
            if (txtBoxDueEndDate.Text == "")
            {
                DueEndDate = null;
            }
            else
            {
                DueEndDate = txtBoxDueEndDate.Text.ToString();
                criteria = criteria + " [" + "Due End Date = " + DueEndDate + "]";
            }

            // Get Aff list from listbox and assign to variable
            String affnbr = "";
            for (int i = 0; i < lstBoxAff.Items.Count; i++)
            {

                {
                    affnbr = affnbr + lstBoxAff.Items[i].Value + "','";
                }
            }

            if (affnbr == "")
            {
                affnbr = null;
            }
            else
            {

                affnbr = affnbr.ToString().Substring(0, affnbr.Length - 3);
                affnbr = affnbr.Replace("','", "|");
                affnbr.ToString();
                criteria = criteria + " [" + "Affliation# = " + affnbr + "]";
            }

            // Get prac list from listbox and assign to variable
            String prac = "";
            for (int i = 0; i < lstBoxPrac.Items.Count; i++)
            {

                {
                    prac = prac + lstBoxPrac.Items[i].Value + "','";
                }
            }

            if (prac == "")
            {
                prac = null;
            }
            else
            {

                prac = prac.ToString().Substring(0, prac.Length - 3);
                prac = prac.Replace("','", "|");
                prac.ToString();
                criteria = criteria + " [" + "Prac# = " + prac + "]";
            }

            //Get member num value
            String memNbr = "";
            if (txtBoxMemNbr.Text == "")
            {
                memNbr = null;
            }
            else
            {
                memNbr = txtBoxMemNbr.Text.ToString();
                criteria = criteria + " [Member# = " + memNbr + "]";
            }

            // Get Claim# list from listbox and assign to variable
            String claimno = "";
            for (int i = 0; i < lstBoxClaimNo.Items.Count; i++)
            {

                {
                    claimno = claimno + lstBoxClaimNo.Items[i].Value + "','";
                }
            }

            if (claimno == "")
            {
                claimno = null;
            }
            else
            {

                claimno = claimno.ToString().Substring(0, claimno.Length - 3);
                claimno = claimno.Replace("','", "|");
                claimno.ToString();
                criteria = criteria + " [" + "Claim# = " + claimno + "]";
            }


           
           

           

           
            // Get tinnumber list from listbox and assign to variable
            String tinnumber = "";

            for (int i = 0; i < lstBoxTin.Items.Count; i++)
            {

                {
                    tinnumber = tinnumber + lstBoxTin.Items[i].Value + "','";
                }
            }

            if (tinnumber == "")
            {
                tinnumber = null;
            }
            else
            {

                tinnumber = tinnumber.ToString().Substring(0, tinnumber.Length - 3);
                tinnumber = tinnumber.Replace("','", "|");
                tinnumber.ToString();
                criteria = criteria + "[TIN# " + ddlTinWhere.SelectedItem + " " + tinnumber + "]";
            }

            // Get NPInumber list from listbox and assign to variable
            String npinumber = "";

            for (int i = 0; i < lstBoxNPI.Items.Count; i++)
            {

                {
                    npinumber = npinumber + lstBoxNPI.Items[i].Value + "','";
                }
            }

            if (npinumber == "")
            {
                npinumber = null;
            }
            else
            {

                npinumber = npinumber.ToString().Substring(0, npinumber.Length - 3);
                npinumber = npinumber.Replace("','", "|");
                npinumber.ToString();
                criteria = criteria + "[NPI# " + ddlNpiWhere.SelectedItem + " " + npinumber + "]";
            }

            



            //Get column list from allcolumns listbox and assign to variable
            String allcolumnsv = "";
            for (int i = 0; i < lstbAllColumns.Items.Count; i++)
            {
                allcolumnsv = allcolumnsv + lstbAllColumns.Items[i].Value + "|";
            }
            allcolumnsv = allcolumnsv.ToString().TrimEnd('|');

            //Get column list from allcolumns listbox and assign to variable
            String allcolumns = "";
            for (int i = 0; i < lstbAllColumns.Items.Count; i++)
            {
                allcolumns = allcolumns + lstbAllColumns.Items[i].Text + "|";
            }
            allcolumns = allcolumns.ToString().TrimEnd('|');


            //Get column list from astdcolumns listbox and assign to variable
            String stdcolumnsv = "";
            for (int i = 0; i < lstStdColumns.Items.Count; i++)
            {
                stdcolumnsv = stdcolumnsv + lstStdColumns.Items[i].Value + "|";
            }
            stdcolumnsv = stdcolumnsv.ToString().TrimEnd('|');

            //Get column list from stdcolumns listbox and assign to variable
            String stdcolumns = "";
            for (int i = 0; i < lstStdColumns.Items.Count; i++)
            {
                stdcolumns = stdcolumns + lstStdColumns.Items[i].Text + "|";
            }
            stdcolumns = stdcolumns.ToString().TrimEnd('|');


            ////Get column list from std columns listbox and assign to variable
            //String stdcolumns = "";
            //for (int i = 0; i < lstStdColumns.Items.Count; i++)
            //{
            //    stdcolumns = stdcolumns + lstStdColumns.Items[i].Text + ",";
            //}
            //stdcolumns = stdcolumns.ToString().TrimEnd(',');

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
            connection.Open();


            SqlCommand cmd = new SqlCommand("zzz_procIntranet_ClaimsServiceSearchSortable_List_Test_saveReport", connection); // stored procedure’s name and connection


            cmd.CommandType = CommandType.StoredProcedure; //   choose command type stored procedures

            //cmd.Parameters.Add("@ColumnList", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@ColumnList"].Value = get_valueNL.ToString(); // add parameters value
            cmd.Parameters.Add("@reportname", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@reportname"].Value = txtboxReportName.Text; // add parameters value

            cmd.Parameters.Add("@reportdescription", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@reportdescription"].Value = txtboxReportDescription.Text; // add parameters value

            cmd.Parameters.Add("@Startymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Startymdend"].Value = DOSStDate; // add parameters value

            cmd.Parameters.Add("@Endymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Endymdend"].Value = DOSEndDate; // add parameters value

            cmd.Parameters.Add("@Membernumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Membernumber"].Value = memNbr; // add parameters value

            cmd.Parameters.Add("@aff", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@aff"].Value = affnbr; // add parameters value

            cmd.Parameters.Add("@StartYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDPaid"].Value = PaidStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDPaid"].Value = PaidEndDate; // add parameters value

            cmd.Parameters.Add("@StartYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDDueDate"].Value = DueStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDDueDate"].Value = DueEndDate; // add parameters value

           

            cmd.Parameters.Add("@TinNumberSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TinNumberSearch"].Value = ddlTinWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TinNumber"].Value = tinnumber; // add parameters value

            cmd.Parameters.Add("@NPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NPISearch"].Value = ddlNpiWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@NPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NPINumber"].Value = npinumber; // add parameters value

         

            cmd.Parameters.Add("@StartYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDRcvd"].Value = RcvStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDRcvd"].Value = RcvEndDate; // add parameters value

            cmd.Parameters.Add("@StartYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDTrans"].Value = TransStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDTrans"].Value = TransEndDate; // add parameters value

            

            cmd.Parameters.Add("@lstbAllColumns", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@lstbAllColumns"].Value = allcolumns.ToString();//lstbAllColumns.Items.ToString(); // add parameters value

            cmd.Parameters.Add("@lstStdColumns", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@lstStdColumns"].Value = stdcolumns.ToString(); // add parameters value

            cmd.Parameters.Add("@lstbAllColumnsV", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@lstbAllColumnsV"].Value = allcolumnsv.ToString(); // add parameters value

            cmd.Parameters.Add("@lstStdColumnsV", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@lstStdColumnsV"].Value = stdcolumnsv.ToString(); // add parameters value

            cmd.Parameters.Add("@claimno", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@claimno"].Value = claimno; // add parameters value

          

            cmd.Parameters.Add("@prac", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@prac"].Value = prac; // add parameters value

       

            cmd.ExecuteNonQuery();

            connection.Close();

            //DataTable dt = new DataTable();
            //DataRow dr;
            //dt.TableName = "SavedReport";
            ////creating columns for DataTable  
            //dt.Columns.Add(new DataColumn("WhereClause", typeof(string)));
            //dt.Columns.Add(new DataColumn("FieldName", typeof(string)));
            //dt.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            ////dt.Columns.Add(new DataColumn("Price", typeof(double)));
            //dr = dt.NewRow();
            //dt.Rows.Add(dr);

            //DataTable dt = new DataTable();
            //dt.TableName = "SavedReport";
            //dt.Clear();
            //dt.Columns.Add("WhereClause");
            //dt.Columns.Add("FieldName");
            //dt.Columns.Add("FieldValue");
            //DataRow Dosst = dt.NewRow();
            //DataRow Dosend = dt.NewRow();
            //DataRow Transst = dt.NewRow();
            //DataRow Transend = dt.NewRow();
            //DataRow Recst = dt.NewRow();
            //DataRow Recend = dt.NewRow();
            //DataRow Paidst = dt.NewRow();
            //DataRow Paidend = dt.NewRow();
            //DataRow Duest = dt.NewRow();
            //DataRow Dueend = dt.NewRow();
            //DataRow ProvNo = dt.NewRow();
            //DataRow MemNo = dt.NewRow();
            //DataRow BenNo = dt.NewRow();
            //DataRow ProgCode = dt.NewRow();


            //Dosst["WhereClause"] = "";
            //Dosst["FieldName"] = txtBoxDOSStDate.ID;
            //Dosst["FieldValue"] = txtBoxDOSStDate.Text;

            //Dosend["WhereClause"] = "";
            //Dosend["FieldName"] = txtBoxDOSEndDate.ID;
            //Dosend["FieldValue"] = txtBoxDOSEndDate.Text;

            //Transst["WhereClause"] = "";
            //Transst["FieldName"] = txtBoxTransStartDate.ID;
            //Transst["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxTransStartDate.Text) ? "" : txtBoxTransStartDate.Text;

            //Transend["WhereClause"] = "";
            //Transend["FieldName"] = txtBoxTransEndDate.ID;
            //Transend["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxTransEndDate.Text) ? "" : txtBoxTransEndDate.Text;

            //Recst["WhereClause"] = "";
            //Recst["FieldName"] = txtBoxRecievedStartDate.ID;
            //Recst["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxRecievedStartDate.Text) ? "" : txtBoxRecievedStartDate.Text;

            //Recend["WhereClause"] = "";
            //Recend["FieldName"] = txtBoxRecievedEndDate.ID;
            //Recend["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxRecievedEndDate.Text) ? "" : txtBoxRecievedEndDate.Text;

            //Paidst["WhereClause"] = "";
            //Paidst["FieldName"] = txtBoxPaidStartDate.ID;
            //Paidst["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxPaidStartDate.Text) ? "" : txtBoxPaidStartDate.Text;

            //Paidend["WhereClause"] = "";
            //Paidend["FieldName"] = txtBoxPaidEndDate.ID;
            //Paidend["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxPaidEndDate.Text) ? "" : txtBoxPaidEndDate.Text;

            //Duest["WhereClause"] = "";
            //Duest["FieldName"] = txtBoxDueStartDate.ID;
            //Duest["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxDueStartDate.Text) ? "" : txtBoxDueStartDate.Text;

            //Dueend["WhereClause"] = "";
            //Dueend["FieldName"] = txtBoxDueEndDate.ID;
            //Dueend["FieldValue"] = string.IsNullOrWhiteSpace(txtBoxDueEndDate.Text) ? "" : txtBoxDueEndDate.Text;

            //ProvNo["WhereClause"] = "";
            //ProvNo["FieldName"] = lstBoxAff.ID;
            //ProvNo["FieldValue"] = string.IsNullOrWhiteSpace(affnbr) ? "" : affnbr;

            //MemNo["WhereClause"] = "";
            //MemNo["FieldName"] = txtBoxMemNbr.ID;
            //MemNo["FieldValue"] = string.IsNullOrWhiteSpace(memNbr) ? "" : memNbr;

            //BenNo["WhereClause"] = "";
            //BenNo["FieldName"] = txtBoxBenftNbr.ID;
            //BenNo["FieldValue"] = string.IsNullOrWhiteSpace(benefit) ? "" : benefit;

            //BenNo["WhereClause"] = "";
            //BenNo["FieldName"] = lstBoxProgram.ID;
            //BenNo["FieldValue"] = string.IsNullOrWhiteSpace(program) ? "" : program;

            //dt.Rows.Add(Dosst);
            //dt.Rows.Add(Dosend);
            //dt.Rows.Add(Transst);
            //dt.Rows.Add(Transend);
            //dt.Rows.Add(Recst);
            //dt.Rows.Add(Recend);
            //dt.Rows.Add(Paidst);
            //dt.Rows.Add(Paidend);
            //dt.Rows.Add(Duest);
            //dt.Rows.Add(Dueend);
            //dt.Rows.Add(ProvNo);
            //dt.Rows.Add(MemNo);
            //dt.Rows.Add(BenNo);
            //dt.Rows.Add(ProgCode);

            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);

            //SqlBulkCopy objbulk = new SqlBulkCopy(connection);
            //connection.Open();
            ////assigning Destination table name  
            //objbulk.DestinationTableName = "[dbo].[ClaimsAdhocsavedReports]";
            ////Mapping Table column  
            //objbulk.ColumnMappings.Add("WhereClause", "WhereClause");
            //objbulk.ColumnMappings.Add("FieldName", "FieldName");
            //objbulk.ColumnMappings.Add("FieldValue", "FieldValue");
            ////objbulk.ColumnMappings.Add("Price", "Price");
            ////inserting bulk Records into DataBase   
            //objbulk.WriteToServer(dt);

        }

        protected void ddlSavedReport_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlSavedReport.SelectedValue == "0")
            {
                Response.Redirect("ClaimsAdhocReport.aspx");

            }
            else
            {
                string ddlvalue = ddlSavedReport.SelectedValue;

                //ClearFields(Form.Controls);

                DataTable dt = new DataTable();
                string query = "select * from [Intranet2012].[dbo].[ClaimsAdhocsavedReports] where SavedRptID =" + "'" + ddlvalue + "'";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand(query, connection);
                //cmd.Parameters.Add(@rn, SqlDbType.Text);
                //cmd.Parameters["@rn"].Value = ddlSavedReport.SelectedValue;

                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                da.Dispose();



                //load all items form saved report to form
                txtBoxDOSStDate.Text = dt.Rows[0]["txtBoxDOSStDate"].ToString();
                txtBoxDOSEndDate.Text = dt.Rows[0]["txtBoxDOSEndDate"].ToString();
                txtBoxTransStartDate.Text = dt.Rows[0]["txtBoxTransStartDate"].ToString();
                txtBoxTransEndDate.Text = dt.Rows[0]["txtBoxTransEndDate"].ToString();
                txtBoxRecievedStartDate.Text = dt.Rows[0]["txtBoxRecievedStartDate"].ToString();
                txtBoxRecievedEndDate.Text = dt.Rows[0]["txtBoxRecievedEndDate"].ToString();
                txtBoxPaidStartDate.Text = dt.Rows[0]["txtBoxPaidStartDate"].ToString();
                txtBoxPaidEndDate.Text = dt.Rows[0]["txtBoxPaidEndDate"].ToString();
                txtBoxDueStartDate.Text = dt.Rows[0]["txtBoxDueStartDate"].ToString();
                txtBoxDueEndDate.Text = dt.Rows[0]["txtBoxDueEndDate"].ToString();

                //provider
                lstBoxAff.Items.Clear();
                string provider = dt.Rows[0]["lstBoxAff"].ToString();
                if (string.IsNullOrWhiteSpace(provider))
                {
                    provider = null;
                }
                else
                {
                    string[] providervalues = provider.Split('|');
                    for (int i = 0; i < providervalues.Length; i++)
                    {
                        providervalues[i] = providervalues[i].Trim();
                    }

                    foreach (string item in providervalues)
                    {
                        lstBoxAff.Items.Add(item);
                    }
                }

                //prac
                lstBoxPrac.Items.Clear();
                string prac = dt.Rows[0]["lstBoxPrac"].ToString();
                if (string.IsNullOrWhiteSpace(prac))
                {
                    prac = null;
                }
                else
                {
                    string[] pracvalues = prac.Split('|');
                    for (int i = 0; i < pracvalues.Length; i++)
                    {
                        pracvalues[i] = pracvalues[i].Trim();
                    }

                    foreach (string item in pracvalues)
                    {
                        lstBoxPrac.Items.Add(item);
                    }
                }


                //member#
                txtBoxMemNbr.Text = dt.Rows[0]["txtBoxMemNbr"].ToString();

                //claim#
                lstBoxClaimNo.Items.Clear();
                string claimno = dt.Rows[0]["lstBoxClaimNo"].ToString();
                if (string.IsNullOrWhiteSpace(claimno))
                {
                    claimno = null;
                }
                else
                {
                    string[] claimnovalues = claimno.Split('|');
                    for (int i = 0; i < claimnovalues.Length; i++)
                    {
                        claimnovalues[i] = claimnovalues[i].Trim();
                    }

                    foreach (string item in claimnovalues)
                    {
                        lstBoxClaimNo.Items.Add(item);
                    }
                }


               
                //tin
                ddlTinWhere.SelectedValue = dt.Rows[0]["ddlTinWhere"].ToString();

                lstBoxTin.Items.Clear();
                string tin = dt.Rows[0]["lstBoxTin"].ToString();
                if (string.IsNullOrWhiteSpace(tin))
                {
                    tin = null;
                }

                else
                {
                    string[] tinvalues = tin.Split('|');
                    for (int i = 0; i < tinvalues.Length; i++)
                    {
                        tinvalues[i] = tinvalues[i].Trim();
                    }

                    foreach (string item in tinvalues)
                    {
                        lstBoxTin.Items.Add(item);
                    }
                }

                //npi
                ddlNpiWhere.SelectedValue = dt.Rows[0]["ddlNpiWhere"].ToString();

                lstBoxNPI.Items.Clear();
                string npi = dt.Rows[0]["lstBoxNPI"].ToString();
                if (string.IsNullOrWhiteSpace(npi))
                {
                    npi = null;
                }

                else
                {
                    string[] npivalues = npi.Split('|');
                    for (int i = 0; i < npivalues.Length; i++)
                    {
                        npivalues[i] = npivalues[i].Trim();
                    }

                    foreach (string item in npivalues)
                    {
                        lstBoxNPI.Items.Add(item);
                    }
                }

               
                //lstAllColumns Values
                lstbAllColumns.Items.Clear();

                string lstbAllColumns1 = dt.Rows[0]["lstbAllColumnsV"].ToString();
                string[] lstbAllColumnsvalues = lstbAllColumns1.Split('|');

                string lstbAllColumns12 = dt.Rows[0]["lstbAllColumns"].ToString();
                string[] lstbAllColumnsvalues2 = lstbAllColumns12.Split('|');


                for (int i = 0; i < lstbAllColumnsvalues.Length; i++)
                {
                    lstbAllColumnsvalues[i] = lstbAllColumnsvalues[i].Trim();

                }

                for (int i = 0; i < lstbAllColumnsvalues2.Length; i++)
                {
                    lstbAllColumnsvalues2[i] = lstbAllColumnsvalues2[i].Trim();

                }

                //using (var e1 = GetEnumerator())
                //using (var e2 = lstbAllColumnsvalues2.GetEnumerator())
                //{
                //    while (e1.MoveNext() && e2.MoveNext())
                //    {
                //        var item1 = e1.Current;
                //        var item2 = e2.Current;
                //        lstbAllColumns.Items.Add(new ListItem(item1, item2));
                //        // use item1 and item2
                //    }
                //}

                var zip = lstbAllColumnsvalues.Zip(lstbAllColumnsvalues2, (n, p) => new { n, p });
                //foreach (string item in lstbAllColumnsvalues)
                //{
                //    //lstbAllColumns.Items.Add(item);
                //    lstbAllColumns.Items.Add(new ListItem(item,item));


                //}

                //foreach (string item in lstbAllColumnsvalues2)
                //{
                //    //lstbAllColumns.Items.Add(item);
                //    lstbAllColumns.Items.Add(new ListItem(item));


                //}


                foreach (var z in zip)
                {
                    //lstbAllColumns.Items.Add(item);
                    lstbAllColumns.Items.Add(new ListItem(z.p, z.n));


                }


                //lstStdColumns Values
                lstStdColumns.Items.Clear();
                string lstbStdColumns1 = dt.Rows[0]["lstStdColumnsV"].ToString();
                string[] lstbStdColumnsvalues = lstbStdColumns1.Split('|');

                string lstbStdColumns12 = dt.Rows[0]["lstStdColumns"].ToString();
                string[] lstbStdColumnsvalues2 = lstbStdColumns12.Split('|');


                for (int i = 0; i < lstbStdColumnsvalues.Length; i++)
                {
                    lstbStdColumnsvalues[i] = lstbStdColumnsvalues[i].Trim();

                }

                for (int i = 0; i < lstbStdColumnsvalues2.Length; i++)
                {
                    lstbStdColumnsvalues2[i] = lstbStdColumnsvalues2[i].Trim();

                }

                //using (var e1 = GetEnumerator())
                //using (var e2 = lstbAllColumnsvalues2.GetEnumerator())
                //{
                //    while (e1.MoveNext() && e2.MoveNext())
                //    {
                //        var item1 = e1.Current;
                //        var item2 = e2.Current;
                //        lstbAllColumns.Items.Add(new ListItem(item1, item2));
                //        // use item1 and item2
                //    }
                //}

                var zip2 = lstbStdColumnsvalues.Zip(lstbStdColumnsvalues2, (n, p) => new { n, p });
                //foreach (string item in lstbAllColumnsvalues)
                //{
                //    //lstbAllColumns.Items.Add(item);
                //    lstbAllColumns.Items.Add(new ListItem(item,item));


                //}

                //foreach (string item in lstbAllColumnsvalues2)
                //{
                //    //lstbAllColumns.Items.Add(item);
                //    lstbAllColumns.Items.Add(new ListItem(item));


                //}


                foreach (var z in zip2)
                {
                    //lstbAllColumns.Items.Add(item);
                    lstStdColumns.Items.Add(new ListItem(z.p, z.n));



                }
                lblReportSavedSucc0.ForeColor = Color.Green;
                lblReportSavedSucc0.Text = "Report' " + ddlSavedReport.SelectedItem.ToString().Split('-')[0].ToString().Trim() + "' loaded - choose view, excel, reset or edit criteria to continue.";
                lblReportSavedSucc0.Visible = true;
                //btnReset.Focus();




                for (int d = 0; d < ddlSavedReport.Items.Count; d++)
                {
                    ddlSavedReport.Items[d].Attributes.Add("title", ddlSavedReport.Items[d].Text);
                }



                ////lstStdColumns
                //lstStdColumns.Items.Clear();
                //string lstStdColumns1 = dt.Rows[0]["lstStdColumns"].ToString();
                //string[] lstStdColumnsvalues = lstStdColumns1.Split(',');
                //for (int i = 0; i < lstStdColumnsvalues.Length; i++)
                //{
                //    lstStdColumnsvalues[i] = lstStdColumnsvalues[i].Trim();
                //}

                //foreach (string item in lstStdColumnsvalues)
                //{
                //    lstStdColumns.Items.Add(item);
                //}


                //txtBoxDatesMissing.Visible = false;
            }
        }

        protected void ddlSavedReport_DataBound(object sender, EventArgs e)
        {
            for (int d = 0; d < ddlSavedReport.Items.Count; d++)
            {
                ddlSavedReport.Items[d].Attributes.Add("title", ddlSavedReport.Items[d].Text);
            }
        }

        protected void btnAddClaim_Click(object sender, EventArgs e)
        {

            if (txtBoxClaimNo.Text.Trim() == "")
            {
                txtBoxClaimNo.Text = "";
            }
            else
            {
                lstBoxClaimNo.Items.Add(txtBoxClaimNo.Text.ToUpper());
                txtBoxClaimNo.Text = "";
            }
        }

        protected void lstBoxClaimNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxClaimNo.Items.Remove(lstBoxClaimNo.SelectedItem);
        }

        public static void ClearFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                string strCntName = (contl.GetType()).Name;
                switch (strCntName)
                {
                    case "TextBox":
                        TextBox tbSource = (TextBox)contl;
                        tbSource.Text = "";
                        break;
                    case "RadioButtonList":
                        RadioButtonList rblSource = (RadioButtonList)contl;
                        rblSource.SelectedIndex = -1;
                        break;
                    case "DropDownList":
                        DropDownList ddlSource = (DropDownList)contl;
                        ddlSource.SelectedIndex = -1;
                        break;
                    case "ListBox":
                        ListBox lbsource = (ListBox)contl;
                        lbsource.SelectedIndex = -1;
                        break;
                }
                ClearFields(contl.Controls);
            }
        }

        protected void btnAllToStd_Click(object sender, EventArgs e)
        {
            MoveItems(true);// true since we add
        }

        private void MoveItems(bool isAdd)
        {
            if (isAdd)// means if you add items to the right box
            {
                for (int i = lstbAllColumns.Items.Count - 1; i >= 0; i--)
                {
                    if (lstbAllColumns.Items[i].Selected)
                    {
                        lstStdColumns.Items.Add(lstbAllColumns.Items[i]);
                        lstStdColumns.ClearSelection();
                        lstbAllColumns.Items.Remove(lstbAllColumns.Items[i]);
                    }
                }
            }
            else // means if you remove items from the right box and add it back to the left box
            {
                for (int i = lstStdColumns.Items.Count - 1; i >= 0; i--)
                {
                    if (lstStdColumns.Items[i].Selected)
                    {
                        lstbAllColumns.Items.Add(lstStdColumns.Items[i]);
                        lstbAllColumns.ClearSelection();
                        lstStdColumns.Items.Remove(lstStdColumns.Items[i]);
                    }
                }
            }
        }

        protected void btnStdToAll_Click(object sender, EventArgs e)
        {
            MoveItems(false); // false since we remove
        }

        protected void btnAddPrac_Click(object sender, EventArgs e)
        {
            if (txtBoxPrac.Text.Trim() == "")
            {
                txtBoxPrac.Text = "";
            }
            else
            {
                lstBoxPrac.Items.Add(txtBoxPrac.Text.ToUpper());
                txtBoxPrac.Text = "";
            }
        }

        protected void lstBoxPrac_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxPrac.Items.Remove(lstBoxPrac.SelectedItem);
        }

        protected void lnkNewSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimsAdhocReport.aspx");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            DataTable dt = grvData.DataSource as DataTable;


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