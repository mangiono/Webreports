<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstFillADHD.aspx.cs" Inherits="WebReports.FirstFillADHD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>First Fill ADHD</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblTitle" runat="server" Text="Please Browse\choose for 'ADHD Drugs' excel file - please ensure that the file is in 'xlsx' format. Once file path is loaded click 'Attach provider data' button."></asp:Label>
        <br />
        <br />
      <asp:FileUpload ID="FileUpload1" runat="server" Height="20px" Width="530px"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" Display="Dynamic" ErrorMessage="Please select a file to upload." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        <br />
    <asp:Button ID="btnImportExcel" runat="server" OnClick="Button1_Click" Text="Attach provider data" />
        <br />
        <asp:Label ID="lblmsgdone" runat="server" Text="Label" Visible="False" ForeColor="Green"></asp:Label>
        <br />
        <asp:Button ID="btndownload" runat="server" OnClick="btndownload_Click" Text="Export to excel" Visible="False" CausesValidation="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblOr" runat="server" Text="OR" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNewQuery" runat="server" OnClick="btnNewQuery_Click" Text="Process another file" Visible="False" />
        <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>
    </div>
    </form>
</body>
</html>

