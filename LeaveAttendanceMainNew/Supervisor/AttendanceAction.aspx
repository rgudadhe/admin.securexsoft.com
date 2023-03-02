<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceAction.aspx.vb" Inherits="AttendanceAction" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attendace Approval</title>
    <LINK href= "../../styles/Report.css" type="text/css" rel="stylesheet">
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Names="Trebuchet MS" Font-Italic=true Font-Bold=true SetFocusOnError=true runat="server" ErrorMessage="Please Enter Reason for Attendance DisApprove" ControlToValidate=txtReason></asp:RequiredFieldValidator>
        </center>
        <asp:Table ID="Table1" runat="server" HorizontalAlign=Center GridLines=Both >
            <asp:TableRow ID="TableRow1" HorizontalAlign=Center CssClass="HeaderDiv" runat="server"  >
                <asp:TableCell ID="TableCell1" ColumnSpan=2 runat="server" HorizontalAlign=Center>Ateendance Not Sanctioned Reason</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Font-Names="Trebuchet MS" Font-Size=Smaller >
                    Reason
                </asp:TableCell>
                <asp:TableCell>
                    <textarea id="txtReason" rows="10" cols="70" runat=server style="font-family:Trebuchet MS"></textarea>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign=Center ColumnSpan=2>
                    <asp:Button ID="btnDisapprove" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text="Disapprove" CssClass="Buttons" />
                    <br>
                    <FONT FACE="Trebuchet MS" SIZE="2">                         
                        <a href="AttendanceApproval.aspx">BACK</a>
                    </FONT>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    
    </div>
    </form>
</body>
</html>
