<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RoutingToolMainPage.aspx.vb" Inherits="RoutingTool_RoutingToolMainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <%--<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>--%>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>


</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
            <div id="cap"></div>
            <div id="main">
                <h1>View/Route Jobs</h1>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                    <asp:Table ID="Table2"  runat="server"  Width="100%" CssClass="common">
                    </asp:Table>
                    <input id="Button1" type="button" class="button" value="Refresh Page" onclick="refresh();"/><br />
                </asp:Panel>
            </div> 
        </div>
    </form>
</body>
</html>
