<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceAction.aspx.vb" Inherits="AttendanceAction" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "styles\Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Names="Trebuchet MS" runat="server" ErrorMessage="Please Enter Reason for Leave DisApprove" ControlToValidate=txtReason></asp:RequiredFieldValidator>
        </center>
        <asp:Table ID="Table1" runat="server" HorizontalAlign=Center GridLines=Both >
            <asp:TableRow ID="TableRow1" HorizontalAlign=Center runat="server"  >
                <asp:TableCell ID="TableCell1" ColumnSpan=2 CssClass="HeaderDiv" runat="server">Ateendance Not Sanctioned Reason</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Font-Names="Trebuchet MS" Font-Size=Smaller >
                    Reason
                </asp:TableCell>
                <asp:TableCell>
                    <textarea id="txtReason" rows="10" cols="50" runat=server></textarea>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign=Center ColumnSpan=2>
                    <asp:Button ID="btnDisapprove" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text="Disapprove" />
                    <br>
                    <FONT FACE="Trebuchet MS" SIZE="2">                         
                        <a href="AttendanceApprove.aspx">BACK</a>
                    </FONT>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    
    </div>
    </form>
</body>
</html>
