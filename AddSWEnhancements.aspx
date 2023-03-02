<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddSWEnhancements.aspx.vb" Inherits="Enhancements" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" />
    <title>Add System Alert</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Add SecureWeb Enhancement Details</h1>
    <div>
     <table width="80%">
            <tr>
            <td style="text-align: right" valign="top"  >
                    Release Date</td>
                <td style="text-align: left;">
                     <asp:TextBox ID="TxtDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender></td>
            </tr> 
            <tr>
                <td style="text-align: right" valign="top"  >
                    Enhancement</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtEnhancement" runat="server" CssClass="common"  Width="520px"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="text-align: right" valign="top"  >
                    Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDesc" runat="server" MaxLength="450"  CssClass="common" TextMode="MultiLine" Height="168px" Width="520px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button"  /></td>
            </tr>
            
        </table>
        <br />
        <asp:Label ID="iresponse" CssClass="common" runat="server" Text=""></asp:Label><br />
        </div>
            
        <a href="ViewSWEnhancements.aspx" target="_self" ><strong><span class="common">View Enhancements</span></strong></a>
        </div> 
        </div>
        </asp:Panel>
    </form>
</body>
</html>
