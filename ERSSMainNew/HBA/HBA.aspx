<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HBA.aspx.vb" Inherits="ERSSMainNew_HBA_HBA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HBA</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

</head>
<body style=" text-align:left" >
    <form id="frmHBA" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>HBA</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="HBATabContainer" AutoPostBack="true" runat=server Width="100%" ScrollBars="None" >
            <Ajax:TabPanel ID="RaiseTicket" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblRequest" runat="server" CssClass="common" Text="Raise Ticket"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" height="410" frameborder=0 src="../NewTicket.aspx" id="NewTicket"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="View" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblHistory" runat="server" CssClass="common" Text="Ticket History"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" height="410" frameborder="0" src="../PastTickets.aspx" id="Iframe2"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Sch" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblSchedule" runat="server" CssClass="common" Text="Send Schedule"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="100%" height="410" frameborder="0" src="proud.aspx" id="Iframe1"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
