<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewBalReport.aspx.vb" Inherits="MIS_DailyMins" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<script language="JavaScript" type="text/javascript">

function PostBilling()
{
var checkbox_choices = 0;

for(var i=0; i < document.all.chBillAccID.length; i++){
if(document.all.chBillAccID[i].checked)
checkbox_choices = checkbox_choices + 1; 
}

if (checkbox_choices > 0 || document.all.chBillAccID.checked)
{
displayWindow = window.open('', "newWin7", " width=600,height=200,scrollbars=1,menubar=0,toolbar=0,location=0,status=1");
document.BillDet.target = "newWin7";
document.BillDet.action = "PostBilling.aspx";
document.BillDet.submit();
document.BillDet.target = "_self";
document.BillDet.action = "BillReport.aspx";
}
else
{
alert("No checkbox is selected");
}
}

function highlightRow(InputNode) {
//alert(InputNode.checked);
//	alert(document.BillDet.x.value);

     var el = InputNode;
     while (el.nodeName.toLowerCase() != 'tr')
           el = el.parentNode;
           //alert(e1.parentnode);
 //    el.style.backgroundColor = (InputNode.checked) ? '#eee8aa' : '#d7dbdd';
    if(InputNode.checked)
     {
     //el.style.backgroundColor='#eee8aa';
     el.style.background='url(../../images/tab-hover.gif)'; 
     ChVal = true;
     for (var i=0;i<document.BillDet.elements.length;i++)
			{
			//alert(document.BillDet.elements[i].value);
				if (document.BillDet.elements[i].name == 'chBillAccID' && document.BillDet.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}
	 }
     else
     {
     el.style.background='url(../../images/tbbg2.jpg)'; 
     //el.style.backgroundColor='#d7dbdd';
      ChVal = false;
         for (var i=0;i<document.BillDet.elements.length;i++)
			{
			//alert(document.BillDet.elements[i].value);
				if (document.BillDet.elements[i].name == 'chBillAccID' && document.BillDet.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}

   }
   //alert(ChVal);   
	document.BillDet.SelAll.checked=ChVal; 

}


function changeAll() {
		if (document.BillDet.SelAll.checked) {
			elval = true;
			//document.BillDet.Selection.value = "false";
			//document.BillDet.selectbutton.value = "Select All";
		} 
		else 
		{
			elval = false;
			//document.BillDet.Selection.value = "true";
			//document.BillDet.selectbutton.value = "De-Select All";
		}
		for (var i=0;i<document.BillDet.elements.length;i++)
			{
				//document.BillDet.elements[i].checked = elval;
				//alert(document.BillDet.elements[i].name);
				if (document.BillDet.elements[i].name == 'chBillAccID')
				{
				document.BillDet.elements[i].checked = elval;
				highlightRow(document.BillDet.elements[i]);
				}
				}
				}
</script>
    <title>Billing Report</title>
</head>
<body>
    <form id="BillDet" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Balace Report</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Table ID="tblMins" runat="server" HorizontalAlign="Left" Width="50%">
                <asp:TableRow>
                    <asp:TableCell CssClass="HeaderDiv" ColumnSpan="2">
                        View Balance Report
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Left">
                    <asp:TableCell runat="server" CssClass="alt1"  horizontalAlign="Center" ID="TableCell9" Width="200px">Account Name</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" horizontalAlign="Center" ID="TableCell8" Width="100px">Balance</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
