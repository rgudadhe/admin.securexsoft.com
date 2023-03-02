<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IndirectJobsA.aspx.vb" Inherits="RoutingTool_Default"   %>

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
           
                        <br />
                        <strong>Selected Mins : &nbsp;</strong><asp:Label ID="lblmins" runat="server" CssClass="common" ForeColor="#C00000"></asp:Label>
                        &nbsp; &nbsp; &nbsp; <strong>Selected Jobs : &nbsp;</strong><asp:Label ID="lbljobs"
                            runat="server" CssClass="common" ForeColor="#C00000"></asp:Label><br />
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
                                            <asp:TableCell runat="server" CssClass="alt1" >
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Job#</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Status</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Username</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">TAT</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Priority</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Duration</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Submit Date</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell1" runat="server">Due Date</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" runat="server">Physician Name</asp:TableCell>
                                            <asp:TableCell CssClass="alt1" ID="TableCell2" runat="server">Direct</asp:TableCell>
                                            
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                                        <asp:Label ID="lblTotJobs" runat="server" Font-Bold="True" CssClass="common"></asp:Label>
                                        &nbsp; <strong>Total Mins:
                                            <asp:Label ID="LblTMins" runat="server" Font-Bold="True" CssClass="common"></asp:Label>
                                            &nbsp; Direct Mins:
                                            <asp:Label ID="LblDMins" runat="server" Font-Bold="True" CssClass="common"></asp:Label></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">   <ContentTemplate>  
                                        <asp:DropDownList ID="DLStatus" runat="server" AutoPostBack="True" CssClass="common" >
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DLChoice" runat="server"  CssClass="common">
                                        </asp:DropDownList>
                                       </ContentTemplate>  </asp:UpdatePanel>  
                                    <asp:Button ID="Button1" CssClass="button" runat="server" Text="Assign" /></td> 
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

