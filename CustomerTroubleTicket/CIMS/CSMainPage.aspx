<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CSMainPage.aspx.vb" Inherits="CSMainPage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CIMS</title>
    <link href="Styles/Report.css" rel=stylesheet />
    <link href="Styles/Accordin.css" rel=stylesheet />
</head>
<body style="background-color:WhiteSmoke">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ajaxToolkit:TabContainer ID="CSTabContainer" runat=server Width=100%>
            <ajaxToolkit:TabPanel ID="TabActiveTickets" runat=server>
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text="ActiveTickets"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>   
                            <br /><br />
                            <div id="DivTab" runat=server>
                                <asp:Table ID="tblView" runat="server" HorizontalAlign=Center BorderColor=LightBlue BorderWidth=1 Font-Names="Arial" Width=95%>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign=Left>
                                            View Tickets : 
                                            <asp:Label ID="lblView" runat="server" Text=""></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign=Right>
                                            View : 
                                            <asp:DropDownList ID="DropDownView" runat="server" Font-Names="Arial" AutoPostBack=true>
                                                <asp:ListItem Text="Any" Value="An"></asp:ListItem>
                                                <asp:ListItem Text="Open Tickets" Value="OT"></asp:ListItem>
                                                <asp:ListItem Text="Closed Tickets" Value="CT"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow><asp:TableCell ColumnSpan=2></asp:TableCell></asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign=Left>
                                            <asp:LinkButton ID="LinkBtnOpenTickets" runat="server" CausesValidation=false Font-Names="Arial"><asp:Label ID="lblLinkBtnOpenTickets" runat="server" Font-Names="Arial" Text=""></asp:Label></asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign=Right>
                                            <asp:LinkButton ID="LinkBtnCloseTickets"  runat="server" CausesValidation=false Font-Names="Arial"><asp:Label ID="lblLinkBtnCloseTickets" runat="server" Font-Names="Arial" Text=""></asp:Label></asp:LinkButton>
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
                                                <asp:GridView ID="GridViewCustTickets" Width="100%" Height=280 Font-Names="Arial" AllowPaging=true AllowSorting=true runat="server"
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
                                                    <asp:TemplateField HeaderText="Account Name" HeaderStyle-HorizontalAlign=Center SortExpression="AccName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccName" runat="server" Text=<%#Eval("AccName")%>></asp:Label>
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
                                        <asp:Table ID="tblViewTicket" runat="server" Visible=false HorizontalAlign=Center BorderColor=LightBlue BorderWidth=1 Font-Names="Arial" Width=95%>
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size=Large ForeColor=Crimson Text="View Ticket"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell><BR><BR><BR></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell VerticalAlign=Bottom>
                                                    <asp:Button ID="BtnReply" runat="server" CssClass="Buttons" Font-Names="Arial" CommandArgument="Reply" Text="Reply to Customer"  CausesValidation=false /> &nbsp
                                                    <asp:Button ID="BtnForward" runat="server" CssClass="Buttons" Text="Forward Ticket" Font-Names="Arial" CommandArgument="Forward" CausesValidation=false /> &nbsp
                                                    <asp:Button ID="BtnPrint"  runat="server" CssClass="Buttons" Font-Names="Arial" Text="Print" UseSubmitBehavior=false CausesValidation=false /> &nbsp
                                                    <asp:Button ID="BtnLog" runat="server" CssClass="Buttons" Font-Names="Arial" Text="Log" CausesValidation=false /> &nbsp
                                                    <asp:Button ID="BtnReloadTicketList" CssClass="Buttons" runat="server" Font-Names="Arial" Text="Reload Ticket List" CausesValidation=false /> &nbsp<hr style="color:LightBlue;">
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
                                                                <asp:Button ID="BtnSubmit" Font-Names="Arial" CausesValidation=false runat="server" Text="Submit"/>
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
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabIssueManagement" runat=server>
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" Text="Issue Management"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelIssueManagement" runat="server">
                        <ContentTemplate>
                            <asp:Table ID="Table2" Width="100%" Font-Names="Arial" runat="server" GridLines=Horizontal>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:GridView ID="GridViewIssueTypes" runat="server" AllowSorting=true AllowPaging=True
                                            Width=100% BackColor="#eaeaea" ShowFooter=true Font-Names="Arial" Font-Size=Small AutoGenerateColumns=false DataSourceID="sqlDataSource1" DataKeyNames="IssueID" OnRowEditing="GridViewIssueTypes_RowEditing" OnRowUpdating="GridViewIssueTypes_RowUpdating" OnRowDeleting = "GridViewIssueTypes_RowDeleting">
                                            <HeaderStyle HorizontalAlign=Center  CssClass="ReportHeaderDiv" ForeColor=white />
                                            <AlternatingRowStyle Backcolor="#F7F7F7" />
                                            <SelectedRowStyle BackColor="#123456" />
                                            <FooterStyle BackColor=white />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="txtIssueID" runat="server" Value='<%#Eval("IssueID") %>' /> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Name" SortExpression="IssueName" HeaderStyle-CssClass="ReportHeaderDiv" HeaderStyle-HorizontalAlign=Center HeaderStyle-Width="25%">
                                                    <ItemTemplate><%#Eval("IssueName")%></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtIssueName" Text='<%# Eval("IssueName")%>' Font-Names="Arial" runat="server" Width="95%"></asp:TextBox>
                                                    </EditItemTemplate>    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" SortExpression="IssueDesc" HeaderStyle-CssClass="ReportHeaderDiv" HeaderStyle-HorizontalAlign=Center HeaderStyle-Width="60%">
                                                    <ItemTemplate><%#Eval("IssueDesc")%></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtIssueDesc" Text='<%# Eval("IssueDesc")%>' Font-Names="Arial" runat="server" Width="95%"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Edit" CausesValidation=false ShowEditButton="True"/>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkDeleteCate" CommandName="Delete" runat="server" CausesValidation=false>Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>     
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <ajaxToolkit:Accordion ID="MyAccordion" runat="server" Width="100%"
                                            HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" FadeTransitions="true" SelectedIndex=-1 FramesPerSecond="40" 
                                            TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                            <Panes>
                                                <ajaxToolkit:AccordionPane ID="AddIssueTypePane" runat=server>
                                                <Header>Add New Issue Type</Header>
                                                <Content>
                                                    <asp:Table ID="tblAddIssueType" HorizontalAlign=Center runat="server" Font-Names="Arial" Font-Size=Small GridLines=Both Width=82%>
                                                        <asp:TableRow>
                                                            <asp:TableCell Width=10% HorizontalAlign=Right>
                                                                <asp:Label ID="lblIssueName" runat="server" Text="Issue Name"></asp:Label> 
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="75%">
                                                                <asp:TextBox ID="txtIssueName" runat="server" Font-Names="Arial" Width=50%></asp:TextBox> &nbsp &nbsp
                                                                <asp:RequiredFieldValidator ID="RequiredFieldIssueType" Font-Names="Arial" Font-Bold=true Font-Italic=True runat="server" ErrorMessage="Please enter Issue Type" ControlToValidate="txtIssueName"></asp:RequiredFieldValidator>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell Width=10% VerticalAlign=Top HorizontalAlign=Right>
                                                                <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="75%">
                                                                <textarea id="txtDesc" rows="4" style="font-family:Arial" cols="75" runat=server></textarea> &nbsp
                                                                <asp:RequiredFieldValidator ID="RequiredFieldIssueDesc" Font-Names="Arial" Font-Bold=true Font-Italic=True runat="server" ErrorMessage="Please enter Issue Description" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell ColumnSpan=2 HorizontalAlign=center> 
                                                                <asp:Button ID="BtnAddIssueType" runat="server" Font-Names="Arial" Font-Size=small Text="Add" UseSubmitBehavior=true />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                               </Content>
                                               </ajaxToolkit:AccordionPane>
                                           </Panes>
                                       </ajaxToolkit:Accordion>
                                    </asp:TableCell>
                                </asp:TableRow>   
                            </asp:Table>
                            <asp:SqlDataSource ID="sqlDataSource1" runat=server ConnectionString='Server=win11619;Database=ETS;UID=SA;PWD=c4t!ar0und' SelectCommand="SELECT * FROM dbo.tblCustomerIssueType where IsDeleted IS NULL" >
                            </asp:SqlDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabSearch" BorderColor=LightBlue BorderWidth=1 runat=server>
                <HeaderTemplate>
                    <asp:Label ID="lblTab3" runat="server" Text="Search"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
                        <ContentTemplate>
                            <asp:Table ID="Table1" BackColor="GhostWhite" runat=server BorderColor=Lightblue BorderWidth=1 Font-Names="Arial" Width=95% HorizontalAlign=Center>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        Look for : 
                                        <asp:TextBox ID="txtLookfor" Font-Names="Arial" Width=200 Height=18 runat="server" BorderColor=LightBlue BorderStyle=Solid BorderWidth=1></asp:TextBox> &nbsp &nbsp
                                        In : 
                                        <asp:DropDownList ID="DropDownLookIn" Font-Names="Arial" runat="server" Width=100>
                                            <asp:ListItem Value="Any" Text="Any"></asp:ListItem>
                                            <asp:ListItem Value="AccName" Text="Account Name"></asp:ListItem>
                                            <asp:ListItem Value="Subject" Text="Subject"></asp:ListItem>
                                            <asp:ListItem Value="Message" Text="Message"></asp:ListItem>
                                        </asp:DropDownList>&nbsp &nbsp
                                        Priority : 
                                        <asp:DropDownList ID="DropDownPrioritySearch" Font-Names="Arial" Width=100 runat="server">
                                            <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                        </asp:DropDownList>
                                        <hr style="color:White" />
                                        From : 
                                        <asp:DropDownList ID="DropDownFromMonth" Font-Names="Arial" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownFromDay" Font-Names="Arial" Width=40 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownFromYear" Font-Names="Arial" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp &nbsp
                                        To:
                                        <asp:DropDownList ID="DropDownToMonth" Font-Names="Arial" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownToDay" Font-Names="Arial" Width=40 runat="server">
                                        </asp:DropDownList> &nbsp
                                        <asp:DropDownList ID="DropDownToYear" Font-Names="Arial" Width=70 runat="server">
                                        </asp:DropDownList> &nbsp &nbsp &nbsp 
                                        Forwarded Ticket To : 
                                        <asp:DropDownList ID="DropDownDeptIDInSearch" Font-Names="Arial" Width=150 runat="server">
                                        </asp:DropDownList>
                                        <hr style="color:White" />
                                        Status : 
                                        <asp:DropDownList ID="DropDownStatusInSearch" Font-Names="Arial" Width=70 runat="server">
                                            <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                            <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                            <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                                        </asp:DropDownList> &nbsp
                                        Order By :
                                        <asp:DropDownList ID="DropDownOrderByInSearch" Font-Names="Arial" Width=120 runat="server">
                                            <asp:ListItem Text="Priority" Value="Priority"></asp:ListItem>
                                            <asp:ListItem Text="Date Posted" Value="DatePosted"></asp:ListItem>
                                            <asp:ListItem Text="Account Name" Value="A.AccName"></asp:ListItem>
                                        </asp:DropDownList> &nbsp 
                                        <asp:DropDownList ID="DropDownOrderByDirection" Font-Names="Arial" Width=100 runat="server">
                                            <asp:ListItem Text="Descending" Value="Desc"></asp:ListItem>
                                            <asp:ListItem Text="Ascending" Value="Asc"></asp:ListItem>
                                        </asp:DropDownList> &nbsp &nbsp &nbsp
                                        <asp:Button ID="BtnSearch" runat="server" CssClass="Buttons" Text="Search" Font-Names="Arial" CausesValidation=false /> &nbsp &nbsp
                                        <asp:Button ID="BtnDefault" runat="server" CssClass="Buttons" Text="Default" Font-Names="Arial" CausesValidation=false />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <BR><BR><BR>
                            <asp:Table ID="tblSearchResult" runat="server" Width="95%" BorderColor=Lightblue BorderWidth=1 GridLines=Vertical Font-Names="Arial" HorizontalAlign=Center>
                                <asp:TableRow CssClass="ReportSMSelected">
                                    <asp:TableCell HorizontalAlign=Center Width=10%>
                                        Ticket No 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=30%>
                                        Subject
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=15%>
                                        DatePosted
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center Width=15%>
                                        Account Name
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
                            <BR>
                            <asp:Table ID="tblSearchTicketNoDummay" runat="server" HorizontalAlign=center Width=95%>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign=Right>
                                        <asp:Table ID="tblSearchTicketNo" runat="server" BorderColor=LightBlue BorderWidth=1>
                                            <asp:TableRow>  
                                                <asp:TableCell>
                                                    <asp:Label ID="Label4" runat="server" Text="Ticket No : "></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell BackColor="GhostWhite">
                                                    <asp:TextBox ID="txtSearchTicketNo" Font-Names="Arial" Width=100 Height=18 BorderColor=LightBlue BorderStyle=Solid BorderWidth=1 runat="server"></asp:TextBox> &nbsp
                                                    <asp:Button ID="BtnSearchTicketNo" runat="server" CssClass="Buttons" Font-Names="Arial" Text="Search" CausesValidation=false />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate> 
            </ajaxToolkit:TabPanel> 
            <ajaxToolkit:TabPanel ID="TabTicketManagement" BorderColor=LightBlue BorderWidth=1 runat=server>
                <HeaderTemplate>
                    <asp:Label ID="Label5" runat="server" Text="Ticket Management"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelInTicketManagement" runat="server">
                        <ContentTemplate>
                            <asp:Table ID="tblSearchInTicketManagement" runat="server" BackColor="GhostWhite" BorderColor=Lightblue BorderWidth=1 Font-Names="Arial" Width=95% HorizontalAlign=Center>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        Look for :
                                        <asp:TextBox ID="txtLookforInTicketManagement" runat="server" Font-Names="Arial" Width=200 Height=18 BorderColor=LightBlue BorderStyle=Solid BorderWidth=1></asp:TextBox> &nbsp &nbsp    
                                        In : 
                                        <asp:DropDownList ID="DropDownLookINTicketManagement" runat="server" Font-Names="Arial" Width=100>
                                            <asp:ListItem Value="Any" Text="Any"></asp:ListItem>
                                            <asp:ListItem Value="UserName" Text="UserID"></asp:ListItem>
                                            <asp:ListItem Value="FirstName" Text="FirstName"></asp:ListItem>
                                            <asp:ListItem Value="LastName" Text="LastName"></asp:ListItem>
                                        </asp:DropDownList> &nbsp &nbsp
                                        Department : 
                                        <asp:DropDownList ID="DropDownListDeptIDINTicketManagement" runat="server" Font-Names="Arial" Width=200 BorderColor=LightBlue BorderStyle=Solid BorderWidth=1 EnableViewState=true>
                                        </asp:DropDownList> &nbsp &nbsp
                                        <asp:Button ID="BtnSearchInTicketManagement" CssClass="Buttons" CausesValidation=false runat="server" Font-Names="Arial" Text="Search" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <BR><BR><BR>
                            <asp:Table ID="tblSearchResultInTicketManagement" runat="server" Width="95%" BorderColor=Lightblue BorderWidth=1 GridLines=Vertical Font-Names="Arial" HorizontalAlign=Center>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan=5>
                                        <asp:RadioButtonList ID="RadioButtonAccess" AutoPostBack=true OnSelectedIndexChanged="RadioButtonAccess_SelectedIndexChanged" Font-Names="Arial" runat="server">
                                            <asp:ListItem Text="Read Tickets" Value="Read" Selected=True></asp:ListItem>
                                            <asp:ListItem Text="Update Tickets" Value="Update"></asp:ListItem>
                                        </asp:RadioButtonList> 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="ReportSMSelected">
                                    <asp:TableCell HorizontalAlign=Center>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center>
                                        UserID
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center> 
                                        Name
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign=Center>
                                        Department
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        Ticket Access
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table><BR>                            
                            <asp:Table ID="tblSearchTableInTicketManagement" runat="server" Width=95% HorizontalAlign=Center>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign=Center>
                                        <asp:Button ID="BtnSubmitInTicketManagement" CssClass="Buttons" runat="server" CausesValidation=false Font-Names="Arial" Text="Update Access" /> &nbsp
                                        <asp:Button ID="BtnRemoveInTicketManagement" CssClass="Buttons" runat="server" CausesValidation=false Font-Names="Arial" Text="Remove Access" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID=RadioButtonAccess EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>          
        </ajaxToolkit:TabContainer>
    </div>
    </form>
</body>
</html>
