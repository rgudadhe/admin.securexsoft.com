<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TATReport.aspx.vb" Inherits="TATReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TAT Report</title>
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
                        TAT Report
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="tblMainSearch" HorizontalAlign="Center" runat="server" Width="65%">
                <asp:TableRow ID="TableRow1" runat="server" >
                    <asp:TableCell ID="TableCell1" HorizontalAlign="Center" Width="30%" runat="server" CssClass="alt1">
                        Start Date
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell2" HorizontalAlign="Center" Width="30%" runat="server" CssClass="alt1">
                        End Date
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell3" HorizontalAlign="Center" runat="server" CssClass="alt1">
                        Status
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell ID="TableCell4" Width="30%" runat="server">
                        <asp:TextBox ID="txtStartDate" CssClass="common" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgSDate"  ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" CssClass="cal_Theme1" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                        </ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell5" Width="30%" runat="server">
                        <asp:TextBox ID="txtEndDate" CssClass="common"  runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgEDate" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" Enabled="True">
                        </ajaxToolkit:CalendarExtender>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell6" runat="server">
                        <asp:DropDownList ID="DropDownStatus" CssClass="common" runat="server" Width="98%">
                            <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                            <asp:ListItem Text="Less than 24" Value="L24"></asp:ListItem>
                            <asp:ListItem Text="More than 24" Value="G24"></asp:ListItem>
                            <asp:ListItem Text="Less than 48" Value="L48"></asp:ListItem>
                            <asp:ListItem Text="More than 48" Value="G48"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>   
        <br />
        <center>
            <input id="btnSubmit" class="button" type="submit" value="Submit" runat="server" />
        </center>     
        <BR>
        <hr>
        <BR>
        <center><asp:Label ID="lblTickets" runat="server" CssClass="common" Text="" Visible="false"></asp:Label></center>
        <center>
        <asp:Repeater ID="TATReport1" runat="server" >
            <HeaderTemplate>
                <table align="center" width="90%">
                       <tr align="center"> 
                            <td colspan="6" class="HeaderDiv">
                                TAT Report
                            </td>
                       </tr>
                        <tr>
                            <td class="alt1">Ticket No.</td>
                            <td class="alt1">Date Posted</td>
                            <td class="alt1">Raise By</td>
                            <td class="alt1">Issue Type</td>
                            <td class="alt1">Group</td>
                            <td class="alt1">TAT</td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <a class="common" href="TicketHistory.aspx?TID=<%#DataBinder.Eval(Container, "DataItem.TicketID") %>"><%#DataBinder.Eval(Container, "DataItem.TicketNo")%></a>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDescription" cssclass="common" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.DatePosted") %>'></asp:Label>
                    </td>
                    <td align="Left">
                        <asp:Label ID="lblIssueType" cssclass="common" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.FirstName") & DataBinder.Eval(Container, "DataItem.LastName") %>'></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDateCreated" cssclass="common" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.IssueName") %>'></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblStatus" cssclass="common" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Name") %>'></asp:Label>
                    </td>
                    <td align="left" class="common">
                        <asp:Label ID="lblTAT" cssclass="common" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.TAT") %>'></asp:Label>
                    </td>
                </tr>                
            </ItemTemplate>
            <FooterTemplate>
                </Table>
            </FooterTemplate>
        </asp:Repeater>
        </center>
    </div>
    </form>
</body>
</html>
