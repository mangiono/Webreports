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
    public partial class ClaimsAdhocReportTemp : System.Web.UI.Page
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
                criteria = criteria + "[Division# " + ddlDivisionWhere.SelectedItem + " " + division.Replace("','", "|") + "]";
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

            //Get billtype num value
            String billtype = "";
            if (txtboxBillType.Text == "")
            {
                billtype = null;
            }
            else
            {
                billtype = txtboxBillType.Text.ToString();
                criteria = criteria + " [BillType# = " + billtype + "]";
            }

            //Get Pay to 
            String payto = "";
            if (ddlPayTo.SelectedValue == "")
            {
                payto = null;
            }
            else
            {
                payto = ddlPayTo.SelectedValue.ToString();
                criteria = criteria + "[Pay To " + ddlPayToWhere.SelectedItem + " " + payto.Replace("','", "|") + "]";
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
            cmd.Parameters["@StartYMDRcvd"].Value = RcvStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDRcvd"].Value = RcvEndDate; // add parameters value

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

            cmd.Parameters.Add("@billtype", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@billtype"].Value = billtype; // add parameters value

            cmd.Parameters.Add("@prac", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@prac"].Value = prac; // add parameters value

            cmd.Parameters.Add("@PayTo", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@PayTo"].Value = payto; // add parameters value


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
                criteria = criteria + "[Division# " + ddlDivisionWhere.SelectedItem + " " + division.Replace("','", "|") + "]";
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

            //Get billtype num value
            String billtype = "";
            if (txtboxBillType.Text == "")
            {
                billtype = null;
            }
            else
            {
                billtype = txtboxBillType.Text.ToString();
                criteria = criteria + " [BillType# = " + billtype + "]";
            }

            //Get Pay to 
            String payto = "";
            if (ddlPayTo.SelectedValue == "")
            {
                payto = null;
            }
            else
            {
                payto = ddlPayTo.SelectedValue.ToString();
                criteria = criteria + "[Pay To " + ddlPayToWhere.SelectedItem + " " + payto.Replace("','", "|") + "]";
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
                cmd.Parameters["@StartYMDRcvd"].Value = RcvStDate; // add parameters value

                cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@EndYMDRcvd"].Value = RcvEndDate; // add parameters value

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

                cmd.Parameters.Add("@billtype", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@billtype"].Value = billtype; // add parameters value

                cmd.Parameters.Add("@prac", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@prac"].Value = prac; // add parameters value

                cmd.Parameters.Add("@PayTo", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
                cmd.Parameters["@PayTo"].Value = payto; // add parameters value

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
                program = program.Replace("','", "|");
                program.ToString();
                criteria = criteria + "[Program = " + program + "]";

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
                location = location.Replace("','", "|");
                location.ToString();
                criteria = criteria + "[Location = " + location + "]";
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
                division = division.Replace("','", "|");
                division.ToString();
                criteria = criteria + "[Division# " + ddlDivisionWhere.SelectedItem + " " + division + "]";
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
                tpinumber = tpinumber.Replace("','", "|");
                tpinumber.ToString();
                criteria = criteria + "[TPI# " + ddlTPIWhere.SelectedItem + " " + tpinumber + "]";
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
                status = status.Replace("','", "|");
                status.ToString();
                criteria = criteria + "[Claim Status " + ddlClaimStatusWhere.SelectedItem + " " + status + "]";
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
                get_valueMOD = get_valueMOD.Replace("','", "|");
                get_valueMOD.ToString();
                criteria = criteria + "[Modifier " + ddlModifierWhere.SelectedItem + " " + get_valueMOD + "]";
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
                spec = spec.Replace("','", "|");
                spec.ToString();
                criteria = criteria + "[Provider Speciality " + ddlProviderSpecWhere.SelectedItem + " " + spec + "]";
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
                provstatus = provstatus.Replace("','", "|");
                provstatus.ToString();
                criteria = criteria + "[Provider Status " + ddlProviderStatusWhere.SelectedItem + " " + provstatus + "]";
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
                get_valueEX = get_valueEX.Replace("','", "|");
                get_valueEX.ToString();
                criteria = criteria + "[Ex Code " + ddlExCodeInWhere.SelectedItem + " " + get_valueEX + "]";
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
                get_valueEXNotIn = get_valueEXNotIn.Replace("','", "|");
                get_valueEXNotIn.ToString();
                criteria = criteria + "[Ex Code " + ddlExCodeNInWhere.SelectedItem + " " + get_valueEXNotIn + "]";
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
                diagnosis = diagnosis.Replace("','", "|");
                diagnosis.ToString();
                criteria = criteria + "[Diagnosis " + ddlDiagnosisWhere.SelectedItem + " " + diagnosis + "]";
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
                procedureCode = procedureCode.Replace("','", "|");
                procedureCode.ToString();
                criteria = criteria + "[CPT " + ddlCPTWhere.SelectedItem + " " + procedureCode + "]";
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
                revenue = revenue.Replace("','", "|");
                revenue.ToString();
                criteria = criteria + "[Revenue Code " + ddlRevCodeWhere.SelectedItem + " " + revenue + "]";
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
                drg = drg.Replace("','", "|");
                drg.ToString();
                criteria = criteria + "[DRG " + ddlDRGWhere.SelectedItem + " " + drg + "]";
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
                treatment = treatment.Replace("','", "|");
                treatment.ToString();
                criteria = criteria + "[Treatment Type " + ddlTreatmentTypeWhere.SelectedItem + " " + treatment + "]";
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
                benefitpackage = benefitpackage.Replace("','", "|");
                benefitpackage.ToString();
                criteria = criteria + "[Benefit Package " + ddlBenefitPackageWhere.SelectedItem + " " + benefitpackage + "]";
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
                hatcode = hatcode.Replace("','", "|");
                hatcode.ToString();
                criteria = criteria + "[Hat Code " + ddlHatCodeWhere.SelectedItem + " " + hatcode + "]";
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
                region = region.Replace("','", "|");
                region.ToString();
                criteria = criteria + "[Region " + ddlRegionWhere.SelectedItem + " " + region + "]";
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
                illness = illness.Replace("','", "|");
                illness.ToString();
                criteria = criteria + "[CI(Illness) " + ddlCIIllnessWhere.SelectedItem + " " + illness + "]";
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
                payservice = payservice.Replace("','", "|");
                payservice.ToString();
                criteria = criteria + "[Pay Serv Code " + ddlPayServCodeWhere.SelectedItem + " " + payservice + "]";
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
                payclass = payclass.Replace("','", "|");
                payclass.ToString();
                criteria = criteria + "[Pay Class " + ddlPayClassWhere.SelectedItem + " " + payclass + "]";
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
                exception = exception.Replace("','", "|");
                exception.ToString();
                criteria = criteria + "[Exception Fee " + ddlExceptionFeeWhere.SelectedItem + " " + exception + "]";
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
                op = op.Replace("','", "|");
                op.ToString();
                criteria = criteria + "[Op " + ddlOPWhere.SelectedItem + " " + op + "]";
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
                feeschedule = feeschedule.Replace("','", "|");
                feeschedule.ToString();
                criteria = criteria + "[Fee Schedule " + ddlFeeScheduleWhere.SelectedItem + " " + feeschedule + "]";
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
                discharge = discharge.Replace("','", "|");
                discharge.ToString();
                criteria = criteria + "[Discharge " + ddlDischargeWhere.SelectedItem + " " + discharge + "]";
            }

            //Get billtype num value
            String billtype = "";
            if (txtboxBillType.Text == "")
            {
                billtype = null;
            }
            else
            {
                billtype = txtboxBillType.Text.ToString();
                criteria = criteria + " [BillType = " + billtype + "]";
            }


            //Get payto 
            String payto = "";
            if (ddlPayTo.SelectedValue == "")
            {
                payto = null;
            }
            else
            {
                payto = ddlPayTo.SelectedValue.ToString();
                criteria = criteria + "[Pay To " + ddlPayToWhere.SelectedItem + " " + payto.Replace("','", "|") + "]";
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
            cmd.Parameters["@StartYMDRcvd"].Value = RcvStDate; // add parameters value

            cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@EndYMDRcvd"].Value = RcvEndDate; // add parameters value

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

            cmd.Parameters.Add("@billtype", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@billtype"].Value = billtype; // add parameters value

            cmd.Parameters.Add("@prac", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@prac"].Value = prac; // add parameters value

            cmd.Parameters.Add("@PayTo", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@PayTo"].Value = payto; // add parameters value

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


                //benefit
                txtBoxBenftNbr.Text = dt.Rows[0]["txtBoxBenftNbr"].ToString();


                //program
                lstBoxProgram.SelectedIndex = -1;
                string program = dt.Rows[0]["lstBoxProgram"].ToString();

                if (string.IsNullOrWhiteSpace(program))
                {
                    program = null;
                }

                else
                {
                    string[] programvalues = program.Split('|');
                    for (int i = 0; i < programvalues.Length; i++)
                    {
                        programvalues[i] = programvalues[i].Trim();
                    }

                    foreach (string item in programvalues)
                    {
                        lstBoxProgram.Items.FindByValue(item).Selected = true;
                    }
                }


                //locationcode
                lstBoxLocCode.SelectedIndex = -1;
                string locationcode = dt.Rows[0]["lstBoxLocCode"].ToString();
                if (string.IsNullOrWhiteSpace(locationcode))
                {
                    locationcode = null;
                }

                else
                {

                    string[] locationcodevalues = locationcode.Split('|');
                    for (int i = 0; i < locationcodevalues.Length; i++)
                    {
                        locationcodevalues[i] = locationcodevalues[i].Trim();
                    }

                    foreach (string item in locationcodevalues)
                    {
                        lstBoxLocCode.Items.FindByValue(item).Selected = true;
                    }
                }

                //division
                ddlDivisionWhere.SelectedValue = dt.Rows[0]["ddlDivisionWhere"].ToString(); ;

                lstBoxDivision.SelectedIndex = -1;
                string division = dt.Rows[0]["lstBoxDivision"].ToString();

                if (string.IsNullOrWhiteSpace(division))
                {
                    division = null;
                }

                else
                {
                    string[] divisionvalues = division.Split('|');
                    for (int i = 0; i < divisionvalues.Length; i++)
                    {
                        divisionvalues[i] = divisionvalues[i].Trim();
                    }

                    foreach (string item in divisionvalues)
                    {
                        lstBoxDivision.Items.FindByValue(item).Selected = true;
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

                //tpi
                ddlTPIWhere.SelectedValue = dt.Rows[0]["ddlTPIWhere"].ToString();

                lstBoxTPI.Items.Clear();
                string tpi = dt.Rows[0]["lstBoxTPI"].ToString();
                if (string.IsNullOrWhiteSpace(tpi))
                {
                    tpi = null;
                }

                else
                {
                    string[] tpivalues = tpi.Split('|');
                    for (int i = 0; i < tpivalues.Length; i++)
                    {
                        tpivalues[i] = tpivalues[i].Trim();
                    }

                    foreach (string item in tpivalues)
                    {
                        lstBoxTPI.Items.Add(item);
                    }
                }

                //claimstatus
                ddlClaimStatusWhere.SelectedValue = dt.Rows[0]["ddlClaimStatusWhere"].ToString();

                lstBoxClaimStatus.Items.Clear();
                string claimstatus = dt.Rows[0]["lstBoxClaimStatus"].ToString();
                if (string.IsNullOrWhiteSpace(claimstatus))
                {
                    claimstatus = null;
                }

                else
                {
                    string[] claimstatusvalues = claimstatus.Split('|');
                    for (int i = 0; i < claimstatusvalues.Length; i++)
                    {
                        claimstatusvalues[i] = claimstatusvalues[i].Trim();
                    }

                    foreach (string item in claimstatusvalues)
                    {
                        lstBoxClaimStatus.Items.Add(item);
                    }
                }

                //modifier
                ddlModifierWhere.SelectedValue = dt.Rows[0]["ddlModifierWhere"].ToString();

                lstBoxModifier.Items.Clear();
                string modifier = dt.Rows[0]["lstBoxModifier"].ToString();
                if (string.IsNullOrWhiteSpace(modifier))
                {
                    modifier = null;
                }

                else
                {
                    string[] modifiervalues = modifier.Split('|');
                    for (int i = 0; i < modifiervalues.Length; i++)
                    {
                        modifiervalues[i] = modifiervalues[i].Trim();
                    }

                    foreach (string item in modifiervalues)
                    {
                        lstBoxModifier.Items.Add(item);
                    }
                }

                //provspec
                ddlProviderSpecWhere.SelectedValue = dt.Rows[0]["ddlProviderSpecWhere"].ToString();

                lstBoxProviderSpec.Items.Clear();
                string provspec = dt.Rows[0]["lstBoxProviderSpec"].ToString();
                if (string.IsNullOrWhiteSpace(provspec))
                {
                    provspec = null;
                }

                else
                {
                    string[] provspecvalues = provspec.Split('|');
                    for (int i = 0; i < provspecvalues.Length; i++)
                    {
                        provspecvalues[i] = provspecvalues[i].Trim();
                    }

                    foreach (string item in provspecvalues)
                    {
                        lstBoxProviderSpec.Items.Add(item);
                    }
                }

                //provstatus
                ddlProviderStatusWhere.SelectedValue = dt.Rows[0]["ddlProviderStatusWhere"].ToString();

                lstBoxProviderStatus.Items.Clear();
                string provstatus = dt.Rows[0]["lstBoxProviderStatus"].ToString();
                if (string.IsNullOrWhiteSpace(provstatus))
                {
                    provstatus = null;
                }

                else
                {
                    string[] provstatusvalues = provstatus.Split('|');
                    for (int i = 0; i < provstatusvalues.Length; i++)
                    {
                        provstatusvalues[i] = provstatusvalues[i].Trim();
                    }

                    foreach (string item in provstatusvalues)
                    {
                        lstBoxProviderStatus.Items.Add(item);
                    }
                }

                //ex code
                lstboxEx.Items.Clear();
                string excode = dt.Rows[0]["lstboxEx"].ToString();
                if (string.IsNullOrWhiteSpace(excode))
                {
                    excode = null;
                }

                else
                {
                    string[] excodevalues = excode.Split('|');
                    for (int i = 0; i < excodevalues.Length; i++)
                    {
                        excodevalues[i] = excodevalues[i].Trim();
                    }

                    foreach (string item in excodevalues)
                    {
                        lstboxEx.Items.Add(item);
                    }
                }

                //ex code ni
                lstboxExNi.Items.Clear();
                string excodeni = dt.Rows[0]["lstboxExNi"].ToString();
                if (string.IsNullOrWhiteSpace(excodeni))
                {
                    excodeni = null;
                }

                else
                {
                    string[] excodenivalues = excodeni.Split('|');
                    for (int i = 0; i < excodenivalues.Length; i++)
                    {
                        excodenivalues[i] = excodenivalues[i].Trim();
                    }

                    foreach (string item in excodenivalues)
                    {
                        lstboxExNi.Items.Add(item);
                    }
                }

                //diagnosis
                ddlDiagnosisWhere.SelectedValue = dt.Rows[0]["ddlDiagnosisWhere"].ToString();

                lstBoxDiagnosis.Items.Clear();
                string diagnosis = dt.Rows[0]["lstBoxDiagnosis"].ToString();
                if (string.IsNullOrWhiteSpace(diagnosis))
                {
                    diagnosis = null;
                }

                else
                {
                    string[] diagnosisvalues = diagnosis.Split('|');
                    for (int i = 0; i < diagnosisvalues.Length; i++)
                    {
                        diagnosisvalues[i] = diagnosisvalues[i].Trim();
                    }

                    foreach (string item in diagnosisvalues)
                    {
                        lstBoxDiagnosis.Items.Add(item);
                    }
                }

                //procedurecode
                ddlCPTWhere.SelectedValue = dt.Rows[0]["ddlCPTWhere"].ToString();

                lstBoxCPT.Items.Clear();
                string procedurecode = dt.Rows[0]["lstBoxCPT"].ToString();
                if (string.IsNullOrWhiteSpace(procedurecode))
                {
                    procedurecode = null;
                }

                else
                {
                    string[] procedurecodevalues = procedurecode.Split('|');
                    for (int i = 0; i < procedurecodevalues.Length; i++)
                    {
                        procedurecodevalues[i] = procedurecodevalues[i].Trim();
                    }

                    foreach (string item in procedurecodevalues)
                    {
                        lstBoxCPT.Items.Add(item);
                    }
                }

                //revcode
                ddlRevCodeWhere.SelectedValue = dt.Rows[0]["ddlRevCodeWhere"].ToString();

                lstBoxRevCode.Items.Clear();
                string revcode = dt.Rows[0]["lstBoxRevCode"].ToString();
                if (string.IsNullOrWhiteSpace(revcode))
                {
                    revcode = null;
                }

                else
                {
                    string[] revcodevalues = revcode.Split('|');
                    for (int i = 0; i < revcodevalues.Length; i++)
                    {
                        revcodevalues[i] = revcodevalues[i].Trim();
                    }

                    foreach (string item in revcodevalues)
                    {
                        lstBoxRevCode.Items.Add(item);
                    }
                }

                //drg
                ddlDRGWhere.SelectedValue = dt.Rows[0]["ddlDRGWhere"].ToString();

                lstBoxDRG.Items.Clear();
                string drg = dt.Rows[0]["lstBoxDRG"].ToString();
                if (string.IsNullOrWhiteSpace(drg))
                {
                    drg = null;
                }

                else
                {
                    string[] drgvalues = drg.Split('|');
                    for (int i = 0; i < drgvalues.Length; i++)
                    {
                        drgvalues[i] = drgvalues[i].Trim();
                    }

                    foreach (string item in drgvalues)
                    {
                        lstBoxDRG.Items.Add(item);
                    }
                }

                //claimtype
                ddlClaimType.SelectedValue = dt.Rows[0]["ddlClaimType"].ToString();

                //treatment
                ddlTreatmentTypeWhere.SelectedValue = dt.Rows[0]["ddlTreatmentTypeWhere"].ToString();

                lstBoxTreatmentType.Items.Clear();
                string treatment = dt.Rows[0]["lstBoxTreatmentType"].ToString();
                if (string.IsNullOrWhiteSpace(treatment))
                {
                    treatment = null;
                }

                else
                {
                    string[] treatmentvalues = treatment.Split('|');
                    for (int i = 0; i < treatmentvalues.Length; i++)
                    {
                        treatmentvalues[i] = treatmentvalues[i].Trim();
                    }

                    foreach (string item in treatmentvalues)
                    {
                        lstBoxTreatmentType.Items.Add(item);
                    }
                }

                //benefitpackage
                ddlBenefitPackageWhere.SelectedValue = dt.Rows[0]["ddlBenefitPackageWhere"].ToString();

                lstBoxBenefitPackage.Items.Clear();
                string benefitpackage = dt.Rows[0]["lstBoxBenefitPackage"].ToString();
                if (string.IsNullOrWhiteSpace(benefitpackage))
                {
                    benefitpackage = null;
                }

                else
                {
                    string[] benefitpackagevalues = benefitpackage.Split('|');
                    for (int i = 0; i < benefitpackagevalues.Length; i++)
                    {
                        benefitpackagevalues[i] = benefitpackagevalues[i].Trim();
                    }

                    foreach (string item in benefitpackagevalues)
                    {
                        lstBoxBenefitPackage.Items.Add(item);
                    }
                }

                //hatcode
                ddlHatCodeWhere.SelectedValue = dt.Rows[0]["ddlHatCodeWhere"].ToString();

                lstBoxHatCode.Items.Clear();
                string hatcode = dt.Rows[0]["lstBoxHatCode"].ToString();
                if (string.IsNullOrWhiteSpace(hatcode))
                {
                    hatcode = null;
                }

                else
                {
                    string[] hatcodevalues = hatcode.Split('|');
                    for (int i = 0; i < hatcodevalues.Length; i++)
                    {
                        hatcodevalues[i] = hatcodevalues[i].Trim();
                    }

                    foreach (string item in hatcodevalues)
                    {
                        lstBoxHatCode.Items.Add(item);
                    }
                }

                //region
                ddlRegionWhere.SelectedValue = dt.Rows[0]["ddlRegionWhere"].ToString();

                lstBoxRegion.Items.Clear();
                string region = dt.Rows[0]["lstBoxRegion"].ToString();
                if (string.IsNullOrWhiteSpace(region))
                {
                    region = null;
                }

                else
                {
                    string[] regionvalues = region.Split('|');
                    for (int i = 0; i < regionvalues.Length; i++)
                    {
                        regionvalues[i] = regionvalues[i].Trim();
                    }

                    foreach (string item in regionvalues)
                    {
                        lstBoxRegion.Items.Add(item);
                    }
                }

                //cilillness
                ddlCIIllnessWhere.SelectedValue = dt.Rows[0]["ddlCIIllnessWhere"].ToString();

                lstBoxCIIllness.Items.Clear();
                string cilillness = dt.Rows[0]["lstBoxCIIllness"].ToString();
                if (string.IsNullOrWhiteSpace(cilillness))
                {
                    cilillness = null;
                }

                else
                {
                    string[] cilillnessvalues = cilillness.Split('|');
                    for (int i = 0; i < cilillnessvalues.Length; i++)
                    {
                        cilillnessvalues[i] = cilillnessvalues[i].Trim();
                    }

                    foreach (string item in cilillnessvalues)
                    {
                        lstBoxCIIllness.Items.Add(item);
                    }
                }

                //payservcode
                ddlPayServCodeWhere.SelectedValue = dt.Rows[0]["ddlPayServCodeWhere"].ToString();

                lstBoxPayServCode.Items.Clear();
                string payservcode = dt.Rows[0]["lstBoxPayServCode"].ToString();
                if (string.IsNullOrWhiteSpace(payservcode))
                {
                    payservcode = null;
                }

                else
                {
                    string[] payservcodevalues = payservcode.Split('|');
                    for (int i = 0; i < payservcodevalues.Length; i++)
                    {
                        payservcodevalues[i] = payservcodevalues[i].Trim();
                    }

                    foreach (string item in payservcodevalues)
                    {
                        lstBoxPayServCode.Items.Add(item);
                    }
                }

                //payclass
                ddlPayClassWhere.SelectedValue = dt.Rows[0]["ddlPayClassWhere"].ToString();

                lstBoxPayClass.Items.Clear();
                string payclass = dt.Rows[0]["lstBoxPayClass"].ToString();
                if (string.IsNullOrWhiteSpace(payclass))
                {
                    payclass = null;
                }

                else
                {
                    string[] payclassvalues = payclass.Split('|');
                    for (int i = 0; i < payclassvalues.Length; i++)
                    {
                        payclassvalues[i] = payclassvalues[i].Trim();
                    }

                    foreach (string item in payclassvalues)
                    {
                        lstBoxPayClass.Items.Add(item);
                    }
                }

                //exepfee
                ddlExceptionFeeWhere.SelectedValue = dt.Rows[0]["ddlExceptionFeeWhere"].ToString();

                lstBoxExceptionFee.Items.Clear();
                string exepfee = dt.Rows[0]["lstBoxExceptionFee"].ToString();
                if (string.IsNullOrWhiteSpace(exepfee))
                {
                    exepfee = null;
                }

                else
                {
                    string[] exepfeevalues = exepfee.Split('|');
                    for (int i = 0; i < exepfeevalues.Length; i++)
                    {
                        exepfeevalues[i] = exepfeevalues[i].Trim();
                    }

                    foreach (string item in exepfeevalues)
                    {
                        lstBoxExceptionFee.Items.Add(item);
                    }
                }

                //op
                ddlOPWhere.SelectedValue = dt.Rows[0]["ddlOPWhere"].ToString();

                lstBoxOP.Items.Clear();
                string op = dt.Rows[0]["lstBoxOP"].ToString();
                if (string.IsNullOrWhiteSpace(op))
                {
                    op = null;
                }

                else
                {
                    string[] opvalues = op.Split('|');
                    for (int i = 0; i < opvalues.Length; i++)
                    {
                        opvalues[i] = opvalues[i].Trim();
                    }

                    foreach (string item in opvalues)
                    {
                        lstBoxOP.Items.Add(item);
                    }
                }

                //feesched
                ddlFeeScheduleWhere.SelectedValue = dt.Rows[0]["ddlFeeScheduleWhere"].ToString();

                lstBoxFeeSchedule.Items.Clear();
                string feesched = dt.Rows[0]["lstBoxFeeSchedule"].ToString();
                if (string.IsNullOrWhiteSpace(feesched))
                {
                    feesched = null;
                }

                else
                {
                    string[] feeschedvalues = feesched.Split('|');
                    for (int i = 0; i < feeschedvalues.Length; i++)
                    {
                        feeschedvalues[i] = feeschedvalues[i].Trim();
                    }

                    foreach (string item in feeschedvalues)
                    {
                        lstBoxFeeSchedule.Items.Add(item);
                    }
                }

                //discharge
                ddlDischargeWhere.SelectedValue = dt.Rows[0]["ddlDischargeWhere"].ToString();

                lstBoxDischarge.Items.Clear();
                string discharge = dt.Rows[0]["lstBoxDischarge"].ToString();
                if (string.IsNullOrWhiteSpace(discharge))
                {
                    discharge = null;
                }

                else
                {
                    string[] dischargevalues = discharge.Split('|');
                    for (int i = 0; i < dischargevalues.Length; i++)
                    {
                        dischargevalues[i] = dischargevalues[i].Trim();
                    }

                    foreach (string item in dischargevalues)
                    {
                        lstBoxDischarge.Items.Add(item);
                    }
                }


                //billtype
                txtboxBillType.Text = dt.Rows[0]["txtboxBillType"].ToString();

                //payto
                ddlPayTo.SelectedValue = dt.Rows[0]["ddlPayTo"].ToString();

                //amount charged
                ddlAmtChargeWhere.SelectedValue = dt.Rows[0]["ddlAmtChargeWhere"].ToString();
                txtBoxAmtCharge.Text = dt.Rows[0]["txtBoxAmtCharge"].ToString();

                //amount paid
                ddlAmtPaidWhere.SelectedValue = dt.Rows[0]["ddlAmtPaidWhere"].ToString();
                txtBoxAmtPaid.Text = dt.Rows[0]["txtBoxAmtPaid"].ToString();


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