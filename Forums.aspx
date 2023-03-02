<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Forums.aspx.vb" Inherits="testets_Forums" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="images/style.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
</head>
<body>

<!-- wrap starts here -->
<div id="wrap">

				
			
	<!-- content-wrap starts -->
	<div id="content-wrap" class="three-col"  >	
	
		
		<div id="sidebar">
			
			
			<ul class="sidemenu">				
				<li><a href="Forums.aspx">Discussion Forum</a></li>
				<li><a href="AddTopic.aspx">Add New Topic</a></li>
		</ul>	
				
		<!-- sidebar ends -->		
		</div>

		
		
		
		<div id="main">
		    <h1 class="headerstyle"><span style='color: #ff9933'>
                <img src="images/icon1.jpg" width="48" height="43" />
                Forum</span></h1>
			    
            <asp:Table ID="Table1" runat="server" Width="100%" Font-Names="Trebuchet MS" Font-Size="Small"  VerticalAlign="Top">
            <asp:TableRow  ForeColor="#FF8000" runat="server">
            <asp:TableCell runat="server">
            Forum</asp:TableCell> 
            <asp:TableCell runat="server">
            Topics</asp:TableCell>
            <asp:TableCell runat="server">
            Posts</asp:TableCell>
            <asp:TableCell runat="server">
            Last Topic</asp:TableCell>  </asp:TableRow> 
            </asp:Table>
                  
          	
		</div>
		
	<!-- content-wrap ends-->	
	</div>
		
	<!-- footer starts -->			
	<div id="footer-wrap"></div>
	<!-- footer ends-->	
	
<!-- wrap ends here -->
</div>

</body>
</html>
