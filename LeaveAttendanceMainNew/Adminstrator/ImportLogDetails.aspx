<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportLogDetails.aspx.vb" Inherits="ImportLogDetails" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Log Details</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
		<a class="common" href="JavaScript:history.go(-1)">Return to list</a>
		<div>
            <asp:Table id="tblDataImported" runat="server" width="100%">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv" ColumnSpan="10">
                        Log Details
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="HeaderDiv">&nbsp</asp:TableCell>
                    <asp:TableCell ColumnSpan="5" CssClass="HeaderDiv">
                        Before Data Imported
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="4" CssClass="HeaderDiv">
                        After Data Imported
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="alt1" width="10%">UserName</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="40%">Employee Name</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="10%">CL</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="10%">EL</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="20%">WeeklyOff1</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="20%">WeeklyOff2</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="10%">CL</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="10%">EL</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="20%">WeeklyOff1</asp:TableCell>
                    <asp:TableCell CssClass="alt1" width="20%">WeeklyOff2</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
		</div>
    </form>
</body>
</html>
