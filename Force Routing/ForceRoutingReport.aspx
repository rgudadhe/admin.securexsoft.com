<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRoutingReport.aspx.vb" Inherits="Force_Routing_ForceRoutingReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Force Routing Report</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
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
    <form id="form1" method="post" target="ForceRoutingReportResult" runat="server">
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <div>
            <table> 
                <tr>
                    <td>
                        <asp:UpdatePanel ID="up2" runat="server">
                            <ContentTemplate>                
                                <table style="background-color: whitesmoke" width="40%" runat="server">
                                    <tr>
                                        <td style="height: 25px;">
                                            <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" CssClass="SearchCol" Height="20" ReadOnly="true" TabIndex="100" Text="Routing By"></asp:TextBox>                        
                                        </td>
                                        <td style="height: 25px;" id="UserLeveltxt" visible="false" runat="server">
                                            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" CssClass="SearchCol" Height="20" ReadOnly="true" TabIndex="100" Text="User Level" Visible="false"  ></asp:TextBox>                        
                                        </td>        
                                    </tr>
                                    <tr>
                                        <td style="height: 26px">
                                            <asp:DropDownList ID="ddlRouting" runat="server" Width="150" Font-Names="Trebuchet MS" AutoPostBack=true >
                                                <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Account" Value="Account"></asp:ListItem>
                                                <asp:ListItem Text="Dictator" Value="Dictator"></asp:ListItem>
                                                <asp:ListItem Text="Template" Value="Template"></asp:ListItem>
                                                <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 26px;" id="UserLevel" visible="false" runat="server"   >
                                            <asp:DropDownList ID="ULevel" runat="server" Width="150" Font-Names="Trebuchet MS" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td style="height: 25px;">
                                    <%--<asp:Label ID="Label1" runat="server" CssClass="SearchCol"></asp:Label>    --%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input name="SEARCH" type="submit" value="Search" style="height: 26px; font-family:Trebuchet MS; font-size:12px; " onclick="javascript:return Chk();"   />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            
            <iframe id="ForceRoutingReportResult" frameborder="0" name="ForceRoutingReportResult" src="ForceRoutingReportResult.aspx" style="width: 100%; height: 368px"></iframe>
        </div>
    </form>
</body>
</html>
