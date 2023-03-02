    <%@ Page Language="vb" Inherits="ets.Main_Page" Src="Main.aspx.vb" %>
<%@ Register TagPrefix="obspl" Namespace="OboutInc.Splitter2" Assembly="obout_Splitter2_Net" %>
<%@ Register TagPrefix="spl" Namespace="OboutInc.Splitter2" Assembly="obout_Splitter2_Net" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>E-Dictate - The Best Value Transcription Solution</title>
<style type="text/css">

{
	font-family:Verdana;
	font-size:8pt;
}			
.text
{
	background-color:#ebe9ed;
	font-size:11px;
	text-align:center;
}
.textContent
{
	font-size:11px;
	text-align:center;
}
</style>
</head>


<frameset cols="19,81" id="Main">		
<frame name="left" src="MainLeft.aspx.exclude">		
<frame name="right" src="Main_right.aspx?cId=Profile|Comman/Profile.aspx" height="100%">	
</frameset>

<%--<body>
<form>

		
<spl:Splitter id="mySpl" runat="server" StyleFolder="styles/default_light" CookieDays="0">
	<LeftPanel WidthDefault="170" WidthMin="175" WidthMax="175">
		<content Url="Mainleft.aspx" />
	</LeftPanel>
	<RightPanel>
		<content Url="Main_right.aspx?cId=Profile|Comman/Profile.aspx" />					
	</RightPanel>	
</spl:Splitter>
</form>
</body>--%>
</html>
