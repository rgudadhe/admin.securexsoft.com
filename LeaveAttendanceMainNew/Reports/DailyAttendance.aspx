<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyAttendance.aspx.vb" Inherits="LeaveSanctioned" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Sanctioned Report</title>
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
	    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
        <asp:Table ID="Table1" runat="server" Width="286px" GridLines=Both BorderColor="LightBlue">
            <asp:TableRow CssClass="HeaderDiv" runat="server">
                <asp:TableCell HorizontalAlign=Center runat="server">
                    Leave Sanctioned Report
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign=Left>
                    <asp:Label ID="lblView" runat="server" Font-Names="Trebuchet MS" Font-Italic=True ForeColor=Gray Text="Select Date to view Report " Font-Size=Small></asp:Label>
                    <asp:TextBox ID="txtDateReport" runat=server Width=70px Font-Names="Trebuchet MS"></asp:TextBox>    
                                            <asp:ImageButton ID="imgSDate" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateReport" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Button ID="BtnSubmit" runat="server" CssClass="Buttons" Text="Submit" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <BR><BR>
            <asp:Table ID="Table3" runat="server" Width=600 Visible=false>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign=Left>
                        <asp:LinkButton ID="btnExport" runat="server">Export Results</asp:LinkButton>        
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        
        <asp:Table ID="Table2" runat="server" Font-Names="Trebuchet MS" GridLines=Both BorderColor="LightBlue" Visible=false Width=600>
            <asp:TableRow CssClass="SMSelected">
                    <asp:TableCell HorizontalAlign=Center>
                        UserName
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=Center>
                        Employee Name
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=Center>
                        Status
                    </asp:TableCell>
                </asp:TableRow>
        </asp:Table>
        </center>
</div>
    </form>
</body>
</html>
