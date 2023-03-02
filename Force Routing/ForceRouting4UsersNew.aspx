<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRouting4UsersNew.aspx.vb" Inherits="ForceRouting4UserResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../App_Themes/Css/RoutingTool.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="Form1" method="post" target="ForceRouting4UserResultNew" runat="server" >
    <div>
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>                
                <table style="font-family:Trebuchet MS; font-size:8pt;">
                    <tr class="SMSelected">
                        <td align="center">
                            User Name
                        </td>
                        <td align="center">
                            User ID
                        </td>        
                        <td align="center">
                            User Level
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="UName" runat="server" TabIndex="3" Font-Names="Trebuchet MS" Font-Size="8pt" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="UID" runat="server" TabIndex="3" Font-Names="Trebuchet MS" Font-Size="8pt" Width="130px"></asp:TextBox></td>    
                        <td>
                            <asp:DropDownList ID="ULevel" Font-Names="Trebuchet MS" Font-Size="8pt" runat="server">
                            </asp:DropDownList></td>
                    </tr>                    
                    
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="5">
                    <input name="SEARCH" type="submit" value="Search" />
                </td>
            </tr>
        </table>
        <iframe id="ForceRouting4UserResultNew" frameborder="0" name="ForceRouting4UserResultNew" src="ForceRouting4UserResultNew.aspx" style="width: 100%; height: 368px"
            ></iframe>    
    </div>
    </form>
</body>
</html>
