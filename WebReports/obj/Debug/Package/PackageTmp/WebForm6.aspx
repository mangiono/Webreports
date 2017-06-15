<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="WebApplication7.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $(document).ajaxStart(function () {
                $("#loadingDiv").css("display", "block");
            });
            $(document).ajaxComplete(function () {
                $("#loadingDiv").css("display", "none");
            });
            $("button").click(function () {
                $("#txt").load("AjaxPageLoad.aspx");
                $("#btnClick").css("display", "none");
                return false;
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="txt">
            <h2>Let AJAX change this text</h2>
            <br /><br />
        </div>
        <button id="btnClick">Change Content</button>
        <div id="loadingDiv" style="display: none; width: 100px; height: 40px; border: 1px solid black; position: absolute; top: 50%; left: 50%; padding: 0px;">
           <b><h3>Loading..</h3></b> 
        </div>
    </form>
    <table align="left">
        <tr>
            <td colspan="5">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
            <td>label in here</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body>
</html>
