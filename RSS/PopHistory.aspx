<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopHistory.aspx.vb" Inherits="RSS_PopHistory" %>
<%@ Register Tagprefix="RSS" Tagname="History"
	 Src="History.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <RSS:History ID="History1" runat="server" />
    </div>
    </form>
</body>
</html>
