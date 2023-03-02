<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReLogin.aspx.vb" Inherits="Login_Login" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel='stylesheet' type='text/css' href="../Main.css"/>
    <title>Untitled Page</title>
</head>
<body>
<form id="form1" runat="server"  > 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
            <tr>
                <td colspan="3" style="background-image:url(../images/settings.jpg); height:30px; text-align: left;">
                    <span style="font-size: 10pt; font-family: Trebuchet MS"><strong>Login Page<span
                        style="font-size: 12pt"><span style="font-family: Times New Roman">&nbsp;</span></span></strong></span></td>
            </tr>
    </table>
        <asp:Table ID="Table1" runat="server" GridLines="Both"
            Width="100%" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="DimGray" HorizontalAlign="Left">
            <asp:TableRow ID="TableRow1" runat="server" cssclass="setting">
                <asp:TableCell ID="TableCell1" runat="server"><asp:Label ID="Label1" runat="server" Text="Username"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server"><asp:TextBox ID="name"  runat="server"  Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server" cssclass="setting">
                <asp:TableCell ID="TableCell3" runat="server"><asp:Label ID="Label3" runat="server" Text="Password"></asp:Label></asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server"><asp:TextBox ID="password" TextMode="password"  runat="server"  Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" cssclass="setting">
                <asp:TableCell ID="TableCell5" runat="server" ColumnSpan="2"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/submit.jpg" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
         <asp:Label ID="lblMessage" runat="server" />
<asp:RequiredFieldValidator runat="server" ID="UNReq"
            ControlToValidate="name"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />Username is required." />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE"
            TargetControlID="UNReq"
            HighlightCssClass="validatorCalloutHighlight" />
            
            <asp:RequiredFieldValidator runat="server" ID="PWReq"
            ControlToValidate="Password"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />Password is required." />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
            TargetControlID="PWReq"
            HighlightCssClass="validatorCalloutHighlight" />
</form>


 
            
</body>
</html>
