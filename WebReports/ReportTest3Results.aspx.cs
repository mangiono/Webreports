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
    public partial class ReportTest3Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
            ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);


            //             //String memNbr = "";
            //             //if (txtBoxMemNbr.Text == "")
            //             //{
            //             //    memNbr = null;
            //             //}
            //             //else
            //             //{
            //             //    memNbr = txtBoxMemNbr.Text.ToString();
            //             //}
            //             string memNbr = null;
            //             TextBox memNbrT =
            //                 (TextBox)PreviousPage.FindControl("txtBoxMemNbr");
            //             if (memNbrT != null)
            //             {
            //                 memNbr = memNbrT.Text; 
            //             }


            //             string dosst = null;
            //             TextBox dosstT =
            //                 (TextBox)PreviousPage.FindControl("txtBoxDOSStDate");
            //             if (dosstT != null)
            //             {
            //                 dosst = dosstT.Text;
            //             }


            //             string dosend = null;
            //             TextBox dosendT =
            //                 (TextBox)PreviousPage.FindControl("txtBoxDOSStDate");
            //             if (dosendT != null)
            //             {
            //                 dosend = dosendT.Text;
            //             }


            //             string get_valueNL = null;
            //             TextBox get_valueNLT =
            //(TextBox)PreviousPage.FindControl("txtBoxColumns");
            //             if (get_valueNLT != null)
            //             {
            //                 get_valueNL = get_valueNLT.Text;
            //             }



            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Intranet2012ConnectionString"].ConnectionString);

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("zzz_procIntranet_ClaimsServiceSearchSortable_List_Test", connection); // stored procedure’s name and connection

            cmd.CommandType = CommandType.StoredProcedure; //   choose command type stored procedures

            cmd.Parameters.Add("@ColumnList", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@ColumnList"].Value = (string)(Session["get_valueNLT"]);  // add parameters value

            cmd.Parameters.Add("@Startymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Startymdend"].Value = (string)(Session["DOSStDateT"]); // add parameters value

            cmd.Parameters.Add("@Endymdend", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Endymdend"].Value = (string)(Session["DOSEndDateT"]); // add parameters value

            cmd.Parameters.Add("@Membernumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            cmd.Parameters["@Membernumber"].Value = (string)(Session["memNbrT"]); // add parameters value

            //cmd.Parameters.Add("@aff", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@aff"].Value = affnbr; // add parameters value

            //cmd.Parameters.Add("@Modifier", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Modifier"].Value = get_valueMOD; // add parameters value

            //cmd.Parameters.Add("@ModifierSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@ModifierSearch"].Value = ddlModifierWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Ex", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@EX"].Value = get_valueEX; // add parameters value

            //cmd.Parameters.Add("@NotEx", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@NotEx"].Value = get_valueEXNotIn; // add parameters value

            //cmd.Parameters.Add("@StartYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@StartYMDPaid"].Value = PaidStDate; // add parameters value

            //cmd.Parameters.Add("@EndYMDPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@EndYMDPaid"].Value = PaidEndDate; // add parameters value

            //cmd.Parameters.Add("@StartYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@StartYMDDueDate"].Value = DueStDate; // add parameters value

            //cmd.Parameters.Add("@EndYMDDueDate", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@EndYMDDueDate"].Value = DueEndDate; // add parameters value

            //cmd.Parameters.Add("@Location", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Location"].Value = location; // add parameters value

            //cmd.Parameters.Add("@program", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@program"].Value = program; // add parameters value

            //cmd.Parameters.Add("@TinNumberSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@TinNumberSearch"].Value = ddlTinWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@TinNumber"].Value = tinnumber; // add parameters value

            //cmd.Parameters.Add("@NPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@NPISearch"].Value = ddlNpiWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@NPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@NPINumber"].Value = npinumber; // add parameters value

            //cmd.Parameters.Add("@TPISearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@TPISearch"].Value = ddlTPIWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@TPINumber", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@TPINumber"].Value = tpinumber; // add parameters value

            //cmd.Parameters.Add("@StartYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@StartYMDRcvd"].Value = DueStDate; // add parameters value

            //cmd.Parameters.Add("@EndYMDRcvd", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@EndYMDRcvd"].Value = DueEndDate; // add parameters value

            //cmd.Parameters.Add("@StartYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@StartYMDTrans"].Value = TransStDate; // add parameters value

            //cmd.Parameters.Add("@EndYMDTrans", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@EndYMDTrans"].Value = TransEndDate; // add parameters value

            //cmd.Parameters.Add("@Status", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Status"].Value = status; // add parameters value

            //cmd.Parameters.Add("@StatusSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@StatusSearch"].Value = ddlClaimStatusWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Diag", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Diag"].Value = diagnosis; // add parameters value

            //cmd.Parameters.Add("@DiagSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@DiagSearch"].Value = ddlDiagnosisWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Revenue", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Revenue"].Value = revenue; // add parameters value

            //cmd.Parameters.Add("@RevenueSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@RevenueSearch"].Value = ddlRevCodeWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@DRG", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@DRG"].Value = drg; // add parameters value

            //cmd.Parameters.Add("@DRGSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@DRGSearch"].Value = ddlDRGWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Treatment", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Treatment"].Value = treatment; // add parameters value

            //cmd.Parameters.Add("@TreatmentSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@TreatmentSearch"].Value = ddlTreatmentTypeWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Spec1", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Spec1"].Value = spec; // add parameters value

            //cmd.Parameters.Add("@Spec1Search", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Spec1Search"].Value = ddlProviderSpecWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@ProviderStatus", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@ProviderStatus"].Value = provstatus; // add parameters value

            //cmd.Parameters.Add("@ProviderStatusSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@ProviderStatusSearch"].Value = ddlProviderStatusWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@ClaimType", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@ClaimType"].Value = claimtype; // add parameters value

            //cmd.Parameters.Add("@Benefit", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Benefit"].Value = benefit; // add parameters value

            //cmd.Parameters.Add("@Benefitpackage", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Benefitpackage"].Value = benefitpackage; // add parameters value

            //cmd.Parameters.Add("@BenefitpackageSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@BenefitpackageSearch"].Value = ddlBenefitPackageWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@PayServiceCode", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@PayServiceCode"].Value = payservice; // add parameters value

            //cmd.Parameters.Add("@PayServiceCodeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@PayServiceCodeSearch"].Value = ddlPayServCodeWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@HatCode", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@HatCode"].Value = hatcode; // add parameters value

            //cmd.Parameters.Add("@HatCodeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@HatCodeSearch"].Value = ddlHatCodeWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@PayClass", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@PayClass"].Value = payclass; // add parameters value

            //cmd.Parameters.Add("@PayClassSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@PayClassSearch"].Value = ddlPayClassWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Exception", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Exception"].Value = exception; // add parameters value

            //cmd.Parameters.Add("@ExceptionSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@ExceptionSearch"].Value = ddlExceptionFeeWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Region", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Region"].Value = region; // add parameters value

            //cmd.Parameters.Add("@RegionSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@RegionSearch"].Value = ddlRegionWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Op", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Op"].Value = op; // add parameters value

            //cmd.Parameters.Add("@OpSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@OpSearch"].Value = ddlOPWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@FeeSchedule", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@FeeSchedule"].Value = feeschedule; // add parameters value

            //cmd.Parameters.Add("@FeeScheduleSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@FeeScheduleSearch"].Value = ddlFeeScheduleWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Division", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Division"].Value = division; // add parameters value

            //cmd.Parameters.Add("@DivisionSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@DivisionSearch"].Value = ddlDivisionWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@Illness", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@Illness"].Value = illness; // add parameters value

            //cmd.Parameters.Add("@IllnessSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@IllnessSearch"].Value = ddlCIIllnessWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@AmtCharge", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@AmtCharge"].Value = amtcharge; // add parameters value

            //cmd.Parameters.Add("@AmtChargeSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@AmtChargeSearch"].Value = ddlAmtChargeWhere.SelectedValue; // add parameters value

            //cmd.Parameters.Add("@AmtPaid", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@AmtPaid"].Value = amtpaid; // add parameters value

            //cmd.Parameters.Add("@AmtPaidSearch", SqlDbType.VarChar, 8000); // add parameters with dbtype and size
            //cmd.Parameters["@AmtPaidSearch"].Value = ddlAmtPaidWhere.SelectedValue; // add parameters value


            SqlDataAdapter dp = new SqlDataAdapter(cmd);

            //System.Threading.Thread.Sleep(5000);
            dp.Fill(dt); // fill results to datatable
            connection.Close();


            grvData.DataSource = dt;
            grvData.DataBind();
            grvData.Visible = true;


        }
    }
}