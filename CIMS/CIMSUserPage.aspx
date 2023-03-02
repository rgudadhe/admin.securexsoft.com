<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CIMSUserPage.aspx.vb" Inherits="CIMSUserPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CIMS Tickets Page</title>
    <link href="Styles/Report.css" rel=stylesheet />
    <link href="Styles/Accordin.css" rel=stylesheet />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="CIMSUserTabContainer" runat=server Width="100%" >
            <Ajax:TabPanel runat=server ID="BrowseTickets" BorderColor=lightblue BorderStyle=Solid BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="Label3" runat="server" Text="Browse Tickets"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br /><br /><br />
                            <div id="DivTab" runat=server>
                            <asp:Table ID="tblView" runat="server" HorizontalAlign=Center BorderColor=LightBlue BorderWidth=1 Font-Names="Trebuchet MS" Width=95%>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign=Left>
                                        View Tickets : 
                                        <asp:Label ID="lblView" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Right>
                                        View : 
                                        <asp:DropDownList ID="DropDownView" runat="server" Font-Names="Trebuchet MS" AutoPostBack=true>
                                            <asp:ListItem Text="Any" Value="An"></asp:ListItem>
                                            <asp:ListItem Text="Open Tickets" Value="OT"></asp:ListItem>
                                            <asp:ListItem Text="Closed Tickets" Value="CT"></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow><asp:TableCell ColumnSpan=2></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign=Left>
                                        <asp:LinkButton ID="LinkBtnOpenTickets" runat="server" CausesValidation=false Font-Names="Trebuchet MS"><asp:Label ID="lblLinkBtnOpenTickets" runat="server" Font-Names="Trebuchet MS" Text=""></asp:Label></asp:LinkButton>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Right>
                                        <asp:LinkButton ID="LinkBtnCloseTickets"  runat="server" CausesValidation=false Font-Names="Trebuchet MS"><asp:Label ID="lblLinkBtnCloseTickets" runat="server" Font-Names="Trebuchet MS" Text=""></asp:Label></asp:LinkButton>
                                    </asp:TableCell>
                               </asp:TableRow>
                               <asp:TableRow><asp:TableCell ColumnSpan=2><BR></asp:TableCell></asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign=Left ColumnSpan=2>
                                        Tickets : 
                                        <asp:Label ID="lblTicketCount" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height=280>
                                    <asp:TableCell ColumnSpan=2 VerticalAlign=Top>
                                        <div id="TblCellDiv"  runat=server>
                                        <asp:GridView ID="GridViewCustTickets" Width="100%" Height=280 Font-Names="Trebuchet MS" AllowPaging=true AllowSorting=true runat="server"
                                            BackColor="LightGoldenrodYellow" BorderColor=BurlyWood BorderStyle=Solid BorderWidth=2 AutoGenerateColumns=false DataSourceID="SqlDataSourceTickets" DataKeyNames="TicketID" ShowFooter=true Font-Size=Small>
                                            <AlternatingRowStyle BackColor=PaleGoldenrod />
                                            <SelectedRowStyle BackColor=Goldenrod />
                                            <HeaderStyle Height=20 CssClass="ReportSMSelected" ForeColor=Beige HorizontalAlign=Center />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ticket No" HeaderStyle-HorizontalAlign=Center SortExpression="TicketNo" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTicketNo" runat="server" Text=<%#Eval("TicketNo")%> ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign=Center SortExpression="Subject">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Action" CommandName="Action" CommandArgument=<%#Eval("TicketID")%> runat="server" CausesValidation=false><%#Eval("Subject") %></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign=Center SortExpression="TicketDetails">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTicketDetails" runat="server" Text=<%#Eval("TicketDetails")%> ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DatePosted" SortExpression="DatePosted" HeaderStyle-HorizontalAlign=Center>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDatePosted" runat="server" Text=<%#Eval("DatePosted")%> ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Priority" SortExpression="Priority" HeaderStyle-HorizontalAlign=Center>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriority" runat="server" Text=<%#Eval("Priority")%> ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>    
                                        </asp:GridView>
                                        </div>
                                        <asp:SqlDataSource ID="SqlDataSourceTickets" runat="server"></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Table ID="tblViewTicket" runat="server" Visible=false HorizontalAlign=Center BorderColor=LightBlue BorderWidth=1 Font-Names="Trebuchet MS" Width=95%>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size=Large ForeColor=Crimson Text="View Ticket"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell><BR><BR><BR></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell VerticalAlign=Bottom>
                                                <asp:Button ID="BtnAction" runat="server" CssClass="Buttons" Font-Names="Trebuchet MS" Text="Action"  CausesValidation=false />&nbsp
                                                <asp:Button ID="BtnReloadTicketList" CssClass="Buttons" runat="server" Font-Names="Trebuchet MS" Text="Reload Ticket List" CausesValidation=false /> &nbsp
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblTicketSubject" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell VerticalAlign=Top>
                                                <asp:Table ID="tblViewTicketDetails" runat="server" BorderStyle=Solid BorderWidth=1 Width=80% Visible=false>
                                                    <asp:TableRow>
                                                        <asp:TableCell ID="tblCellPlaceHolder">
                                                            <asp:PlaceHolder ID="PlaceHolerID" runat=server>
                                                            </asp:PlaceHolder>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell HorizontalAlign=Center ColumnSpan=4>
                                                            <asp:Button ID="BtnSubmit" Font-Names="Trebuchet MS"  CausesValidation=false runat="server" Text="Submit"/>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Table ID="tblViewTicketHistory" runat="server" Width=100% BorderStyle=solid BorderWidth=1>
                                                    <asp:TableRow CssClass="ReportHeaderDiv">
                                                        <asp:TableCell Width=30%>
                                                            From
                                                        </asp:TableCell>
                                                        <asp:TableCell>
                                                            Message
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>                            
                                </ContentTemplate>                            
                            </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownView" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="TestTab3" BorderColor=LightBlue BorderWidth=1 runat=server >
                <HeaderTemplate>
                    <asp:Label ID="lblTab3" runat="server" Text="Search"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
                        <ContentTemplate>
                            <asp:Table ID="Table1" BackColor="GhostWhite" runat=server BorderColor=Lightblue BorderWidth=1 Font-Names="Trebuchet MS" Width=95% HorizontalAlign=Center>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        Look for : 
                                        <asp:TextBox ID="txtLookfor" Font-Names="Trebuchet MS" Width=200 Height=18 runat="server" BorderColor=LightBlue BorderStyle=Solid BorderWidth=1></asp:TextBox> &nbsp &nbsp
                                        In : 
                                        <asp:DropDownList ID="DropDownLookIn" Font-Names="Trebuchet MS" runat="server" Width=100>
                                            <asp:ListItem Value="Any" Text="Any"></asp:ListItem>
                                            <asp:ListItem Value="Subject" Text="Subject"></asp:ListItem>
                                            <asp:ListItem Value="Message" Text="Message"></asp:ListItem>
                                        </asp:DropDownList>&nbsp &nbsp
                                        Priority : 
                                        <asp:DropDownList ID="DropDownPrioritySearch" Font-Names="Trebuchet MS" Width=100 runat="server">
                                            <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                        </asp:DropDownList>
                                        <hr style="color:White" />
                                        From : 
                                        <asp:DropDownList ID="DropDownFromMonth" Font-Names="Trebuchet MS" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownFromDay" Font-Names="Trebuchet MS" Width=40 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownFromYear" Font-Names="Trebuchet MS" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp &nbsp
                                        To:
                                        <asp:DropDownList ID="DropDownToMonth" Font-Names="Trebuchet MS" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownToDay" Font-Names="Trebuchet MS" Width=40 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownToYear" Font-Names="Trebuchet MS" Width=70 runat="server">
                                        </asp:DropDownList>
                                        <hr style="color:White" />
                                        Status : 
                                        <asp:DropDownList ID="DropDownStatusInSearch" Font-Names="Trebuchet MS" Width=70 runat="server">
                                            <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                            <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                            <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                                        </asp:DropDownList> &nbsp
                                        Order By :
                                        <asp:DropDownList ID="DropDownOrderByInSearch" Font-Names="Trebuchet MS" Width=80 runat="server">
                                            <asp:ListItem Text="Priority" Value="Priority"></asp:ListItem>
                                            <asp:ListItem Text="Date Posted" Value="DatePosted"></asp:ListItem>
                                        </asp:DropDownList> &nbsp 
                                        <asp:DropDownList ID="DropDownOrderByDirection" Font-Names="Trebuchet MS" Width=100 runat="server">
                                            <asp:ListItem Text="Descending" Value="Desc"></asp:ListItem>
                                            <asp:ListItem Text="Ascending" Value="Asc"></asp:ListItem>
                                        </asp:DropDownList> &nbsp &nbsp &nbsp
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="Buttons" Font-Names="Trebuchet MS" CausesValidation=false /> &nbsp &nbsp
                                        <asp:Button ID="BtnDefault" runat="server" Text="Default" CssClass="Buttons" Font-Names="Trebuchet MS" CausesValidation=false />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <BR><BR><BR>
                            <asp:Table ID="tblSearchResult" runat="server" Width="95%" BorderColor=Lightblue BorderWidth=1 GridLines=Vertical Font-Names="Trebuchet MS" HorizontalAlign=Center>
                                <asp:TableRow CssClass="ReportSMSelected">
                                    <asp:TableCell HorizontalAlign=Center Width=10%>
                                        Ticket No 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=45%>
                                        Subject
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=15%>
                                        DatePosted
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=8%>
                                        Priority
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=7%>
                                        Status
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=5%>
                                        Age
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
