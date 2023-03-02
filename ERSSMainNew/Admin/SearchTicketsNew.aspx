<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SearchTicketsNew.aspx.vb" Inherits="ERSSMainNew_Admin_SearchTicketsNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search Tickets</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../../App_Themes/JS/tooltip.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
    <div>
        <table width="100%">
            <tr>
                <td class="alt1">
                    Ticket No
                </td>
                <td class="alt1">
                    Start Date
                </td>
                <td class="alt1">
                    End Date
                </td>
                <td class="alt1">
                    Status
                </td>
                <td class="alt1">
                    First Name    
                </td>
                <td class="alt1">
                    Last Name
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="common" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="common" Width="80px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                </td>                        
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="common" Width="80px"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="common" Width="80px">
                        <asp:ListItem Text="Any" Selected="True" Value=""></asp:ListItem>    
                        <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtFName" runat="server" CssClass="common" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtLName" runat="server"  CssClass="common" Width="100px"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3" class="alt1">
                    Issue Categoires   
                </td>
                <td colspan="3" class="alt1">
                    Issue Types
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DropDownList ID="ddlICate" runat="server" CssClass="common" AutoPostBack="true" Width="95%">
                    </asp:DropDownList>
                </td>
                <td colspan="3" style="width:100%">
                    <asp:DropDownList ID="ddlIType" runat="server" CssClass="common" Width="95%">
                    </asp:DropDownList>
                </td>
                <td style="border:0">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" />    
                </td>
            </tr>
        </table>
        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" CssClass="cal_Theme1" popupbuttonid="ImgBntsDate" targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
        <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" CssClass="cal_Theme1" popupbuttonid="ImgBnteDate" targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>
        <br /><br />
        <table id="tblResult" runat="server" visible="false" width="100%">
            <tr>
                <td>
                    <asp:GridView ID="GridViewSearchResults" Width="100%" AllowPaging="True" AllowSorting="True" 
                        DataKeyNames="TicketID" AutoGenerateColumns="False" ShowFooter="True" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Ticket No" HeaderStyle-HorizontalAlign="Center" SortExpression="TicketNo" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblTicketID" runat="server" Text=<%#Eval("TicketNo") %>></asp:Label>--%>
                                    <a href="ActionTicket.aspx?ID=<%#Eval("TicketID")%>&From=Search"><%#Eval("TicketNo") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IssueName" HeaderStyle-CssClass="alt1" HeaderStyle-HorizontalAlign="Center" SortExpression="IssueName">
                                <ItemTemplate>
                                    <asp:Label ID="iblIssueName" runat="server" Text=<%#Eval("IssueName")%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IssueDescription" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:Label ID="iblIssueDetails" runat="server" Text=<%#ValidateString(Eval("Description").ToString())%>></asp:Label>
                                </ItemTemplate>    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority" HeaderStyle-CssClass="alt1" SortExpression="Priority">
                                <ItemTemplate>
                                    <asp:Label ID="lblPriority" runat="server" Text=<%#Eval("Priority") %>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Posted" HeaderStyle-CssClass="alt1" SortExpression="DatePosted">
                                <ItemTemplate>
                                    <asp:Label ID="lblDataPosted" runat="server" Text=<%#Eval("DatePosted").ToShortDateString() %>></asp:Label>                                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Raise By" HeaderStyle-CssClass="alt1" SortExpression="FirstName">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text=<%#Eval("FirstName") &" "& Eval("LastName") %>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="ActionTicket" CommandName="ActionTicket" CommandArgument='<%#Eval("TicketID")%>' OnClientClick=<%# "javascript:Test('" + Eval("TicketID").ToString() + "')" %>  runat="server">Action</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
        <%--<label style="font-family:Trebuchet MS; color:Blue; cursor:hand "  onmouseover="tip_it('ToolTip','Description','Hi, if you could add Username column along with UserID in Audit Reports under Quality Audits on SecureIT it would be helpful.  Regards, Jharna.'); " onmouseout="hideIt('ToolTip');"> more >></label>--%>
        <div style='position:absolute; visibility:hidden; z-index:1000;' id='ToolTip'></div>
    </form>
</body>
</html>
