<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CIMS.aspx.vb" Inherits="CIMS_CIMS" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Trouble Ticket</title>
    <link rel='stylesheet' type='text/css' href="../Main.css"/>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="ToolTip.js" type="text/javascript"></script>
     <style type="text/css" >
   

a, a:visited {	
	color: #150517; 
	background: inherit;
	text-decoration: none;		
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
a:hover {
	color: #544E4F;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}


   </style>  
   
    <script type="text/JavaScript" >
        function IMG1_onclick() 
        {
            url="RaiseTicket.aspx";
	        newwindow=window.open(url,'name','height=275,width=680, left=300, top=100');
	        if (window.focus) {newwindow.focus()}
        }

</script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="background-image:url(../images/voicefiles.jpg); height:30px; text-align: left;">
                    <span style="font-size: 8pt; font-family: Arial"><strong>Trouble Ticket<span
                        style="font-size: 8pt"><span style="font-family: Times New Roman">&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp <img id="Guide" src="../Images/icon_help_16x16.gif" style="cursor:hand ;" onMouseover="tip_it('ToolTip','Trouble Ticket','Use this link to create a Trouble Ticket securely and communicate with our Support Team or HelpDesk.  The link allows users to log and keep track of issues such as Missing Reports, Turnaround time, etc.  For users who do not have email software, this is a great way to communicate securely with Support team and get issues resolved promptly.','../images/voicefiles.jpg'); " onMouseout="hideIt('ToolTip');" width="16" height="16"  /></span></span></strong>
                    </span>
                </td>
            </tr>
        </table>
        <div style="text-align: right">
              <input id="IMG1" type="button" value="New Ticket" onclick="return IMG1_onclick()" class="button"  />
        </div>
        <div style="text-align: center">
            <asp:Panel ID="Panel2" runat="server" width="100%">
                <table width="100%">
                    <tr>
                        <td class="Voice4" style="text-align: center" >
                            Search Tickets
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="(Show Details...)"/>
                        </td>
                    </tr>
                </table> 
           </asp:Panel>
           <asp:Panel ID="Panel1" runat="server" width="100%">
                <table width="100%">
            
                    <tr>
                        <td class="Voice5" style="text-align: center" >
                            Status
                        </td>
                        <td class="Voice5" style="text-align: center" >
                            Ticket Number
                        </td>
                        <td class="Voice5" style="text-align: center" colspan=2>
                           Posted Date
                        </td>
                        <td class="Voice5" style="text-align: center" >
                           Priority
                        </td>  
                    </tr>
                    <tr>
                        <td class="Voice5" style="text-align: center">
                            <asp:DropDownList ID="DropDownStatusInSearch" Font-Names="Arial" Font-Size="8pt" Width=70 runat="server" AutoPostBack="true" >
                                <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="Voice5" style="text-align: center">
                            <asp:TextBox ID="txtSearchTicketNo" Font-Names="Arial" Font-Size="8pt" runat="server"></asp:TextBox>
                        </td>
                        <td class="Voice5" style="text-align: center">
                            <asp:TextBox ID="TXTDate1" runat="server" Width="70"  Font-Names="Arial" Font-Size="8pt" ></asp:TextBox>
                            <asp:ImageButton runat="Server" ID="ImageButton3" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" /><br />
                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="TXTDate1" PopupButtonID="ImageButton3" />
                        </td>
                        <td class="Voice5" style="text-align: center">
                            <asp:TextBox ID="TXTDate2" runat="server" Width="70"  Font-Names="Arial" Font-Size="8" ></asp:TextBox>
                            <asp:ImageButton runat="Server" ID="ImageButton4" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" /><br />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXTDate2" PopupButtonID="ImageButton4" />
                        </td>
                        <td class="Voice5" style="text-align: center">
                            <asp:DropDownList ID="DropDownPrioritySearch" Font-Names="Arial" Font-Size="8pt" Width=100 runat="server">
                                <asp:ListItem Text="Any" Value="Any"></asp:ListItem>
                                <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                <asp:ListItem Text="High" Value="High"></asp:ListItem>
                            </asp:DropDownList>
                        </td>        
                    </tr>
                    <tr class="Voice">
                        <td style="text-align: center;" colspan="5" class="Voice">
                            <asp:Button ID="Button2" runat="server"  CssClass="button" Text="Search"   />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpesetting" runat="Server"
                    TargetControlID="Panel1"
                    ExpandControlID="Panel2"
                    CollapseControlID="Panel2" 
                    ImageControlID="Image1"    
                    ExpandedText="(Hide Details...)"
                    CollapsedText="(Show Details...)"
                    ExpandedImage="~/images/collapse_blue.jpg"
                    CollapsedImage="~/images/expand_blue.jpg"
                    SuppressPostBack="true" EnableViewState="true" 
            />
            
        </div>  
        <br />
        <asp:Panel ID="Panel3" runat="server" width="99%">
            <asp:CompleteGridView  ID="MyDataGrid" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" CellPadding="2" AllowSorting="True" 
                Font-Names="Arial"   DataSourceID="SqlDataSource1"   
                Font-Size="8" 
                Width="100%" CellSpacing="2" PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" ShowCount="True" Font-Italic="False" CaptionAlign="Bottom" SortAscendingImageUrl="~/Images/asc.gif" SortDescendingImageUrl="~/Images/desc.gif" DataKeyNames="TicketID" ShowInsertRow="True" >
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                <HeaderStyle ForeColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="8" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Voice5"></HeaderStyle>
                <EditRowStyle BackColor="#999999"></EditRowStyle>
                <PagerStyle CssClass="Voice5" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center"></PagerStyle>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" VerticalAlign="Middle"></AlternatingRowStyle>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerSettings PreviousPageText="Previous" LastPageImageUrl="~/Images/Last.GIF" PreviousPageImageUrl="~/Images/Prev.GIF" FirstPageImageUrl="~/Images/First.GIF" NextPageImageUrl="~/Images/next.GIF" PageButtonCount="25" Mode="NextPreviousFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                <Columns>
                    <%--<asp:BoundField DataField="TicketID" HeaderText="ID" Visible="false" />--%>
                    <asp:TemplateField HeaderText="Ticket Number" SortExpression="TicketNo">
                        <ItemTemplate>
                            <a href="Action.aspx?ID=<%#Eval("TicketID") %>"><u><%#Eval("TicketNo") %></u></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="TicketDetails" HeaderText="Description" SortExpression="TicketDetails" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DatePosted" HeaderText="Date Posted" SortExpression="DatePosted" />
                    <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                </Columns>
            </asp:CompleteGridView>
          </asp:Panel>   
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString = "<%$ ConnectionStrings:SecureWebConnectionString %>"  >
          </asp:SqlDataSource>    
    </div>
    <div style='position:absolute; visibility:hidden; z-index:1000;' id='ToolTip'></div>
    </form>
</body>
</html>
