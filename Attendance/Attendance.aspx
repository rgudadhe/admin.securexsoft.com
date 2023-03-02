<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Attendance.aspx.vb" Inherits="Attendance" Debug="true"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "../Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Table ID="Table1" runat="server" Height="29px" Width="600px" BorderStyle=Outset BorderWidth=2px>
            <asp:TableRow ID=AttMain BorderStyle=Inset BorderWidth=2px CssClass="ReportHeaderDiv" runat="server" >
                <asp:TableCell HorizontalAlign=Left BorderStyle=Inset BorderWidth=2px Font-Names="Trebuchet MS" runat="server" >
                    Attendance 
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderStyle=Inset BorderWidth=2px HorizontalAlign=Left runat="server">
                <asp:TableCell ColumnSpan=2 BorderStyle=Inset BorderWidth=2px runat="server">
                    <asp:Button ID="cmdSignIn" Font-Names="Trebuchet MS" Text="SignIn" runat="server" />
                    <asp:Button ID="cmdSignOut" Font-Names="Trebuchet MS" Text="SignOut" runat="server"/>
                    <asp:Button ID="cmdStartBreak" Font-Names="Trebuchet MS" Text="Start Break" runat="server"/>
                    <asp:Button ID="cmdEndBreak" Font-Names="Trebuchet MS" Text="End Break" runat="server"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
