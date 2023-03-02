<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PastTickets.aspx.vb" Inherits="ERSSMainNew_PastTickets" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ticket History</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    
</head>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridViewhistory').dataTable( {
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
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblCancel" runat="server" Width="100%" >
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv">
                    Ticket History
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None">
                    <asp:GridView ID="GridViewhistory" runat="server" AutoGenerateColumns="false" Width="100%">
                        <AlternatingRowStyle BackColor="OldLace"   />
                        <FooterStyle HorizontalAlign="Right"/>  
                        <Columns>
                            <asp:TemplateField HeaderText="Ticket No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1%" HeaderStyle-CssClass="Header"> 
                        		<ItemTemplate>
                            	    <asp:Label ID="TicketNo" runat="server" Text=<%#Eval("TicketNo") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Description" ItemStyle-VerticalAlign="Top"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="Desc" runat="server" Text=<%#Eval("Description")%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IssueName" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-CssClass="Header" > 
                                <ItemTemplate>
		                            <asp:Label ID="IssueName" runat="server" Text=<%#Eval("IssueName")%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DatePosted" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="DatePosted" runat="server" Text=<%#Eval("DatePosted").ToShortDateString() %>></asp:Label>
                                  </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                        		<ItemTemplate>
                            	    <asp:Label ID="Status" runat="server" Text=<%#Eval("Status") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="alt1"> 
                        		<ItemTemplate>
                            	    <a href="DetailPastTicket.aspx?TID=<%#Eval("TicketID") %>">Details</a>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--<asp:SqlDataSource ID="SQLDataSrc" runat="server">
	                </asp:SqlDataSource>--%>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
