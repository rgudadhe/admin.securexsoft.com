<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewCategory.aspx.vb" Inherits="Department_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New Account Category</h1>
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
        <table width="80%">
            <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center">
                   Category Details</td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    Category</td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtCategory" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                    Priority</td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtPriority" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; height: 26px;" colspan="4">
                    <asp:Button ID="Button1" runat="server"  Text="Submit" CssClass="button" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label ID="MsgDisp" runat="server" CssClass="Title"
            ForeColor="#C00000" Height="16px" Width="496px"></asp:Label>
        <br />
        <br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" ControlToValidate="TxtCategory" runat="server" ErrorMessage="Category field is a must " ></asp:RequiredFieldValidator>&nbsp;<br />
         <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" ControlToValidate="TxtPriority" runat="server" ErrorMessage="Priority field is a must " ></asp:RequiredFieldValidator>&nbsp;<br />        
    
    </asp:Panel>
        &nbsp;&nbsp;
        </div> 
        </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
    
</body>
</html>
