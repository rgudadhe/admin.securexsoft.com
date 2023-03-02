<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormPrintTollFree.aspx.vb" Inherits="FormPrintTollFree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Toll Free Instruction</title>

	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
<center>
    <form id="form1" runat="server">
	<div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
    <div>
   <h1>Toll Free Instruction</h1>
   <table width="95%">
        <tr>
            <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv">
                <strong><span style="color: Black">Select Account from Partitions Below</span></strong></td>
        </tr>
        <tr style="font-size:smaller; font-weight:bold; text-align: center;">
            <td colspan="2" style="text-align: center; height: 15px; font-weight:bold;">HASH Partition <span style="font-size: 8pt">[866-239-1729 / 800-385-4418</span>]</td>
            <td colspan="2" style="text-align: center; height: 15px; font-weight:bold">No HASH Partition <span style="font-size: 8pt">[800-801-9270 / 866-890-5003]</span></td>
        </tr>
        <tr style="font-size:smaller; font-weight:bold">
            <td align="right" style="text-align: right; height: 15px;">Account Name</td>
            <td>
                <asp:DropDownList ID="hashact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
            <td style="text-align: right; height: 15px;">Account Name</td>
            <td><asp:DropDownList ID="nhashact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Button ID="Button1" runat="server" Text="   Print   " CssClass="button" /></td>
            <td colspan="2" style="text-align: center;">
                <asp:Button ID="Button2" runat="server" Text="   Print   " CssClass="button" /></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv"><strong><span style="color: black">Account wise Physician Details from Below</span></strong></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right; height: 15px;">Account Name</td>
            <td colspan="2" align="left"><asp:DropDownList ID="vact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="Button3" runat="server" Text="   View   " CssClass="button"/></td>
             
        </tr>
       
        <tr>
            <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv"><strong><span style="color: black">
                Search ID</span></strong></td>
             
        </tr>
       
        <tr>
            <td colspan="2" style="text-align: right; height: 15px;">
                Enter ID</td>
             
            <td colspan="2" align="left">
                <asp:TextBox ID="txtpid" runat="server" style="text-align: left"></asp:TextBox>
                                  </td>
             
        </tr>
       
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="Button4" runat="server" Text="Search" CssClass="button" />
            </td>
             
        </tr>
       
    </table>
    
    </div>
	</div>
    </form>
</center>
</body>
</html>
