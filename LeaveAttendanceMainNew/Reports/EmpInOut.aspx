<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpInOut.aspx.vb" Inherits="EmpInOut" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="Table1" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" HorizontalAlign="Center" Width="400px" GridLines=Both>
            <asp:TableRow runat="server" CssClass="HeaderDiv">
                <asp:TableCell runat="server" HorizontalAlign=Center ColumnSpan=2>
                    Attendance Report
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
                
    </div>
    </form>
</body>
</html>
