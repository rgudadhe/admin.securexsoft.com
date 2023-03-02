<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeavePolicy.aspx.vb" Inherits="LeavePolicy" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Policy</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
		<div>
            <asp:Table ID="tblLeavePolicy" runat="server" Width="75%" HorizontalAlign="Center">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv" ColumnSpan="5">
                    Leave Policy
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="50" HorizontalAlign="Center" CssClass="alt1">
                    Day
                    </asp:TableCell>
                    <asp:TableCell Width="100" HorizontalAlign="center" CssClass="alt1">
                    Month
                    </asp:TableCell>
                    <asp:TableCell Width="100" HorizontalAlign="Center" CssClass="alt1">
                    Casual Leaves
                    </asp:TableCell>
                    <asp:TableCell Width="100" HorizontalAlign="Center" CssClass="alt1">
                    Earned Leaves
                    </asp:TableCell>
                    <asp:TableCell Width="70" CssClass="alt1">
                        &nbsp
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
		</div>
	</form>
</body>
</html>
