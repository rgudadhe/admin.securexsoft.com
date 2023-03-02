<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpStatus_old.aspx.vb" Inherits="RoutingTool_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Untitled Page</title>
   <style type="text/css"  >

.noScroll 
{ 
     position:relative; 
     top:expression(this.offsetParent.scrollTop); 
} 
  
		table {
			text-align: left;
			font-size: 12px;
			font-family: verdana;
			background: #c0c0c0;
		}
 
		table thead  {
			cursor: pointer;
		}
 
		table thead tr,
		table tfoot tr {
			background: #c0c0c0;
		}
 
		table tbody tr {
			background: #f0f0f0;
		}
 
		td, th {
			border: 1px solid white;
		}
	
   a, a:visited {	
	color: #326ea1; 
		
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
a:hover {
	color: #383d44;
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
   </style> 

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
                                              <asp:Table ID="Table2"  runat="server" BorderColor="Silver" style="text-align: center" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" Width="100%" Font-Size="Small" >

                            <asp:TableRow ID="TableRow2"   runat="server" style="text-align: center" VerticalAlign="Top">
                       
                                <asp:TableCell ID="TableCell8" runat="server" >User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell9"  runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell10"  runat="server">SchMins</asp:TableCell >
                                 <asp:TableCell ID="TableCell11" runat="server">STime</asp:TableCell >
                                  <asp:TableCell ID="TableCell12"  runat="server">ETime</asp:TableCell >
                                <asp:TableCell runat="server"  ID="TableCell13" ><asp:Label ID="LDone" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell  runat="server" ID="TableCell14" ><asp:Label ID="LOut" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell ID="TableCell15"  runat="server" >Pending</asp:TableCell >
                                <asp:TableCell ID="TableCell16"   runat="server">Direct Mins</asp:TableCell >
                                
                            </asp:TableRow> 
                           </asp:Table>  
                        
                           
                                              <asp:Table ID="Table4"  runat="server" BorderColor="Silver" style="text-align: center" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" Width="100%" Font-Size="Small" >

                            <asp:TableRow ID="TableRow1"  runat="server" style="text-align: center" VerticalAlign="Top">
                       
                                <asp:TableCell ID="TableCell1" runat="server">User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell2"  runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell3"  runat="server">SchMins</asp:TableCell >
                                 <asp:TableCell ID="TableCell6" runat="server">STime</asp:TableCell >
                                  <asp:TableCell ID="TableCell7"  runat="server">ETime</asp:TableCell >
                                <asp:TableCell runat="server"  ID="CellMdone" ><asp:Label ID="LDone1" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell  runat="server" ID="CellCout" ><asp:Label ID="LOut1" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell ID="TableCell4"  runat="server" >Pending</asp:TableCell >
                                <asp:TableCell ID="TableCell5"   runat="server">Direct Mins</asp:TableCell >
                                
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
