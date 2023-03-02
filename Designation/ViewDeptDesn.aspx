<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewDeptDesn.aspx.vb" Inherits="Department_Default" EnableViewState="true" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Designation Details</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />    
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />    

</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table width="50%">
            <tr>
                <td colspan="4" class="HeaderDiv">
                    Department Details</td>
            </tr>
          
            <tr>
                <td style="text-align: right">
                    Department</td>
                <td style="text-align: left">
                    <asp:TextBox ID="TxtDeptname" runat="server" Enabled="False" CssClass="common"></asp:TextBox></td>
            </tr>
              <tr>
                <td style="text-align: right">
                        *Designation</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesnName" runat="server" CssClass="common"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesnDesc" CssClass="common" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server"  Text="Submit" CssClass="button"  />
                    &nbsp
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button"  />
                </td> 
            </tr>
        </table>
        <br />
        <asp:Label ID="MsgDisp" runat="server" CssClass="common" ForeColor="Red" Height="16px" Width="496px"></asp:Label><br />
        <br />
        <br />
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtDeptName"
                    ErrorMessage="You must enter a value into Department Name" >
                    
</asp:RequiredFieldValidator>
        <br />
        <asp:HiddenField ID="HDesiID" runat="server" />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
