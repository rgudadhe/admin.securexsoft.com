<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Services.aspx.vb" Inherits="Services" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href="../styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <table runat="server" id="table1" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Server Name" Font-Names="Trebuchet MS" Font-Size="12px" Font-Bold=true ForeColor=Crimson Font-Italic=true></asp:Label> : &nbsp
                    <%--<asp:DropDownList ID="ddlServerName" Font-Names="Trebuchet MS" Font-Size="12px" Width="150" runat="server" AutoPostBack=true>
                        <asp:ListItem Text="dbserver" Value="dbserver" Selected=True></asp:ListItem>
                        <asp:ListItem Text="onlinemtr" Value="onlinemtr"></asp:ListItem>
                        <asp:ListItem Text="win11617" Value="win11617"> </asp:ListItem>
                        <asp:ListItem Text="win11616" Value="win11616"></asp:ListItem>
                        <asp:ListItem Text="t-server" Value="t-server"></asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:Label ID="lblServerName" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Font-Bold=true ForeColor=Crimson Font-Italic=true Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Timer runat="server" id="UpdateTimer" interval="30000" ontick="UpdateTimer_Tick" />
        <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
            </Triggers>
            <ContentTemplate>
                <%--<asp:Label runat="server" id="DateStampLabel" />--%>
                <asp:GridView ID="DataGrid2" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" ShowFooter="true" BorderWidth=1 BorderColor=ActiveBorder GridLines=Both Width="100%">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="top"></RowStyle>
                    <AlternatingRowStyle BackColor="White"  ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="top"/>
                    <PagerStyle BackColor="#5D7B9D"  Font-Bold="True" ForeColor="black"  VerticalAlign="Top"  />
                    <HeaderStyle BackColor=DarkGoldenrod Font-Bold="True" ForeColor="White" Height="15" VerticalAlign="Top" CssClass="SMSelectedGrid"   />
                    <FooterStyle Font-Bold="True" ForeColor="black"   Font-Names="Arial" Font-Size="8" HorizontalAlign="Center" VerticalAlign="Top"   />  
                    <emptydatarowstyle backcolor="LightBlue" forecolor="Red" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    </form>
</body>
</html>
