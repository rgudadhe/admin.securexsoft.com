<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeftRequestFrameApproval.aspx.vb" Inherits="LeaveAttendanceMainNew_Employee_LeftRequestFrame" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Left Frame</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="common" style="text-align:left">
        <a href="LeaveApproval.aspx" target="MainApproval">Leave Approval</a><BR>
        <a href="AttendanceApproval.aspx" target="MainApproval">Attendance Approval</a><BR>
        <a href="CancelApproval.aspx" target="MainApproval">Cancel Approval</a><BR><BR><BR>
    </div>
        <asp:Table ID="tblLeaveBal" runat="server" Width="100%" HorizontalAlign="Left" Visible="false" >
            <asp:TableRow>
                <asp:TableCell BorderStyle="None" BorderWidth="0" HorizontalAlign="Right">
                    Casual Leaves : 
                    <asp:Label ID="lblCL" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None" BorderWidth="0" HorizontalAlign="Right">
                    Earned Leaves : 
                    <asp:Label ID="lblEL" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None" BorderWidth="0" HorizontalAlign="Right">
                    Total Leaves : 
                    <asp:Label ID="lblTL" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>    
    </form>
</body>
</html>
