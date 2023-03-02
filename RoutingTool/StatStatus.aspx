<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatStatus.aspx.vb" Inherits="RoutingTool_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Untitled Page</title>
</head>
<body>
   <form id="form1" runat="server">
      
            <table id="Table1" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'" width="100%">
                <tr>
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        <span style="color: white; font-family: Trebuchet MS"><strong><em>Routing Tool</em></strong></span></td>
                </tr>
               <tr>
               <td>
               <table id="Table3" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'" width="100%">
               <tr>
               <td style="width: 15%" >
                   Account Name: -
               </td>
                   <td  style="width: 35%" >
                       <asp:Label ID="LblActName" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                           Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
                   <td style="width: 15%" >
                        Total Mins</td>
                   <td style="width: 35%" >
                       <asp:Label ID="LblTotmins" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                           Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
               </tr>
                <tr>
                    <td >
                       Status</td>
                    <td>
                        <asp:Label ID="Lblstatus" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
                    <td>
                        Pending Mins</td>
                    <td>
                        <asp:Label ID="LblPendMins" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
               </tr>
               </Table> 
                   <br />
                   <asp:Button ID="lblDsp" runat="server" Text="Button" /></td></tr> 
                <tr>
                    <td style="text-align: center" valign="top">
                        <asp:Table ID="Table2" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                            <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                                <asp:TableCell runat="server">Employee Name</asp:TableCell>
                                <asp:TableCell runat="server">Employee ID</asp:TableCell>
                                <asp:TableCell runat="server">Scheduled Mins</asp:TableCell>
                                <asp:TableCell runat="server">Mins Done</asp:TableCell>
                                <asp:TableCell runat="server">Mins CheckOut</asp:TableCell>
                                <asp:TableCell runat="server">Pending</asp:TableCell>
                                <asp:TableCell runat="server">Direct Mins</asp:TableCell>
                                
                            </asp:TableRow>
                            </asp:Table><asp:Table ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                                <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                                    <asp:TableCell runat="server">Employee Name</asp:TableCell>
                                    <asp:TableCell runat="server">Employee ID</asp:TableCell>
                                    <asp:TableCell runat="server">Scheduled Mins</asp:TableCell>
                                    <asp:TableCell runat="server">Mins Done</asp:TableCell>
                                    <asp:TableCell runat="server">Mins CheckOut</asp:TableCell>
                                    <asp:TableCell ID="TableCell1" runat="server">Pending</asp:TableCell>
                                <asp:TableCell ID="TableCell2" runat="server">Direct Mins</asp:TableCell>
                                
                                </asp:TableRow>
                            </asp:Table>
                           </td>
                            
                        </tr>
                        </table> 
        <br />
        <br />
        <br />
    </form>
</body>
</html>
