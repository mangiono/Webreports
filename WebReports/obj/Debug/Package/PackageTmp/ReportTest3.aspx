<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTest3.aspx.cs" Inherits="WebReports.ReportTest3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
     

    </style>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
    <asp:Label ID="lblMain" runat="server" Text="Please select criteria for report"></asp:Label>
            <asp:RadioButtonList ID="radioBtnListChoose" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="screen">View on screen</asp:ListItem>
                <asp:ListItem Value="excel">Export to excel</asp:ListItem>
            </asp:RadioButtonList>
    <br />
    <table>
        <tr>
            <td class="auto-style1">DOS Start Date:</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtBoxDOSStDate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValiDosStartDate" runat="server" ControlToValidate="txtBoxDOSStDate" ErrorMessage="DOS start date required" Font-Bold="False" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style1"></td>
            <td class="auto-style1">DOS End Date:</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtBoxDOSEndDate" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style1">
                <asp:RequiredFieldValidator ID="ValiDosStartDate0" runat="server" ControlToValidate="txtBoxDOSEndDate" ErrorMessage="DOS end date required" Font-Bold="False" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Trans Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxTransStartDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>Trans End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxTransEndDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Received Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxRecievedStartDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>Received End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxRecievedEndDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Paid Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxPaidStartDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>Paid End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxPaidEndDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Due Start Date:</td>
            <td>
                <asp:TextBox ID="txtBoxDueStartDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>Due End Date:</td>
            <td>
                <asp:TextBox ID="txtBoxDueEndDate" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Affiliation:</td>
            <td>
                <asp:TextBox ID="txtBoxAff" runat="server"></asp:TextBox>
            &nbsp;<asp:Button ID="btnAddAff" runat="server" Text="Add Aff" OnClick="btnAddAff_Click" CausesValidation="False" />
                <asp:ListBox ID="lstBoxAff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBoxAff_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Member #:</td>
            <td>
                <asp:TextBox ID="txtBoxMemNbr" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
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
                <asp:TextBox ID="txtBoxTIN" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddTIN" runat="server" Text="Add TIN" OnClick="btnAddTIN_Click" CausesValidation="False" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxTin" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTin_SelectedIndexChanged"></asp:ListBox>
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
                <asp:TextBox ID="txtBoxNPI" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddNPI" runat="server" Text="Add NPI" OnClick="btnAddNPI_Click" CausesValidation="False" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstBoxNPI" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxNPI_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">TPI #:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlTPIWhere" runat="server">
                    <asp:ListItem Selected="True" Value="in">Equal to</asp:ListItem>
                    <asp:ListItem Value="not in">Not Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxTPI" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddTPI" runat="server" Text="Add TPI" OnClick="btnAddTPI_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13"></td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxTPI" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTPI_SelectedIndexChanged"></asp:ListBox>
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
                <asp:TextBox ID="txtBoxClaimStatus" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddClaimStatus" runat="server" Text="Add Claim Status" Width="109px" OnClick="btnAddClaimStatus_Click" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstBoxClaimStatus" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxClaimStatus_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
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
                <asp:ListBox ID="lstBoxModifier" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxModifier_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxProviderSpec" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxProviderSpec_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxProviderStatus" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxProviderStatus_SelectedIndexChanged"></asp:ListBox>
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
                <asp:TextBox ID="txtBoxExCode" runat="server" ></asp:TextBox>
                <asp:Button ID="btnAddEx" runat="server" OnClick="btnAddEx_Click" Text="Add EX Code" Width="96px" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstboxEx" runat="server" AutoPostBack="True" Height="68px" OnSelectedIndexChanged="lstboxEx_SelectedIndexChanged" Width="86px"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Ex Code:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlExCodeNInWhere" runat="server" Enabled="False">
                    <asp:ListItem Selected="True" Value="not in">Not Equal to</asp:ListItem>
                    <asp:ListItem Value="in">Equal to</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBoxExNiCode" runat="server" ></asp:TextBox>
                <asp:Button ID="btnAddExNi" runat="server" OnClick="btnAddExNi_Click" Text="Add EX Code" Width="96px" CausesValidation="False" />
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">
                <asp:ListBox ID="lstboxExNi" runat="server" AutoPostBack="True" Height="68px" OnSelectedIndexChanged="lstboxExNi_SelectedIndexChanged" Width="85px"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
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
                <asp:ListBox ID="lstBoxDiagnosis" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxDiagnosis_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxCPT" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxCPT_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxRevCode" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxRevCode_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxDRG" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxDRG_SelectedIndexChanged"></asp:ListBox>
            </td>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">Claim Type:</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlClaimTypeWhere0" runat="server" Enabled="False">
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
                <asp:ListBox ID="lstBoxTreatmentType" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxTreatmentType_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxBenefitPackage" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxBenefitPackage_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxHatCode" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxHatCode_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxRegion" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxRegion_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxCIIllness" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxCIIllness_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxPayServCode" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxPayServCode_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxPayClass" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxPayClass_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxExceptionFee" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxExceptionFee_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxOP" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxOP_SelectedIndexChanged"></asp:ListBox>
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
                <asp:ListBox ID="lstBoxFeeSchedule" runat="server" Height="50px" Width="91px" AutoPostBack="True" OnSelectedIndexChanged="lstBoxFeeSchedule_SelectedIndexChanged"></asp:ListBox>
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
    <asp:Label ID="Label3" runat="server" Text="Select columns for the report"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblCS" runat="server" Text="Column"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" Text="Columns selected for report - (Click to remove)"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    <asp:ListBox ID="lstbAllColumns" runat="server" Height="145px" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lstbCSTbl_SelectedIndexChanged" DataSourceID="dSourceAvailColumns" DataTextField="Text" DataValueField="Value">
    </asp:ListBox>
    <asp:SqlDataSource ID="dSourceAvailColumns" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="select 	'CH.Resolution' as Value,'Resolution' as Text "></asp:SqlDataSource>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ListBox ID="lstStdColumns" runat="server" AutoPostBack="True" Height="142px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" style="margin-top: 0px" DataSourceID="dSourceStdColumns" DataTextField="Text" DataValueField="Value"></asp:ListBox>
    <asp:SqlDataSource ID="dSourceStdColumns" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="select 	'CS.Aff#' as Value,'Aff#' as Text 
