<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditShift.aspx.vb" Inherits="TechReports_EditShift" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <asp:RequiredFieldValidator ID="RequiredFieldtxtShiftPrefix" runat="server" ErrorMessage="Please enter shift prefix " Font-Names="Trebuchet MS" SetFocusOnError=true Font-Bold=true Font-Italic=True Font-Size=Small ControlToValidate="txtPrefix"></asp:RequiredFieldValidator>
        <BR><asp:RequiredFieldValidator ID="RequiredFieldShiftName" runat="server" ErrorMessage="Please enter shift name " Font-Names="Trebuchet MS" SetFocusOnError=true Font-Bold=true Font-Italic=True Font-Size=Small ControlToValidate="txtShiftName"></asp:RequiredFieldValidator>
        <asp:Table ID="tblMain" runat="server" Font-Names="Trebuchet MS" Font-Size=Small ForeColor=Gray Font-Italic=true GridLines=Both>
            <asp:TableRow CssClass="SMSelected">
                <asp:TableCell ColumnSpan=2 HorizontalAlign=Center>
                    Edit Shift
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Shift Prefix
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtPrefix" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Shift Name
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtShiftName" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <BR>
        <BR>
            <asp:Button ID="BtnSubmit" Font-Names="Trebuchet MS" runat="server" Text="Update" CssClass="Buttons" />
            <asp:Button ID="BtnDelete" runat="server" Font-Names="Trebuchet MS" Text="Delete" CssClass="Buttons" />
        </center>
    </div>
    </form>
</body>
</html>
