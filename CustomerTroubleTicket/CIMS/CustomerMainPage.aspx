<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustomerMainPage.aspx.vb" Inherits="CustomerMainPage" EnableViewState="true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language=javascript type="text/javascript">
    function OpenPage(Str)
    {
        alert('Test')
        alert(Str)
    }
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Untitled Page</title>
    <link href="Styles/Report.css" rel=stylesheet />
    <link href="Styles/Accordin.css" rel=stylesheet />
</head>
<body style="background-color:WhiteSmoke">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <Ajax:TabContainer ID="TestTabContainer" runat=server Width="100%">
            <Ajax:TabPanel ID="TestTab1" runat=server BorderColor=lightblue BorderStyle=Solid BorderWidth=2 >
                <HeaderTemplate>
                    <asp:Label ID="Label3" runat="server" Text="Browse Tickets"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%--<br /><br /><br />--%>
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
                                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size=Large ForeColor=Crimson Text="View Ticket"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell><BR><BR><BR></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell VerticalAlign=Bottom>
                                                <asp:Button ID="BtnAction" runat="server" CssClass="Buttons" Font-Names="Arial" Text="Action"  CausesValidation=false />&nbsp
                                                <asp:Button ID="BtnPrint"  runat="server" CssClass="Buttons" Font-Names="Arial" Text="Print" UseSubmitBehavior=false CausesValidation=false /> &nbsp
                                                <asp:Button ID="BtnLog" runat="server" CssClass="Buttons" Font-Names="Arial" Text="Log" CausesValidation=false /> &nbsp
                                                <asp:Button ID="BtnReloadTicketList" CssClass="Buttons" runat="server" Font-Names="Arial" Text="Reload Ticket List" CausesValidation=false /> &nbsp
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
                                                            <asp:Button ID="BtnSubmit" Font-Names="Arial"  CausesValidation=false runat="server" Text="Submit"/>
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
            <Ajax:TabPanel ID="TestTab2" runat=server BorderColor=lightblue BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" Text="New Ticket"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <center>
                    <asp:Table ID="Table1" runat="server" Width="85%" GridLines=Both>
                        <asp:TableRow CssClass="ReportHeaderDiv">
                            <asp:TableCell ColumnSpan=2 HorizontalAlign=Center>
                                Report New Issue
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat=server>
                            <asp:TableCell runat=server HorizontalAlign=Right VerticalAlign=Top Width=20%>
                                <asp:Label ID="lblSubject" Font-Names="Arial" Font-Size=Small Font-Italic=True ForeColor=Gray  runat="server" Text="Subject :"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign=Left>
                                <asp:TextBox ID="txtSubject" runat="server" Font-Names="Arial" Width=60%></asp:TextBox>&nbsp &nbsp
                                <asp:RequiredFieldValidator ID="RequiredFieldSubject" runat="server" ErrorMessage="Please enter subject" Font-Size=small Font-Names="Arial" Font-Bold=true Font-Italic=true SetFocusOnError=true ControlToValidate="txtSubject"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign=Right VerticalAlign=Top Width=20%>
                                <asp:Label ID="lblIssueType" Font-Names="Arial" Font-Size=Small Font-Italic=True ForeColor=Gray runat="server" Text="Issue Type :"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:DropDownList ID="DropDownIssueType" runat="server" Font-Names="Arial" Width=150>
                                </asp:DropDownList> &nbsp &nbsp
                                <asp:RequiredFieldValidator ID="RequiredFieldIssueType" runat="server" ErrorMessage="Please select issue type"  Font-Size=small Font-Names="Arial" Font-Bold=true Font-Italic=true SetFocusOnError=true ControlToValidate="DropDownIssueType"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Font-Names="Arial" HorizontalAlign=Right VerticalAlign="top" Width="20%" ForeColor=Gray Font-Italic=true Font-Size=small >
                                Issue Description : 
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <textarea id="txtIssueDesc" rows="4" cols="65" style="font-family:Arial" runat=server></textarea>&nbsp &nbsp    
                                <asp:RequiredFieldValidator ID="RequiredFieldIssueDesc" runat="server" ErrorMessage="Please enter issue description" Font-Size=Small Font-Names="Arial" Font-Bold=true Font-Italic=true SetFocusOnError=true ControlToValidate="txtIssueDesc"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign=Right ForeColor=Gray>
                                <asp:Label ID="lblPriority" runat="server" Font-Names="Arial" Font-Italic=True Font-Size=small Text="Priority :"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign=Left>
                                <asp:DropDownList ID="DropDownPriority" Font-Names="Arial" Font-Size=Small runat="server" Width=150>
                                    <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                    <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                    <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                </asp:DropDownList>&nbsp &nbsp
                                <asp:RequiredFieldValidator ID="RequiredFieldPriority" runat="server" Font-Names="Arial" Font-Bold=true Font-Italic=True Font-Size=small ErrorMessage="Please select priority" ControlToValidate="DropDownPriority"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan=2 HorizontalAlign=Left>
                                <Ajax:Accordion ID="AttachmentSection" Width="100%" runat=server 
                                    HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" FadeTransitions="true" SelectedIndex=-1 FramesPerSecond="40" 
                                    TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <Ajax:AccordionPane runat=server ID="AccordionPaneID">
                                            <Header>Attachment</Header>
                                            <Content>
                                                <asp:Table ID="tblAttach" runat="server" HorizontalAlign=center>
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:FileUpload ID="FileUploadAttachment" Font-Names="Arial" runat="server" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>    
                                            </Content>
                                        </Ajax:AccordionPane>
                                    </Panes>
                                </Ajax:Accordion>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan=2 HorizontalAlign=center>
                                <asp:Button ID="BtnAddNew" runat="server" Font-Names="Arial" UseSubmitBehavior=true CausesValidation=true Text="Report New Issue" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </center>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="TestTab3" BorderColor=LightBlue BorderWidth=1 runat=server >
                <HeaderTemplate>
                    <asp:Label ID="lblTab3" runat="server" Text="Search"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
                        <ContentTemplate>
                            <asp:Table BackColor="GhostWhite" runat=server BorderColor=Lightblue BorderWidth=1 Font-Names="Arial" Width=95% HorizontalAlign=Center>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        Look for : 
                                        <asp:TextBox ID="txtLookfor" Font-Names="Arial" Width=200 Height=18 runat="server" BorderColor=LightBlue BorderStyle=Solid BorderWidth=1></asp:TextBox> &nbsp &nbsp
                                        In : 
                                        <asp:DropDownList ID="DropDownLookIn" Font-Names="Arial" runat="server" Width=100>
                                            <asp:ListItem Value="Any" Text="Any"></asp:ListItem>
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
                                        </asp:DropDownList>
                                        <hr style="color:White" />
                                        Status : 
                                        <asp:DropDownList ID="DropDownStatusInSearch" Font-Names="Arial" Width=70 runat="server">
                                            <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                            <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                            <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                                        </asp:DropDownList> &nbsp
                                        Order By :
                                        <asp:DropDownList ID="DropDownOrderByInSearch" Font-Names="Arial" Width=80 runat="server">
                                            <asp:ListItem Text="Priority" Value="Priority"></asp:ListItem>
                                            <asp:ListItem Text="Date Posted" Value="DatePosted"></asp:ListItem>
                                        </asp:DropDownList> &nbsp 
                                        <asp:DropDownList ID="DropDownOrderByDirection" Font-Names="Arial" Width=100 runat="server">
                                            <asp:ListItem Text="Descending" Value="Desc"></asp:ListItem>
                                            <asp:ListItem Text="Ascending" Value="Asc"></asp:ListItem>
                                        </asp:DropDownList> &nbsp &nbsp &nbsp
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="Buttons" Font-Names="Arial" CausesValidation=false /> &nbsp &nbsp
                                        <asp:Button ID="BtnDefault" runat="server" Text="Default" CssClass="Buttons" Font-Names="Arial" CausesValidation=false />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <BR><BR><BR>
                            <asp:Table ID="tblSearchResult" runat="server" Width="95%" BorderColor=Lightblue BorderWidth=1 GridLines=Vertical Font-Names="Arial" HorizontalAlign=Center>
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
                                                    <asp:Button ID="BtnSearchTicketNo" runat="server" Font-Names="Arial" CssClass="Buttons" Text="Search" CausesValidation=false />
                                                </asp:TableCell>
                                            </asp:TableRow>
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
