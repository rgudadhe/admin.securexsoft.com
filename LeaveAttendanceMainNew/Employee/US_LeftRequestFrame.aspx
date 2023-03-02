<%@ Page Language="VB" AutoEventWireup="false" CodeFile="US_LeftRequestFrame.aspx.vb" Inherits="LeaveAttendanceMainNew_Employee_USLeftRequestFrame" %>

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
        <a href="../US_LeaveRequest.aspx"  target="Main">Leave Request</a><BR>
        <a href="../US_LeaveCanReq.aspx" target="Main">Cancel Leave</a><BR>
      <%--  <a href="../AttendanceRequest.aspx" target="Main">Attendance Request</a><BR><BR><BR>--%>
    </div>
    <p></p>
        <asp:Table ID="tblLeaveBal" runat="server" Width="100%" HorizontalAlign="Left" CssClass="common" >
            <asp:TableRow Visible="false">
                <asp:TableCell BorderStyle="None" BorderWidth="0"  HorizontalAlign="Right">
                    Casual Leaves : 
                    <asp:Label ID="lblCL" runat="server" Text="" CssClass="common"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Visible="false">
                <asp:TableCell BorderStyle="None" BorderWidth="0" HorizontalAlign="Right">
                    Earned Leaves : 
                    <asp:Label ID="lblEL" runat="server" Text="" CssClass="common"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None" BorderWidth="0" HorizontalAlign="Right">
                    Total Leaves : 
                    <asp:Label ID="lblTL" runat="server" Text="" CssClass="common"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell BorderStyle="None" BorderWidth="0"><br /><br /></asp:TableCell></asp:TableRow>
            <asp:TableRow BorderStyle="None" BorderWidth="0" ID="LWPRow" Visible="false"  CssClass="common">
                <asp:TableCell>
                    Leave Without Pay : 
                    <asp:Label ID="lblLWP" runat="server" Text="" CssClass="common"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>    
    </form>
</body>
</html>
