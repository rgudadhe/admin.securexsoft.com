<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormNewAccount.aspx.vb" Inherits="FormNewAccount"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<script type="text/javascript">
function Showalert() {
alert('Account ');
}
</script>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
        <td colspan=2 style="height: 18px; background-color:#000099">    
            <span style="font-size: 10pt; font-family: Arial; color: #ffffff;" ><strong>Enter Account Name Below</strong></span></td>
    </tr>
    <tr>
        <td>
            <span style="font-size: 10pt; font-family: Arial"><span style="font-size: 9pt">Account
                Name</span> </span>
        </td><td style="width: 306px"><asp:TextBox ID="TextBox1" runat="server" Width="320px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox></td>
    </tr>
	<tr>
	<td>
        <span style="font-size: 9pt; font-family: Arial">Account ID </span>
    </td><td style="width: 306px"><asp:TextBox ID="TextBox2" runat="server" Width="320px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox></td>
	</tr>
	<tr>
	<td>
        <span style="font-size: 10pt; font-family: Arial"><span style="font-size: 9pt">Account Type</span> </span>
    </td><td style="width: 306px"><asp:TextBox ID="TextBox3" runat="server" Width="320px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox></td>
	</tr>
    <tr>
    <td colspan=2><asp:Button ID="Button1" runat="server" Text="Submit" /></td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
