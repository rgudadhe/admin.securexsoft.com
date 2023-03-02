<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sch.aspx.vb" Inherits="Sch" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Schedulars Moinitoring Tool</title>
    <LINK href="../styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Timer runat="server" id="UpdateTimer" interval="30000" ontick="UpdateTimer_Tick" />
        <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
            </Triggers>
            <ContentTemplate>
                <table width="100%" style="font-family:Trebuchet MS; font-style:italic; font-weight:bold ; font-size:8pt; color:Crimson;">
                    <tr>
                        <td>
                            Server Name : 
                            <asp:Label ID="lblServer" runat="server" Text=""></asp:Label>  
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            Date Time : 
                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>--%>
                </table>
                <asp:Table GridLines=Both ID="tblSch" runat="server" Width="100%" Font-Names="Trebuchet MS" Font-Size="8pt">
                    <asp:TableRow CssClass="SMSelectedGrid">
                        <asp:TableCell>
                            SchedularName
                        </asp:TableCell>
                        <asp:TableCell Width="30%">
                            Schedule
                        </asp:TableCell>
                        <asp:TableCell>
                            LastRun
                        </asp:TableCell>
                        <asp:TableCell>
                            NextRun
                        </asp:TableCell>
                        <asp:TableCell>
                            Difference
                        </asp:TableCell>
                        <asp:TableCell>
                            State
                        </asp:TableCell>
                        <asp:TableCell>
                            Exit-Code
                        </asp:TableCell>
                        <asp:TableCell>
                            Comment
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
