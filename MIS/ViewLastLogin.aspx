<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLastLogin.aspx.vb" Inherits="RoutingTool_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <title>Users last login</title>
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
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Last Login</h1>
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:Table ID="Table2"  runat="server">
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="TableCell4" CssClass="HeaderDiv" ColumnSpan="3"    runat="server">Login Trail</asp:TableCell >
                    </asp:TableRow> 
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell1" CssClass="alt"  runat="server">User Name</asp:TableCell >
                        <asp:TableCell ID="TableCell2" CssClass="alt"  runat="server">User ID</asp:TableCell >
                        <asp:TableCell ID="TableCell3" CssClass="alt"  runat="server">Last Login</asp:TableCell >
                    </asp:TableRow> 
                </asp:Table>
            </asp:Panel>
        <br />
        <br />
       <asp:HiddenField ID="HSelLvl" runat="server" />
      <asp:HiddenField ID="CurrDate" runat="server" /> 
        <br />
        </div> 
        </div> 
    </form>
</body>
</html>

