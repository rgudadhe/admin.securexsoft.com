<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeaveCanReq.aspx.vb" Inherits="LeaveCanReq" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Cancel</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridViewCancel').dataTable( {
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 3, "asc" ]]
				} );
			} );
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblCancel" runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv">
                    Cancel Leave Application
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None">
                    <asp:GridView ID="GridViewCancel" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Leave Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("TypeOfLeave") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="StartDate" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="StartDate" runat="server" Text=<%#Eval("StartDate").ToShortDateString()%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EndDate" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="StartDate" runat="server" Text=<%#Eval("EndDate").ToShortDateString()%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application Date" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="StartDate" runat="server" Text=<%#Eval("AppDate").ToShortDateString() %>></asp:Label>
                                  </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" SortExpression="Reason" HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("Reason") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="Header"> 
                        		<ItemTemplate>
                            	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("Status") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>                    	    
                            <asp:TemplateField HeaderText="Cancel" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <a href="CancelDateSelection.aspx?LeaveID=<%# DataBinder.Eval(Container.DataItem, "LeaveID" )%>" onclick="window.open('CancelDateSelection.aspx?LeaveID=<%# DataBinder.Eval(Container.DataItem, "LeaveID" )%>','', 'width=450,height=240,status=1,scrollbars=1');return false;">Cancel</a>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
