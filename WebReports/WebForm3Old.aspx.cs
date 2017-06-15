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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add attributes to relevant text boxes to only allow typing in numerics
            txtBoxAmtCharge.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxAmtPaid.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxClaimStatus.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxNPI.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxTIN.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxTPI.Attributes.Add("onkeydown", "return NumericTextBox(event)");

            //string display = txtAllDates.Text;
            //////    ////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //  Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {

            txtBoxMemNbr.Focus();

            if (txtBoxDOSStDate.Text == "" && txtBoxDueStartDate.Text == "" && txtBoxPaidStartDate.Text == "" && txtBoxRecievedStartDate.Text == "" && txtBoxTransStartDate.Text == "")
            {

                txtBoxDatesMissing.Focus();
                //lblDatesMissing.Text = "HELLOOOOOOOOOOOO";
                txtBoxDatesMissing.Visible = true;
                return;
            }

            else
            {
                txtBoxDatesMissing.Visible = false;
            }

            if(lstStdColumns.Items.Count == 0)
            {
                lblFieldReg.Visible = true;//Items not added in ListBox 
                lstStdColumns.Focus();
            }

            else

            {
                txtBoxDatesMissing.Visible = false;

                System.Threading.Thread.Sleep(2000);
                DataTable dt = Getdata();


                Session.Add("dt", dt);
                Session.Add("Type", "Screen");


                Response.Redirect("WebForm3Results.aspx", false);
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
                criteria =  criteria + " [" + "DOS End Date = " + DOSEndDate + "]";
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
                criteria = criteria + " [" + "Affliation# = " + affnbr.Replace("','","|") + "]";
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

            //Get benefit
            String benefit = "";
            if (txtBoxBenftNbr.Text == "")
            {
                benefit = null;
            }
            else
            {
                benefit = txtBoxBenftNbr.Text.ToString();
                criteria = criteria + " [Benefit# = " + benefit + "]";
            }

            // Get program list from listbox and assign to variable
            String program = "";
            for (int i = 0; i < lstBoxProgram.Items.Count; i++)
            {
                if (lstBoxProgram.Items[i].Selected)
                {
                    program = program + lstBoxProgram.Items[i].Value + "','";
                    
                }
            }

            if (program == "")
            {
                program = null;
            }
            else
            {

                program = program.ToString().Substring(0, program.Length - 3);
                program.ToString();
                criteria = criteria + "[Program = " + program.Replace("','", "|") + "]";

            }

            // Get location list from listbox and assign to variable
            String location = "";
            for (int i = 0; i < lstBoxLocCode.Items.Count; i++)
            {
                if (lstBoxLocCode.Items[i].Selected)
                {
                    location = location + lstBoxLocCode.Items[i].Value + "','";
                }
            }

            if (location == "")
            {
                location = null;
            }
            else
            {

                location = location.ToString().Substring(0, location.Length - 3);
                location.ToString();
                criteria = criteria + "[Location = " + location.Replace("','", "|") + "]";
            }

            // Get division list from listbox and assign to variable
            String division = "";
            for (int i = 0; i < lstBoxDivision.Items.Count; i++)
            {
                if (lstBoxDivision.Items[i].Selected)
                {
                    division = division + lstBoxDivision.Items[i].Value + "','";
                }
            }

            if (division == "")
            {
                division = null;
            }
            else
            {

                division = division.ToString().Substring(0, division.Length - 3);
                division.ToString();
                criteria = criteria + "[Division# " + ddlDivisionWhere.SelectedItem + " "+ division.Replace("','", "|") + "]";
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

            // Get TPInumber list from listbox and assign to variable
            String tpinumber = "";

            for (int i = 0; i < lstBoxTPI.Items.Count; i++)
            {

                {
                    tpinumber = tpinumber + lstBoxTPI.Items[i].Value + "','";
                }
            }

            if (tpinumber == "")
            {
                tpinumber = null;
            }
            else
            {

                tpinumber = tpinumber.ToString().Substring(0, tpinumber.Length - 3);
                tpinumber.ToString();
                criteria = criteria + "[TPI# " + ddlTPIWhere.SelectedItem + " " + tpinumber.Replace("','", "|") + "]";
            }
         

            // Get status list from listbox and assign to variable
            String status = "";

            for (int i = 0; i < lstBoxClaimStatus.Items.Count; i++)
            {

                {
                    status = status + lstBoxClaimStatus.Items[i].Value + "','";
                }
            }

            if (status == "")
            {
                status = null;
            }
            else
            {

                status = status.ToString().Substring(0, status.Length - 3);
                status.ToString();
                criteria = criteria + "[Claim Status " + ddlClaimStatusWhere.SelectedItem + " " + status.Replace("','", "|") + "]";
            }

            // Get Modifier list from listbox and assign to variable
            String get_valueMOD = "";
            for (int i = 0; i < lstBoxModifier.Items.Count; i++)
            {

                {
                    get_valueMOD = get_valueMOD + lstBoxModifier.Items[i].Value + "','";
                }
            }

            if (get_valueMOD == "")
            {
                get_valueMOD = null;
            }
            else
            {
                get_valueMOD = get_valueMOD.ToString().Substring(0, get_valueMOD.Length - 3);
                get_valueMOD.ToString();
                criteria = criteria + "[Modifier " + ddlModifierWhere.SelectedItem + " " + get_valueMOD.Replace("','", "|") + "]";
            }

            // Get spec list from listbox and assign to variable
            String spec = "";

            for (int i = 0; i < lstBoxProviderSpec.Items.Count; i++)
            {

                {
                    spec = spec + lstBoxProviderSpec.Items[i].Value + "','";
                }
            }

            if (spec == "")
            {
                spec = null;
            }
            else
            {

                spec = spec.ToString().Substring(0, spec.Length - 3);
                spec.ToString();
                criteria = criteria + "[Provider Speciality " + ddlProviderSpecWhere.SelectedItem + " " + spec.Replace("','", "|") + "]";
            }



            // Get prov status list from listbox and assign to variable
            String provstatus = "";

            for (int i = 0; i < lstBoxProviderStatus.Items.Count; i++)
            {

                {
                    provstatus = provstatus + lstBoxProviderStatus.Items[i].Value + "','";
                }
            }

            if (provstatus == "")
            {
                provstatus = null;
            }
            else
            {

                provstatus = provstatus.ToString().Substring(0, provstatus.Length - 3);
                provstatus.ToString();
                criteria = criteria + "[Provider Status " + ddlProviderStatusWhere.SelectedItem + " " + provstatus.Replace("','", "|") + "]";
            }


            // Get Ex in list from listbox and assign to variable
            String get_valueEX = "";

            for (int i = 0; i < lstboxEx.Items.Count; i++)
            {

                {
                    get_valueEX = get_valueEX + lstboxEx.Items[i].Value + "','";
                }
            }

            if (get_valueEX == "")
            {
                get_valueEX = null;
            }
            else
            {

                get_valueEX = get_valueEX.ToString().Substring(0, get_valueEX.Length - 3);
                get_valueEX.ToString();
                criteria = criteria + "[Ex Code " + ddlExCodeInWhere.SelectedItem + " " + get_valueEX.Replace("','", "|") + "]";
            }

            // Get Ex Not in in list from listbox and assign to variable
            String get_valueEXNotIn = "";

            for (int i = 0; i < lstboxExNi.Items.Count; i++)
            {

                {
                    get_valueEXNotIn = get_valueEXNotIn + lstboxExNi.Items[i].Value + "','";
                }
            }

            if (get_valueEXNotIn == "")
            {
                get_valueEXNotIn = null;
            }
            else
            {

                get_valueEXNotIn = get_valueEXNotIn.ToString().Substring(0, get_valueEXNotIn.Length - 3);
                get_valueEXNotIn.ToString();
                criteria = criteria + "[Ex Code " + ddlExCodeNInWhere.SelectedItem + " " + get_valueEXNotIn.Replace("','", "|") + "]";
            }

            // Get diagnosis list from listbox and assign to variable
            String diagnosis = "";

            for (int i = 0; i < lstBoxDiagnosis.Items.Count; i++)
            {

                {
                    diagnosis = diagnosis + lstBoxDiagnosis.Items[i].Value + "','";
                }
            }

            if (diagnosis == "")
            {
                diagnosis = null;
            }
            else
            {

                diagnosis = diagnosis.ToString().Substring(0, diagnosis.Length - 3);
                diagnosis.ToString();
                criteria = criteria + "[Diagnosis " + ddlDiagnosisWhere.SelectedItem + " " + diagnosis.Replace("','", "|") + "]";
            }

            // Get Procedure list from listbox and assign to variable
            String procedureCode = "";

            for (int i = 0; i < lstBoxCPT.Items.Count; i++)
            {

                {
                    procedureCode = procedureCode + lstBoxCPT.Items[i].Value + "','";
                }
            }

            if (procedureCode == "")
            {
                procedureCode = null;
            }
            else
            {

                procedureCode = procedureCode.ToString().Substring(0, procedureCode.Length - 3);
                procedureCode.ToString();
                criteria = criteria + "[CPT " + ddlCPTWhere.SelectedItem + " " + procedureCode.Replace("','", "|") + "]";
            }


            // Get revenue list from listbox and assign to variable
            String revenue = "";

            for (int i = 0; i < lstBoxRevCode.Items.Count; i++)
            {

                {
                    revenue = revenue + lstBoxRevCode.Items[i].Value + "','";
                }
            }

            if (revenue == "")
            {
                revenue = null;
            }
            else
            {

                revenue = revenue.ToString().Substring(0, revenue.Length - 3);
                revenue.ToString();
                criteria = criteria + "[Revenue Code " + ddlRevCodeWhere.SelectedItem + " " + revenue.Replace("','", "|") + "]";
            }

            // Get DRG list from listbox and assign to variable
            String drg = "";

            for (int i = 0; i < lstBoxDRG.Items.Count; i++)
            {

                {
                    drg = drg + lstBoxDRG.Items[i].Value + "','";
                }
            }

            if (drg == "")
            {
                drg = null;
            }
            else
            {

                drg = drg.ToString().Substring(0, drg.Length - 3);
                drg.ToString();
                criteria = criteria + "[DRG " + ddlDRGWhere.SelectedItem + " " + drg.Replace("','", "|") + "]";
            }

            //Get ClaimType 
            String claimtype = "";
            if (ddlClaimType.SelectedValue == "")
            {
                claimtype = null;
            }
            else
            {
                claimtype = ddlClaimType.SelectedValue.ToString();
                criteria = criteria + "[Claim Type " + ddlClaimTypeWhere.SelectedItem + " " + claimtype.Replace("','", "|") + "]";
            }

            // Get treatment list from listbox and assign to variable
            String treatment = "";

            for (int i = 0; i < lstBoxTreatmentType.Items.Count; i++)
            {

                {
                    treatment = treatment + lstBoxTreatmentType.Items[i].Value + "','";
                }
            }

            if (treatment == "")
            {
                treatment = null;
            }
            else
            {

                treatment = treatment.ToString().Substring(0, treatment.Length - 3);
                treatment.ToString();
                criteria = criteria + "[Treatment Type " + ddlTreatmentTypeWhere.SelectedItem + " " + treatment.Replace("','", "|") + "]";
            }

            // Get benefit package list from listbox and assign to variable
            String benefitpackage = "";

            for (int i = 0; i < lstBoxBenefitPackage.Items.Count; i++)
            {

                {
                    benefitpackage = benefitpackage + lstBoxBenefitPackage.Items[i].Value + "','";
                }
            }

            if (benefitpackage == "")
            {
                benefitpackage = null;
            }
            else
            {

                benefitpackage = benefitpackage.ToString().Substring(0, benefitpackage.Length - 3);
                benefitpackage.ToString();
                criteria = criteria + "[Benefit Package " + ddlBenefitPackageWhere.SelectedItem + " " + benefitpackage.Replace("','", "|") + "]";
            }

            // Get hatcode package list from listbox and assign to variable
            String hatcode = "";

            for (int i = 0; i < lstBoxHatCode.Items.Count; i++)
            {

                {
                    hatcode = hatcode + lstBoxHatCode.Items[i].Value + "','";
                }
            }

            if (hatcode == "")
            {
                hatcode = null;
            }
            else
            {

                hatcode = hatcode.ToString().Substring(0, hatcode.Length - 3);
                hatcode.ToString();
                criteria = criteria + "[Hat Code " + ddlHatCodeWhere.SelectedItem + " " + hatcode.Replace("','", "|") + "]";
            }

            // Get region package list from listbox and assign to variable
            String region = "";

            for (int i = 0; i < lstBoxRegion.Items.Count; i++)
            {

                {
                    region = region + lstBoxRegion.Items[i].Value + "','";
                }
            }

            if (region == "")
            {
                region = null;
            }
            else
            {

                region = region.ToString().Substring(0, region.Length - 3);
                region.ToString();
                criteria = criteria + "[Region " + ddlRegionWhere.SelectedItem + " " + region.Replace("','", "|") + "]";
            }

            // Get illness package list from listbox and assign to variable
            String illness = "";

            for (int i = 0; i < lstBoxCIIllness.Items.Count; i++)
            {

                {
                    illness = illness + lstBoxCIIllness.Items[i].Value + "','";
                }
            }

            if (illness == "")
            {
                illness = null;
            }
            else
            {

                illness = illness.ToString().Substring(0, illness.Length - 3);
                illness.ToString();
                criteria = criteria + "[CI(Illness) " + ddlCIIllnessWhere.SelectedItem + " " + illness.Replace("','", "|") + "]";
            }

            // Get payservice package list from listbox and assign to variable
            String payservice = "";

            for (int i = 0; i < lstBoxPayServCode.Items.Count; i++)
            {

                {
                    payservice = payservice + lstBoxPayServCode.Items[i].Value + "','";
                }
            }

            if (payservice == "")
            {
                payservice = null;
            }
            else
            {

                payservice = payservice.ToString().Substring(0, payservice.Length - 3);
                payservice.ToString();
                criteria = criteria + "[Pay Serv Code " + ddlPayServCodeWhere.SelectedItem + " " + payservice.Replace("','", "|") + "]";
            }

            // Get payclass package list from listbox and assign to variable
            String payclass = "";

            for (int i = 0; i < lstBoxPayClass.Items.Count; i++)
            {

                {
                    payclass = payclass + lstBoxPayClass.Items[i].Value + "','";
                }
            }

            if (payclass == "")
            {
                payclass = null;
            }
            else
            {

                payclass = payclass.ToString().Substring(0, payclass.Length - 3);
                payclass.ToString();
                criteria = criteria + "[Pay Class " + ddlPayClassWhere.SelectedItem + " " + payclass.Replace("','", "|") + "]";
            }

            // Get exception package list from listbox and assign to variable
            String exception = "";

            for (int i = 0; i < lstBoxExceptionFee.Items.Count; i++)
            {

                {
                    exception = exception + lstBoxExceptionFee.Items[i].Value + "','";
                }
            }

            if (exception == "")
            {
                exception = null;
            }
            else
            {

                exception = exception.ToString().Substring(0, exception.Length - 3);
                exception.ToString();
                criteria = criteria + "[Exception Fee " + ddlExceptionFeeWhere.SelectedItem + " " + exception.Replace("','", "|") + "]";
            }

            // Get op package list from listbox and assign to variable
            String op = "";

            for (int i = 0; i < lstBoxOP.Items.Count; i++)
            {

                {
                    op = op + lstBoxOP.Items[i].Value + "','";
                }
            }

            if (op == "")
            {
                op = null;
            }
            else
            {

                op = op.ToString().Substring(0, op.Length - 3);
                op.ToString();
                criteria = criteria + "[Op " + ddlOPWhere.SelectedItem + " " + op.Replace("','", "|") + "]";
            }



            // Get fee schedule package list from listbox and assign to variable
            String feeschedule = "";

            for (int i = 0; i < lstBoxFeeSchedule.Items.Count; i++)
            {

                {
                    feeschedule = feeschedule + lstBoxFeeSchedule.Items[i].Value + "','";
                }
            }

            if (feeschedule == "")
            {
                feeschedule = null;
            }
            else
            {

                feeschedule = feeschedule.ToString().Substring(0, feeschedule.Length - 3);
                feeschedule.ToString();
                criteria = criteria + "[Fee Schedule " + ddlFeeScheduleWhere.SelectedItem + " " + feeschedule.Replace("','", "|") + "]";
            }

            // Get Discharge list from listbox and assign to variable
            String discharge = "";

            for (int i = 0; i < lstBoxDischarge.Items.Count; i++)
            {

                {
                    discharge = discharge + lstBoxDischarge.Items[i].Value + "','";
                }
            }

            if (discharge == "")
            {
                discharge = null;
            }
            else
            {

                discharge = discharge.ToString().Substring(0, discharge.Length - 3);
                discharge.ToString();
                criteria = criteria + "[Discharge " + ddlDischargeWhere.SelectedItem + " " + discharge.Replace("','", "|") + "]";
            }

            //Get Amount Charge 
            String amtcharge = "";
            if (txtBoxAmtCharge.Text == "")
            {
                amtcharge = null;
            }
            else
            {
                amtcharge = txtBoxAmtCharge.Text.ToString();
                criteria = criteria + "[Amount Charge " + ddlAmtChargeWhere.SelectedItem + " " + amtcharge.Replace("','", "|") + "]";
            }

            //Get Paid Charge 
            String amtpaid = "";
            if (txtBoxAmtPaid.Text == "")
            {
                amtpaid = null;
            }
            else
            {
                amtpaid = txtBoxAmtPaid.Text.ToString();
                criteria = criteria + "[Amount Paid " + ddlAmtPaidWhere.SelectedItem + " " + amtpaid.Replace("','", "|") + "]";
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

            cmd.Parameters.Add("@Modifier", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Modifier"].Value = get_valueMOD; // add parameters value

            cmd.Parameters.Add("@ModifierSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ModifierSearch"].Value = ddlModifierWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Ex", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EX"].Value = get_valueEX; // add parameters value

            cmd.Parameters.Add("@NotEx", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NotEx"].Value = get_valueEXNotIn; // add parameters value

            cmd.Parameters.Add("@StartYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDPaid"].Value = PaidStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDPaid"].Value = PaidEndDate; // add parameters value

            cmd.Parameters.Add("@StartYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDDueDate"].Value = DueStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDDueDate"].Value = DueEndDate; // add parameters value

            cmd.Parameters.Add("@Location", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Location"].Value = location; // add parameters value

            cmd.Parameters.Add("@program", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@program"].Value = program; // add parameters value

            cmd.Parameters.Add("@TinNumberSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TinNumberSearch"].Value = ddlTinWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TinNumber"].Value = tinnumber; // add parameters value

            cmd.Parameters.Add("@NPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NPISearch"].Value = ddlNpiWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@NPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@NPINumber"].Value = npinumber; // add parameters value

            cmd.Parameters.Add("@TPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TPISearch"].Value = ddlTPIWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@TPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TPINumber"].Value = tpinumber; // add parameters value

            cmd.Parameters.Add("@StartYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDRcvd"].Value = DueStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDRcvd"].Value = DueEndDate; // add parameters value

            cmd.Parameters.Add("@StartYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StartYMDTrans"].Value = TransStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDTrans"].Value = TransEndDate; // add parameters value

            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Status"].Value = status; // add parameters value

            cmd.Parameters.Add("@StatusSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@StatusSearch"].Value = ddlClaimStatusWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@proc", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@proc"].Value = procedureCode; // add parameters value

            cmd.Parameters.Add("@procSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@procSearch"].Value = ddlCPTWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Diag", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Diag"].Value = diagnosis; // add parameters value

            cmd.Parameters.Add("@DiagSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@DiagSearch"].Value = ddlDiagnosisWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Revenue", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Revenue"].Value = revenue; // add parameters value

            cmd.Parameters.Add("@RevenueSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@RevenueSearch"].Value = ddlRevCodeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@DRG", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@DRG"].Value = drg; // add parameters value

            cmd.Parameters.Add("@DRGSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@DRGSearch"].Value = ddlDRGWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Treatment", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Treatment"].Value = treatment; // add parameters value

            cmd.Parameters.Add("@TreatmentSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@TreatmentSearch"].Value = ddlTreatmentTypeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Spec1", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Spec1"].Value = spec; // add parameters value

            cmd.Parameters.Add("@Spec1Search", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Spec1Search"].Value = ddlProviderSpecWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@ProviderStatus", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ProviderStatus"].Value = provstatus; // add parameters value

            cmd.Parameters.Add("@ProviderStatusSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ProviderStatusSearch"].Value = ddlProviderStatusWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@ClaimType", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ClaimType"].Value = claimtype; // add parameters value

            cmd.Parameters.Add("@Benefit", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Benefit"].Value = benefit; // add parameters value

            cmd.Parameters.Add("@Benefitpackage", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Benefitpackage"].Value = benefitpackage; // add parameters value

            cmd.Parameters.Add("@BenefitpackageSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@BenefitpackageSearch"].Value = ddlBenefitPackageWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@PayServiceCode", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@PayServiceCode"].Value = payservice; // add parameters value

            cmd.Parameters.Add("@PayServiceCodeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@PayServiceCodeSearch"].Value = ddlPayServCodeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@HatCode", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@HatCode"].Value = hatcode; // add parameters value

            cmd.Parameters.Add("@HatCodeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@HatCodeSearch"].Value = ddlHatCodeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@PayClass", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@PayClass"].Value = payclass; // add parameters value

            cmd.Parameters.Add("@PayClassSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@PayClassSearch"].Value = ddlPayClassWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Exception", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Exception"].Value = exception; // add parameters value

            cmd.Parameters.Add("@ExceptionSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ExceptionSearch"].Value = ddlExceptionFeeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Region", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Region"].Value = region; // add parameters value

            cmd.Parameters.Add("@RegionSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@RegionSearch"].Value = ddlRegionWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Op", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Op"].Value = op; // add parameters value

            cmd.Parameters.Add("@OpSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@OpSearch"].Value = ddlOPWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@FeeSchedule", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@FeeSchedule"].Value = feeschedule; // add parameters value

            cmd.Parameters.Add("@FeeScheduleSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@FeeScheduleSearch"].Value = ddlFeeScheduleWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Division", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Division"].Value = division; // add parameters value

            cmd.Parameters.Add("@DivisionSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@DivisionSearch"].Value = ddlDivisionWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Illness", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Illness"].Value = illness; // add parameters value

            cmd.Parameters.Add("@IllnessSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@IllnessSearch"].Value = ddlCIIllnessWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@AmtCharge", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@AmtCharge"].Value = amtcharge; // add parameters value

            cmd.Parameters.Add("@AmtChargeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@AmtChargeSearch"].Value = ddlAmtChargeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@AmtPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@AmtPaid"].Value = amtpaid; // add parameters value

            cmd.Parameters.Add("@AmtPaidSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@AmtPaidSearch"].Value = ddlAmtPaidWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@Discharge", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Discharge"].Value = discharge; // add parameters value

            cmd.Parameters.Add("@DischargeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@DischargeSearch"].Value = ddlDischargeWhere.SelectedValue; // add parameters value

            cmd.Parameters.Add("@claimno", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@claimno"].Value = claimno; // add parameters value

            SqlDataAdapter dp = new SqlDataAdapter(cmd);

            //System.Threading.Thread.Sleep(5000);
            dp.Fill(dt); // fill results to datatable
            connection.Close();

            txtCriteria.Text = criteria;

            Session.Add("CriteriaInfo", criteria);
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

        protected void btnAddEx_Click(object sender, EventArgs e)
        {

             if (txtBoxExCode.Text.Trim() == "")
            {
                txtBoxExCode.Text = "";
            }
            else
            {
                lstboxEx.Items.Add(txtBoxExCode.Text.ToUpper());
                txtBoxExCode.Text = "";
            }
         
        }

        protected void lstboxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstboxEx.Items.Remove(lstboxEx.SelectedItem);
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

        protected void btnAddTPI_Click(object sender, EventArgs e)
        {
            if (txtBoxTPI.Text.Trim() == "")
            {
                txtBoxTPI.Text = "";
            }
            else
            {
                lstBoxTPI.Items.Add(txtBoxTPI.Text.ToUpper());
                txtBoxTPI.Text = "";
            }
        }

        protected void btnAddClaimStatus_Click(object sender, EventArgs e)
        {
            if (txtBoxClaimStatus.Text.Trim() == "")
            {
                txtBoxClaimStatus.Text = "";
            }
            else
            {
                lstBoxClaimStatus.Items.Add(txtBoxClaimStatus.Text.ToUpper());
                txtBoxClaimStatus.Text = "";
            }

        }

        protected void btnAddModifier_Click(object sender, EventArgs e)
        {
            if (txtBoxModifier.Text.Trim() == "")
            {
                txtBoxClaimStatus.Text = "";
            }
            else
            {
                lstBoxModifier.Items.Add(txtBoxModifier.Text.ToUpper());
                txtBoxModifier.Text = "";
            }
        }

        protected void btnAddProviderSpec_Click(object sender, EventArgs e)
        {
            if (txtBoxProviderSpec.Text.Trim() == "")
            {
                txtBoxProviderSpec.Text = "";
            }
            else
            {
                lstBoxProviderSpec.Items.Add(txtBoxProviderSpec.Text.ToUpper());
                txtBoxProviderSpec.Text = "";
            }
        }

        protected void btnAddProviderStatus_Click(object sender, EventArgs e)
        {
            if (txtBoxProviderStatus.Text.Trim() == "")
            {
                txtBoxProviderSpec.Text = "";
            }
            else
            {
                lstBoxProviderStatus.Items.Add(txtBoxProviderStatus.Text.ToUpper());
                txtBoxProviderStatus.Text = "";
            }
        }

        protected void btnAddExNi_Click(object sender, EventArgs e)
        {
                                    

            if (txtBoxExNiCode.Text.Trim() == "")
            {
              
                txtBoxExNiCode.Text = "";
            }
            else
            {
                lstboxExNi.Items.Add(txtBoxExNiCode.Text.ToUpper());
                txtBoxExNiCode.Text = "";
            }
        }

        protected void btnAddDiagnosis_Click(object sender, EventArgs e)
        {

            if (txtBoxDiagnosis.Text.Trim() == "")
            {
                txtBoxDiagnosis.Text = "";
            }
            else
            {
                lstBoxDiagnosis.Items.Add(txtBoxDiagnosis.Text.ToUpper());
                txtBoxDiagnosis.Text = "";
            }
        }

        protected void btnAddCPT_Click(object sender, EventArgs e)
        {
            if (txtBoxCPT.Text.Trim() == "")
            {
                txtBoxCPT.Text = "";
            }
            else
            {
                lstBoxCPT.Items.Add(txtBoxCPT.Text.ToUpper());
                txtBoxCPT.Text = "";
            }
        }

        protected void btnAddRevCode_Click(object sender, EventArgs e)
        {
            if (txtBoxRevCode.Text.Trim() == "")
            {
                txtBoxRevCode.Text = "";
            }
            else
            {
                lstBoxRevCode.Items.Add(txtBoxRevCode.Text.ToUpper());
                txtBoxRevCode.Text = "";
            }
        }

        protected void btnAddDRG_Click(object sender, EventArgs e)
        {
            if (txtBoxDRG.Text.Trim() == "")
            {
                txtBoxDRG.Text = "";
            }
            else
            {
                lstBoxDRG.Items.Add(txtBoxDRG.Text.ToUpper());
                txtBoxDRG.Text = "";
            }
        }



        protected void btnAddTreatmentType_Click(object sender, EventArgs e)
        {
            if (txtBoxTreatmentType.Text.Trim() == "")
            {
                txtBoxTreatmentType.Text = "";
            }
            else
            {
                lstBoxTreatmentType.Items.Add(txtBoxTreatmentType.Text.ToUpper());
                txtBoxTreatmentType.Text = "";
            }

        }

        protected void btnAddBenefitPackage_Click(object sender, EventArgs e)
        {
            if (txtBoxBenefitPackage.Text.Trim() == "")
            {
                txtBoxBenefitPackage.Text = "";
            }
            else
            {
                lstBoxBenefitPackage.Items.Add(txtBoxBenefitPackage.Text.ToUpper());
                txtBoxBenefitPackage.Text = "";
            }

        }

        protected void btnAddHatCode_Click(object sender, EventArgs e)
        {
            if (txtBoxHatCode.Text.Trim() == "")
            {
                txtBoxHatCode.Text = "";
            }
            else
            {
                lstBoxHatCode.Items.Add(txtBoxHatCode.Text.ToUpper());
                txtBoxHatCode.Text = "";
            }
        }

        protected void btnAddRegion_Click(object sender, EventArgs e)
        {
            if (txtBoxRegion.Text.Trim() == "")
            {
                txtBoxRegion.Text = "";
            }
            else
            {
                lstBoxRegion.Items.Add(txtBoxRegion.Text.ToUpper());
                txtBoxRegion.Text = "";
            }
        }

        protected void btnAddCIIllness_Click(object sender, EventArgs e)
        {
            if (txtBoxCIIllness.Text.Trim() == "")
            {
                txtBoxCIIllness.Text = "";
            }
            else
            {
                lstBoxCIIllness.Items.Add(txtBoxCIIllness.Text.ToUpper());
                txtBoxCIIllness.Text = "";
            }
        }

        protected void btnPayServCode_Click(object sender, EventArgs e)
        {
            if (txtBoxPayServCode.Text.Trim() == "")
            {
                txtBoxPayServCode.Text = "";
            }
            else
            {
                lstBoxPayServCode.Items.Add(txtBoxPayServCode.Text.ToUpper());
                txtBoxPayServCode.Text = "";
            }

        }

        protected void btnAddPayClass_Click(object sender, EventArgs e)
        {
            if (txtBoxPayClass.Text.Trim() == "")
            {
                txtBoxPayClass.Text = "";
            }
            else
            {
                lstBoxPayClass.Items.Add(txtBoxPayClass.Text.ToUpper());
                txtBoxPayClass.Text = "";
            }
        }

        protected void btnAddExceptionFee_Click(object sender, EventArgs e)
        {
            if (txtBoxExceptionFee.Text.Trim() == "")
            {
                txtBoxExceptionFee.Text = "";
            }
            else
            {
                lstBoxExceptionFee.Items.Add(txtBoxExceptionFee.Text.ToUpper());
                txtBoxExceptionFee.Text = "";
            }
        }

        protected void btnAddOP_Click(object sender, EventArgs e)
        {
            if (txtBoxOP.Text.Trim() == "")
            {
                txtBoxOP.Text = "";
            }
            else
            {
                lstBoxOP.Items.Add(txtBoxOP.Text.ToUpper());
                txtBoxOP.Text = "";
            }
        }

        protected void btnAddFeeSchedule_Click(object sender, EventArgs e)
        {
            if (txtBoxFeeSchedule.Text.Trim() == "")
            {
                txtBoxFeeSchedule.Text = "";
            }
            else
            {
                lstBoxFeeSchedule.Items.Add(txtBoxFeeSchedule.Text.ToUpper());
                txtBoxFeeSchedule.Text = "";
            }
        }

        protected void lstBoxProviderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxProviderStatus.Items.Remove(lstBoxProviderStatus.SelectedItem);

        }

        protected void lstBoxTPI_SelectedIndexChanged(object sender, EventArgs e)
        {

            lstBoxTPI.Items.Remove(lstBoxTPI.SelectedItem);

        }

        protected void lstBoxTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxTin.Items.Remove(lstBoxTin.SelectedItem);
        }

        protected void lstBoxNPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxNPI.Items.Remove(lstBoxNPI.SelectedItem);
        }

        protected void lstBoxClaimStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxClaimStatus.Items.Remove(lstBoxClaimStatus.SelectedItem);
        }

        protected void lstBoxModifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxModifier.Items.Remove(lstBoxModifier.SelectedItem);
        }

        protected void lstBoxProviderSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxProviderSpec.Items.Remove(lstBoxProviderSpec.SelectedItem);
        }

        protected void lstboxExNi_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstboxExNi.Items.Remove(lstboxExNi.SelectedItem);
        }

        protected void lstBoxDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxDiagnosis.Items.Remove(lstBoxDiagnosis.SelectedItem);
        }

        protected void lstBoxCPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxCPT.Items.Remove(lstBoxCPT.SelectedItem);
        }

        protected void lstBoxRevCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxRevCode.Items.Remove(lstBoxRevCode.SelectedItem);
        }

        protected void lstBoxDRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxDRG.Items.Remove(lstBoxDRG.SelectedItem);
        }


        protected void lstBoxTreatmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxTreatmentType.Items.Remove(lstBoxTreatmentType.SelectedItem);
        }

        protected void lstBoxBenefitPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxBenefitPackage.Items.Remove(lstBoxBenefitPackage.SelectedItem);
        }

        protected void lstBoxHatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxHatCode.Items.Remove(lstBoxHatCode.SelectedItem);
        }

        protected void lstBoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxRegion.Items.Remove(lstBoxRegion.SelectedItem);
        }

        protected void lstBoxCIIllness_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxCIIllness.Items.Remove(lstBoxCIIllness.SelectedItem);
        }

        protected void lstBoxPayServCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxPayServCode.Items.Remove(lstBoxPayServCode.SelectedItem);
        }

        protected void lstBoxPayClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxPayClass.Items.Remove(lstBoxPayClass.SelectedItem);
        }

        protected void lstBoxExceptionFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxExceptionFee.Items.Remove(lstBoxExceptionFee.SelectedItem);
        }

        protected void lstBoxOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxOP.Items.Remove(lstBoxOP.SelectedItem);
        }

        protected void lstBoxFeeSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxFeeSchedule.Items.Remove(lstBoxFeeSchedule.SelectedItem);
        }

        protected void lstBoxAff_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxAff.Items.Remove(lstBoxAff.SelectedItem);
        }

      

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm3.aspx");
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            txtBoxMemNbr.Focus();

            if (txtBoxDOSStDate.Text == "" && txtBoxDueStartDate.Text == "")
            {

                //lblDatesMissing.Text = "HELLOOOOOOOOOOOO";
                txtBoxDatesMissing.Visible = true;
                //txtBoxAff.Focus();
                //btnExportExcel.Attributes.Add("onclick", "javascript:scroll(0,0);return false;");
                txtBoxDatesMissing.Focus();
            //string display = "No records found";
                //////ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language='javascript'>alert('" + display + "')</script>");
                return;

            }

            if (lstStdColumns.Items.Count == 0)
            {
                lblFieldReg.Visible = true;//Items not added in ListBox 
                lstbAllColumns.Focus();
                //btnSearch.Focus();
            }

            else
            {
                txtBoxDatesMissing.Visible = false;
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

        protected void lstBoxDischarge_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxDischarge.Items.Remove(lstBoxDischarge.SelectedItem);
        }

        protected void btnAddDischarge_Click(object sender, EventArgs e)
        {
            if (txtBoxDischarge.Text.Trim() == "")
            {
                txtBoxDischarge.Text = "";
            }
            else
            {
                lstBoxDischarge.Items.Add(txtBoxDischarge.Text.ToUpper());
                txtBoxDischarge.Text = "";
            }
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
    }
}