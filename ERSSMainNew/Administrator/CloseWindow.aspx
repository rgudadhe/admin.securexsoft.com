<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CloseWindow.aspx.vb" Inherits="CloseWindow" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">
    function cwindow()
    {
	    opener.window.location.reload();
	    //opener.window.location="EditIssueCategory.aspx"
	    window.close();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body onload="javascript:cwindow();">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
