<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewUpd.aspx.vb" Inherits="ViewUpd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

    <title>View Updates</title>
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#MyDataGrid').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">
            <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Updates</h1>
            <asp:Panel ID="Panel2" runat="server" width="400">
           <table width="100%">
            <tr>
                <td colspan="2" style="text-align: center;" class="HeaderDiv">
                   View Updates
                </td>
            </tr>
             <tr>
                <td class="HeaderDiv" colspan="2" style="text-align: center" >
                   Search Updates
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
           </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="400">
           <table width="100%">
             <tr>
                <td colspan="2" style="text-align: center" class="alt1">
                    Date Posted
                 </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="TxtDate1" runat="server" Width="70" CssClass="common"></asp:TextBox>
                              <asp:ImageButton runat="Server" ID="ImageButton3" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" Height="20px" Width="25px" /><br />
        <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="TXTDate1" 
            PopupButtonID="ImageButton3" />
                    </td>
                 <td style="text-align: center">
                      <asp:TextBox ID="TxtDate2" runat="server" Width="70" CssClass="common"></asp:TextBox>
                               <asp:ImageButton runat="Server" ID="ImageButton2" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" Height="20px" Width="25px" /><br />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXTDate2" 
            PopupButtonID="ImageButton2" />
                 </td>
                 
            </tr>
            
           <tr>
                <td style="text-align: center;" colspan="2">
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
        ExpandedImage="~/App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="~/App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
    
   <br />
            <asp:GridView ID="MyDataGrid" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Date Posted" ItemStyle-Width="100" HeaderStyle-CssClass="Header">
                        <ItemTemplate>
                            <%#Eval("DateDisp").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department" ItemStyle-Width="100" HeaderStyle-CssClass="Header">
                        <ItemTemplate>
                            <%#Eval("Department").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="100" HeaderStyle-CssClass="Header">
                        <ItemTemplate>
                            <%#Eval("SubText").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="Header">
                        <ItemTemplate>
                            <%#Eval("Details").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDelete" CommandName="Delete" CommandArgument='<%#Eval("TrackID").toString()%>' runat="server" CausesValidation="false" CssClass="common">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
          </div> 
          </div> 
        </asp:Panel>
       
             
            
        
    </form>
</body>
</html>
