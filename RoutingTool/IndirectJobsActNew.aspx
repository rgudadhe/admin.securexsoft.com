<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IndirectJobsActNew.aspx.vb" Inherits="RoutingTool_IndirectJobsActNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../App_Themes/Css/RoutingTool.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
           <tr>
           <td  width="50%" valign="top">
                        <table id="Table5" runat="server"   border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                            
                        
                            font-family: 'Trebuchet MS'" >
                            <tr>
                                <td colspan="2" style="width: 100%; text-align: center; height: 15px; background-image:url('../App_Themes/Images/background_parentselected.gif')" valign="top" >
                                    <span style="color: white; font-family: Trebuchet MS"><strong><em>Indirect Jobs AccountWise</em></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Repeater ID="rptDetailsForAcc" runat="server">
                                        <HeaderTemplate>
                                            <table cellpadding="2" cellspacing="2" border="1">
                                                <tr class="SMSelected" style="text-align: center">
                                                    <td>Account Name</td>            
                                                    <td>Total Jobs</td>            
                                                    <td>Total Mins</td>  
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align:left"><%#Container.DataItem("AccountName")%></td>             
                                                <td><%#IIf(CInt(Container.DataItem("reccount")) > 0, "<a href=indirectjobsANew.aspx?accountid=" & Container.DataItem("accountid").ToString & " Target=_Blank>" & CInt(Container.DataItem("reccount")) & "</a>", Container.DataItem("reccount"))%></td> 
                                                <td><%#Format(Container.DataItem("Mins") / 60, "00")%></td>  
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong></span> 
                                        <asp:Label ID="lblTotJobs" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            <span style="color: #ff0033"><strong>Total Mins :</strong></span>  
                                            <asp:Label ID="LblTMins" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></td>
                            </tr>
                            
                        </table>
        
       
           </td>
           <td  width="50%" valign="top">
                        <table id="Table1" runat="server"   border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                        
                        
                            font-family: 'Trebuchet MS'" >
                            <tr>
                                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center; height: 15px;background-image:url('../App_Themes/Images/background_parentselected.gif')" valign="top">
                                    <span style="color: white; font-family: Trebuchet MS"><strong><em>Indirect Jobs MTWise</em></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Repeater ID="rptDetailsForMT" runat="server">
                                        <HeaderTemplate>
                                            <table cellpadding="2" cellspacing="2" border="1">
                                                <tr class="SMSelected" style="text-align: center">
                                                    <td>User Name</td>            
                                                    <td>User ID</td>
                                                    <td>Total Jobs</td>            
                                                    <td>Total Mins</td>  
                                                    <td>Indirect Jobs</td>
                                                    <td>Indirect Mins</td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align:left"><%#Container.DataItem("MTName")%></td>             
                                                <td style="text-align:left"><%#Container.DataItem("userName")%></td>
                                                <td><%#Container.DataItem("reccount")%></td>
                                                <td><%#Format(Container.DataItem("Mins") / 60, "00")%></td>  
                                                <td><%#IIf(CInt(Container.DataItem("indJobs")) > 0, "<a href=indirectjobsNew.aspx?Userid=" & Container.DataItem("Userid").ToString & " Target=_Blank>" & CInt(Container.DataItem("indJobs")) & "</a>", Container.DataItem("indJobs"))%></td> 
                                                <td><%#Format(Container.DataItem("indMins") / 60, "00")%></td>  
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                                        <asp:Label ID="lblTotJobs1" runat="server" Font-Bold="True"></asp:Label>
                                             <span style="color: #ff0033"><strong>Total Mins:</strong>
                                            <asp:Label ID="LblTMins1" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Indirect Jobs :</strong>
                                        <asp:Label ID="lblTotiJobs1" runat="server" Font-Bold="True"></asp:Label>
                                            <span style="color: #ff0033"><strong>Total Indirect Mins :</strong> 
                                            <asp:Label ID="LblTiMins1" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            
                        </table>
        
       
           </td>
           </tr></table>
    </div>
    </form>
</body>
</html>
