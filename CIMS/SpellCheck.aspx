<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SpellCheck.aspx.vb" Inherits="SpellCheck" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Spell Check</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Table ID="Table1" runat="server" Width="81%" Height="118px" GridLines=Both>
                <asp:TableRow runat="server">
                    <asp:TableCell HorizontalAlign=Left ColumnSpan=2 runat="server">
                        <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Text="Not In Dictionary :"></asp:Label>            
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell HorizontalAlign=Left RowSpan=3 runat="server">
                        <textarea id="txtInputString" style="font-family:Trebuchet MS"  rows="4" cols="70"></textarea>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" VerticalAlign=Middle>
                        <asp:Button ID="BtnIgnoreOnce" runat="server" Font-Names="Trebuchet MS" Text="Ignore Once" Width="100%" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell VerticalAlign=Middle runat="server">
                        <asp:Button ID="BtnIgnoreAll" runat="server" Font-Names="Trebuchet MS" Text="Ignore All" Width="100%" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign=Left ColumnSpan=2>
                        <asp:Label ID="Label2" runat="server" Font-Names="Trebuchet MS" Text="Suggestions :"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign=Left RowSpan=3 runat=server>
                        <textarea id="txtOutputString" style="font-family:Trebuchet MS"  rows="4" cols="70"></textarea>                        
                    </asp:TableCell>
                </asp:TableRow>
               <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell1" runat="server" VerticalAlign=Middle>
                        <asp:Button ID="BtnChange" runat="server" Font-Names="Trebuchet MS" Text="Replace" Width="100%" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell ID="TableCell2" VerticalAlign=Middle runat="server">
                        <asp:Button ID="BtnChangeAll" runat="server" Font-Names="Trebuchet MS" Text="Replace All" Width="100%" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </center>        
    </div>
    </form>
</body>
</html>
