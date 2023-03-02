<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SummaryReport.aspx.vb" Inherits="SummaryReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Summary Report</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <center>
            <asp:Table ID="tblMain" runat="server" Width="75%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv">
                        Summary Report
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <Br/>
            <asp:Table ID="tblMainSearch" HorizontalAlign="Center" runat="server" Width="55%">
                <asp:TableRow ID="TableRow1" runat="server" >
                    <asp:TableCell ID="TableCell1" HorizontalAlign="Center" Width="30%" runat="server" CssClass="alt1">
                        Start Date
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell2" HorizontalAlign="Center" Width="30%" runat="server" CssClass="alt1">
                        End Date
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell ID="TableCell4" Width="30%" runat="server">
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgSDate"  ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" CssClass="cal_Theme1" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell5" Width="30%" runat="server">
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgEDate" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" CssClass="cal_Theme1" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                </asp:TableRow>
           </asp:Table>
           <br />
           <input id="btnSubmit" type="submit" value="Submit" class="button" runat="server" />
        </center>
        <BR>
        <hr />
        <BR>
        <asp:Table ID="Table1" runat="server" Width="90%" Visible="false" HorizontalAlign="Center">
            <asp:TableRow Height="25">
                <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                    Summary Report
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <ajaxToolkit:Accordion ID="MyAccordion" runat="server" Width="100%"
                                HeaderCssClass="accordionHeader" FadeTransitions="true" SelectedIndex="-1" FramesPerSecond="40" 
                                TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    </ajaxToolkit:Accordion>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
