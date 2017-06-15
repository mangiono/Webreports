<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="claimsaff.aspx.cs" Inherits="WebReports.claimsaff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:ListBox runat="server" ID="myListbox" Rows="10" Width="25%" data-bind="options: elements">
    </asp:ListBox>
    <br />
    <asp:TextBox runat="server" ID="newElement"></asp:TextBox>
    <input type="button" id="addElement" value="Add element" data-bind="click: addElement" />
    </div>
    </form>

<script type="text/javascript" src="Scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="Scripts/knockout-2.1.0.js"></script>
<script type="text/javascript">
    $(function () {
        var model = {
            elements: ko.observableArray(),
            addElement: function () {
                this.elements.push($("#<%= this.newElement.ClientID %>").val());
            }
        };

        ko.applyBindings(model);
    });

</script>
</body>
</html>
