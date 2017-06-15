<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sandbox.aspx.cs" Inherits="WebReports.sandbox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="text" id="txtBox1"></input>
<span id="label1">Hello There!</span> 
    </div>
    </form>

    <script>
        $('#txtBox1').focus(function(){
             $('#label1').hide();
        });
</script>
</body>
</html>
