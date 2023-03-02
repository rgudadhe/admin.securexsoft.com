<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IndirectJobsAct.aspx.vb" Inherits="RoutingTool_Default"   %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
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
 

</head>
<body onunload="RefWindow();"  onload="unselect();" >
   <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Indirect Jobs Status</h1>
       <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table width="100%" >
           <tr>
           <td  width="50%" valign="top" style="border:0">
                        <table id="Table5" runat="server">
                            <tr>
                                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center; height: 15px;" valign="top">
                                    Indirect Jobs AccountWise
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Table ID="Table4" runat="server" Width="100%">
                                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                                            <asp:TableCell ID="TableCell1" runat="server" CssClass="alt1">Account Name</asp:TableCell>
                                            <asp:TableCell ID="TableCell2" runat="server" CssClass="alt1">Total Jobs</asp:TableCell>
                                            <asp:TableCell ID="TableCell3" runat="server" CssClass="alt1">Total Mins</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong></span> 
                                        <asp:Label ID="lblTotJobs" runat="server" Font-Bold="True" CssClass="common"></asp:Label>
                                            &nbsp; Total Mins:
                                            <asp:Label ID="LblTMins" runat="server" Font-Bold="True" CssClass="common"></asp:Label></td>
                            </tr>
                            
                        </table>
        
       
           </td>
           <td  width="50%" valign="top" style="border:0">
                        <table id="Table1" runat="server">
                            <tr>
                                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center; height: 15px;" valign="top">
                                    Indirect Jobs MTWise</td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Table ID="Table2" runat="server" Width="100%">
                                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                                            <asp:TableCell ID="TableCell4" CssClass="alt1" runat="server">User Name</asp:TableCell>
                                            <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">User ID</asp:TableCell>
                                            <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server">Total Jobs</asp:TableCell>
                                            <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">Total Mins</asp:TableCell>
                                            <asp:TableCell ID="TableCell8" CssClass="alt1" runat="server">Indirect Jobs</asp:TableCell>
                                            <asp:TableCell ID="TableCell9" CssClass="alt1" runat="server">Indirect Mins</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                                        <asp:Label ID="lblTotJobs1" runat="server" Font-Bold="True" CssClass="common"></asp:Label>
                                            &nbsp; Total Mins:
                                            <asp:Label ID="LblTMins1" runat="server" Font-Bold="True" CssClass="common"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Indirect Jobs :</strong>
                                        <asp:Label ID="lblTotiJobs1" runat="server" CssClass="common" Font-Bold="True"></asp:Label>
                                            &nbsp; Total Indirect Mins:
                                            <asp:Label ID="LblTiMins1" runat="server" CssClass="common" Font-Bold="True"></asp:Label></td>
                            </tr>
                            
                        </table>
        
       
           </td>
           </tr></table>
                  <asp:HiddenField ID="HAccID" runat="server" />
       
                  <asp:HiddenField ID="TotJobs" runat="server" />
                  <asp:HiddenField ID="ProLevel" runat="server" />
       
                  <asp:HiddenField ID="HUserID" runat="server" /><asp:HiddenField ID="ActDisp" runat="server" />
                  &nbsp;
       </asp:Panel>
           </div> 
           </div> 
                  
           

    </form>
</body>
</html>

