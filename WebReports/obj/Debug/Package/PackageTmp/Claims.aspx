<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Claims.aspx.cs" Inherits="WebReports.Claims" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
 
</style>

</head>


<body>
    <form id="form1" runat="server">
<asp:ListBox ID="ListBox1" runat="server" Width="150" Height="60" onchange="YourChangeEventJS(this)" ></asp:ListBox>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button2" runat="server" Text="GridDisplay"  OnClick="Button2_Click"/>
                        <asp:GridView ID="grvData" runat="server"></asp:GridView>
                </ContentTemplate>
            <Triggers>
       <%--// If button is present outside update panel then specify AsynPostBackTrigger--%>
       <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" /> 
    </Triggers>
        </asp:UpdatePanel>
<br />
        <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
<div class="progress">
    <img src="loader.gif" />&nbsp;please wait...
</div>
</ProgressTemplate>

        </asp:UpdateProgress>
<hr />
<asp:TextBox ID="txtValue" runat="server" />
<asp:Button ID="btnAdd" Text="Add" runat="server" OnClientClick="return AddValues()" />

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

    function YourChangeEventJS(ddl) {
        ddl.remove(ddl.selectedIndex);
        //alert(ddl.selectedIndex);
    }
   
    
</script>


   
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        
  


    </form>
</body>


</html>
