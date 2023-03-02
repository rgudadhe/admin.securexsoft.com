<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Adminstrator.aspx.vb" Inherits="LeaveAttendanceMainNew_Adminstrator_Adminstrator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Adminstator</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <LINK href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body style="text-align:left">
    <form id="frmAdmin" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Manage Leaves</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="AdminTabContainer" runat="server" AutoPostBack="true" Width="100%" ScrollBars="None" ActiveTabIndex="0" >
            <Ajax:TabPanel ID="DailyAttendance" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblAttendance" runat="server" CssClass="common" Text="Import Daily Attendance"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>   
                    <iframe width=100% height="420" frameborder="0" src="ImportDailyAttendance.aspx" id="ImportAttendance"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="LeaveBalance" runat="server" Height="700">
                <HeaderTemplate>
                    <asp:Label ID="lblBal" runat="server" Text="Leave Balance"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="15%" height="410"  frameborder="0" src="LeftRequestFrameBalance.aspx"  id="Iframe1"></iframe>
                    <iframe width="80%" height="410"  frameborder="0" src="RigthReuestFrameBalance.aspx"    id="Iframe2"></iframe>   
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Management" runat="server" Height="700">
                <HeaderTemplate>
                    <asp:Label ID="lblManagement" runat="server" Text="Management"></asp:Label>
                </HeaderTemplate>                
                <ContentTemplate>
                    <iframe width="15%" height="410"  frameborder="0" src="LeftRequestFrameManagement.aspx"  id="Iframe5"></iframe>
                    <iframe width="80%" height="410"  frameborder="0" src="RigthReuestFrameManagement.aspx"  id="Iframe6"></iframe>   
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" CssClass="common" Text="Duty Roster"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>   
                    <iframe width=100% height="420" frameborder="0" src="../Supervisor/ViewRoster.aspx?Admin=True" id="Iframe3"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
