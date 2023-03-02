<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeaveApproval.aspx.vb" Inherits="LeaveAttendanceMainNew_Supervisor_LeaveApproval" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Approval</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
        <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridViewLApprove').dataTable( {
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                             		{ "asSorting": [ "desc", "asc" ] },
		                            null,
		                            null
	                              ] ,
                    "aaSorting": [[ 4, "asc" ]]
				} );
			} );
</script>

</head>
<body>
    <form id="frmApproval" runat="server">
    <div>
        <asp:Table ID="tblCancel" runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv">
                    Leave Application Approval
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GridViewLApprove" runat="server"  Width="100%" PageSize="100"  AutoGenerateColumns="false">
                        <AlternatingRowStyle BackColor="OldLace"   />
                        <FooterStyle HorizontalAlign="Right"  />  
                        <Columns>
                            <asp:TemplateField HeaderText="Name" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" SortExpression="Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <asp:Label ID="Name" runat="server" Text=<%#Eval("Name") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"  SortExpression="TypeOfLeave" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"  HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="LeaveType" runat="server" Text=<%#Eval("TypeOfLeave")%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StartDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" SortExpression="StartDate" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"  HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="StartDate" runat="server" Text=<%#Eval("StartDate").ToShortDateString()%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EndDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" SortExpression="EndDate" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"  HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="EndDate" runat="server" Text=<%#Eval("EndDate").ToShortDateString()%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" SortExpression="AppDate" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"  HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="AppDate" runat="server" Text=<%#Eval("AppDate").ToShortDateString() %>></asp:Label>
                                  </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" HeaderStyle-HorizontalAlign="Center" SortExpression="Reason" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <asp:Label ID="Reason" runat="server" Text=<%#Eval("Reason") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="LeaveBalance" HeaderStyle-HorizontalAlign="Center" SortExpression="TL" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-CssClass="Header"> 
                        		<ItemTemplate>
                            	    <asp:Label ID="LB" runat="server" Text=<%# IIF(ISDBNULL(Eval("TL")),0,Eval("TL")) %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>                    	    
                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                                    <FONT FACE="Arial" SIZE="2">                         
                                        <a href='LeaveAction.aspx?Str=Approve&LeaveID=<%# DataBinder.Eval(Container.DataItem, "LeaveID" )%>'>Approve</a>  
                                    </FONT>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                    	    <asp:TemplateField>
                    	        <ItemTemplate>
                    	            &nbsp;&nbsp;
                    	        </ItemTemplate>
                    	    </asp:TemplateField>
                    	    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-CssClass="Header">
                    	        <ItemTemplate>
                    	            <FONT FACE="Arial" SIZE="2">                         
                    	                <a href='LeaveAction.aspx?Str=DisApprove&LeaveID=<%# DataBinder.Eval(Container.DataItem, "LeaveID" )%>'>Disapprove</a>                     	        
                    	            </FONT> 
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
