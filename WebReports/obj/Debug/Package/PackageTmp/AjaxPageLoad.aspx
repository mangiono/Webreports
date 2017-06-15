<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxPageLoad.aspx.cs" Inherits="WebApplication7.Ajax.AjaxPageLoad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form  runat="server" enctype="multipart/form-data" >
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <button type="submit"><i class="fa fa-envelope-o"></i></button>
    </div>
    </form>
</body>
</html>
