<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewDeptDesn.aspx.vb" Inherits="Department_Default" EnableViewState="true" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Designation</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New Designation</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
      
        <table  width="80%">
            <tr>
                <td colspan="4" class="HeaderDiv">
                    Department Details</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    
                        *Designation</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptName" CssClass="common" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Department</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="DeptName" runat="server" EnableViewState="true">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptDesc" CssClass="common" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" CssClass="button" runat="server"  Text="Submit" /></td>
            </tr>
        </table>
        <br />
          <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Medium"
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
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
