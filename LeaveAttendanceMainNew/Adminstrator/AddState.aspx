<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddState.aspx.vb" Inherits="AddState" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>Add State</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <asp:Table ID="Table1" runat="server" Width="297px" >
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="HeaderDiv" HorizontalAlign="Center">
                    Select State
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Left">
                    <asp:Label ID="Label1" runat="server" CssClass="common" Text="Select State"></asp:Label> &nbsp 
                    <asp:DropDownList ID="DropDownState" runat="server" Width="180px" CssClass="common">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell HorizontalAlign="Center" runat="server">
                    <center>
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="button" />
                    </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </center>
    </div>
    </form>
</body>
</html>
