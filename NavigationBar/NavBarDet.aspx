<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NavBarDet.aspx.vb" Inherits="CategoryDet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Navigation Bar</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
        <asp:Label ID="MsgDisp" runat="server" ForeColor="#C00000" CssClass="Title" Width="496px"></asp:Label>
        <table width="100%">
            <tr>
                <td colspan="2" class="alt1">
                    Navigation Details</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    *Navigation Bar Details
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtNavBar" CssClass="common" runat="server"></asp:TextBox>
                    </td>
            </tr>
            
              
            <tr>
                <td style="text-align: center; " colspan="2">
                    <center>
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Submit" />
                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Delete" />
                    <asp:Button ID="Button3" runat="server" CssClass="button" Text="Close Window" OnClientClick="window.close();window.opener.location.reload();"  />
                    </center>
                    <asp:HiddenField ID="CategoryID" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtNavBar"
                    ErrorMessage="You must enter a value into Navigation Bar">
</asp:RequiredFieldValidator>
   
    </div>
        </asp:Panel>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
