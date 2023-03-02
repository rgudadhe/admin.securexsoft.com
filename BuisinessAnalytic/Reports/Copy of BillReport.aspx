<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="Copy of BillReport.aspx.vb" Inherits="MIS_DailyMins" %>

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
    <title>Customer Report</title>
</head>
<body>
    <form id="BillDet" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Summary</h1>
        <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="500">
             <tr>
                <td class="HeaderDiv" >
                    Search
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="500">
                <tr >
                    <td style="text-align: center" width="30%" class="alt1" >
                        Month
                    </td>
                    <td style="text-align: center" width="30%" class="alt1">
                        Year
                    </td>
                    <td style="text-align: center" width="30%" class="alt1">
                        Cycle
                    </td> 
               </tr>
            <tr >
                 <td style="text-align: center" width="30%" >
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                                             </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="30%">
                    <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                     
                    </asp:DropDownList>
                </td>
               <td style="text-align: center" width="30%">
                    <asp:DropDownList ID="DLCycle" runat="server" CssClass="common">
                        <asp:ListItem Text="Cycle1" Value="1"></asp:ListItem>  
                        <asp:ListItem Text="Cycle2" Value="2"></asp:ListItem>  
                        <asp:ListItem Text="Month" Value="3"></asp:ListItem>  
                     </asp:DropDownList>
                </td> 
           </tr>
           <tr>
                <td style="text-align: center;" colspan="3" class="alt1" >
                    <asp:Button ID="tblsubmit" cssClass="button" runat="server" Text="Submit" />
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
        
        
        <asp:Table ID="tblMins" runat="server" Width="100%" HorizontalAlign="Center">
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" ID="tblDtls" ColumnSpan="14" CssClass="HeaderDiv"></asp:TableCell>
                </asp:TableRow>
               
            <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server" CssClass="alt1" ID="TableCell9">Account Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="TableCell8">AccNo</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="TableCell7">Mode</asp:TableCell> 
               <asp:TableCell runat="server" CssClass="alt1" ID="TableCell6">Cycle</asp:TableCell>  
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell2">65 CPL</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell3">STAT</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell5">Billing Units</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell6">Billing Stat Units</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell10">Billing Amt</asp:TableCell>
               <asp:TableCell runat="server" CssClass="alt1" ID="TableCell1">VAS Charges</asp:TableCell> 
               <asp:TableCell runat="server" CssClass="alt1" ID="TableCell2">Total Billing</asp:TableCell> 
               <asp:TableCell runat="server" CssClass="alt1" ID="TableCell3">Post Billing</asp:TableCell> 
               <asp:TableCell runat="server" CssClass="alt1" ID="TableCell4">Invoice#</asp:TableCell> 
               <asp:TableCell runat="server" CssClass="alt1" ID="TableCell5">Download</asp:TableCell> 
              
              
            </asp:TableRow>
           
        </asp:Table>
        <br />
        <asp:Button ID="btnEmail" runat="server"    CssClass="button" Text="E-Mail" Visible="False" />
        <asp:Button ID="btnPost" runat="server"  OnClientClick="PostBilling();return false;" CssClass="button" Text="Post" Visible="False" />
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
