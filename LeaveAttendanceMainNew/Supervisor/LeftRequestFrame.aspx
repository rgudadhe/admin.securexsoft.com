<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeftRequestFrame.aspx.vb" Inherits="LeaveAttendanceMainNew_Employee_LeftRequestFrame" %>

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
        <a href="../LeaveRequest.aspx"  target="Main">Leave Request</a><BR>
        <a href="../LeaveCanReq.aspx" target="Main">Cancel Leave</a><BR>
        <a href="../AttendanceRequest.aspx" target="Main">Attendance Request</a><BR><BR><BR>
    </div>
        <asp:Table ID="tblLeaveBal" runat="server" Width="100%" HorizontalAlign="Left"  CssClass="common">
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
