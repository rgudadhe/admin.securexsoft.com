<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="SendReport.aspx.vb" Inherits="MIS_DailyMins" %>

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
<link href= "../../../styles/Default.css" type="text/css" rel="stylesheet"/>
<style type="text/css" >
tr.tblbg {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg1 {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg2 {
	background: #e7e6e6 url(../images/tbbg2.jpg) repeat;
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
/* links */
a, a:visited {	
	color: #000000; 
	background: inherit;
	text-decoration: none;		
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
a:hover {
	color: #000000;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}

tr.tblbgbody {
	background: #e7e6e6 url(../images/bgorange1.JPG) repeat;
	
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
input.button { 
	font: bold 8pt Arial, Sans-serif; 
	height: 24px;
	margin: 0;
	padding: 2px 3px; 
	color: #ffffff;
	background: #E56717;
	border: 1px solid #dadada;
}

</style>
<script language="JavaScript">

function PostBilling()
{
var checkbox_choices = 0;

for(var i=0; i < document.all.chBillAccID.length; i++){
if(document.all.chBillAccID[i].checked)
checkbox_choices = checkbox_choices + 1; 
}

if (checkbox_choices > 0 )
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
    <title>Untitled Page</title>
</head>
<body>
    <form id="BillDet" runat="server">
  
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
             <tr class="tblbg">
                <td colspan="3" style="text-align: center" >
                    <span style="font-size: 8pt; font-family: Arial"><strong>Billing Report</strong></span>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2">
            
            <tr class="tblbg2">
                 <td style="text-align: center" width="30%" height="60">
                     <span style="font-size: 8pt;  font-family: Arial"><strong>
                    Month:</strong></span><br />
                     
                     <asp:DropDownList ID="DLMonth" runat="server"
                            Font-Names="Arial" Font-Size="8">
                           <asp:ListItem Text="August" Value="8"></asp:ListItem>  
                           <asp:ListItem Text="September" Value="9"></asp:ListItem>  
                        </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="30%">
                    <strong><span style="font-size: 8pt;  font-family: Arial">
                        Year:</span></strong><br />
                        <asp:DropDownList ID="DLYear" runat="server"
                            Font-Names="Arial" Font-Size="8">
                           <asp:ListItem Text="2008" Value="2008"></asp:ListItem>  
                           </asp:DropDownList>
                </td>
               <td style="text-align: center" width="30%">
                    <strong><span style="font-size: 8pt;  font-family: Arial">
                        Cycle:</span></strong><br />
                        <asp:DropDownList ID="DLCycle" runat="server"
                            Font-Names="Arial" Font-Size="8">
                           <asp:ListItem Text="Cycle1" Value="1"></asp:ListItem>  
                           <asp:ListItem Text="Cycle2" Value="2"></asp:ListItem>  
                           
                        </asp:DropDownList>
                </td> 
                
           </tr>
           <tr class="tblbg2">
                <td style="text-align: center;" colspan="3" >
                    <asp:Button ID="tblsubmit" cssClass="button" runat="server" Text="Submit" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
     
     <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        
        
        <asp:Table ID="tblMins" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="8"  Width="100%" BorderColor="Silver" GridUnits="both"  HorizontalAlign="Center" CssClass="tblbg" >
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center"  CssClass="tblbgbody" ForeColor = "White" Font-Size="8"    >
                <asp:TableCell runat="server" ID="tblDtls"  ColumnSpan="14" >Billing Report</asp:TableCell>
                </asp:TableRow>
               
            <asp:TableRow runat="server" HorizontalAlign="Center" CssClass="tblbg"  >
                <asp:TableCell runat="server" ID="TableCell9">Account Name</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell8">AccNo</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell7">Mode</asp:TableCell> 
                <asp:TableCell runat="server" ID="R1Cell2">65 CPL</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell3">STAT</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell5">Billing Units</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell6">Billing Stat Units</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell10">Billing Amt</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell1">VAS Charges</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell2">Total Billing</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell3">Post Billing</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell4">Invoice#</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell5">Download</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell6">E-Mail</asp:TableCell> 
              
            </asp:TableRow>
           
        </asp:Table>
        <br />
        <asp:Button ID="btnEmail" runat="server"    CssClass="button" Text="E-Mail" Visible="False" />
        <asp:Button ID="btnPost" runat="server"  OnClientClick="PostBilling();return false;" CssClass="button" Text="Post" Visible="False" />
      

   
    </form>
</body>
</html>
