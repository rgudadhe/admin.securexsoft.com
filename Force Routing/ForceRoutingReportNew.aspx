<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRoutingReportNew.aspx.vb" Inherits="Force_Routing_ForceRoutingReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Force Routing Report</title>
    <link href= "../App_Themes/Css/RoutingTool.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript">
        function Chk()
        {
            if (document.getElementById('ddlRouting').value=='')
            {
                alert('Please select routing by')
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" method="post" target="ForceRoutingReportResultNew" runat="server">
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <div>
            <table> 
                <tr>
                    <td>
                        <asp:UpdatePanel ID="up2" runat="server">
                            <ContentTemplate>                
                                <table width="40%" runat="server" style="font-family:Trebuchet MS; font-size:8pt">
                                    <tr class="SMSelected">
                                        <td align="center">
                                            Routing By
                                        </td>
                                        <td id="UserLeveltxt" visible="false" runat="server" align="center">
                                            User Level
                                        </td>        
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlRouting" runat="server" Width="150" Font-Names="Trebuchet MS" Font-Size="10pt" AutoPostBack="true" >
                                                <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Account" Value="Account"></asp:ListItem>
                                                <asp:ListItem Text="Dictator" Value="Dictator"></asp:ListItem>
                                                <asp:ListItem Text="Template" Value="Template"></asp:ListItem>
                                                <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td id="UserLevel" visible="false" runat="server"   >
                                            <asp:DropDownList ID="ULevel" runat="server" Width="150" Font-Names="Trebuchet MS" Font-Size="10pt" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <input name="SEARCH" type="submit" value="Search" style="height: 26px; font-family:Trebuchet MS; font-size:12px; " onclick="javascript:return Chk();"   />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            
            <iframe id="ForceRoutingReportResultNew" frameborder="0" name="ForceRoutingReportResultNew" src="ForceRoutingReportResultNew.aspx" style="width: 100%; height: 368px"></iframe>
        </div>
    </form>
</body>
</html>
