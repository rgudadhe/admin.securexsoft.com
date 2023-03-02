<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRouting4Template.aspx.vb" Inherits="ForceRouting4TempResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="Form1" method="post" target="ForceRouting4TempResult" runat="server" >
    <div>
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>                
                <table style="background-color: whitesmoke">
                    <tr>
                        <td style="height: 25px; width: 237px;">
                            <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Template Name" Width="240px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="height: 26px; width: 237px;">
                            <asp:TextBox ID="TName" runat="server" TabIndex="3" Width="248px"></asp:TextBox></td>
                        
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
        <iframe id="ForceRouting4TempResult" frameborder="0" name="ForceRouting4TempResult" src="ForceRouting4TempResult.aspx" style="width: 100%; height: 368px"
            ></iframe>    
    </div>
    </form>
</body>
</html>
