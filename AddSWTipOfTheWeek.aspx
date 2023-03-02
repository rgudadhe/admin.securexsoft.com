<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddSWTipOfTheWeek.aspx.vb" Inherits="TipOfTheWeek" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" />
    <title>Add Tip Of the week</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Add SecureWeb Tip Of the week</h1>
    <div>
     <table width="80%">
            
            
            <tr>
                <td style="text-align: right" valign="top"  >
                    Tip Of the week</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtTip" runat="server" CssClass="common"  Width="520px"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="text-align: right" valign="top"  >
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesc" MaxLength="500"  runat="server" CssClass="common" TextMode="MultiLine" Height="168px" Width="520px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button"  /></td>
            </tr>
            
        </table>
        <br />
        <asp:Label ID="iresponse" CssClass="common" runat="server" Text=""></asp:Label><br />
        </div>
         
        <a href="ViewSWTipOfTheWeek.aspx" target="_self" ><strong><span class="common">View Tip Of The Week</span></strong></a>
        </div> 
        </div>
        </asp:Panel>
    </form>
</body>
</html>
