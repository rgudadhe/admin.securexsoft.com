<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewCategory.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Category</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New Category</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <div>
        
        <table  width="80%" >
            <tr>
                <td colspan="4" class="HeaderDiv">
                    Users Category</td>
            </tr>
            <tr>
                <td style="text-align: right">
                        Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtName" runat="server" CssClass="common"></asp:TextBox>
                    </td>
            </tr>
              <tr>
                <td style="text-align: right">
                    Prefix</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtPrefix" runat="server" CssClass="common"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesc" runat="server" CssClass="common"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4" class="alt1">
                    <asp:Button ID="Button1" CssClass="button" runat="server"  Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="MsgDisp" runat="server" CssClass="Title"
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
        <br />
        <br />
        <br />
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtName" 
                    ErrorMessage="You must enter a value into Category Name" >
</asp:RequiredFieldValidator><br />
<asp:RequiredFieldValidator  Display="None" id="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPrefix" 
                    ErrorMessage="You must enter prefix" >
</asp:RequiredFieldValidator>
    </div>
            </asp:Panel>
    
    </div> 
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
