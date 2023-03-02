<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User.aspx.vb" Inherits="ERSSMainNew_User_User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>User</title>
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body style="background-color:WhiteSmoke">
    <form id="frmUser" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="UserTabContainer" AutoPostBack=true runat=server Width="100%" ScrollBars=None >
            <Ajax:TabPanel ID="RaiseTicket" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblRequest" runat="server" Text="Raise Ticket"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=100% height="410" frameborder=0 src="../NewTicket.aspx" id="NewTicket"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="View" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblHistory" runat="server" Text="Ticket History"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=100% height="410" frameborder=0 src="../PastTickets.aspx" id="Iframe2"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Sch" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSchedule" runat="server" Text="Send Schedule"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=100% height="410" frameborder=0 src="../HBA/proud.aspx" id="Iframe1"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
    </form>
</body>
</html>
