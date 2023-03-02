<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormPrintTollFree1.aspx.vb" Inherits="FormPrintTollFree1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Toll Free Instruction</title>
<style>
 div {
     
    }
table{
text-align:center;
}
    .style2
    {
        color: #FFFFFF;
        font-weight: bold;
        font-size: medium;
    }
    .style3
    {
        font-size: small;
        font-weight: bold;
    }
</style>
</head>
<body>
<center>
    <form id="form1" runat="server">
      <div>
    <h2 style="font-family: Arial; text-align: center;">Toll Free Instruction</h2>
      <table>
    <tr align="center">
        <td align="center">
    <table style="font-family: Arial; border-color:Navy" border="1">
        <tr>
            <td colspan="4" style="background-color:Blue">
                <strong><span style="color: #ffffcc">Select Account from Partitions Below</span></strong></td>
        </tr>
        <tr style="font-size:small; font-weight:bold">
            <td colspan="2">HASH Partition <span style="font-size: 8pt">[866-239-1729 / 800-385-4418</span>]</td>
            <td colspan="2">No HASH Partition <span style="font-size: 8pt">[800-801-9270 / 866-890-5003]</span></td>
        </tr>
        <tr style="font-size:small; font-weight:bold">
            <td align="right">Account Name</td>
            <td>
                <asp:DropDownList ID="hashact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
            <td align="right">Account Name</td>
            <td><asp:DropDownList ID="nhashact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" Text="   Print   " /></td>
            <td colspan="2">
                <asp:Button ID="Button2" runat="server" Text="   Print   " /></td>
        </tr>
        <tr>
            <td colspan="4" style="background-color:Blue"><strong><span style="color: #ffffcc">Account wise Physician Details from Below</span></strong></td>
        </tr>
        <tr>
            <td colspan="2" style="font-size:small; font-weight:bold" align="right">Account Name</td>
            <td colspan="2" align="left"><asp:DropDownList ID="vact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="Button3" runat="server" Text="   View   " /></td>
             
        </tr>
       
        <tr>
            <td colspan="4" style="background-color:Blue" class="style2">
                Search ID</td>
             
        </tr>
       
        <tr>
            <td colspan="2" style="text-align: right" class="style3">
                Enter ID</td>
             
            <td colspan="2" align="left">
                <asp:TextBox ID="txtpid" runat="server" style="text-align: left"></asp:TextBox>
                                  </td>
             
        </tr>
       
        <tr>
            <td colspan="4">
                <asp:Button ID="Button4" runat="server" Text="Search" />
            </td>
             
        </tr>
       
    </table>
    </td>
    </tr>
    </table>
    </div>
    </form>
</center>
</body>
</html>
