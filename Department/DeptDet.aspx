<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DeptDet.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Department Details</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Label ID="MsgDisp" runat="server" CssClass="Title" ForeColor="#C00000" Height="16px" Width="496px"></asp:Label>
        <table width="100%">
            <tr>
                <td colspan="4" class="HeaderDiv">
                    Department Details</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    *Department Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptName" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptDesc" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Submit" />
                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Delete" />
                    <asp:Button ID="Button3" runat="server" CssClass="button" Text="Close Window" OnClientClick="window.close();window.opener.location.reload();"  />
                    <asp:HiddenField ID="DeptID" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtDeptName"
                    ErrorMessage="You must enter a value into Department Name"  >
</asp:RequiredFieldValidator>
        </asp:Panel>
        
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
