<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SamplesReport.aspx.vb" Inherits="Samples_SamplesReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Samples Report</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Samples Report</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
                <table width="100%">
                    <tr>
                        <td style="width:50%; border:0" align="center" valign="top" >
                            <table width="100%">
                                <tr>
                                    <td align="left" style="border:0">
                                        <asp:LinkButton ID="LnkExport" runat="server" CssClass="common" >Export Result</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%; border:0">
                                        <asp:Repeater ID="rptDetails" runat="server">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="2" class="HeaderDiv"  align="center">
                                                            Samples Available
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alt1" align="center">Physician Name </td>            
                                                        <td class="alt1" align="center">No. of Samples </td>            
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="common" >
                                                    <td style="width:80%" align="left"><%#Container.DataItem("PhysicianName")%></td>             
                                                    <td style="width:20%" align="center"><%#Container.DataItem("Counter")%></td>  
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr bgcolor="#cccccc" class="common" >
                                                    <td style="width:80%" align="left"><%#Container.DataItem("PhysicianName")%></td>             
                                                    <td style="width:20%" align="center"><%#Container.DataItem("Counter")%></td>  
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>        
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:50%; border:0" align="center" valign="top">    
                            <table width="100%">
                                <tr>
                                    <td align="left" style="border:0">
                                        <asp:LinkButton ID="LnkExport1" runat="server" CssClass="common" >Export Result</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%; border:0">
                                        <asp:Repeater ID="rptDetails1" runat="server">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="2" class="HeaderDiv"  align="center">
                                                            Samples <b>NOT</b> Available
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="alt1" align="center">Physician Name </td>            
                                                        <td class="alt1" align="center">No. of Samples </td>            
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="common">
                                                    <td style="width:80%" align="left"><%#Container.DataItem("PhysicianName")%></td>             
                                                    <td style="width:20%" align="center"><%#Container.DataItem("Counter")%></td>  
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr bgcolor="#cccccc" class="common">
                                                    <td style="width:80%" align="left"><%#Container.DataItem("PhysicianName")%></td>             
                                                    <td style="width:20%" align="center"><%#Container.DataItem("Counter")%></td>  
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>    
            </div>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
