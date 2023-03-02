<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Administrator.aspx.vb" Inherits="ERSSMainNew_Administrator_Administrator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administator</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body style=" text-align:left">
    <form id="frmAdministator" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Define Issues</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <Ajax:TabContainer ID="AdministatorTabContainer" AutoPostBack="true" runat="server" Width="100%" ScrollBars="None">
                    <Ajax:TabPanel ID="IssueCategories" runat="server">
                        <HeaderTemplate>
                            <asp:Label ID="lblIssueC" CssClass="common" runat="server" Text="Issue Categories"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe width="100%" height="410" frameborder="0" src="IssuesCate.aspx" id="NewTicket"></iframe>
                        </ContentTemplate>
                    </Ajax:TabPanel>
                    <Ajax:TabPanel ID="IssueTypes" runat="server">
                        <HeaderTemplate>
                            <asp:Label ID="lblIssueTypes" runat="server" CssClass="common" Text="Issue Types"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe width="100%" height="410" frameborder="0" src="IssueType.aspx" id="Iframe1"></iframe>
                        </ContentTemplate>
                    </Ajax:TabPanel>
                    <Ajax:TabPanel ID="Assignments" runat="server">
                        <HeaderTemplate>
                            <asp:Label ID="lblAssign" runat="server" CssClass="common" Text="Issue Assignments"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe width="100%" height="410" frameborder="0" src="IssueAssignmentNew.aspx" id="Iframe2"></iframe>
                        </ContentTemplate>
                    </Ajax:TabPanel>   
                </Ajax:TabContainer>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
