<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddUpdates.aspx.vb" Inherits="AddUpdates" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" />
    <title>Add Updates</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New Updates</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
     <table width="80%">
            <tr>
                <td colspan="4" style="text-align: center;" class="HeaderDiv">
                    Add New Updates
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="common">
                    Department</td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="DLDept" CssClass="common" runat="server">
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td style="text-align: right" class="common">
                    Subject
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtSub" CssClass="common" runat="server" Width="520px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="common" valign="top"  >
                    Details
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesc" runat="server" CssClass="common" TextMode="MultiLine" Height="168px" Width="520px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button"  /></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="iresponse" CssClass="Title" ForeColor="#C00000" runat="server" Text=""></asp:Label><br />
        </div>
        <br />
        <a href="ViewUpd.aspx" target="_self" ><strong><span style="color: #ff0000" class="common">View Updates</span></strong></a>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
