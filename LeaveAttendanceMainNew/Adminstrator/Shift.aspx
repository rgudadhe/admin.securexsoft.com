<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Shift.aspx.vb" Inherits="TechReports_Shift" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Shift Management</title>
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
		<div>
        <center>
            <asp:Table ID="tblMain" runat="server" GridLines=Both Width=65%>
                <asp:TableRow CssClass="HeaderDiv">
                    <asp:TableCell HorizontalAlign=Center ColumnSpan=3>
                        Shifts Details
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="SMSelected" HorizontalAlign=Center>
                    <asp:TableCell Width="30%" HorizontalAlign=Center>
                        Prefix
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=Center>
                        ShiftName
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=center> 
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>    
		</div>
    </form>
</body>
</html>
