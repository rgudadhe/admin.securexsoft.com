<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TechDailyAttendance.aspx.vb" Inherits="TechReports_TechDailyAttendance"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Daily Attendance</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
	    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell CssClass="HeaderDiv">
                        Daily Attendance 
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblView" runat="server" Text="Select Date to view Report " CssClass="common" ></asp:Label>
                         <asp:TextBox ID="txtDateReport" runat="server" Width="70px" CssClass="common" ></asp:TextBox>    
                                            <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateReport" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>

                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <center>
                            <asp:Button ID="BtnSubmit" runat="server" CssClass="button" Text="Submit" />
                        </center>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <BR><BR>
            <asp:Table ID="Table2" runat="server" Width="500" Visible="false">
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None"> 
                        <div style="text-align:left">
                        <asp:LinkButton ID="btnExport" CssClass="common" runat="server">Export Results</asp:LinkButton>        
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table3" runat="server" Width="500" CssClass="common" Visible="false">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                        UserName
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                        Employee Name
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                        Status
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>
        </div>
   </form>
</body>
</html>
