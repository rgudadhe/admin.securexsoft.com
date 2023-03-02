<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormAddPhysician.aspx.vb" Inherits="FormAddPhysician" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add a Physician</title>
	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body style="font-size: 12pt">
      <form id="form1" runat="server">
	    <div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
   <h1>Add new Physician</h1>
    <div>
    <table width="45%">
                <tr>
                <td colspan="2" style="text-align: center; height: 15px;" class="HeaderDiv" ">
                    Enter Details below</td>
                </tr>
                <tr>
                <td>Account Name</td><td>
                    <asp:DropDownList ID="acct" runat="server" AppendDataBoundItems="True" AutoPostBack="true">
                    </asp:DropDownList></td>
                </tr>
                <tr>
                <td>Dictator Last Name</td><td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Dictator First Name</td><td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Keypad</td><td>
                    <asp:DropDownList ID="kpad" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList></td>
                </tr>
                <tr>
                <td>ID</td><td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Password</td><td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Customer Type</td><td>
                    <asp:DropDownList ID="cust" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList></td>
                </tr>
        <tr>
            <td>Customer ID</td>
            <td>
                <asp:TextBox ID="txtcustid" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Partition</td>
            <td>
                 <asp:DropDownList ID="listpartition" runat="server" AppendDataBoundItems="True" AutoPostBack="true">
                 </asp:DropDownList>
                        </td>
        </tr>
        <tr>
            <td>Partition No</td>
            <td>
                 <asp:TextBox ID="txtpno" runat="server"></asp:TextBox>
                        </td>
        </tr>
                <tr>
                <td colspan=2 align="centre"> 
                    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button" /></td>
                               </tr>    
    </table>
    </div>
    </form>
</body>
</html>
