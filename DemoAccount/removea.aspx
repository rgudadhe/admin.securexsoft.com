<%@ Page Language="VB" AutoEventWireup="false" CodeFile="removea.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Remove Demo Field</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

<script language="javascript" type="text/javascript">
// <!CDATA[

function IMG1_onclick() {
 self.close();
}

// ]]>
</script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <table id="MainTable">
            <tr>
                <td class="HeaderDiv">
                    Demo Configuration</td>
            </tr>
            <tr>
                <td style="text-align: center" valign="top">
                    <strong>
                        <asp:Label ID="Label1" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label></strong></td>
            </tr>
           
        </table>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Close Window" CssClass="button" onClientClick="return IMG1_onclick()" />
    </form>
</body>
</html>
