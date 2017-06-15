<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="listboxclientside.aspx.cs" Inherits="WebReports.listboxclientside" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        <br />
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <form id="form1" runat="server">
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <div>
<input id="MyTextbox" type="text" />
<input type="button" value="Add Item" onclick="javascript:addNewItem()" />
<input type="button" value="Remove Item" onclick="javascript: pullItem()" />
<br />
<br />
<select multiple="true" id="MyListbox" name="MyListbox"  runat ="server" onclick="deleteItem()">
</select>
    </div>
<script type="text/javascript">
 function addNewItem() {
        var textbox = document.getElementById('MyTextbox');
        var listbox = document.getElementById('MyListbox');
        var newOption = document.createElement('option');
        newOption.value = textbox.value; // The value that this option will have
        newOption.innerHTML = textbox.value; // The displayed text inside of the <option> tags
        listbox.appendChild(newOption);
        textbox.value = "";
    }
    function deleteItem() {
        var listbox = document.getElementById('MyListbox');
        if (listbox.selectedIndex != -1) {
            listbox.remove(listbox.selectedIndex);
        }
    }
    function pullItem() {
        var listbox = document.getElementById('MyListbox');
        if (listbox.selectedIndex != -1) {
            textbox.value = "dfsdfsd";
        }
        var textbox = document.getElementById('MyTextbox');
        textbox.value = "dfsdfsd";
    }

</script>

    </form>
</body>
</html>
