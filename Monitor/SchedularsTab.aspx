<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchedularsTab.aspx.vb" Inherits="SchedularsTab" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <Ajax:TabContainer ID="SchedularsTabContainer" runat=server Width="100%" ActiveTabIndex="0">
            <Ajax:TabPanel ID="Server1" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSch1" runat="server" Text="dbserver"></asp:Label>
                </HeaderTemplate>   
                <ContentTemplate>
                    <iframe height="405" width="100%"  frameborder=0  src="Sch.aspx?Server=dbserver" id="Iframe1"></iframe> 
                </ContentTemplate> 
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Server2" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSch2" runat="server" Text="onlinemtr"></asp:Label>
                </HeaderTemplate>   
                <ContentTemplate>
                    <iframe height="405" width="100%"  frameborder=0  src="Sch.aspx?Server=onlinemtr" id="Iframe2"></iframe> 
                </ContentTemplate> 
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Server3" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSch3" runat="server" Text="win11617"></asp:Label>
                </HeaderTemplate>   
                <ContentTemplate>
                    <iframe height="405" width="100%"  frameborder=0  src="Sch.aspx?Server=win11617" id="Iframe3"></iframe> 
                </ContentTemplate> 
            </Ajax:TabPanel>  
            <Ajax:TabPanel ID="Server4" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSch4" runat="server" Text="win11616"></asp:Label>
                </HeaderTemplate>   
                <ContentTemplate>
                    <iframe height="405" width="100%"  frameborder=0  src="Sch.aspx?Server=win11616" id="Iframe4"></iframe> 
                </ContentTemplate> 
            </Ajax:TabPanel>   
            <Ajax:TabPanel ID="Server5" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSch5" runat="server" Text="t-server"></asp:Label>
                </HeaderTemplate>   
                <ContentTemplate>
                    <iframe height="405" width="100%"  frameborder=0  src="Sch.aspx?Server=t-server" id="Iframe5"></iframe> 
                </ContentTemplate> 
            </Ajax:TabPanel>                                
        </Ajax:TabContainer>
    </div>
    </form>
</body>
</html>
