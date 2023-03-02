<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewCooArticle.aspx.vb" Inherits="testets_Default3" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="styles/Default.css"type="text/css" rel="stylesheet" />

    <title>Untitled Page</title>
   <style type="text/css" >
tr.tblbg {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg1 {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg2 {
	background: #e7e6e6 url(../images/tbbg2.jpg) repeat;
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
/* links */
a, a:visited {	
	color: #000000; 
	background: inherit;
	text-decoration: none;		
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
a:hover {
	color: #000000;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}

tr.tblbgbody {
	background: #e7e6e6 url(../images/bgorange1.JPG) repeat;
	
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
input.button1 { 
	font: bold 8pt Arial, Sans-serif; 
	height: 24px;
	margin: 0;
	padding: 2px 3px; 
	color: #ffffff;
	background: #E56717;
	border: 1px solid #dadada;
}

</style> 
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
    
       <table width="100%">
           <tr>
                <td colspan="4" style="text-align: center;" class="HeaderDiv">
                    <span style="font-family: Trebuchet MS"><strong style="font-family: Trebuchet MS, Serif">
                   View COO Message</span></td>
            </tr>
            </table>
             <div style="text-align: center">
        <br />
            <asp:Panel ID="Panel2" runat="server" width="400">
           <table width="100%">
            
             <tr>
                <td  class="HeaderDiv" colspan="2" style="text-align: center" >
                    <strong><span style="font-size: 8pt; font-family: Arial">
                   Search COO Message</span></strong>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
           </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="400">
           <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
            
             <tr>
                <td class="DEMO5" colspan="2" style="text-align: center" >
                    <strong><span style="font-size: 8pt; font-family: Arial">Date Posted&nbsp;</span></strong></td>
                 
                
            </tr>
            
            <tr>
                <td class="DEMO5" style="text-align: center">
                    <asp:TextBox ID="TxtDate1" runat="server" Width="70"  Font-Names="Arial" Font-Size="8" ></asp:TextBox>
                              <asp:ImageButton runat="Server" ID="ImageButton3" ImageUrl="images/calendar.png" AlternateText="Click to show calendar" Height="20px" Width="25px" /><br />
        <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="TXTDate1" 
            PopupButtonID="ImageButton3" />
                    </td>
                 <td class="DEMO5" style="text-align: center">
                      <asp:TextBox ID="TxtDate2" runat="server" Width="70"  Font-Names="Arial" Font-Size="8" ></asp:TextBox>
                               <asp:ImageButton runat="Server" ID="ImageButton2" ImageUrl="images/calendar.png" AlternateText="Click to show calendar" Height="20px" Width="25px" /><br />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXTDate2" 
            PopupButtonID="ImageButton2" />
                 </td>
                 
            </tr>
            
           <tr class="DEMO">
                <td style="text-align: center;" colspan="2" class="DEMO">
                      <asp:Button ID="Button2" runat="server"  CssClass="button" Text="Search"   /></td>
            </tr>
        </table>
    </asp:Panel>
     
     <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
    </div>
   <br />
           <asp:CompleteGridView  ID="MyDataGrid" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" CellPadding="2" AllowSorting="True" DataKeyNames="TrackID" 
                    Font-Names="Arial"   
                    Font-Size="8"  
                     Width="100%" CellSpacing="2" PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" ShowCount="True" Font-Italic="False" CaptionAlign="Bottom" SortAscendingImageUrl="~/Images/asc.gif" SortDescendingImageUrl="~/Images/desc.gif" ShowInsertRow="True" >

               <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                <HeaderStyle BackColor="LightSteelBlue" ForeColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="8" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TransStatus1"></HeaderStyle>
                <EditRowStyle BackColor="#999999"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center" CssClass="TransStatus1"></PagerStyle>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" VerticalAlign="Middle"></AlternatingRowStyle>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerSettings PreviousPageText="Previous" LastPageImageUrl="~/Images/Last.GIF" PreviousPageImageUrl="~/Images/Prev.GIF" FirstPageImageUrl="~/Images/First.GIF" NextPageImageUrl="~/Images/next.GIF" PageButtonCount="25" Mode="NextPreviousFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                 <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("TrackID")%>' CommandName="Remove">Remove</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DateDisp" HeaderText="Date Posted" SortExpression="DateDisp" />
                            <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details" />
                        </Columns>
                </asp:CompleteGridView>
               
    </form>
</body>
</html>
