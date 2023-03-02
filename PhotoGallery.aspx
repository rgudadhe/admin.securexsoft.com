<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PhotoGallery.aspx.vb" Inherits="ets_profile" %>

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
				<li><a href="News.aspx">From the Desk of COO</a></li>
				<li><a href="eConnect.aspx">e-Connect</a></li>
				<li><a href="EdNews.aspx">News</a></li>
				<li><a href="updates.aspx">Updates</a></li>	
				<li><a href="Photogallery.aspx">Photo Gallery</a></li>	
			</ul>	
				
		<!-- sidebar ends -->		
		</div>

		
		
		
		<div id="main">
		<form runat="server">
		
					  
			  <h1>
                  <span style="color: #ff9933"> <img src="images/gallery_sml.png"  /> Photo Gallery</span></h1> 
			  	<asp:label id="CurrentLocation" runat="server" />
  <br /><br />
            <asp:Panel ID="Panel1" runat="server" >
  <asp:label id="DirectoryList" runat="server"/>
  <asp:label id="PictureList" runat="server" />			          
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center"  >
            <table border='2' cellpadding='2' style='margin-top:0; margin-left:10;width:570;height:480;'><tr><td colspan="2" style="background-color: transparent; background-image: url(images/fborder.jpg);" >                
            <asp:Image ID="iphoto" runat="server" Width="445px" Height="322px" style='margin-top:52px; margin-left:32px; margin-bottom:52px; margin-right:45px;' />          
               </td> </tr> 
               <tr>
               <td style="text-align:left;"  >
                   <asp:label id="LPrevious" runat="server"/></td>
               <td style="text-align:right;"><asp:label id="LNext" runat="server"/></td></tr></table>  
            </asp:Panel>
              <em><span style="color: #ff9933">
            <asp:Label ID="lblCount" runat="server" Font-Bold="True"></asp:Label></span></em>
            <em><span style="color: #ff9933; text-align:right;">
            <br />
            <br />
            <asp:Label ID="lblShow" runat="server" Font-Bold="True"></asp:Label></span></em>
          
            <asp:HiddenField ID="HRoot" runat="server" />
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
