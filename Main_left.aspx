
<%@ Page Inherits="ets.Main_left" Src="Main_left.aspx.vb" %>
<%@ Register TagPrefix="osm" Namespace="OboutInc.SlideMenu" Assembly="obout_SlideMenu3_Pro_Net" %>
<%@ Register TagPrefix="oajax" Namespace="OboutInc" Assembly="obout_AJAXPage" %>



<html>
	<head>
		<script language="javascript">
			function LoadMainPage(cId)
			{
				// select the click-ed node from the slidemenu using a callback to the server and a callbackpanel for update
				ob_post.post(null, 'UpdateSlideMenu', function(){}, {"cId":cId});				
				// load the right content page with the details
				window.parent.mySpl.loadPage('RightContent', 'Main_right.aspx?cId=' + cId)
			}
		</script>
	</head>
	<body>
		
		<oajax:CallbackPanel id="cp_slidemenu" runat="server">
			<content>			
				<osm:SlideMenu
						id = "pro7"
						runat = "server"
						StyleFolder = "styles/pro_7"
						Height = 183
						SelectedId = "Profile"
						Speed = 25>
					<MenuItems>		    
														
					</menuitems>
				</osm:SlideMenu>
				
			</content>
		</oajax:CallbackPanel>        
	</body>
</html>