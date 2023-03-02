<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CategoryDet.aspx.vb" Inherits="CategoryDet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Category Details</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
        <asp:Label ID="MsgDisp" runat="server" ForeColor="#C00000" Height="16px" Width="496px" CssClass="Title"></asp:Label>
        <table width="100%">
            <tr>
                <td colspan="4" class="HeaderDiv">
                    Users Category Details</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    *Category Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtName" CssClass="common" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesc" CssClass="common" runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td style="text-align: right">
                    Prefix</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtPrefix" CssClass="common" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Submit" />
                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Delete" />
                    <asp:Button ID="Button3" runat="server" CssClass="button" Text="Close Window" OnClientClick="window.close();window.opener.location.reload();"  />
                    <asp:HiddenField ID="CategoryID" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        
    <asp:RequiredFieldValidator  Display="None" id="valRequired" runat="server" ControlToValidate="TxtName"
                    ErrorMessage="You must enter a value into Category Name">
                    
</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator  Display="None" id="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPrefix"
                    ErrorMessage="You must enter prefix">
                    
</asp:RequiredFieldValidator>
    </div>
        </asp:Panel>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
