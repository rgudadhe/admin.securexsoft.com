<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceApproval.aspx.vb" Inherits="LeaveAttendanceMainNew_Supervisor_AttendanceApproval" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attendance Approval</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridViewAApprove').dataTable( {
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 6, "asc" ]]
				} );
			} );
    </script>

</head>
<body>
    <form id="frmAttApproval" runat="server">
    <div>
        <asp:Table ID="tblAttendanceApprove" runat="server" Width="100%" >
            <asp:TableRow>
                <asp:TableCell CssClass="HeaderDiv" HorizontalAlign="Center">
                    Attendance Request Approval
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None">
                    <asp:GridView ID="GridViewAApprove" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <asp:Label ID="Name" runat="server" Text=<%#Eval("Name") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attendance Date" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="LeaveType" runat="server" Text=<%#Eval("AttDate").toShortDateString()%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Time" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="StartDate" runat="server" Text=<%#Eval("SignIn")%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Time" ItemStyle-VerticalAlign=Top SortExpression="SignOut" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="EndDate" runat="server" Text=<%#Eval("SignOut")%>></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="Header" > 
                        		<ItemTemplate>
                            	    <asp:Label ID="Reason" runat="server" Text=<%#Eval("Reason") %>></asp:Label>
                        		</ItemTemplate>
                    	    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application Date" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="Header"> 
                                <ItemTemplate>
		                            <asp:Label ID="AppDate" runat="server" Text=<%#Eval("AppDate").ToShortDateString() %>></asp:Label>
                                  </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="Header"> 
                        		<ItemTemplate>
                                    <FONT FACE="Arial" SIZE="2">                         
                                        <a href='AttendanceAction.aspx?Str=Approve&AttReqID=<%# DataBinder.Eval(Container.DataItem, "AttReqID" )%>'>Approve</a> /
                                        <a href='AttendanceAction.aspx?Str=DisApprove&AttReqID=<%# DataBinder.Eval(Container.DataItem, "AttReqID" )%>'>Disapprove</a> 
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
