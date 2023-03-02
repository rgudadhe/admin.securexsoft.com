<%@ Page Language="VB" AutoEventWireup="false" CodeFile="status.aspx.vb" Inherits="status" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>e-Dictate Updates</title>
    <script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="viewupdates.aspx?trackid="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=200,width=500,left=300, top=100,scrollbars=yes');
	if (window.focus) {newwindow.focus()}
}
</script> 
   <style type="text/css" >
   
  img {
	background: #FAFAFA;
   border: 1px solid #DCDCDC;
	padding: 0px;
}
img.float-right {
  	margin: 5px 0px 10px 10px;  
}
img.float-left {
  	margin: 5px 10px 10px 0px;
}


   a, a:visited {	
	color: white; 
	background: inherit;
	text-decoration: none;		
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
a:hover {
	color: white;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
</style> 
</head>
<body style="margin-left:0; margin-top:0;" bgcolor="whitesmoke">
    <form id="form1" runat="server">
    <div>
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td  style="width:103; background-color:HotTrack;">
       <img alt="" src="images/updates3.gif" border="0" width="139" height="21" />
      </td><td   style="width:100%;"><asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="White" BackColor="#3090C7" ></asp:Label>
      </td>
        </tr></table>
    </div>
    </form>
</body>
</html>
