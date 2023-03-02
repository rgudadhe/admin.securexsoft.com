<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePass.aspx.vb" Inherits="ChangePass" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>Reset Password</title>
   
</head>
<body>
    <form id="form1" runat="server">
    
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <div id="body">
    <div id="cap"></div>
    <div id="main" style="margin-top:2px; " >
    <h1>Edit Password</h1>
        <table width="400">
        <tr><td colspan="2" style="text-align:center;" class="HeaderDiv"  >
            Edit Password</td></tr>
        <tr><td>Old Password</td>
        <td><asp:TextBox ID="TxtOPass" TextMode="Password"   runat="server"></asp:TextBox></td></tr>
        <tr class="alt1" ><td>New Password</td><td><asp:TextBox ID="TxtNPass" TextMode="Password" runat="server"></asp:TextBox> </td></tr>
        <tr><td>Confirm Password</td><td><asp:TextBox ID="TxtCPass"  TextMode="Password" runat="server"></asp:TextBox>
        </td></tr>
        <tr><td colspan="2" style="text-align:center;" >
            <asp:Button ID="Button1" runat="server" CssClass="button"  Text="Submit" /></td></tr>
        </table>
        <div style="text-align:left; " >
        <asp:Label ID="lblMsg" runat="server" ></asp:Label><br />
        <asp:RequiredFieldValidator  Display="None"   ID="RequiredFieldValidator2" runat="server" ErrorMessage="Old Password is required " ControlToValidate="TxtOPass"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None"  ID="RequiredFieldValidator1" runat="server" ErrorMessage="New Password is required " ControlToValidate="TxtNPass"></asp:RequiredFieldValidator><br />
        <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" ControlToValidate="TxtNPass" runat="server" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$#$!%*?&])[A-Za-z\d$@$#$!%*?&]{8,}"  ErrorMessage="Password must be 8 character long with one upper, one special and alphanumeric."></asp:RegularExpressionValidator><br />
        <asp:RequiredFieldValidator  Display="None"   ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm Password is required " ControlToValidate="TxtCPass"></asp:RequiredFieldValidator>
        
       </div> 
    </div>
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
