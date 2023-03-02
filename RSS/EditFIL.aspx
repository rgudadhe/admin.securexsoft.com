<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditFIL.aspx.vb" Inherits="RSS_EditFIL" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="tbl1" style="font:10pt; font-family:Trebuchet MS" cellpadding="1" cellspacing="1" border="1" width="100%">
        <%--<tr><td colspan="8" style="font-family: Verdana; font-size: 10pt;">File Name: </td></tr>
        <TR>
            <TD class="SMSelected">Dictation Code</TD>
            <TD class="SMSelected">Work Type</TD>
        </TR>
        <TR>
            <TD><asp:TextBox ID="txtCode" runat="server"></asp:TextBox></TD>
            <TD><asp:TextBox ID="txtWT" runat="server"></asp:TextBox></TD>
        </TR>--%>
        <tr>
            <td class="SMSelected" align="left">
               File Name : 
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtfilename" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align=center>
                <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Re-Import" />
            </td>
        </tr>
      </table>
    </div>
    <asp:HiddenField ID="hdnRecID" runat="server" />
        <asp:HiddenField ID="hdnMD5Value" runat="server" />
    </form>
</body>
</html>
