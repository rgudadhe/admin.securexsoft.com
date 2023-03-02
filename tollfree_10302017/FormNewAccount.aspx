<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormNewAccount.aspx.vb" Inherits="FormNewAccount"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add Account</title>
   <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />

</head>
<body bgcolor="White">
    <form id="form1" runat="server">
	<div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
	<h1>Add Account for Toll-free</h1>
    <div>
    <table>
    <tr>
        <td colspan=2 style="height: 18px;" class="HeaderDiv"> Enter Account Details Below </td>
    </tr>
    <tr>
        <td> Account Name </td>
		<td style="width: 306px">
		<asp:DropDownList ID="ddaccname" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Width="320px"></asp:DropDownList>
		</td>
    </tr>
	<tr>
	<td> Account ID </td>
	<td style="width: 306px"><asp:TextBox ID="txtaccid" runat="server" Width="320px" Font-Names="Arial" Font-Size="9pt" ReadOnly="True"></asp:TextBox>
	</td>
	</tr>
	<tr>
	<td>Account Type</td>
	<td style="width: 306px"><asp:TextBox ID="txtacctype" runat="server" Width="320px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox>
	</td>
	</tr>
    <tr>
    <td colspan=2 style="text-align: center;" ><asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button"/></td>
    </tr>
    </table>
    </div>
	</div>
    </form>
</body>
</html>
