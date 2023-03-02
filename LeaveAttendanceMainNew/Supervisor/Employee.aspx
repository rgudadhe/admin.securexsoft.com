<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee.aspx.vb" Inherits="LeaveAttendanceMainNew_Employee_Employee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee Page</title>
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../styles/Report.css" type="text/css" rel="stylesheet">
    
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
<body style="background-color:WhiteSmoke">
    <form id="frmEmployee" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="EmpTabContainer" runat=server Width="100%" ScrollBars=None ActiveTabIndex="0" AutoPostBack=true >
            <Ajax:TabPanel ID="DailyAttendance" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblAttendance" runat="server" Text="Daily Attendance"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelAttendance" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYear" Font-Names="Trebuchet MS" Font-Size=small runat="server" Width=75 Height=20>
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonth" Font-Names="Trebuchet MS" Font-Size=small runat="server" Width=85 Height=20>
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation=false Font-Names="Trebuchet MS" Font-Size=Small CssClass="Buttons" Height=20/>
                            <asp:Table ID="tblMain" HorizontalAlign=Center GridLines=Both runat="server" Width=100%>
                                <asp:TableRow CssClass="HeaderDiv">
                                    <asp:TableCell HorizontalAlign=Left>
                                        <asp:Button ID="BtnPrev" runat="server" CssClass="ButtonTransparent" ForeColor=Goldenrod Text="<<" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width="80%">
                                        <asp:Label ID="lblMonthName" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Right>
                                        <asp:Button ID="BtnNext" CssClass="ButtonTransparent" ForeColor=Goldenrod runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan=3>
                                        <asp:Table ID="tblMainCalendar" GridLines=Both BorderColor=LightBlue BorderStyle=Solid BorderWidth=1px runat="server" Width=100%>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="SendRequest1" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2 Height="700">
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" Text="Leave Request"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=20% height="410"  frameborder=0  src=LeftRequestFrame.aspx id="LeftFrame"></iframe>
                    <iframe width=80% height="410"  frameborder=0 src=RigthReuestFrame.aspx  id="RightFrame"></iframe>   
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Status" runat=server BorderColor=lightblue BorderStyle=Solid BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="LeaveStatus" runat="server" Text="Leave Status"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYearStatus" Font-Names="Trebuchet MS" Font-Size=small runat="server" Width=75 Height=20>
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonthStatus" Font-Names="Trebuchet MS" Font-Size=small runat="server" Width=85 Height=20>
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGoStatus" runat="server" Text="Go" CausesValidation=false Font-Names="Trebuchet MS" Font-Size=Small CssClass="Buttons" Height=20/>
                            <asp:Table ID="tblMainStatus" HorizontalAlign=Center runat="server" Width=100% BorderColor=LightBlue BorderWidth=1 >
                                <asp:TableRow CssClass="HeaderDiv">
                                    <asp:TableCell HorizontalAlign=Left>
                                        <asp:Button ID="BtnPrevStatus" runat="server" CssClass="ButtonTransparent" ForeColor=Goldenrod  Text="<<" CausesValidation=false />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width="80%">
                                        <asp:Label ID="lblMonthNameStatus" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Right>
                                        <asp:Button ID="BtnNextStatus" CssClass="ButtonTransparent" CausesValidation=false ForeColor=Goldenrod runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan=3>
                                        <asp:Table ID="tblMainCalendarStatus" BorderColor=LightBlue BorderStyle=Solid BorderWidth=1px runat="server" Width=100% GridLines=Both >
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer> 
    </div>
    </form>
</body>
</html>
