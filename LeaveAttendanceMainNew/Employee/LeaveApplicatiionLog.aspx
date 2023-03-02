<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeaveApplicatiionLog.aspx.vb" Inherits="LeaveAttendanceMainNew_LeaveApplicatiionLog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Application Log</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblMain" runat="server" Width="100%" CssClass="common">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" ColumnSpan="6" HorizontalAlign="Center" CssClass="HeaderDiv" runat="server">
                    Leave Application Log 
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell CssClass="alt1" >
                    Leave Type
                </asp:TableCell>
                <asp:TableCell CssClass="alt1">
                    Action By
                </asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" CssClass="alt1">
                    Action On
                </asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" CssClass="alt1">  
                    Action Details
                </asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" CssClass="alt1">
                    Leave Balance Before Action 
                </asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server" CssClass="alt1">
                    Leave Balance After Action 
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