union all
select	'CS.Claim#', 'Claim#'
union all 
Select 'CS.Service#', 'Service#'
union all
Select    '(CS.AmtCharge  * .01) as AmtCharge','AmtCharge'  
union all 
Select    '(CS.AmtPay  * .01)  as AmtPay', 'AmtPay'
union all 
Select 'CS.PAID', 'Paid' 
union all 
Select 'CS.Prog#','Prog#' 
union all 
Select 'CS.Status','Status' 
union all 
Select 'CS.Location', 'Location'
union all 
Select 'CS.YMDEff', 'YMDEff'
union all 
Select 'CS.YMDEnd', 'YMDEnd'
union all 
Select 'CS.YMDDue', 'YMDDue'
union all 
Select 'CS.Authorization#', 'Authorization' 
union all 
Select 'CS.Proc#', 'Proc#'
union all 
Select 'CS.Proc2#', 'Proc2#'
union all 
Select 'CS.Modifier', 'Modifier'
union all 
Select 'CS.Modifier2', 'Modifier'
union all 
Select '(CS.[AmtAllow-P] * .01)  as Allowable', 'Allowable'
union all 
Select 'CS.Ex1', 'EX1'
union all 
Select 'CS.Ex2',  'EX2'
union all 
Select 'CS.Ex3',  'EX3'
union all 
Select 'CS.Ex4',  'EX4'
union all 
Select 'aff.IRS#', 'IRS#'
union all 
Select 'CS.OP#', 'OP#'
union all 
Select 'CS.[Business-Unit]', 'BusinessUnit' 
union all 
Select 'CS.[Claim-Type]', 'ClaimType'
union all 
Select 'aff.Spec1', 'Spec1' 
union all 
Select 'CS.[Count]', '[Count]'
union all 
Select 'CS.[Pay-Service]', 'Service'
union all 
Select 'CS.FeeSched', 'FeeSched'
union all 
Select 'CS.Region', 'Region'
union all 
Select 'aff.[Pay-Class]', 'PayClass'
union all 
Select 'CS.Diag#', 'Diag#'
union all 
Select 'CS.Benefit', 'Benefit'
union all 
Select 'CS.[Benefit-PKG]', 'BenefitPKG'
union all 
Select 'CS.Division#', 'Division#'
union all 
Select 'CS.YMDTrans', 'YMDTrans'
union all	
Select 'CS.YMDPaid','YMDPaid' 
union all
Select 'MH.Member#', 'Member'
union all 
Select 'MH.LastName', 'LastName'
union all 
Select 'MH.FirstName', 'FirstName'
union all 
Select 'MH.YMDBirth', 'YMDBirth'
union all 
Select 'ClaimsSql.dbo.GetAge(MH.YMDBirth, GETDATE()) AS CurrentAge', 'CurrentAge' 
union all
Select 'CH.[Patient-Ctl#]', 'PatientCtl#'
union all 
Select 'CH.DRG#', 'DRG#'
union all		 
Select '(CH.AmtCharge  * .01) as totalamountcharge','TotalAmountCharge'
union all 
Select 'aff.[Exception-Fee]', 'ExceptionFee'
union all   
Select 'aff.npi', 'NPI'"></asp:SqlDataSource>
            <asp:TextBox ID="txtBoxColumns" runat="server" Visible="False"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
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
    
</script>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>
    <asp:GridView ID="grvData" runat="server" Visible="False">
    </asp:GridView>
    <asp:Button ID="btnSearch" runat="server" OnClick="btnViewReport_Click" Text="Search" />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export to Excel" Visible="False" />
       
    </form>
</body>
</html>
