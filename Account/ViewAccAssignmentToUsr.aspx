<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewAccAssignmentToUsr.aspx.vb" Inherits="Account_ViewAccAssignmentToUsr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Account Assignments</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridView1').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
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
    <div>
        <table width="100%" style="text-align:left ">
            <tr>
                <td style="border:0">
                    Emp/HBA Name : 
                    <asp:DropDownList ID="ddlUsrs" runat="server" Width="200" >
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td style="border:0">
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Firebrick" CssClass="Title1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border:0">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%">
                           <Columns>
                            <asp:BoundField DataField="Emp/HBA Name" HeaderText="Emp/HBA Name" HeaderStyle-CssClass="Header" />
                            <asp:BoundField DataField="AccountName" HeaderText="AccountName" HeaderStyle-CssClass="Header" />
                            <asp:BoundField DataField="LevelName" HeaderText="LevelName" HeaderStyle-CssClass="Header" />
                           </Columns> 
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
