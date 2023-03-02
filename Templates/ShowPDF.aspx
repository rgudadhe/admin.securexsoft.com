<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowPDF.aspx.vb" Inherits="Dictation_Search_ShowPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
	function Load()
	{
		document.getElementById('Pdf').Load("https://sxf1.securexsoft.com/dictation search/Test.Pdf#toolbar=0");
		document.getElementById('zoomN').value="75"
	}
	</script>
	<script>

document.oncontextmenu=new Function("return false"); 
</script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
     
             <asp:Label ID="Label1" runat="server"   CssClass="Title1" ForeColor="Maroon"></asp:Label>  
    <asp:Panel runat="server" ID="OpenPnl">
 <object data="<% =MediaURL%>"type="<% =MediaType %>" width="100%" height="800px"> 
             </object>
             </asp:Panel> 
    </div>
    </form>
</body>
</html>
