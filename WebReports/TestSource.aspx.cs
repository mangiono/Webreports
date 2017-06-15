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

namespace WebReports
{
    public partial class TestSource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add attributes to relevant text boxes to only allow typing in numerics
            txtBoxAmtCharge.Attributes.Add("onkeydown", "return NumericTextBox(event)");
            txtBoxAmtPaid.Attributes.Add("onkeydown", "return NumericTextBox(event)");
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            ////Get member num value
            //String memNbr = "";
            //if (txtBoxMemNbr.Text == "")
            //{
            //    memNbr = null;
            //}
            //else
            //{
            //    memNbr = txtBoxMemNbr.Text.ToString();
            //}

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

            }

            //Get ClaimType start date
            String claimtype = "";
            if (ddlClaimType.SelectedValue == "")
            {
                claimtype = null;
            }
            else
            {
                claimtype = ddlClaimType.SelectedValue.ToString();
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
            }



            //Get column list from listbox and assign to variable
            String get_valueNL = "";
            for (int i = 0; i < lstStdColumns.Items.Count; i++)
            {
                get_valueNL = get_valueNL + lstStdColumns.Items[i].Value + ",";
            }
            get_valueNL = get_valueNL.ToString().TrimEnd(',');
            txtBoxColumns.Text = get_valueNL;


            if (radioBtnListChoose.SelectedValue == "screen")
            {

                Server.Transfer("ReportTest3Results.aspx");
            }
            //Button1.Visible = true;

            if (radioBtnListChoose.SelectedValue == "excel")
            {
                //grvData.DataSource = dt;
                grvData.DataBind();
                grvData.Visible = false;

                DataTable table = new DataTable();
                CreateTable(grvData, ref table);

                //string file = new User_Login_CS.Helper.ExcelHelper().ExportToExcel(table, "tempexcelfile");
                string rootPath = HttpContext.Current.Server.MapPath("~").ToString();

                string fname = System.Guid.NewGuid().ToString();

                string localCopy = fname + ".xlsx";


                //TextBox3.Text = file.ToString();
                //TextBox4.Text = rootPath.ToString();
                //TextBox2.Text = rootPath + localCopy;
                //File.Move(file, rootPath + "\\" + localCopy);




                Response.Redirect("\\\\app_nonpar\\" + localCopy);

                // File.Delete(@"c:\Windows\Temp\tempexcelfile.xlsx");
                //File.Delete("\\\\app_nonpar\\" + localCopy);
                //UpdatePanel1.Visible = false;
            }

        }

        protected void btnAddEx_Click(object sender, EventArgs e)
        {

            //btnAddEx.Attributes.Add("onclick", "return false;");

            if (txtBoxExCode.Text.Trim() == "")
            {
                txtBoxExCode.Text = "";
            }
            else
            {
                lstboxEx.Items.Add(txtBoxExCode.Text.ToUpper());
                txtBoxExCode.Text = "";
            }
            //lstboxEx.Items.Add("Craig");
            //lstboxEx.DataBind();
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
            //ListBox1.Items.Add(lstbCSTbl.SelectedValue);
            lstbAllColumns.Items.Remove(lstbAllColumns.SelectedItem);

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



            ListItem dataItem = new ListItem();
            dataItem.Text = lstStdColumns.SelectedItem.ToString();
            dataItem.Value = lstStdColumns.SelectedValue;
            lstbAllColumns.Items.Add(dataItem);
            //if (dataItem.Value.Substring(0, 2) == "CS")
            //{
            //    lstbAllColumns.Items.Add(dataItem);
            //}



            lstStdColumns.Items.Remove(lstStdColumns.SelectedItem);
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            CreateTable(grvData, ref table);

            //string file = new User_Login_CS.Helper.ExcelHelper().ExportToExcel(table, "tempexcelfile");
            string rootPath = HttpContext.Current.Server.MapPath("~").ToString();

            string fname = System.Guid.NewGuid().ToString();

            string localCopy = fname + ".xlsx";


            //TextBox3.Text = file.ToString();
            //TextBox4.Text = rootPath.ToString();
            //TextBox2.Text = rootPath + localCopy;
            //File.Move(file, rootPath + "\\" + localCopy);




            Response.Redirect("\\\\app_nonpar\\" + localCopy);

            // File.Delete(@"c:\Windows\Temp\tempexcelfile.xlsx");
            //File.Delete("\\\\app_nonpar\\" + localCopy);



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
    }
}