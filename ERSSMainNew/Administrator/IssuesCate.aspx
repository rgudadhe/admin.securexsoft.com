<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IssuesCate.aspx.vb" Inherits="ERSSMain_IssuesCate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Issue Categories</title>
    <link href="../../App_Themes/Css/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/Common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	    <asp:Table ID="table1" runat="server" HorizontalAlign="Center" Width="70%">
	        <asp:TableRow>
	            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center" CssClass="HeaderDiv">
	                Issue Categories
	            </asp:TableCell>
	        </asp:TableRow>
	        <asp:TableRow>
	            <asp:TableCell CssClass="alt1">
	                Category Name
	            </asp:TableCell>
	            <asp:TableCell CssClass="alt1">
	                Category Description
	            </asp:TableCell>
	            <asp:TableCell CssClass="alt1">
	                &nbsp
	            </asp:TableCell>
	        </asp:TableRow>
	    </asp:Table> 
    </form>
</body>
</html>
