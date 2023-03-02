<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="ERSSMainNew_Reports_Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet">
</head>
<body style=" text-align:left">
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Reports</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="AdministatorTabContainer" AutoPostBack="true" runat="server" Width="100%" ScrollBars="None">
            <Ajax:TabPanel ID="TurnAround" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblTAT" CssClass="common" runat="server" Text="TurnAround"></asp:Label>
                </HeaderTemplate> 
                <ContentTemplate>
                    <iframe width="100%" height="410" frameborder="0" src="TATReport.aspx" id="TATReport"></iframe>    
                </ContentTemplate>   
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Summary" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" CssClass="common" Text="Summary"></asp:Label>
                </HeaderTemplate> 
                <ContentTemplate>
                    <iframe width="100%" height="410" frameborder="0" src="SummaryReport.aspx" id="Iframe1"></iframe>    
                </ContentTemplate>   
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" CssClass="common" Text="Production Schedule"></asp:Label>
                </HeaderTemplate> 
                <ContentTemplate>
                    <iframe width=100% height="410" frameborder="0" src="ChkProd.aspx" id="Iframe2"></iframe>    
                </ContentTemplate>   
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
   </div> 
   </div> 
    </form>
</body>
</html>
