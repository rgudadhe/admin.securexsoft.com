<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee.aspx.vb" Inherits="LeaveAttendanceMainNew_Employee_Employee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee Page</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    
    <script language="JavaScript">
<!--
function calcHeight()
{
//find the height of the internal page
var the_height=
document.getElementById('LeftFrame').contentWindow.
alert(the_height);
document.body.scrollHeight;

//change the height of the iframe
document.getElementById('LeftFrame').height=
the_height;
}
//-->
</script>


</head>
<body style=" text-align:left">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Employee</h1>
    <form id="frmEmployee" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="EmpTabContainer" runat="server" Width="100%" ScrollBars="None" ActiveTabIndex="0" AutoPostBack="true">
            <Ajax:TabPanel ID="DailyAttendance" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblAttendance" runat="server" Text="Daily Attendance" CssClass="common"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelAttendance" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYear" CssClass="common" runat="server" Width="75" Height="20">
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonth" CssClass="common" runat="server" Width="85" Height="20">
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height=20/>
                            <asp:Table ID="tblMain" HorizontalAlign="Center" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrev" runat="server" CssClass="button" ForeColor="Goldenrod" Text="<<" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthName" runat="server" Text="" CssClass="common"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNext" CssClass="button" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Table ID="tblMainCalendar" runat="server" Width="100%" CssClass="common">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="SendRequest1" runat="server" Height="700">
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" Text="Leave and Attendance Request"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="20%" height="410"  frameborder="0"  src="LeftRequestFrame.aspx" id="LeftFrame"></iframe>
                    <iframe width="80%" height="410"  frameborder="0" src="RigthReuestFrame.aspx"  id="RightFrame" style="float:right;"></iframe>   
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Status" runat="server" >
                <HeaderTemplate>
                    <asp:Label ID="LeaveStatus" runat="server" Text="Leave Status" CssClass="common"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYearStatus" CssClass="common" runat="server" Width="75" Height="20">
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonthStatus" CssClass="common" runat="server" Width="85" Height="20">
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGoStatus" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height="20" />
                            <asp:Table ID="tblMainStatus" HorizontalAlign="Center" runat="server" Width="100%">
                                <asp:TableRow >
                                    <asp:TableCell HorizontalAlign="Left" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrevStatus" runat="server" CssClass="button" ForeColor="Goldenrod"  Text="<<" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthNameStatus" runat="server" Text="" CssClass="common"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNextStatus" CssClass="button" CausesValidation="false" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Table ID="tblMainCalendarStatus" runat="server" Width="100%" CssClass="common">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="DutyRoster" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblDuty" runat="server" Text="Duty Roster" CssClass="common"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=100% height="410" frameborder="0" src="EmployeeDutyRoster.aspx" id="EDutyRosterframe"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer> 
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
