<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewCategory.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" >
    
       
        <table style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border="2" cellpadding="2"  width="80%">
            <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center">
                    <span style="font-size: 0.8em; font-family: Trebuchet MS; color: #ffffff;"><strong style="font-family: Trebuchet MS, Serif">
                        <I>New Category</I></strong></span></td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">Category</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtCategory" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">Priority</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtPriority" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; height: 26px;" colspan="4">
                    <asp:Button ID="Button1" runat="server"  Text="Submit" />
                    
                    
                </td>
            </tr>
        </table>
        <br />
         <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Medium"
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>&nbsp;
        <br />
        <br />
        <br />
        
    
    </asp:Panel>
        &nbsp;&nbsp;
    </form>
    
</body>
</html>
