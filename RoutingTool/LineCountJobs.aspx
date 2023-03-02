<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LineCountJobs.aspx.vb" Inherits="RoutingTool_Default"   %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Jobs Routing</title>
      <script language="javascript" type="text/javascript"  >
   function changeAll() {
   var vmins1=0;
		if (document.form1.SelJob.checked) {
			elval = true;
			
		} else {
			elval = false;
		}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			    document.form1.elements[i].checked = elval;
	    if (Left(document.form1.elements[i].name,7) == 'TransID')
			{
			//alert('Hello');
			highlightRow(document.form1.elements[i]);
		
			
			}
			}
				
			}


function Left(str, n){
	if (n <= 0)
	    return "";
	else if (n > String(str).length)
	    return str;
	else
	    return String(str).substring(0,n);
}

function highlightRow(InputNode) {

	var vmins1=0;
	var vjobs=0;
     var el = InputNode;
     while (el.nodeName.toLowerCase() != 'tr')
           el = el.parentNode;
           //alert(e1.parentnode);
 //    el.style.backgroundColor = (InputNode.checked) ? '#eee8aa' : '#d7dbdd';
    if(InputNode.checked)
     {
//     el.style.backgroundColor='#eee8aa';
//     el.style.color='#808080';
     ChVal = true;
     for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==true)
				{
				
                var vmins = document.form1.elements[i].value.split('#');
				var vmins2 = vmins[1];
				vmins1 = parseFloat(vmins1) + parseInt(vmins2);
				vjobs++;
				
				//alert(vmins1);
				}
				    
				}
	 }
     else
     {
//     el.style.backgroundColor='#d7dbdd';
//          el.style.color='#808080';
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==true)
				{
				
                var vmins = document.form1.elements[i].value.split('#');
				var vmins2 = vmins[1];
				vmins1 = parseFloat(vmins1) + parseInt(vmins2);
				vjobs++;
				//alert(vmins1);
				}
				}

   }
   document.getElementById("lblmins").innerHTML=Math.round((vmins1/60));
   document.getElementById("lbljobs").innerHTML=vjobs;
   //alert(ChVal);   
	document.form1.SelJob.checked=ChVal; 

}

function RefWindow()
{
//window.opener.location.reload();
//window.close();
}

function unselect()
{
for (var i=0;i<document.form1.elements.length;i++)
		{
		    document.form1.elements[i].checked = false;
	    	}
}


   </script>
 <style>
<!--
.TableR
{
   padding:2px; border-right:4px solid #afb5b8; border-top:4px solid #fff; background:#FEFDFE; border-left:4px solid #fff; COLOR: #000;
    BORDER-BOTTOM: 4px solid #afb5b8;
    TEXT-ALIGN: center;
    TEXT-TRANSFORM: uppercase;
    }
.noScroll 
{ 
     position:relative; 
     top:expression(this.offsetParent.scrollTop); 
} 
.CommRow
{ 
	
    padding:2px; border-right:1px solid #afb5b8; border-top:1px solid #fff; background:#B32D00; border-left:1px solid #fff;
    BORDER-BOTTOM: 1px solid #afb5b8;    
   TEXT-TRANSFORM: uppercase;

    COLOR: #000000;
    TEXT-ALIGN: center;
} 

.TDNormal{style="border-left-stylesolid"; border-left-width:1; border-right-style:solid; border-right-width:1; border-bottom-style:solid; border-bottom-width:1; font-family:Trebuchet MS; font-variant:normal; font-size:8pt; color="#000080";text-transform:capitalize;}
.TDComm{style="border-left-stylesolid"; border-left-width:1; border-right-style:solid; border-right-width:1; border-bottom-style:solid; border-bottom-width:1; font-family:Trebuchet MS; font-variant:normal; font-size:8pt; text-transform:capitalize;}


a:link.nav   { font-family: Arial,San Serif; font-size: 11px; text-decoration: none }
a:visited.nav { font-family: Arial,San Serif; font-size: 11px; text-decoration: none }
a:active.nav { font-family: Arial,San Serif; font-size: 11px; text-decoration: none }
a:hover.nav  { font-family: Arial,San Serif; font-size: 11px; text-decoration: none; color: #EBDDE2 }
.HEADING     { cursor: hand; font-family: Arial,San Serif; font-size: 12px; color: #333333; 
               background-color: #616D7E; font-weight: none; 
               border: 1px solid #000000;  }
.LINKSOFF    { display: none; font-family: Arial,San Serif; font-size: 12px; color: #000080 }
.LINKSON     { display: inline; font-family: Arial,San Serif; font-size: 12px; color: #000080 }
input.btnhov{
   border-top-color:#c63;
   border-left-color:#c63;
   border-right-color:#930;
   border-bottom-color:#930;}
input.btn{
   color:#050;
   font-family:'trebuchet ms',helvetica,sans-serif;
   font-size:10px;
   font-weight:bold;
   background-color:#fed;
   border:1px solid;
   border-top-color:#696;
   border-left-color:#696;
   border-right-color:#363;
   border-bottom-color:#363;
   width:100px;
   height:18px;   
   filter:progid:DXImageTransform.Microsoft.Gradient      (GradientType=0,StartColorStr='#ffffffff',EndColorStr='#ffeeddaa')
-->
</style>

</head>
<body onunload="RefWindow();"  style="margin-top: 0px; padding-top: 0px; background-color: white; text-align: center;" onload="unselect();" >
   <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
           
                        <br />
                        <strong>Selected Mins : &nbsp;</strong><asp:Label ID="lblmins" runat="server" Font-Bold="True"
                            Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#C00000"></asp:Label>
                        &nbsp; &nbsp; &nbsp; <strong>Selected Jobs : &nbsp;</strong><asp:Label ID="lbljobs"
                            runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#C00000"></asp:Label><br />
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
                                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                                            <asp:TableCell runat="server" >
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                                            <asp:TableCell runat="server">Job#</asp:TableCell>
                                            <asp:TableCell runat="server">Status</asp:TableCell>
                                            <asp:TableCell runat="server">Username</asp:TableCell>
                                            <asp:TableCell runat="server">TAT</asp:TableCell>
                                            <asp:TableCell runat="server">Priority</asp:TableCell>
                                            <asp:TableCell runat="server">Duration</asp:TableCell>
                                            <asp:TableCell runat="server">Submit Date</asp:TableCell>
                                            <asp:TableCell ID="TableCell1" runat="server">Due Date</asp:TableCell>
                                             <asp:TableCell ID="TableCell3" runat="server">Ramaining TAT</asp:TableCell>
                                            <asp:TableCell runat="server">Account Name</asp:TableCell>
                                            <asp:TableCell runat="server">Physician Name</asp:TableCell>
                                            <asp:TableCell ID="TableCell2" runat="server">Direct</asp:TableCell>
                                            
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
                            
                        </table>
        
       
       <asp:HiddenField ID="HAccID" runat="server" />
       
       <asp:HiddenField ID="TotJobs" runat="server" />
       <asp:HiddenField ID="ProLevel" runat="server" />
       
       <asp:HiddenField ID="HUserID" runat="server" /><asp:HiddenField ID="ActDisp" runat="server" />
       &nbsp;
    </form>
</body>
</html>

