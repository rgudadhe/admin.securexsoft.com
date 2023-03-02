<%@ Page Language="VB" AutoEventWireup="false" CodeFile="updates.aspx.vb" Inherits="updates" %>

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
			
			<h1>Contents</h1>
			<ul class="sidemenu">				
				<%--<li><a href="News.aspx">From the Desk of COO</a></li>--%>
				<li><a href="eConnect.aspx">NewsLetter</a></li>
				<li><a href="EdNews.aspx">News</a></li>
				<li><a href="updates.aspx">Updates</a></li>	
				<%--<li><a href="Photogallery.aspx">Photo Gallery</a></li>--%>
			</ul>	
				
		<!-- sidebar ends -->		
		</div>

		
		
		
		<div id="main">
		            <h1><span style='color: #ff9933'>
                        <img src="images/events.JPG" width="78" height="64"/>
                        Updates</span></h1>
			      
                  <asp:Label ID="Label1" runat="server"></asp:Label>
                                 
             <span style="font-size: 10pt;color: #ff9933;">Previous articles</span>
            &nbsp;&nbsp;
            <form id="Form1" runat="server" >
            <asp:DropDownList AutoPostBack="true"  ID="DLDate" runat="server" Font-Names="Trebuchet MS" Font-Size="Small">
            <asp:ListItem Text="Select Month" Value=""></asp:ListItem> 
            </asp:DropDownList>
            </form> 					
				
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
