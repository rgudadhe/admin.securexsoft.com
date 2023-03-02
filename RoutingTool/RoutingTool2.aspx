<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RoutingTool2.aspx.vb" Inherits="RoutingTool_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Untitled Page</title>
    <style type="text/css"  >
.table_scroll
{
    overflow: auto;
    height: 500px;
    border: solid 1px orange;
    height: 480px;
    width: 800px;
}

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
      
            <table id="Table1" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'" width="100%">
                <tr>
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        <span style="color: white; font-family: Trebuchet MS"><strong><em>Routing Tool</em></strong></span></td>
                </tr>
                <tr>
                    <td style="text-align: center" valign="top">
                           <div style=" height:450px; overflow:auto">
                        <asp:Table ID="Table2" runat="server" BorderColor="Silver" style="text-align: center" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" GridLines="both"    Width="100%" Font-Size="Small" >
                            </asp:Table>
                           </div>  
                           </td>
                            
                        </tr>
                        </table> 
        <br />
        <br />
        <br />
    </form>
</body>
</html>
