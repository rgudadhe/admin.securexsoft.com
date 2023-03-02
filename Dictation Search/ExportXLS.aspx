<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExportXLS.aspx.vb" Inherits="ets.Dictation_Search_ExportResult" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:datagrid id="dgResultsData"
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="true" 
				runat="server">
			</asp:datagrid>
    </form>
</body>
</html>
