<%@ Page Language="VB" AutoEventWireup="false" CodeFile="US_AttendanceReports.aspx.vb" Inherits="USAttendanceReports" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attendance Report</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
	    <div>
            <center>
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan=3 runat="server" HorizontalAlign="Center" CssClass="HeaderDiv">
                            Employees Attendance Report 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="common">
                            <b>Month :</b> 
                            <asp:DropDownList ID="DropDownMonth" CssClass="common" runat="server">
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell CssClass="common">
                            <b>Year : </b>
                            <asp:DropDownList ID="DropDownYear" CssClass="common" runat="server">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="Submit" runat="server" cssClass="button" Text="Submit" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </center>
            <center>
                <asp:Table ID="Table3" runat="server" Width="100%" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell ID="ExportRes" HorizontalAlign="Left" BorderStyle="None" BorderWidth="0">
                            <asp:LinkButton ID="ES" CssClass="common" runat="server">Export Results</asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow CssClass="HeaderDiv">
                        <asp:TableCell ID="TableCell1" HorizontalAlign="Center" CssClass="HeaderDiv" >
                            <%--<asp:LinkButton ID="LinkButton1" runat="server">Export Results</asp:LinkButton>--%>
                            Attendance Summary
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="MainRow" runat="server" CssClass="common">
                        <asp:TableCell CssClass="alt1">
                            Name
                        </asp:TableCell>
                        <asp:TableCell CssClass="alt1">
                            ID
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </center>
    </div>
    </form>
</body>
</html>
