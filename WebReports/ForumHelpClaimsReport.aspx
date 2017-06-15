<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForumHelpClaimsReport.aspx.cs" Inherits="WebReports.ForumHelpClaimsReport" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Claims AdHoc Report</title>
    <style type="text/css">
#Background 
{
    position:fixed;
    top:0px;
    bottom:0px;
    left:0px;
    right:0px;
    background-color:Gray;
    filter:alpha(opacity=40);
    opacity:0.4;
}

#Progress 
{
    position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:100px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
        }
        .auto-style5 {
            height: 46px;
        }
        .auto-style7 {
            width: 96px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style2 {
            height: 73px;
        }
        .auto-style5 {
            height: 46px;
        }
        .auto-style6 {
            height: 42px;
        }
        .auto-style7 {
            height: 24px;
        }
        .auto-style8 {
            margin-bottom: 0px;
        }
        .auto-style9 {
            height: 16px;
        }
        .auto-style10 {
            margin-top: 0px;
            margin-left: 140px;
        }
        .auto-style11 {
            height: 8px;
        }
        .auto-style13 {
            width: 145px;
        }
        .auto-style15 {
            margin-top: 0px;
        }
        .auto-style17 {
            width: 728px;
        }
        .auto-style18 {
            height: 30px;
        }
        .auto-style19 {
            height: 28px;
        }
        .auto-style20 {
            height: 26px;
        }
        .auto-style21 {
            height: 23px;
        }
        </style>
    

</head>
<body>
    
    <form id="form1" runat="server">
             <div>
       
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server"></asp:ScriptManager>
        
       </div>

           <asp:UpdatePanel ID="UpdatePanelT" runat="server">
           
            <ContentTemplate> 
<asp:Panel ID="Panel6" runat="server">
    <table>
        <tr>
            <td colspan="4" style="font-size: large">Select a saved report to load or choose criteria for new report:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Saved Reports:</td>
            <td>&nbsp;</td>
            <td>
                <asp:DropDownList ID="ddlSavedReport" runat="server" OnSelectedIndexChanged="ddlSavedReport_SelectedIndexChanged" AutoPostBack="True" DataSourceID="dsSavedReports" DataTextField="ReportName" DataValueField="SavedRptID" Height="20px" OnDataBound="ddlSavedReport_DataBound" Width="250px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="dsSavedReports" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="SELECT distinct SavedRptID ,[ReportName] + ' ----- ' + ReportDescription as ReportName 
FROM [ClaimsAdhocsavedReports]
union select 0,'(Choose a Saved Report)'
order by [ReportName] "></asp:SqlDataSource>
            </td>
            <td>(Hover cursor over report name to see full report description)</td>
            <td>&nbsp;</td>
        </tr>
    </table>
            
          <asp:Label ID="lblReportSavedSucc0" runat="server" Text="Label" Visible="False" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    <br />
  </asp:Panel>           
</ContentTemplate> 

</asp:UpdatePanel>
    

        
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           
            <ContentTemplate> 
                <asp:Panel ID="Panel2" runat="server">
<div>
    <br />
    <table>
        <tr>
            <td title ="Effective or start date" >DOS Start Date:</td>
            <td>        
               <asp:TextBox ID="txtBoxDOSStDate" runat="server" visibility = "hidden"  OnTextChanged="txtBoxDOSStDate_TextChanged" TextMode="Date" ></asp:TextBox>
                 <asp:CompareValidator ID="valDate3" runat="server" ControlToValidate="txtBoxDOSStDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                 </td>
            <td ></td>
            <td >DOS End Date:</td>
            <td >
                <asp:TextBox ID="txtBoxDOSEndDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate4" runat="server" ControlToValidate="txtBoxDOSEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td>
                </td>
        </tr>
        <tr>
            <td title ="Effective or trans date">Trans Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxTransStartDate"  runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate2" runat="server" ControlToValidate="txtBoxTransStartDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
            <td>Trans End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxTransEndDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate5" runat="server" ControlToValidate="txtBoxTransEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td title ="Effective or rec date"">Received Start Date:</td>
            <td >
                <asp:TextBox ID="txtBoxRecievedStartDate"  runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate1" runat="server" ControlToValidate="txtBoxRecievedStartDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td ></td>
            <td >Received End Date:</td>
            <td >
                <asp:TextBox ID="txtBoxRecievedEndDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate6" runat="server" ControlToValidate="txtBoxRecievedEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td ></td>
        </tr>
        <tr>
            <td title ="Effective or paid date">Paid Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxPaidStartDate"  runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate0" runat="server" ControlToValidate="txtBoxPaidStartDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
            <td>Paid End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxPaidEndDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate7" runat="server" ControlToValidate="txtBoxPaidEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td title ="Effective or due date">Due Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxDueStartDate"  runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate" runat="server" ControlToValidate="txtBoxDueStartDate" ErrorMessage="Invalid Date" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" ForeColor="Red"></asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
            <td>Due End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxDueEndDate" runat="server" TextMode="Date" ></asp:TextBox>
                <asp:CompareValidator ID="valDate8" runat="server" ControlToValidate="txtBoxDueEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td title ="Provider#">Provider #:</td>
            <td class="auto-style5">
               
                <asp:TextBox ID="txtBoxAff" runat="server" MaxLength="8" ></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnAddAff" runat="server" Text="Add Prov" OnClick="btnAddAff_Click" CausesValidation="False" />
            </td>
            <td class="auto-style5"></td>
            <td class="auto-style5">
                <asp:ListBox ID="lstBoxAff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBoxAff_SelectedIndexChanged" Width="100px" Height="50px"></asp:ListBox>
                </td>
            <td class="auto-style5">
                <asp:RegularExpressionValidator ID="valiexcodein3" runat="server" ControlToValidate="txtBoxAff" Display="Dynamic" ErrorMessage="Prov no. must be between 4 and 8 characters." ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[\s\S]{4,}$"></asp:RegularExpressionValidator>
            </td>
            <td class="auto-style5"></td>
        </tr>
        <tr>
            <td title="Provider#">Prac #:</td>
            <td class="auto-style5">
                <asp:TextBox ID="txtBoxPrac" runat="server" MaxLength="4"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAddPrac" runat="server" CausesValidation="False" OnClick="btnAddPrac_Click" Text="Add Prac" />
            </td>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style5">
                <asp:ListBox ID="lstBoxPrac" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="lstBoxPrac_SelectedIndexChanged" Width="100px"></asp:ListBox>
            </td>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
        </tr>
        <tr>
            <td  title ="member#" class="auto-style2">Member # or State ID</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtBoxMemNbr" runat="server" MaxLength ="11"></asp:TextBox>
            </td>
            <td class="auto-style2"></td>
            <td class="auto-style2"></td>
            <td class="auto-style2">
                <asp:RegularExpressionValidator ID="valiexcodein1" runat="server" ControlToValidate="txtBoxMemNbr" Display="Dynamic" ErrorMessage="Member # or State ID. must be between 9 and 11 characters." ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[\s\S]{9,}$"></asp:RegularExpressionValidator>
            </td>
            <td class="auto-style2"></td>
        </tr>
        <tr>
            <td  title ="Claim#">Claim #:</td>
            <td>
                <asp:TextBox ID="txtBoxClaimNo" runat="server" MaxLength="12"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddClaim" runat="server" CausesValidation="False" OnClick="btnAddClaim_Click" Text="Add Claim" Width="81px" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxClaimNo" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="lstBoxClaimNo_SelectedIndexChanged" Width="100px"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td  title ="TIN#">TIN #:</td>
            <td>
                <asp:DropDownList ID="ddlTinWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxTIN" runat="server" MaxLength="12" ></asp:TextBox>
                <asp:Button ID="btnAddTIN" runat="server" Text="Add TIN" OnClick="btnAddTIN_Click" CausesValidation="False" Width="109px" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxTin" runat="server" Height="50px" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTin_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td  title ="NPI#">NPI #:</td>
            <td>
                <asp:DropDownList ID="ddlNpiWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxNPI" runat="server" MaxLength="10"></asp:TextBox>
                <asp:Button ID="btnAddNPI" runat="server" Text="Add NPI" OnClick="btnAddNPI_Click" CausesValidation="True" Width="109px" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxNPI" runat="server" Height="50px" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxNPI_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="valiexcodein4" runat="server" ControlToValidate="txtBoxNPI" Display="Dynamic" ErrorMessage="NPI# must be 10 characters" ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[\s\S]{10,}$"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        </table>
    <br />
    <asp:Label ID="lblFields" runat="server" Text="Select columns for the report"></asp:Label>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <table>
        <tr>
            <td >
                <asp:Label ID="lblCS" runat="server" Text="Available Fields"></asp:Label>
                </td>
            <td class="auto-style13">&nbsp;(Ctrl select to move)</td>
            <td class="auto-style17">
                <asp:Label ID="Label4" runat="server" Text="Fields selected for report"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td rowspan="5">
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:ListBox ID="lstbAllColumns" runat="server" AutoPostBack="False" DataSourceID="dSourceAvailColumns" DataTextField="Text" DataValueField="Value" Height="145px" SelectionMode="Multiple" Width="144px" CssClass="auto-style15"></asp:ListBox>
                         </ContentTemplate>
                </asp:UpdatePanel>
                  </td>
            <td >
                &nbsp;&nbsp;
                <asp:Button ID="btnLtoR" runat="server" CausesValidation="False" OnClick="btnLtoR_Click1" Text="&gt; Move All &gt;" Width="113px" />
            </td>
            <td rowspan="5" class="auto-style17">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                 <asp:ListBox ID="lstStdColumns" runat="server" AutoPostBack="False" DataSourceID="dSourceStdColumns" DataTextField="Text" DataValueField="Value" Height="144px" style="margin-top: 0px" Width="144px" SelectionMode="Multiple"></asp:ListBox>

                            <asp:Button ID="btnResetFields" runat="server" CausesValidation="False" OnClick="btnResetFields_Click" Text="Reset Fields" />
                              </ContentTemplate>
                </asp:UpdatePanel>
                
                 <asp:Label ID="lblFieldReg" runat="server" ForeColor="Red" Text="Must have at least 1 field listed" Visible="False"></asp:Label>
            </td>
            <td rowspan="5">
                <asp:TextBox ID="txtCriteria" runat="server" CssClass="auto-style10" Height="16px" TextMode="MultiLine" Visible="False" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >&nbsp;&nbsp;
                <asp:Button ID="btnRtoLAll" runat="server" CausesValidation="False" OnClick="btnRtoLAll_Click" Text="&lt; Move All &lt;" Width="113px" />
            </td>
        </tr>
        <tr>
            <td >&nbsp;&nbsp;
                <asp:Button ID="btnStdToAll" runat="server" OnClick="btnStdToAll_Click" Text="&lt; Selected &lt;" Width="113px" />
            </td>
        </tr>
        <tr>
            <td >&nbsp;&nbsp;
                <asp:Button ID="btnAllToStd" runat="server" OnClick="btnAllToStd_Click" Text="&gt; Selected &gt;" Width="113px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style13" >
                &nbsp;</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="dSourceAvailColumns" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="zzz_procIntranet_ClaimsServiceSearchAvailableColumns" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    &nbsp;<asp:SqlDataSource ID="dSourceStdColumns" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="zzz_procIntranet_ClaimsServiceSearchStandardColumns" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
       


              
 </div>   
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnViewReport_Click" Text="View On Screen" />          
   </asp:Panel>
                   </ContentTemplate> 
     <Triggers>
       <%--// If button is present outside update panel then specify AsynPostBackTrigger--%>
       <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" /> 
    </Triggers>        
</asp:UpdatePanel>

           <%-- <asp:UpdatePanel ID="UpdatePanelD" runat="server" UpdateMode="Always">
           
            <ContentTemplate> 
<asp:Panel ID="Panel5" runat="server">--%>
    <asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="Export to Excel" Visible ="true" Width="116px"/> 
              &nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp; 
              <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset\New Report" CausesValidation="False" /> 
            
<%--             </asp:Panel>           
</ContentTemplate> 

</asp:UpdatePanel>  --%>
        
            <asp:UpdatePanel ID="UpdatePanelC" runat="server">
           
            <ContentTemplate> 
             <asp:Panel ID="Panel1" runat="server">
         
                 <asp:Label ID="lblReportSavedSucc" runat="server" Font-Bold="True" Text="Label" Visible="False"></asp:Label>
         
          <br />
        <table style="border:solid 1px black;" class="auto-style8">
            <tr>
                <td colspan="4" class="auto-style11">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type in report name (required) and report description (optional) to save report:</td>
            </tr>
            <tr>
                <td class="auto-style6">Report name:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="txtboxReportName" runat="server" Width="242px"></asp:TextBox>
                    <asp:Label ID="lblReportNameValid" runat="server" Font-Bold="True" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </td>
                <td class="auto-style6">
                    </td>
                <td class="auto-style6"></td>
            </tr>
            <tr>
                <td class="auto-style7">Report description:</td>
                <td class="auto-style7" colspan="2">
                    <asp:TextBox ID="txtboxReportDescription" runat="server" Height="16px" Width="648px"></asp:TextBox>
                </td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style9"><asp:Button ID="btnSaveRpt" runat="server" OnClick="btnSaveRpt_Click" Text="Save report" />
                </td>
                <td class="auto-style9" colspan="2"></td>
                <td class="auto-style9"></td>
            </tr>
        </table>
 </asp:Panel>           
</ContentTemplate> 

</asp:UpdatePanel>
                      <asp:UpdatePanel ID="UpdatePanelY" runat="server">
           
            <ContentTemplate> 
<asp:Panel ID="Panel8" runat="server" Visible="False">
     <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkNewSearch" runat="server" OnClick="lnkNewSearch_Click" Visible="False">New Search</asp:LinkButton>
                </td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <h3 style="font-family: Arial, Helvetica, sans-serif; color: rgb(60, 118, 178); font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: -webkit-center; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">Claim Report by Affiliation - Search Results</h3>
                </td>
            </tr>
            <tr>
                <td class="auto-style21">
    
                    &nbsp;</td>
                <td class="auto-style21"></td>
                <td class="auto-style21">
                    <asp:Label ID="lblSerachCriteria" runat="server" Text="Search Criteria chosen:"></asp:Label>
                </td>
                <td class="auto-style21"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>
        <asp:TextBox ID="txtCriteriaResults" runat="server" Height="76px" TextMode="MultiLine" Width="386px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
        <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style1">&nbsp;</td>
                <td><asp:Button ID="Button1" runat="server"  style="height: 26px" Text="Export to Excel" OnClick="Button1_Click1" Visible="False" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    <asp:GridView ID="grvData" runat="server" CellPadding="3" EnableViewState="False" GridLines="Horizontal" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
 </asp:Panel>           
</ContentTemplate> 
</asp:UpdatePanel>


          <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    
    <div id="Background"></div>
<div id="Progress">
<img src="waiting.gif" style="vertical-align:middle"/>
Searching - Please Wait... 
</ProgressTemplate>
   </asp:UpdateProgress>       

     </form>
</body>
           
</html>
    <script type="text/javascript">
    function NumericTextBox(evt) {
 
        var charCode = evt.keyCode;
 
        if (evt.ctrlKey == true) {
            if (charCode == 67 || charCode == 86) {
                return true;
            }
        }
        if (charCode == 8 || //backspace
                charCode == 46 || //delete
                charCode == 13)   //enter key
        {
            return true;
        }
        else if (charCode >= 37 && charCode <= 40) //arrow keys
        {
            return true;
        }
 
        else if (charCode >= 48 && charCode <= 57) //0-9 on key pad
        {
            if (evt.shiftKey == true)
                return false;
 
            return true;
        }
        else if (charCode >= 96 && charCode <= 105) //0-9 on num pad
        {
            if (evt.shiftKey == true)
                return false;
 
            return true;
        }
        else
            return false;
    }
   
    
        function validateTextBoxes() {
            //store trimmed values of textboxes into variables
            var txt1 = document.getElementById('txtBoxDOSStDate').value.replace(/^\s+|\s+$/g, '');
            var txt2 = document.getElementById('txtBoxTransStartDate').value.replace(/^\s+|\s+$/g, '');
            var txt3 = document.getElementById('txtBoxRecievedStartDate').value.replace(/^\s+|\s+$/g, '');
            var txt4 = document.getElementById('txtBoxPaidStartDate').value.replace(/^\s+|\s+$/g, '');
            var txt5 = document.getElementById('txtBoxDueStartDate').value.replace(/^\s+|\s+$/g, '');
            if ((txt1 == null || txt1 == "") &&
                (txt2 == null || txt2 == "") &&
                (txt3 == null || txt3 == "") &&
                (txt4 == null || txt4 == "") &&
                (txt5 == null || txt5 == "")) {
                alert('Please have at least one date entered.');
                document.getElementById('<%= txtBoxDOSStDate.ClientID %>').focus();
                return false;
            }
            else
                return true;
        }
</script>