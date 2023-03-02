<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportSearchForm.aspx.vb" Inherits="ReportSearchForm" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "styles\Default.css" type="text/css" rel="stylesheet">
    <LINK href= "Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" action=SummaryReport.aspx  method=post target="bottom" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <center>
            <asp:Table ID="tblMain" runat="server" Width="75%">
                <asp:TableRow CssClass="ReportHeaderDiv">
                    <asp:TableCell HorizontalAlign=Center>
                        Summary Report
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <Br/>
            <asp:Table ID="tblMainSearch" HorizontalAlign=Center runat="server" GridLines=Both Width="55%">
                <asp:TableRow ID="TableRow1" CssClass="ReportSMSelected" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" >
                    <asp:TableCell ID="TableCell1" HorizontalAlign=Center Width="30%" runat="server">
                        Start Date
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell2" HorizontalAlign=Center Width="30%" runat="server">
                        End Date
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server">
                    <asp:TableCell ID="TableCell4" Width="30%" runat="server">
                        <asp:TextBox ID="txtStartDate" Font-Names="Trebuchet MS" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgSDate"  ImageUrl="Calendar.gif" runat="server"/>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell5" Width="30%" runat="server">
                        <asp:TextBox ID="txtEndDate" Font-Names="Trebuchet MS" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgEDate" ImageUrl="Calendar.gif" runat="server" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                </asp:TableRow>
           </asp:Table>
        </center>
        <br />
        <center>
            <input id="btnSubmit" type="submit" value="Submit" style="font-family:Trebuchet MS" runat=server />
        </center>
    </div>
    </form>
</body>
</html>
