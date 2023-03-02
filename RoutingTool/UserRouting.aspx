<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserRouting.aspx.vb" Inherits="RoutingTool_Default" EnableViewState="false"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
    <title>User Routing</title>
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
     //el.style.backgroundColor='#646D7E';
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
   //  el.style.backgroundColor='#003366';
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


function unselect()
{
for (var i=0;i<document.form1.elements.length;i++)
		{
		    document.form1.elements[i].checked = false;
	    	}
}


   </script>
 

</head>
<body onload="unselect();" >
   <form id="form1" runat="server">
            <table id="Table1" width="100%"  class="noScroll">
                <tr class="noScroll">
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        Routing Tool
                    </td>
                </tr>
               
                <tr >
                    <td style="text-align: left; height: 69px;" valign="top">
                          <asp:Table ID="Table2"  runat="server" style="text-align: center" Width="100%">
                            <asp:TableRow ID="TableRow1"   runat="server" style="text-align: center" VerticalAlign="Top">
                                <asp:TableCell ID="TableCell13" CssClass="alt1" runat="server">User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell14"  CssClass="alt1"  runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell15"  CssClass="alt1"  runat="server">SchMins</asp:TableCell >
                                 <asp:TableCell ID="TableCell16"  CssClass="alt1" runat="server">STime</asp:TableCell >
                                 <asp:TableCell ID="TableCell17"  CssClass="alt1" runat="server">ETime</asp:TableCell >
                                <asp:TableCell runat="server"  CssClass="bk" ID="CellMdone" ><asp:Label ID="LDone1" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell  runat="server"  CssClass="bk" ID="CellCout" ><asp:Label ID="LOut" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell ID="TableCell18"  CssClass="alt1"  runat="server" >Pending</asp:TableCell >
                                <asp:TableCell ID="TableCell19"  CssClass="alt1"   runat="server">Direct Mins</asp:TableCell >
                            </asp:TableRow> 
                            </asp:Table>
                        <br />
                        <strong>Selected Mins : &nbsp;</strong><asp:Label ID="lblmins" runat="server" Font-Bold="True"
                            CssClass="common"  ForeColor="#C00000"></asp:Label>
                        &nbsp; &nbsp; &nbsp; <strong>Selected Jobs : &nbsp;</strong><asp:Label ID="lbljobs"
                            runat="server" Font-Bold="True" CssClass="common" ForeColor="#C00000"></asp:Label></td>
                            
                        </tr>
                        </table> &nbsp;&nbsp;<br />
       <table id="Table6" runat="server" width="70%" onclick="return Table6_onclick()">
           <tr>
               <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                   Account Status
               </td>
           </tr>
           <tr>
               <td style="text-align: center" valign="top">
                   <asp:Table ID="Table3" runat="server" CssClass="common" Width="100%">
                   </asp:Table>
               </td>
           </tr>
       </table>
       <br />
                        <table id="Table5" runat="server" width="100%">
                            <tr>
                                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center; height: 15px;" valign="top">
                                    Dictation Details
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Table ID="Table4" runat="server" Width="100%">
                                        <asp:TableRow runat="server" style="text-align: center">
                                            <asp:TableCell CssClass="alt1" runat="server" >
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell1" runat="server">Job#</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell2" runat="server">Status</asp:TableCell>
                                            <asp:TableCell CssClass="bk" ID="TableCell11" runat="server">
                                                <asp:Label ID="LDone" runat="server" ></asp:Label></asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell3" runat="server">TAT</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell4" runat="server">Priority</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell5" runat="server">Duration</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell6" runat="server">Submit Date</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell7" runat="server">Due Date</asp:TableCell>
                                                                                        <asp:TableCell ID="TableCell12" CssClass="alt1" runat="server">Remaining TAT</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell8" runat="server">Account Name</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell9" runat="server">Physician Name</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell20" runat="server">Category</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell10" runat="server">Direct</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" class="common">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                                        <asp:Label ID="lblTotJobs" cssclass="common" runat="server" Font-Bold="True"></asp:Label>
                                        &nbsp; <strong>Total Mins:
                                            <asp:Label ID="LblTMins" cssclass="common" runat="server" Font-Bold="True"></asp:Label>
                                            &nbsp; Direct Mins:
                                            <asp:Label ID="LblDMins" cssclass="common" runat="server" Font-Bold="True"></asp:Label></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" class="alt1">
                                    Dictation Details<asp:Button
                                        ID="submit" runat="server" Text="Assign Jobs" CssClass="button" /></td>
                            </tr>
                        </table>
        
       
       <asp:HiddenField ID="HAccID" runat="server" />
       
       <asp:HiddenField ID="TotJobs" runat="server" />
       <asp:HiddenField ID="ProLevel" runat="server" />
       
       <asp:HiddenField ID="HUserID" runat="server" /><asp:HiddenField ID="ActDisp" runat="server" />
    </form>
</body>
</html>
