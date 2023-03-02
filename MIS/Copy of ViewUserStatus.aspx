<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy of ViewUserStatus.aspx.vb" Inherits="RoutingTool_Default" %>

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
       <script language="javascript" type="text/javascript"  >
      
      var t = new SortableTable(document.getElementById('Table2'), 100);

    function SortableTable (tableEl) {
 
	this.tbody = tableEl.getElementsByTagName('tbody');
	this.thead = tableEl.getElementsByTagName('thead');
	this.tfoot = tableEl.getElementsByTagName('tfoot');
 
	this.getInnerText = function (el) {
		if (typeof(el.textContent) != 'undefined') return el.textContent;
		if (typeof(el.innerText) != 'undefined') return el.innerText;
		if (typeof(el.innerHTML) == 'string') return el.innerHTML.replace(/<[^<>]+>/g,'');
	}
 
	this.getParent = function (el, pTagName) {
		if (el == null) return null;
		else if (el.nodeType == 1 && el.tagName.toLowerCase() == pTagName.toLowerCase())
			return el;
		else
			return this.getParent(el.parentNode, pTagName);
	}
 
	this.sort = function (cell) {
 
	    var column = cell.cellIndex;
	    var itm = this.getInnerText(this.tbody[0].rows[1].cells[column]);
		var sortfn = this.sortCaseInsensitive;
 
		if (itm.match(/\d\d[-]+\d\d[-]+\d\d\d\d/)) sortfn = this.sortDate; // date format mm-dd-yyyy
		if (itm.replace(/^\s+|\s+$/g,"").match(/^[\d\.]+$/)) sortfn = this.sortNumeric;
 
		this.sortColumnIndex = column;
 
	    var newRows = new Array();
	    for (j = 0; j < this.tbody[0].rows.length; j++) {
			newRows[j] = this.tbody[0].rows[j];
		}
 
		newRows.sort(sortfn);
 
		if (cell.getAttribute("sortdir") == 'down') {
			newRows.reverse();
			cell.setAttribute('sortdir','up');
		} else {
			cell.setAttribute('sortdir','down');
		}
 
		for (i=0;i<newRows.length;i++) {
			this.tbody[0].appendChild(newRows[i]);
		}
 
	}
 
	this.sortCaseInsensitive = function(a,b) {
		aa = thisObject.getInnerText(a.cells[thisObject.sortColumnIndex]).toLowerCase();
		bb = thisObject.getInnerText(b.cells[thisObject.sortColumnIndex]).toLowerCase();
		if (aa==bb) return 0;
		if (aa<bb) return -1;
		return 1;
	}
 
	this.sortDate = function(a,b) {
		aa = thisObject.getInnerText(a.cells[thisObject.sortColumnIndex]);
		bb = thisObject.getInnerText(b.cells[thisObject.sortColumnIndex]);
		date1 = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
		date2 = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);
		if (date1==date2) return 0;
		if (date1<date2) return -1;
		return 1;
	}
 
	this.sortNumeric = function(a,b) {
		aa = parseFloat(thisObject.getInnerText(a.cells[thisObject.sortColumnIndex]));
		if (isNaN(aa)) aa = 0;
		bb = parseFloat(thisObject.getInnerText(b.cells[thisObject.sortColumnIndex]));
		if (isNaN(bb)) bb = 0;
		return aa-bb;
	}
 
	// define variables
	var thisObject = this;
	var sortSection = this.thead;
 
	// constructor actions
	if (!(this.tbody && this.tbody[0].rows && this.tbody[0].rows.length > 0)) return;
 
	if (sortSection && sortSection[0].rows && sortSection[0].rows.length > 0) {
		var sortRow = sortSection[0].rows[0];
	} else {
		return;
	}
 
	for (var i=0; i<sortRow.cells.length; i++) {
		sortRow.cells[i].sTable = this;
		sortRow.cells[i].onclick = function () {
			this.sTable.sort(this);
			return false;
		}
	}
 
}

 
   function CurrDate() { 
   var mydate= new Date()
var theyear=mydate.getFullYear()
var themonth=mydate.getMonth()+1
var thetoday=mydate.getDate()


alert(themonth+"/"+thetoday+"/"+theyear)

 document.all.CurrDate.value=themonth+"/"+thetoday+"/"+theyear; 
   }
   </script>
</head>
<body>
   <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       <span style="font-size: 10pt; color: #ff9900; font-family: Trebuchet MS"><strong><em>
       </em></strong></span>
     
           <table style="font-weight: bold; font-size: 10pt; color: #ff9900; font-style: italic; font-family: Trebuchet MS">
               <tr>
                   <td  >
                  Level: 
                   </td>
                   <td>
                    <asp:DropDownList ID="DLLevel" runat="server" AutoPostBack="true" Font-Names="Trebuchet MS" Font-Size="Small">
                    <asp:ListItem Text="Select Level" Value=""></asp:ListItem>
                    </asp:DropDownList> 
                   </td>
                   <td>
                  Current Time:  
                   </td>
                  <td>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">   <ContentTemplate>   <asp:Timer ID="timer1" runat="server" Interval="800" ></asp:Timer><asp:Label ID="lbltime" runat="server" Font-Names="Trebuchet MS" ForeColor="Gray"></asp:Label></ContentTemplate>  </asp:UpdatePanel> 
                   
                   </td> 
               </tr>
              
           </table>
  
      <asp:Panel ID="Panel1" runat="server" Height="50px" Width="100%">
            
            <table id="Table1" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'" width="100%">
                <tr>
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        <span style="color: white; font-family: Trebuchet MS"><strong><em>User Status</em></strong></span></td>
                </tr>
             
                <tr style="font-style: italic">
                    <td style="text-align: left" valign="top">
                    <div style=" height:450px; overflow:auto">
       




                                              <asp:Table ID="Table2"  runat="server" BorderColor="Silver" style="text-align: center" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" Width="100%" Font-Size="Small" >

                            <asp:TableRow ID="TableRow1"  CssClass="noScroll" runat="server" style="text-align: center" VerticalAlign="Top">
                       
                                <asp:TableCell ID="TableCell1" RowSpan="2"  runat="server">User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell2" RowSpan="2" runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell3" RowSpan="2" runat="server">Scheduled Mins</asp:TableCell >
                                <asp:TableCell runat="server"  ID="CellMdone" >Finished Mins</asp:TableCell >
                                <asp:TableCell  runat="server" ID="CellCout" >Assigned Mins</asp:TableCell >
                                <asp:TableCell ID="TableCell4" RowSpan="2" runat="server" >Pending</asp:TableCell >
                                <asp:TableCell ID="TableCell5"  RowSpan="2" runat="server">Direct Mins</asp:TableCell >
                                
                            </asp:TableRow> 

                            
                    <asp:TableRow ID="TableRow2"  CssClass="noScroll" runat="server" style="text-align: center">
                        
                        <asp:TableCell ID="Done" runat="server">
                            <asp:Label ID="LDone" runat="server" ></asp:Label></asp:TableCell>
                        
                        
                        <asp:TableCell ID="COut" runat="server">
                        <asp:Label ID="LOut" runat="server" ></asp:Label></asp:TableCell>
                        
                                
                            </asp:TableRow> 
                            </asp:Table>
                           </div> 
                           </td>
                            
                        </tr>
                        </table> 
                        
                    </asp:Panel>                 
           
        <br />
        <br />
       <asp:HiddenField ID="HSelLvl" runat="server" />
      <asp:HiddenField ID="CurrDate" runat="server" /> 
        <br />
    </form>
</body>
</html>

