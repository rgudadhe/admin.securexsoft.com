<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketHistory.aspx.vb" Inherits="TicketHistory" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ticket History</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
    &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp <Font size="2" name="Arial"><a href="JavaScript:history.go(-1)">Back</a></Font>
    <BR>
    <BR>
    <center>
        <asp:Table ID="Table1" runat="server" Width="90%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Center" ColumnSpan="2" CssClass="HeaderDiv" >
                    Ticket Detials
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Lab" runat="server" CssClass="common" Text="Customer Name : "></asp:Label>
                    <asp:Label ID="lblCustName" runat="server" CssClass="common"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label3" runat="server" CssClass="common" Text="UserName : "></asp:Label>
                    <asp:Label ID="lblUserName" runat="server" CssClass="common"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label5" runat="server" CssClass="common" Text="Ticket No : "></asp:Label>
                    <asp:Label ID="lblTicketNo" runat="server" CssClass="common" Text=<%#Eval("TicketNo") %>></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label7" CssClass="common" runat="server" Text="Issue Type : "></asp:Label>
                    <asp:Label ID="lblIssueName" CssClass="common" runat="server" Text=<%#Eval("IssueName") %>></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label9" CssClass="common" runat="server" Text="Date Created : "></asp:Label>
                    <asp:Label ID="lblDatePosted" CssClass="common" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label11" CssClass="common" runat="server" Text="Priority : "></asp:Label>
                    <asp:Label ID="lblPriority" CssClass="common" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="alt2">
                    <asp:Label ID="lblIssueDetails"  CssClass="common" runat="server" Text="Issue Details : "></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ID="tblCellIssueDetails" HorizontalAlign="Left" ColumnSpan="2" CssClass="common">
                </asp:TableCell>
            </asp:TableRow>            
        </asp:Table>
        <asp:Table ID="tblResponses" runat="server" Width="90%" Visible="false">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" CssClass="alt2">
                    Response History :
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </center>
    </div>
    </form>
</body>
</html>
