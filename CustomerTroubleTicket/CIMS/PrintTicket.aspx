<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintTicket.aspx.vb" Inherits="PrintTicket" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Ticket</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblMain" runat="server" Font-Names="Arial" Font-Size="8pt">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="tlbSubject" runat="server" Font-Names="Arial" Font-Size="8pt" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><Br><BR></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Table ID="tblTicketCreated" runat="server" Font-Names="8pt">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign=Right Width="120">
                                <asp:Label ID="lblTicketNo" runat="server" Font-Bold=true Text="Ticket Number : "></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="TicketNo" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="120" VerticalAlign="Top" >
                                <asp:Label ID="lblDateCreated" runat="server" Font-Bold=true Text="Date : "></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="DateCreated" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
