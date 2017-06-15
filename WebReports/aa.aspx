<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aa.aspx.cs" Inherits="WebReports.aa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    <asp:ListBox ID="ListBox1" name ="ListBox1" runat="server" Width="150" Height="60"></asp:ListBox>
<br />
<hr />
<asp:TextBox ID="txtValue" runat="server" />
<asp:Button ID="btnAdd" Text="Add" runat="server" OnClientClick="return AddValues()" />
    </div>

        <script type="text/javascript">
function AddValues() {
    var txtValue = document.getElementById("<%=txtValue.ClientID %>");
    var listBox = document.getElementById("<%= ListBox1.ClientID%>");
    var option = document.createElement("OPTION");
    option.innerHTML = txtValue.value;
    option.value = txtValue.value;
    listBox.appendChild(option);
    txtValue.value = "";
    return false;
}
</script>
    </form>
</body>
</html>
