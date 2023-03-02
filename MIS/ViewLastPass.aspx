<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLastPass.aspx.vb" Inherits="RoutingTool_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


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
	background: inherit;
	text-decoration: none;		
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
a:hover {
	color: #383d44;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
   </style> 
     
</head>
<body>
   <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       <span style="font-size: 10pt; color: #ff9900; font-family: Trebuchet MS"><strong><em>
       </em></strong></span>
  
      
          
                      <asp:Table ID="Table2"  runat="server" style="text-align: center" Font-Italic="True" Font-Names="Trebuchet MS"  Font-Size="Small" >
                               <asp:TableRow ID="TableRow2" CssClass="SMSelected"    runat="server" style="text-align: center" VerticalAlign="Top">
                                <asp:TableCell ID="TableCell4" ColumnSpan="3"    runat="server">View password last Changed</asp:TableCell >
                                </asp:TableRow> 
                                <asp:TableRow ID="TableRow1" CssClass="SMSelected"    runat="server" style="text-align: center" VerticalAlign="Top">
                                <asp:TableCell ID="TableCell1"   runat="server">User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell2" runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell3"  runat="server">Password last Changed</asp:TableCell >
                            </asp:TableRow> 
                           </asp:Table>
                          
           
           
        <br />
        <br />
       <asp:HiddenField ID="HSelLvl" runat="server" />
      <asp:HiddenField ID="CurrDate" runat="server" /> 
        <br />
    </form>
</body>
</html>

