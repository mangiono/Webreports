<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3Old.aspx.cs" Inherits="WebReports.WebForm3" %>

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
        .auto-style3 {
            height: 15px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            height: 46px;
        }
        .auto-style7 {
            width: 96px;
        }
    </style>
    

</head>
<body>
    <form id="form1" runat="server">
 <div>
       
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
       </div>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
<div>
<%--<asp:CustomValidator ID="cvMobileOrPhoneValidation" runat="server" Display="Dynamic" ForeColor="Red" ClientValidationFunction="MobileOrPhoneValidation" ErrorMessage="Please enter at least one date range." ></asp:CustomValidator>--%>
    <asp:Label ID="lblMain" runat="server" Text="Please select criteria for report"></asp:Label>
    <asp:TextBox ID="txtBoxDatesMissing" runat="server" Text="Please have at least one date (mm/dd/yyyy)."  Visible="True" ForeColor="Red" Enabled="True" ReadOnly="True" Width="500px" Font-Bold="True" BorderColor="Transparent" Font-Size="Small"></asp:TextBox> 
           <asp:RadioButtonList ID="radioBtnListChoose" runat="server" RepeatDirection="Horizontal" Visible="False">
                <asp:ListItem Selected="True" Value="screen">View on screen</asp:ListItem>
                <asp:ListItem Value="excel">Export to excel</asp:ListItem>
            </asp:RadioButtonList>
    <br />
    <table>
        <tr>
            <td class="auto-style3">DOS Start Date:</td>
            <td class="auto-style3">        
               <asp:TextBox ID="txtBoxDOSStDate" runat="server" visibility = "hidden"  OnTextChanged="txtBoxDOSStDate_TextChanged" TextMode="Date" ></asp:TextBox>
                 <asp:CompareValidator ID="valDate3" runat="server" ControlToValidate="txtBoxDOSStDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                 </td>
            <td class="auto-style3"></td>
            <td class="auto-style3">DOS End Date:</td>
            <td class="auto-style3">
                <asp:TextBox ID="txtBoxDOSEndDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate4" runat="server" ControlToValidate="txtBoxDOSEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td class="auto-style3">
                </td>
        </tr>
        <tr>
            <td>Trans Start Date:</td>
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
            <td class="auto-style4">Received Start Date:</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtBoxRecievedStartDate"  runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate1" runat="server" ControlToValidate="txtBoxRecievedStartDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td class="auto-style4"></td>
            <td class="auto-style4">Received End Date:</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtBoxRecievedEndDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="valDate6" runat="server" ControlToValidate="txtBoxRecievedEndDate" ErrorMessage="Invalid Date" ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
            <td class="auto-style4"></td>
        </tr>
        <tr>
            <td>Paid Start Date:</td>
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
            <td>Due Start Date:</td>
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
            <td class="auto-style5">Provider #:</td>
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
            <td>Member # or State ID</td>
            <td>
                <asp:TextBox ID="txtBoxMemNbr" runat="server" MaxLength ="11"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:RegularExpressionValidator ID="valiexcodein1" runat="server" ControlToValidate="txtBoxMemNbr" Display="Dynamic" ErrorMessage="Member # or State ID. must be between 9 and 11 characters." ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[\s\S]{9,}$"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Claim #:</td>
            <td>
                <asp:TextBox ID="txtBoxClaimNo" runat="server" MaxLength="12"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAddClaim" runat="server" CausesValidation="False" OnClick="btnAddClaim_Click" Text="Add Claim#" Width="81px" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxClaimNo" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="lstBoxClaimNo_SelectedIndexChanged" Width="100px"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Benefit #:</td>
            <td>
                <asp:TextBox ID="txtBoxBenftNbr" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Program:</td>
            <td>
                <asp:ListBox ID="lstBoxProgram" runat="server" DataSourceID="dSourceDDL" DataTextField="Product_Name" DataValueField="OptionValue" SelectionMode="Multiple" Width="52px"></asp:ListBox>
                <asp:SqlDataSource ID="dSourceDDL" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="SELECT [OptionValue], [Product_Name] FROM [tblLookupProduct] ORDER BY [Product_Name]"></asp:SqlDataSource>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Location Code:</td>
            <td>
                <asp:ListBox ID="lstBoxLocCode" runat="server" Width="346px" DataSourceID="dSourceLocation" DataTextField="Location" DataValueField="Code" SelectionMode="Multiple"></asp:ListBox>
                <asp:SqlDataSource ID="dSourceLocation" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="SELECT substring([Code#],3,2) as Code,substring([Code#],3,2) + ' - ' + [Description] as Location
  FROM [ClaimsSQL].[dbo].[Dwcodem]
  where left(code#,2) = 'LC'
  and (code# not like '%#%' and code# not like '%*%')
  order by Description
"></asp:SqlDataSource>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Division #:</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlDivisionWhere" runat="server">
                    <asp:ListItem Value="in" Selected="True">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:ListBox ID="lstBoxDivision" runat="server" Width="419px" DataSourceID="dSourceDivision" DataTextField="Division" DataValueField="Division#" SelectionMode="Multiple"></asp:ListBox>
                <asp:SqlDataSource ID="dSourceDivision" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="  select distinct Division#, Division# + ' - '+ Name as Division 
  from [ClaimsSQL].[dbo].[Dwdiv]
  order by Division# + ' - '+ Name"></asp:SqlDataSource>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>TIN #:</td>
            <td>
                <asp:DropDownList ID="ddlTinWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxTIN" runat="server" MaxLength="12" placeholder="Type TIN then add"></asp:TextBox>
                <asp:Button ID="btnAddTIN" runat="server" Text="Add TIN" OnClick="btnAddTIN_Click" CausesValidation="False" Width="109px" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxTin" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTin_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>NPI #:</td>
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
                <asp:ListBox ID="lstBoxNPI" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxNPI_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="valiexcodein4" runat="server" ControlToValidate="txtBoxNPI" Display="Dynamic" ErrorMessage="NPI# must be 10 characters" ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[\s\S]{10,}$"></asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">TPI #:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlTPIWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxTPI" runat="server" MaxLength="12"></asp:TextBox>
                <asp:Button ID="btnAddTPI" runat="server" Text="Add TPI" OnClick="btnAddTPI_Click" CausesValidation="False" Width="109px" />
            </td>
            <td class="auto-style13"></td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxTPI" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTPI_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13"></td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Claim Status:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlClaimStatusWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxClaimStatus" runat="server" MaxLength ="2"></asp:TextBox>
                <asp:Button ID="btnAddClaimStatus" runat="server" Text="Add Claim Status" Width="109px" OnClick="btnAddClaimStatus_Click" CausesValidation="True" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxClaimStatus" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxClaimStatus_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">
                <asp:RegularExpressionValidator ID="valiexcodein2" runat="server" ControlToValidate="txtBoxClaimStatus" Display="Dynamic" ErrorMessage="Claim Status must be 2 characters" ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[\s\S]{2,}$"></asp:RegularExpressionValidator>
            </td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Modifier:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlModifierWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxModifier" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddModifier" runat="server" Text="Add Modifier" Width="109px" OnClick="btnAddModifier_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxModifier" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxModifier_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Provider Specialty:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlProviderSpecWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxProviderSpec" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddProviderSpec" runat="server" Text="Add Prov Spec" Width="109px" OnClick="btnAddProviderSpec_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxProviderSpec" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxProviderSpec_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Provider Status:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlProviderStatusWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxProviderStatus" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddProviderStatus" runat="server" Text="Add Prov Status" Width="109px" OnClick="btnAddProviderStatus_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxProviderStatus" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxProviderStatus_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Ex Code:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlExCodeInWhere" runat="server" Enabled="False">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxExCode" runat="server" MaxLength="2"></asp:TextBox>
                <asp:Button ID="btnAddEx" runat="server" OnClick="btnAddEx_Click" Text="Add EX Code" Width="109px" CausesValidation="True" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstboxEx" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="lstboxEx_SelectedIndexChanged" Width="100px"></asp:ListBox>
            </td>
            <td class="auto-style13">
                <asp:RegularExpressionValidator ID="valiexcodein0" runat="server" ControlToValidate="txtBoxExCode" ErrorMessage="Ex Code must be 2 characters" ForeColor="Red" ValidationExpression="^[\s\S]{2,}$" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
            </td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Ex Code:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlExCodeNInWhere" runat="server" Enabled="False">
                    <asp:ListItem Selected="True" Value="not in">Not Equal to</asp:ListItem>
                    <asp:ListItem Value="in">Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxExNiCode" runat="server" MaxLength="2"></asp:TextBox>
                <asp:Button ID="btnAddExNi" runat="server" OnClick="btnAddExNi_Click" Text="Add EX Code" Width="109px" CausesValidation="True" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstboxExNi" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="lstboxExNi_SelectedIndexChanged" Width="100px"></asp:ListBox>
            </td>
            <td class="auto-style13">
                <asp:RegularExpressionValidator ID="valiexcodein" runat="server" ControlToValidate="txtBoxExNiCode" ErrorMessage="Ex Code must be 2 characters" ForeColor="Red" ValidationExpression="^[\s\S]{2,}$" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
            </td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Diagnosis:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlDiagnosisWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxDiagnosis" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddDiagnosis" runat="server" Text="Add Diagnosis" Width="109px" OnClick="btnAddDiagnosis_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxDiagnosis" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxDiagnosis_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">CPT:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlCPTWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxCPT" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddCPT" runat="server" Text="Add CPT" Width="109px" OnClick="btnAddCPT_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxCPT" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxCPT_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Rev Code:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlRevCodeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxRevCode" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddRevCode" runat="server" Text="Add Rev Code" Width="109px" OnClick="btnAddRevCode_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxRevCode" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxRevCode_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">DRG:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlDRGWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxDRG" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddDRG" runat="server" Text="Add DRG" Width="109px" OnClick="btnAddDRG_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxDRG" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxDRG_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Claim Type:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlClaimTypeWhere" runat="server" Enabled="False">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlClaimType" runat="server">
                    <asp:ListItem> </asp:ListItem>
                    <asp:ListItem>H</asp:ListItem>
                    <asp:ListItem>M</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                &nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Treatment Type:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlTreatmentTypeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxTreatmentType" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddTreatmentType" runat="server" Text="Add Treatment" Width="109px" OnClick="btnAddTreatmentType_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxTreatmentType" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTreatmentType_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Benefit Package:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlBenefitPackageWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxBenefitPackage" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddBenefitPackage" runat="server" Text="Add Benefit" Width="109px" OnClick="btnAddBenefitPackage_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxBenefitPackage" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxBenefitPackage_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Hat Code:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlHatCodeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxHatCode" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddHatCode" runat="server" Text="Add Hat Code" Width="109px" OnClick="btnAddHatCode_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxHatCode" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxHatCode_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Region:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlRegionWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxRegion" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddRegion" runat="server" Text="Add Region" Width="109px" OnClick="btnAddRegion_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxRegion" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxRegion_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">CI (Illness):</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlCIIllnessWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxCIIllness" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddCIIllness" runat="server" Text="Add CI Illness" Width="109px" OnClick="btnAddCIIllness_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxCIIllness" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxCIIllness_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Pay Serv Code:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlPayServCodeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxPayServCode" runat="server"></asp:TextBox>
                <asp:Button ID="btnPayServCode" runat="server" Text="Add Pay Serv" Width="109px" OnClick="btnPayServCode_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxPayServCode" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxPayServCode_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Pay Class:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlPayClassWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxPayClass" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddPayClass" runat="server" Text="Add Pay Class" Width="109px" OnClick="btnAddPayClass_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxPayClass" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxPayClass_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Exception Fee:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlExceptionFeeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxExceptionFee" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddExceptionFee" runat="server" Text="Add Excep Fee" Width="109px" OnClick="btnAddExceptionFee_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxExceptionFee" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxExceptionFee_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Op:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlOPWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxOP" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddOP" runat="server" Text="Add OP" Width="109px" OnClick="btnAddOP_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxOP" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxOP_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Fee Schedule:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlFeeScheduleWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxFeeSchedule" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddFeeSchedule" runat="server" Text="Add Fee Sched" Width="109px" OnClick="btnAddFeeSchedule_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxFeeSchedule" runat="server" Height="50px" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxFeeSchedule_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Discharge Status:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlDischargeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxDischarge" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddDischarge" runat="server" CausesValidation="False" OnClick="btnAddDischarge_Click" Text="Add Discharge" Width="109px" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxDischarge" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="lstBoxDischarge_SelectedIndexChanged" Width="100px"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Amount Charge:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlAmtChargeWhere" runat="server">
                    <asp:ListItem Selected="True" Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="&lt;=">&lt;=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxAmtCharge" runat="server" onkeydown="return (!((event.keyCode&gt;=65 &amp;&amp; event.keyCode &lt;= 95) || event.keyCode &gt;= 106) &amp;&amp; event.keyCode!=32);"></asp:TextBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                &nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Amount Paid:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlAmtPaidWhere" runat="server">
                    <asp:ListItem Selected="True" Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="&lt;=">&lt;=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxAmtPaid" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                &nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        </table>
    <br />
    <asp:Label ID="lblFields" runat="server" Text="Select columns for the report"></asp:Label>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <table>
        <tr>
            <td class="auto-style7">
                <asp:Label ID="lblCS" runat="server" Text="Available Fields"></asp:Label>
                &nbsp;- (Click to Add)</td>
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Fields selected for report - (Click to remove)"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td rowspan="2" class="auto-style7">
                <asp:ListBox ID="lstbAllColumns" runat="server" AutoPostBack="True" DataSourceID="dSourceAvailColumns" DataTextField="Text" DataValueField="Value" Height="145px" OnSelectedIndexChanged="lstbCSTbl_SelectedIndexChanged" SelectionMode="Multiple" Width="144px"></asp:ListBox>
            </td>
            <td>
                <asp:Button ID="btnLtoR" runat="server" OnClick="btnLtoR_Click1" Text="&gt; Move All &gt;" CausesValidation="False" />
            </td>
            <td rowspan="2">
                <asp:ListBox ID="lstStdColumns" runat="server" AutoPostBack="True" DataSourceID="dSourceStdColumns" DataTextField="Text" DataValueField="Value" Height="144px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" style="margin-top: 0px" Width="144px"></asp:ListBox>
                <asp:Button ID="btnResetFields" runat="server" CausesValidation="False" OnClick="btnResetFields_Click" Text="Reset Fields" />
                <asp:Label ID="lblFieldReg" runat="server" ForeColor="Red" Text="Must have at least 1 field listed" Visible="False"></asp:Label>
                <asp:TextBox ID="txtCriteria" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
            </td>
            <td rowspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnRtoLAll" runat="server" CausesValidation="False" OnClick="btnRtoLAll_Click" Text="&lt; Move All &lt;" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="dSourceAvailColumns" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="zzz_procIntranet_ClaimsServiceSearchAvailableColumns" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    &nbsp;<asp:SqlDataSource ID="dSourceStdColumns" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="zzz_procIntranet_ClaimsServiceSearchStandardColumns" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
       


              
 </div>   
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnViewReport_Click" Text="View On Screen" />          
   </ContentTemplate> 
     <Triggers>
       <%--// If button is present outside update panel then specify AsynPostBackTrigger--%>
       <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" /> 
    </Triggers>        
</asp:UpdatePanel>
    <asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="Export to Excel" Visible ="true"/> 
              <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CausesValidation="False" /> 
            
          <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    
    <div id="Background"></div>
<div id="Progress">
<img src="waiting.gif" style="vertical-align:middle"/>
Searching - Please Wait... 
</ProgressTemplate>
   </asp:UpdateProgress>       

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
   
    function hidelabel() {

        <%--if(document.getElementById("<%=txtBoxDatesMissing.ClientID%>").style.visibility <> "hidden")
        {--%>
            // Note that the client ID might be different from the server side ID
            document.getElementById("<%=txtBoxDatesMissing.ClientID%>").style.visibility = "hidden";
            //<%=txtBoxDatesMissing.ClientID%>
        //}
    }

</script>
    </form>
</body>
           
</html>
