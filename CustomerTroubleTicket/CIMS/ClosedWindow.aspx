<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ClosedWindow.aspx.vb" Inherits="CIMS_ClosedWindow" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
    function cwindow()
    {
	    opener.window.location.reload();
	    //opener.window.location="EditIssueCategory.aspx"
	    window.close();
    }
</script>
</head>
<body onload="javascript:cwindow();">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
