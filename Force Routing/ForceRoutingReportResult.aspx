<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRoutingReportResult.aspx.vb" Inherits="Force_Routing_ForceRoutingReportResult" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ForceRouting Report</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMsg" Font-Names="Trebuchet MS" Font-Size="12px" runat="server" Text=""></asp:Label>
        <asp:Table ID="tblResult" EnableViewState="true" Font-Names="Trebuchet MS" Font-Size="12px" Width="450" GridLines="Both" runat="server">
        </asp:Table>
    </div>
    </form>
</body>
</html>
