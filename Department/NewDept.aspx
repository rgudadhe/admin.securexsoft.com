<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewDept.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Department</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New Department</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <div>
        
        <table width="80%">
            <tr>
                <td colspan="4" style="text-align: center;" class="HeaderDiv">
                    Department Details</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    *Department Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptName" runat="server" CssClass="common"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptDesc" CssClass="common" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " class="alt1" colspan="4">
                    <asp:Button ID="Button1" runat="server" CssClass="button"  Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="MsgDisp" runat="server" CssClass="Title" ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
        <br />
        <br />
        <br />
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtDeptName"
                    ErrorMessage="You must enter a value into Department Name"  >
                    
</asp:RequiredFieldValidator>
    </div>
            </asp:Panel>
    
    </div> 
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
