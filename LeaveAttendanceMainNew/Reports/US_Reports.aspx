<%@ Page Language="VB" AutoEventWireup="false" CodeFile="US_Reports.aspx.vb" Inherits="LeaveAttendanceMainNew_Reports_USReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
     <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body style="text-align:left" >
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Reports</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="ReportTabContainer" AutoPostBack="true" runat="server" Width="100%" ScrollBars="None" ActiveTabIndex="0">
            <Ajax:TabPanel ID="Att" runat="Server">
                <HeaderTemplate>
                    <asp:Label ID="Label1" CssClass="common" runat="server" Text="Monthly Attendance"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" frameborder="0" src="US_AttendanceReports.aspx" id="DutyRosterframe" height="420" scrolling="auto"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="DailyAtt" runat="server" Visible="false">
                <HeaderTemplate>
                    <asp:Label ID="Label2" CssClass="common" runat="server" Text="Daily Attendance" Visible="false"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" frameborder="0" src="TechDailyAttendance.aspx" id="Iframe1" height="420" scrolling="auto"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="LeaveSanctioned" runat="Server" Visible="false">
                <HeaderTemplate>
                    <asp:Label ID="Label3" runat="server" CssClass="common" Text="Leave Sanctioned"  Visible="false"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" frameborder="0" src="LeaveSanctioned.aspx" id="Iframe2" height="420" scrolling="auto"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="EmpStatus" runat="server" Width="100%" Height="100%">
                <HeaderTemplate>
                    <asp:Label ID="EmpLeaveStatus" runat="server" Text="Employee On Leave"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelEmp" runat="server" >
                        <ContentTemplate>
                            <asp:Table ID="Table3" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" BorderStyle="None" BorderWidth="0">
                                        <asp:DropDownList ID="DropDownYearStatusEmp" CssClass="common" runat="server" Width="75" Height="20">
                                        </asp:DropDownList> &nbsp 
                                        <asp:DropDownList ID="DropDownMonthStatusEmp" CssClass="common" runat="server" Width="85" Height="20">
                                        </asp:DropDownList> &nbsp
                                        <asp:Button ID="btnGoStatusEmp" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height="20"/>
                                    </asp:TableCell>
                                    <asp:TableCell ID="tblCellDept" HorizontalAlign="Right" BorderStyle="None" BorderWidth="0">
                                        <div style="text-align:right">
                                        <asp:DropDownList ID="DropDownDept" runat="server" AutoPostBack="true" Width="275" Height="20">
                                        </asp:DropDownList>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tblMainStatusEmp" HorizontalAlign="Center" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" Width="10%" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrevStatusEmp" runat="server" CssClass="button" ForeColor="Goldenrod"  Text="<<" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthNameStatusEmp" runat="server" Text="" CssClass="common"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" Width="10%" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNextStatusEmp" CssClass="button" CausesValidation="false" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" Width="100%">
                                        <asp:Table ID="tblMainCalendarStatusEmp" runat="server" Width="100%">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="LeaveReport" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="Label4" runat="server" CssClass="common" Text="Leave Report"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" frameborder="0" src="US_LeaveReport.aspx" id="Iframe3" height="420" scrolling="auto"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="TabPanel1" runat="server" BorderColor="lightblue" Visible="false">
                <HeaderTemplate>
                    <asp:Label ID="Label5" runat="server" CssClass="button" Text="Duty Roster"  Visible="false"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>   
                    <iframe width="100%" height="420" frameborder="0" src="../Supervisor/ViewRoster.aspx?Admin=True" id="Iframe4"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
    </div>  
    </div> 
    </form>
</body>
</html>
