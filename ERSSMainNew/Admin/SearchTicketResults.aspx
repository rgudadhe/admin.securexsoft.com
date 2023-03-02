<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SearchTicketResults.aspx.vb" Inherits="SearchTicketResults" EnableViewStateMac="false"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search Tickets</title>
    <LINK href= "../../Styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Table ID="tblCancel" runat="server" BorderColor=LightBlue BorderWidth=1 Width=100% >
            <asp:TableRow CssClass="HeaderDiv">
                <asp:TableCell HorizontalAlign=Center>
                    Search Tickets
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                        <asp:GridView ID="GridViewSearchResults" Width="100%" Font-Names="Trebuchet MS" AllowPaging=True AllowSorting=True 
                            BackColor=cornsilk  DataKeyNames="TicketID" OnSorting="GridViewSearchResults_Sorting" AutoGenerateColumns=False style="Z-INDEX: 101; LEFT: 13px; POSITION: absolute; TOP: 39px" ShowFooter=True Font-Size=Small runat="server">
                            <RowStyle BackColor=Cornsilk   />
                            <AlternatingRowStyle BackColor=Moccasin  />
                            <HeaderStyle CssClass="SMSelected" />
                            <Columns>
                                <asp:TemplateField HeaderText="Ticket No" HeaderStyle-ForeColor=white HeaderStyle-Font-Size=Small HeaderStyle-HorizontalAlign=Center SortExpression="TicketNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketID" runat="server" Text=<%#Eval("TicketNo") %>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IssueName" HeaderStyle-ForeColor=white HeaderStyle-Font-Size=Small HeaderStyle-HorizontalAlign=Center SortExpression="IssueName">
                                    <ItemTemplate>
                                        <asp:Label ID="iblIssueName" runat="server" Text=<%#Eval("IssueName")%>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority" HeaderStyle-ForeColor=white HeaderStyle-Font-Size=Small HeaderStyle-HorizontalAlign=Center SortExpression="Priority">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriority" runat="server" Text=<%#Eval("Priority") %>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Posted" HeaderStyle-ForeColor=white HeaderStyle-Font-Size=Small HeaderStyle-HorizontalAlign=Center SortExpression="DatePosted">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataPosted" runat="server" Text=<%#Eval("DatePosted") %>></asp:Label>                                                
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Raise By" HeaderStyle-ForeColor=white HeaderStyle-Font-Size=Small HeaderStyle-HorizontalAlign=Center SortExpression="FirstName">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text=<%#Eval("FirstName") &" "& Eval("LastName") %>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ActionTicket" CommandName="ActionTicket" CommandArgument='<%#Eval("TicketID")%>' OnClientClick=<%# "javascript:Test('" + Eval("TicketID").ToString() + "')" %>  runat="server">Action</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
    </div>
    </form>
</body>
</html>
