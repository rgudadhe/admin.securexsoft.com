<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestingSort.aspx.vb" Inherits="TestingSort" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sorting</title>
    <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <script src="App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="LinkButton1" runat="server">Export Result</asp:LinkButton>
        <asp:GridView ID="gv1" AutoGenerateColumns="true" runat="server">
        </asp:GridView>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#gv1').dataTable( {
					"sPaginationType": "full_numbers"

				} );
			} );
</script>
</html>
