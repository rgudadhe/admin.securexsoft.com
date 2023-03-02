<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ServiceMonitor.aspx.vb" Inherits="ServiceMonitor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Monitoring Tool</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Timer runat="server" id="UpdateTimer" interval="30000" ontick="UpdateTimer_Tick" />
        <asp:UpdatePanel ID="TimedPanel" runat="server" UpdateMode=Conditional>
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="lblDateTime" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Font-Bold=true ForeColor=Crimson Font-Italic=true  Text=""></asp:Label> <BR>
            </ContentTemplate>
        </asp:UpdatePanel>
        <Ajax:TabContainer ID="EmpTabContainer" runat=server Width="100%" ActiveTabIndex="0" >
            <Ajax:TabPanel ID="DailyAttendance" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblSch" runat="server" Text="Schedulars"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                   <iframe height="475" width="100%"  frameborder=0  src="SchedularsTab.aspx" id="Iframe1"></iframe> 
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="SendRequest1" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2 Height="700">
                <HeaderTemplate>
                    <asp:Label ID="lblService" runat="server" Text="Services"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe height="475" width="100%" frameborder=0  src="ServicesTab.htm" id="Iframe2"></iframe> 
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
    </form>
</body>
</html>
