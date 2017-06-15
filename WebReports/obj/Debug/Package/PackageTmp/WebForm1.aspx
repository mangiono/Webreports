<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebReports.WebForm1" %>

<!DOCTYPE html>

<html> 
<head> 
    
<title>Adding value to listbox from textbox- Anyforum</title> 
    <style type="text/css">
        /*#Background 
{
    position:fixed;
    top:0px;
    bottom:-32px;
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
    height:50px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
        }*/
    </style>
</head> 
<body> 
<script lang="javascript" type="text/javascript"> 

//$(document).ready(function () {
//    $('#Button2').click(function () {
//        $('.blockMe').block({
//            message: 'Please wait...<br /><img src="loader.gif" />',
//            css: { padding: '10px' }
//        });
//    });
//});


    function btnOnClick(){
        var validated = Page_ClientValidate('All');
        if (validated) {
            //return confirm('Are you sure you want...'); 
        }
    }


</script> 

    <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    dsdsdsdsdsdsdsdsd<br />
   <asp:TextBox ID="txtBoxAff" runat="server"></asp:TextBox>
            &nbsp;<asp:Button ID="btnAddAff" runat="server" Text="Add Aff" OnClick="btnAddAff_Click" CausesValidation="False" />
                <asp:ListBox ID="lstBoxAff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstBoxAff_SelectedIndexChanged"></asp:ListBox>
    <br />
    <br />
    <br />
    <br />
<input name="txtValue" type="text" /> 
<input type="button" name="add" value="Add" onclick="addValue();" /> 
<select name="lstValue" multiple> 
<option value="empty"></select> 
<input type="button" name="delete" value="Delete" onclick="deleteValue();" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBoxAff" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ValidationGroup="All"></asp:RequiredFieldValidator>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
                    </ContentTemplate>
    </asp:UpdatePanel>
        <asp:Button ID="Button1" runat="server" ClickOnce="true" Text="Bind Grid View" 
            ClientIDMode="Static" OnClick="Button1_Click" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
<ProgressTemplate>
<div id="Background"></div>
<div id="Progress">
<img src="loader.gif" style="vertical-align:middle"/>
Fetching Records Please Wait...
</div>
</ProgressTemplate>
</asp:UpdateProgress>
</ContentTemplate>
</asp:UpdatePanel>
        
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId" DataSourceID="SqlDataSource1" Visible="False">
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="UserId" InsertVisible="False" ReadOnly="True" SortExpression="UserId" />
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" />
                <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" SortExpression="LastLoginDate" />
                <asp:BoundField DataField="RoleId" HeaderText="RoleId" SortExpression="RoleId" />
                <asp:BoundField DataField="Salt" HeaderText="Salt" SortExpression="Salt" />
                <asp:BoundField DataField="TelNumber" HeaderText="TelNumber" SortExpression="TelNumber" />
            </Columns>
        </asp:GridView>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Intranet2012ConnectionString %>" SelectCommand="SELECT * FROM [Non_Par_users]"></asp:SqlDataSource>

    </form>
</body> 
</html> 