<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormAddPhysician.aspx.vb" Inherits="FormAddPhysician" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add a Physician</title>
</head>
<body style="font-size: 12pt">
      <form id="form1" runat="server">
    <div>
    <h2>
        <span style="font-family: Arial">Add new Physician</span></h2>
    <table border="1" style="font-family:Arial">
                <tr>
                <td colspan="2" style="background-color:Navy">
                    <strong><span style="font-size: 10pt; color: #ffffff;">Enter Details below</span></strong></td>
                </tr>
                <tr>
                <td style="font-size:small">Account Name</td><td style="height: 24px">
                    <asp:DropDownList ID="acct" runat="server" AppendDataBoundItems="True" AutoPostBack="true">
                    </asp:DropDownList></td>
                </tr>
                <tr>
                <td style="font-size:small">Dictator Last Name</td><td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="font-size:small">Dictator First Name</td><td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="font-size:small">Keypad</td><td>
                    <asp:DropDownList ID="kpad" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList></td>
                </tr>
                <tr>
                <td style="font-size:small">ID</td><td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="font-size:small">Password</td><td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td style="font-size:small">Customer Type</td><td>
                    <asp:DropDownList ID="cust" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList></td>
                </tr>
        <tr>
            <td align="right" style="text-align: left">
                <span style="font-size: 10pt">Customer ID</span></td>
            <td>
                <asp:TextBox ID="txtcustid" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="text-align: left">
                <span style="font-size: 10pt">Partition</td>
            <td>
                 <asp:DropDownList ID="listpartition" runat="server" AppendDataBoundItems="True" AutoPostBack="true">
                 </asp:DropDownList>
                        </td>
        </tr>
        <tr>
            <td align="right" style="text-align: left">
                <span style="font-size: 10pt">Partition No</td>
            <td>
                 <asp:TextBox ID="txtpno" runat="server"></asp:TextBox>
                        </td>
        </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="Submit" runat="server" Text="Submit" /></td>
                 <td>
                     <asp:Button ID="Reset" runat="server" Text="Reset"  /></td>
                </tr>    
    </table>
    </div>
    </form>
</body>
</html>
