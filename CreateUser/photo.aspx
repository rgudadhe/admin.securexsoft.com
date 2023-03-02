<%@ Page Language="VB" AutoEventWireup="false" CodeFile="photo.aspx.vb" Inherits="photo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../Images/Style.css"rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
</head>
<body style="text-align: left">
    <form id="form1" runat="server">
    <div>
    <table width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; ">
           
                    <tr>
                        <td colspan="4" style="height: 24px; text-align: center" >
                        <span style="color: darkorange"><strong>Upload Files</strong></span></td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;  text-align: right;">
                        <span>Submit Photo</span></td>
                    <td colspan="3" style=" text-align: left">
                        <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                </tr>
           <tr>
            <td colspan="4" style="text-align: center">
                &nbsp;<asp:Button ID="Button4" runat="server" CssClass="button" Text="Submit" /></td>
            </tr>
            </table> 
        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Trebuchet MS"></asp:Label>
    </div>
        <asp:HiddenField ID="HUserID" runat="server" />
        <br />
        <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove Photo" />
    </form>
</body>
</html>
