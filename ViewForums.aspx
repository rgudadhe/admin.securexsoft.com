<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewForums.aspx.vb" Inherits="ViewForums" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

    <title>View Forums</title>
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#MyDataGrid').dataTable( {
					//"sPaginationType": "full_numbers"
//                    "aoColumns": [
//                            		{ "asSorting": [ "desc", "asc" ] },
//		                            { "asSorting": [ "desc", "asc" ] },
//		                            { "asSorting": [ "desc", "asc" ] }
//		                            
//	                              ] 
				} );
			} );
</script> 
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Forums</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
        <table width="50%">
            <tr>
                <td class="HeaderDiv" style="text-align: center">
                    View Forums
                </td>
            </tr>
            <tr>
                <td style="border:0">
                    <asp:CompleteGridView  ID="MyDataGrid" runat="server" CssClass="tablesorter" AutoGenerateColumns="False" 
                    PageSize="25" CaptionAlign="Bottom" SortAscendingImageUrl="~/App_Themes/Images/asc.gif" SortDescendingImageUrl="~/App_Themes/Images/desc.gif" Width="100%" OnRowUpdating="MyDataGrid_RowUpdating">

               <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                <EditRowStyle BackColor="#999999" Font-Names="Arial" Font-Size="9pt"></EditRowStyle>
<%--                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center"></PagerStyle>
                <PagerSettings PreviousPageText="Previous" LastPageImageUrl="~/App_Themes/Images/Last.GIF" PreviousPageImageUrl="~/App_Themes/Images/Prev.GIF" FirstPageImageUrl="~/App_Themes/Images/First.GIF" NextPageImageUrl="~/App_Themes/Images/next.GIF" PageButtonCount="25" Mode="NextPreviousFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
--%>                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="1" ItemStyle-Width="1">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdnForumID" Value='<%#Eval("ForumID").toString()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:CheckBox ID="isDeleted" Checked='<%#IIf(ISDBNULL(Eval("IsDeleted")),False,Eval("IsDeleted"))%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDetails" Text='<%# Eval("Details") %>' CssClass="common" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDetails" Text='<%# Eval("Details") %>' runat="server" CssClass="common"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center"  CausesValidation="false"  ShowEditButton="True" HeaderStyle-CssClass="alt1" />
                        </Columns>
            <EmptyDataRowStyle Font-Names="Arial" Font-Size="9pt" />
                </asp:CompleteGridView>
                </td>
            </tr>
        </table>
        
        
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"
            SelectCommand="SELECT [ForumID], [Details], [Topics], [Posts], [isDeleted] FROM [tblforum]    ORDER BY [Details]"
            updatecommand="update [tblforum] set Details = @Details, isdeleted = @isDeleted where ForumID=@ForumID">
        </asp:SqlDataSource>--%>
    
    
    </div>
    
        </asp:Panel>
        </div> 
        </div> 
    
    </form>
</body>
</html>
