<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewNavBar.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Navigation Bar</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New Navigation Bar</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">

        <table width="300">
            <tr>
                <td class="HeaderDiv" colspan="2">
                    Add New Navigation Bar
                </td>
            </tr>
            <tr>
                <td >
                    *Navigation Bar Details
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtNavBar" runat="server" CssClass="common"></asp:TextBox>
                    </td>
            </tr>
             
            
            <tr>
                <td style="text-align: center; " colspan="2">
                    <center>
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Submit" /></center></td>
            </tr>
        </table>
        <br />
        <br />
        <div style="text-align:left">
        
                <asp:Label ID="MsgDisp" runat="server" CssClass="Title"
            ForeColor="#C00000" Width="496px"></asp:Label>
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtNavBar"
                    ErrorMessage="You must enter a value into Field Name">
                    
</asp:RequiredFieldValidator>
</div>
        </asp:Panel>
    </div> 
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
