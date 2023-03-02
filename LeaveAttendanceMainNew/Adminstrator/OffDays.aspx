<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OffDays.aspx.vb" Inherits="OffDays" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>National Offs</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
	    <div>
            <asp:Table ID="tblALL" runat="server" Width="75%" HorizontalAlign="center">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="3" CssClass="HeaderDiv"> 
                        OffDays Results
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="30%" HorizontalAlign="Center" CssClass="alt1">
                        Date
                    </asp:TableCell>
                    <asp:TableCell CssClass="alt1">
                        Description
                    </asp:TableCell>
                    <asp:TableCell CssClass="alt1">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
