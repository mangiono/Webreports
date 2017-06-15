<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportData_Dev.aspx.cs" Inherits="WebReports.ImportData_Dev" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import Excel file in database</title>


    <script type ="text/javascript">
        //var validFilesTypes=["xlsx","xlsx"];
        var validFilesTypes = ["xlsx"];
    function ValidateFile()
    {
      var file = document.getElementById("<%=FileUpload1.ClientID%>");
      

       
        var path = file.value;
      var ext=path.substring(path.lastIndexOf(".")+1,path.length).toLowerCase();
      var isValidFile = false;
      for (var i=0; i<validFilesTypes.length; i++)
      {
        if (ext==validFilesTypes[i])
        {
           
            isValidFile = true;
            
            break;
        }
      }

     
      if (!isValidFile)
      {
          file.value = null;

          alert("Invalid file, please choose following extension(s) :\n\n" + validFilesTypes.join(", "));
        //label.style.color="red";
        //label.innerHTML="Invalid File. Please upload a File with" +
        // " extension:\n\n" + validFilesTypes.join(", ");
        
    var fileUpload = document.getElementById("<%=FileUpload1.ClientID %>");
    var id = fileUpload.id;
    var name = fileUpload.name;
 
    //Create a new FileUpload element.
    var newFileUpload = document.createElement("INPUT");
    newFileUpload.type = "FILE";
 
    //Append it next to the original FileUpload.
    fileUpload.parentNode.insertBefore(newFileUpload, fileUpload.nextSibling);
 
    //Remove the original FileUpload.
    fileUpload.parentNode.removeChild(fileUpload);
   
    newFileUpload.style['width'] = '530px';
    //Set the Id and Name to the new FileUpload.
    newFileUpload.id = id;
    newFileUpload.name = name;
   
    
    return false;

      }
        
        return isValidFile;
        
    }
       

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        IMPORT xlsx excel worksheet into database table.<br />
        <br />
        <asp:Panel ID="pnlMain" runat="server">
        <asp:Label ID="lblTitle" runat="server" Text="Ensure excel file has (unique) headers and that it's an xlsx file type (xls not supported)."></asp:Label>
            <asp:TextBox ID="txtBoxHidden" runat="server" Visible="False"></asp:TextBox>
            <br />
        <br />
        Browse\choose excel xlsx file to import: <asp:FileUpload ID="FileUpload1"  runat="server" Height="20px" Width="530px"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" Display="Dynamic" ErrorMessage="Please choose an excel file." ForeColor="Red" SetFocusOnError="True" Font-Bold="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1" Display="Dynamic" ErrorMessage="Only xlsx files supported." Font-Bold="True" ForeColor="Red" SetFocusOnError="True" ValidationExpression="^.+(.xlsx|.XLSX)$"></asp:RegularExpressionValidator>
        <br />
            <br />
           Choose DB (only 3 for now because of log in account permissions):&nbsp;
            <asp:DropDownList ID="ddlDbase" runat="server">
                <asp:ListItem>Claims_Reporting</asp:ListItem>
                <asp:ListItem>Intranet2012</asp:ListItem>
                <asp:ListItem>StateReports</asp:ListItem>
            </asp:DropDownList>
            <br />
            <%--OnChange="return ValidateFile()"--%>
            <br />
            <asp:Label ID="lblTableName" runat="server" Text="Name of table to be created in database?"></asp:Label>
            <asp:TextBox ID="txtTableName" runat="server" Width="200px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="rqTblName" runat="server" ControlToValidate="txtTableName" Display="Dynamic" ErrorMessage="Please supply a table name." Font-Bold="True" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lblIfExists" runat="server" Text="Select table options:"></asp:Label>
            <asp:RequiredFieldValidator ID="rqftbl" runat="server" ControlToValidate="rbtntblList" ErrorMessage="Choose one of the options." Font-Bold="True" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RadioButtonList ID="rbtntblList" runat="server" OnSelectedIndexChanged="rbtntblList_SelectedIndexChanged">
                <asp:ListItem Value="0">If table exists or not, drop\create the table irrespective.</asp:ListItem>
                <asp:ListItem Value="1">If table does exists, cancel import altogether.</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnChooseSheet" runat="server" OnClick="btnChooseSheet_Click" Text="Choose sheet to import" />
        <br />
            <asp:Label ID="lblChooseSheet" runat="server" Text="Please choose which sheet to import" Visible="False"></asp:Label>
        <br />
            &nbsp;<asp:ListBox ID="lslBoxSheets" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lslBoxSheets_SelectedIndexChanged" Visible="False" Height="103px"></asp:ListBox>
            <asp:Button ID="btnImportExcel" runat="server" CausesValidation="False" OnClick="Button1_Click" Text="Import first sheet" Visible="False" />
    </asp:Panel>
        <asp:Panel ID="pnlResults" runat="server" Visible="False">
            <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New import\Back to home" />
            <br />
            <br />
            <asp:Label ID="lblmsgdone" runat="server" Font-Bold="True" ForeColor="Green" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtSQL" runat="server" BorderColor="White" BorderStyle="None" Width="700px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnViewData" runat="server" CausesValidation="False" OnClick="btnViewData_Click" Text="View imported data on screen" Visible="False" />
        <asp:Label ID="lblrows" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btndownload" runat="server" OnClick="btndownload_Click" Text="Export to excel" Visible="False" CausesValidation="False" />
        <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
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
            </div>
        
    </form>

</body>
</html>

