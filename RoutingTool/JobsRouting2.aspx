<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JobsRouting2.aspx.vb" Inherits="RoutingTool_JobsRouting" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
  <table id="Table1" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'; background-color: ivory; " width="100%"  class="noScroll">
                <tr class="noScroll">
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        <span style="color: white; font-family: Trebuchet MS"><strong><em>Routing Tool</em></strong></span></td>
                </tr>
               
                <tr >
                    <td style="text-align: left; height: 69px;" valign="top">
                        <asp:Table ID="Table2" runat="server" style="text-align: center"  BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                            <asp:TableRow ID="TableRow1" runat="server" cssclass="SMSelected" style="text-align: center">
                                <asp:TableCell ID="TableCell1" runat="server">Employee Name</asp:TableCell>
                                <asp:TableCell ID="TableCell2" runat="server">Employee ID</asp:TableCell>
                                <asp:TableCell ID="TableCell3" runat="server">Scheduled Mins</asp:TableCell>
                                <asp:TableCell ID="TableCell4" runat="server">Mins Done</asp:TableCell>
                                <asp:TableCell ID="TableCell5" runat="server">Mins CheckOut</asp:TableCell>
                                 <asp:TableCell ID="TableCell6" runat="server">Pending</asp:TableCell>
                                  <asp:TableCell ID="TableCell7" runat="server">Direct Mins</asp:TableCell>
                            </asp:TableRow>
                            </asp:Table>
                        <br />
                        <strong>Selected Mins : &nbsp;</strong><asp:Label ID="lblmins" runat="server" Font-Bold="True"
                            Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#C00000"></asp:Label>
                        &nbsp; &nbsp; &nbsp; <strong>Selected Jobs : &nbsp;</strong><asp:Label ID="lbljobs"
                            runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#C00000"></asp:Label></td>
                            
                        </tr>
                        </table> &nbsp;&nbsp;<br />
       <br />
                        <table id="Table5" runat="server"   border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                        
                        
                            font-style: italic; font-family: 'Trebuchet MS'" width="100%">
                            <tr>
                                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center; height: 15px;" valign="top">
                                    <span style="color: white; font-family: Trebuchet MS"><strong><em>Dictation Details</em></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" BorderWidth="2px" CellPadding="2"
                                        CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" Font-Size="Small"
                                        ForeColor="DimGray" GridLines="Both" Width="100%">
                                        <asp:TableRow ID="TableRow2" runat="server" cssclass="SMSelected" style="text-align: center">
                                            <asp:TableCell ID="TableCell8" runat="server" >
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                                            <asp:TableCell ID="TableCell9" runat="server">Job#</asp:TableCell>
                                            <asp:TableCell ID="TableCell10" runat="server">Status</asp:TableCell>
                                            <asp:TableCell ID="TableCell11" runat="server">TAT</asp:TableCell>
                                            <asp:TableCell ID="TableCell12" runat="server">Priority</asp:TableCell>
                                            <asp:TableCell ID="TableCell13" runat="server">Duration</asp:TableCell>
                                            <asp:TableCell ID="TableCell14" runat="server">Submit Date</asp:TableCell>
                                            
                                            <asp:TableCell ID="TableCell15" runat="server">Account Name</asp:TableCell>
                                            <asp:TableCell ID="TableCell16" runat="server">Physician Name</asp:TableCell>
                                            
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                                        <asp:Label ID="lblTotJobs" runat="server" Font-Bold="True"></asp:Label>
                                        &nbsp; <strong>Total Mins:
                                            <asp:Label ID="LblTMins" runat="server" Font-Bold="True"></asp:Label>
                                            &nbsp; Direct Mins:
                                            <asp:Label ID="LblDMins" runat="server" Font-Bold="True"></asp:Label></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    
                                        <asp:DropDownList ID="DLStatus" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Names="Trebuchet MS" Font-Size="Small">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DLChoice" runat="server" AutoPostBack="True"  Font-Bold="True"
                                            Font-Names="Trebuchet MS" Font-Size="Small">
                                        </asp:DropDownList></td> 
                            </tr>
                        </table>
        
       
       <asp:HiddenField ID="HAccID" runat="server" />
       
       <asp:HiddenField ID="TotJobs" runat="server" />
       <asp:HiddenField ID="ProLevel" runat="server" />
       
       <asp:HiddenField ID="HUserID" runat="server" /><asp:HiddenField ID="ActDisp" runat="server" />
      <asp:dropdownlist ID="Dropdownlist1" runat="server" AutoPostBack="true" >
      <asp:ListItem Text="Select" Value=""></asp:ListItem>
      <asp:ListItem Text="Select1" Value="1"></asp:ListItem>
      <asp:ListItem Text="Select2" Value="2"></asp:ListItem> </asp:dropdownlist> 
    </div>
    </form>
</body>
</html>
