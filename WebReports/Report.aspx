<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="WebReports.Report" %>

<!DOCTYPE html>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

<title>Untitled Page</title>

<script lang="javascript" type="text/javascript">

 

function fnMoveItems(lstbxFrom,lstbxTo,type)
{

var varFromBox = document.all(lstbxFrom);var varToBox = document.all(lstbxTo);
 

var hidField = document.getElementById("<%=hfSelectedValues.ClientID%>");
 

if ((varFromBox != null) && (varToBox != null))
{

if(varFromBox.length < 1)
{

alert('There are no items in the source ListBox');

return false;
}

if(varFromBox.options.selectedIndex == -1) // when no Item is selected the index will be -1

{

alert('Please select an Item to move');return false;
}

while ( varFromBox.options.selectedIndex >= 0 )
{

var newOption = new Option(); // Create a new instance of ListItem

newOption.text = varFromBox.options[varFromBox.options.selectedIndex].text;

newOption.value = varFromBox.options[varFromBox.options.selectedIndex].value;

varToBox.options[varToBox.length] = newOption; //Append the item in Target Listbox

if(hidField != null)
{

if(type)
{

hidField.value += newOption.value + ",";
}

else

{

hidField.value = hidField.value.replace(newOption.value + ",","");
}

}

varFromBox.remove(varFromBox.options.selectedIndex); //Remove the item from Source Listbox

}

}

return false;
}

function fnMoveAll(lstFrom,lstTo,type)
{

var varFromBox = document.all(lstFrom);var varToBox = document.all(lstTo);
 

var hidField = document.getElementById("<%=hfSelectedValues.ClientID%>");
 

if ((varFromBox != null) && (varToBox != null))
{

if(varFromBox.length < 1)
{

alert('There are no items in the source ListBox');

return false;
}

 

while(varFromBox.length >= 0)
{

var newOption = new Option(); // Create a new instance of ListItem

newOption.text = varFromBox.options[0].text;

newOption.value = varFromBox.options[0].value;

varToBox.options[varToBox.length] = newOption; //Append the item in Target Listbox

varFromBox.remove(varFromBox.options[0]); //Remove the item from Source Listbox

if(type)
{

hidField.value += newOption.value + ",";
}

else

{

hidField.value = hidField.value.replace(newOption.value + ",","");
}

}

}

 

return false;
}

 

</script>
</head>

 

<body>

<form id="form1" runat="server">

<div>

<table >

<tr>

<td>

<asp:HiddenField ID="hfSelectedValues" runat="server" />

<asp:ListBox ID="lstRoles" runat="server" SelectionMode="Multiple" Height="232px" Width="138px">

<asp:ListItem Value="ramesh"></asp:ListItem>

<asp:ListItem Value ="sundar"></asp:ListItem>

<asp:ListItem Value ="seetharaman"></asp:ListItem>

</asp:ListBox>

 

</td>

<td style="text-align: center">

<input id="hbtnAdd" style="width: 31px" type="button" value=" >" lang="javascript" onclick="fnMoveItems('lstRoles','lstSelectedRoles',true)" /><br /><br />

<input id="hbtnAddText" style="width: 31px" type="button" value="Text" lang="javascript" onclick="fnMoveItems('lstRoles','lstSelectedRoles',true)" /><br /><br />

<input id="hbtnAddAll" style="width: 31px" type="button" value=" >>" onclick="fnMoveAll('lstRoles','lstSelectedRoles',true)" /><br /><br />

<input id="hbtnRemove" style="width: 31px" type="button" value="< " lang="javascript" onclick="fnMoveItems('lstSelectedRoles','lstRoles',false)" /><br /><br />

<input id="hbtnRemoveAll" type="button" value="<< " lang="javascript" onclick="fnMoveAll('lstSelectedRoles','lstRoles',false)" /><br /><br />

</td>

<td>

<asp:ListBox ID="lstSelectedRoles" runat="server" SelectionMode="Multiple" Height="232px" Width="138px"></asp:ListBox>

</td>

</tr>

<tr>

<td align="right" colspan="3">

<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="javascript:return fnAlertSelected()" />

</td>

</tr>

</table>

</div>

</form>
</body>

</html>