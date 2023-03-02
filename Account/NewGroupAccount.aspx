<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewGroupAccount.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Group Account</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Group Account</h1>
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
    
      
        <table  width="80%">
            <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center">
                        New Group Account Form</td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>Account Name</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtAccountName" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>Description</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtDescription" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray">
                <td style="text-align: center; height: 26px;" colspan="4">
                    &nbsp;<asp:Button CssClass="button"  ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" UseSubmitBehavior="False" /></td>
            </tr>
        </table>
        <br />
          <asp:Label ID="MsgDisp" runat="server" CssClass="Title" 
            ForeColor="#C00000" Height="16px" Width="496px"></asp:Label>
        <br />
        
    
    </asp:Panel><br />
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtAccountName"
                ErrorMessage="Please enter Group Account Name" ></asp:RequiredFieldValidator>
                
            </asp:Panel>
            
    </div> 
    </div>     
<asp:RegularExpressionValidator 
    id="RegtDescription"  
    runat="server" 
    ControlToValidate="TxtDescription" 
    ValidationExpression="^[a-zA-Z-]+$"
    ErrorMessage="Description - Please enter valid input."
   />
<asp:RegularExpressionValidator 
    id="RegTxtAname"  
    runat="server" 
    ControlToValidate="TxtAccountName" 
    ValidationExpression="^[a-zA-Z-]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
            
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
    
</body>
</html>
