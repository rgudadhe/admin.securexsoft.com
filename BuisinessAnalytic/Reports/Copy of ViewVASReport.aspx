<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="Copy of ViewVASReport.aspx.vb" Inherits="ViewVasReport" %>

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
    <title>View VAS Report</title>
</head>
<body style="text-align: center">
    <form id="BillDet" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View VAS Report</h1>
        <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">
            <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="500">
             <tr>
                <td colspan="3" style="text-align: center" class="HeaderDiv" >
                    Search VAS Report
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="500">
                <tr >
                 <td style="text-align: center" width="30%" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" width="30%" class="alt1">
                    Year
                </td>
               <td style="text-align: center" width="30%" class="alt1">
                   Account
                </td> 
           </tr>
            <tr >
                 <td style="text-align: center" width="30%" >
                  
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                 <asp:ListItem Text="January" Value="1"></asp:ListItem> 
                 <asp:ListItem Text="February" Value="2"></asp:ListItem> 
                 <asp:ListItem Text="March" Value="3"></asp:ListItem> 
                 <asp:ListItem Text="April" Value="4"></asp:ListItem> 
                 <asp:ListItem Text="May" Value="5"></asp:ListItem> 
                 <asp:ListItem Text="June" Value="6"></asp:ListItem> 
                 <asp:ListItem Text="July" Value="7"></asp:ListItem> 
                 <asp:ListItem Text="August" Value="8"></asp:ListItem> 
                 <asp:ListItem Text="September" Value="9"></asp:ListItem> 
                 <asp:ListItem Text="October" Value="10"></asp:ListItem> 
                  <asp:ListItem Text="November" Value="11"></asp:ListItem> 
                  <asp:ListItem Text="December" Value="12"></asp:ListItem>                         </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="30%">
                    
                        <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                           <asp:ListItem Text="2009" Value="2009"></asp:ListItem>  
                           <asp:ListItem Text="2010" Value="2010"></asp:ListItem> 
                           <asp:ListItem Text="2011" Value="2011"></asp:ListItem>   <asp:ListItem Text="2012" Selected="True"   Value="2012"></asp:ListItem>  
                           </asp:DropDownList>
                </td>
               <td style="text-align: center" width="30%">
                    <asp:DropDownList ID="DLAct" runat="server" CssClass="common">
                    </asp:DropDownList>
                </td> 
           </tr>
           <tr>
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
        ExpandedImage="~/App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="~/App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        
        
        <asp:Table ID="tblMins" runat="server" Width="100%" HorizontalAlign="Center">
            <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" CssClass="tblbgbody"  >
                <asp:TableCell runat="server" CssClass="alt1" ID="TableCell9">Account Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="TableCell8">Description</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="TableCell7">Service Date</asp:TableCell> 
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell2">Quantity</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell3">Rate</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell5">Amount</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell6">Status</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1" ID="R1Cell10">Remove</asp:TableCell>
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
