<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRouting4TemplateNew.aspx.vb" Inherits="ForceRouting4TempResult" %>
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
    <form id="Form1" method="post" target="ForceRouting4TempResultNew" runat="server" >
    <div>
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>                
                <table style="font-family:Trebuchet MS; font-size:8pt;">
                    <tr class="SMSelected">
                        <td align="center">
                            Template Name
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TName" runat="server" TabIndex="3" Width="248px" Font-Names="Trebuchet MS" Font-Size="8pt"></asp:TextBox>
                        </td>
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
        <iframe id="ForceRouting4TempResultNew" frameborder="0" name="ForceRouting4TempResultNew" src="ForceRouting4TempResultNew.aspx" style="width: 100%; height: 368px"
            ></iframe>    
    </div>
    </form>
</body>
</html>
