<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpExcMinsNew.aspx.vb" Inherits="Excess_Minutes_Default" EnableViewState="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Excess Minutes</title>
    <link href= "../App_Themes/Css/RoutingTool.css" type="text/css" rel="stylesheet"/>
   <script language="javascript" type="text/javascript"  >
   function changeAll() {
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
	//alert(InputNode);
     var el = InputNode;
     while (el.nodeName.toLowerCase() != 'tr')
           el = el.parentNode;
           //alert(e1.parentnode);
 //    el.style.backgroundColor = (InputNode.checked) ? '#eee8aa' : '#d7dbdd';
    if(InputNode.checked)
     {
     el.style.backgroundColor='#eee8aa';
     ChVal = true;
     for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}
	 }
     else
     {
     el.style.backgroundColor='#d7dbdd';
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}

   }
   //alert(ChVal);   
	document.form1.SelJob.checked=ChVal; 

}

   </script>
   
 
</head>

<body style="text-align: left">

    <form id="form1" runat="server">
      
            <table id="Table1" width="100%" border="2" cellpadding="2" cellspacing="2" style="font-family:Trebuchet MS; font-size:8pt">
                <tr>
                    <td colspan="2" style="width: 100%; text-align: center;background-image:url('../App_Themes/Images/background_parentselected.gif'); color:White" valign="top">
                        <b>Excess Minutes Assignment</b></td>
                </tr>
                <tr>
                    <td  style="text-align: center" valign="top">
                        <asp:Table ID="Table2" runat="server"  Width="100%" GridLines="Both">
                                <asp:TableRow runat="server" style="text-align: center" CssClass="SMSelected">
                                    <asp:TableCell runat="server">Account Name</asp:TableCell>
                                    <asp:TableCell runat="server">Protocol Mins</asp:TableCell>
                                    <asp:TableCell runat="server">Fresh Mins</asp:TableCell>
                                    <asp:TableCell runat="server">Finished Mins</asp:TableCell>
                                    <asp:TableCell runat="server">Not Finished Mins</asp:TableCell>
                                    <asp:TableCell runat="server">Pending Mins</asp:TableCell>
                                    <asp:TableCell runat="server">Excess Mins</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            </td> 
                        </tr>
                        </table> 
        <br />
        <table id="Table4" width="100%" border="2" cellpadding="2" cellspacing="2" style="font-family:Trebuchet MS; font-size:8pt">
            <tr style="background-image:url('../App_Themes/Images/background_parentselected.gif')">
                <td colspan="2" style="width: 100%; text-align: center; color:White" valign="top">
                   <b>Dictation Details</b></td>
            </tr>
            <tr>
                <td  style="text-align: center" valign="top">
                    <asp:Table ID="Table3" runat="server"  Width="100%" GridLines="Both" Font-Names="Trebuchet MS" Font-Size="8pt">
                        <asp:TableRow runat="server" style="text-align: center" CssClass="SMSelected">
                            <asp:TableCell runat="server">
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                            <asp:TableCell runat="server">Job Number</asp:TableCell>
                            <asp:TableCell runat="server">Duration</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server">Status</asp:TableCell> 
                            <asp:TableCell runat="server">Submit Date</asp:TableCell>
                           <asp:TableCell ID="TableCell1" runat="server">TAT</asp:TableCell>  
                           <asp:TableCell runat="server">Due Date</asp:TableCell> 
                           <asp:TableCell ID="TableCell2" runat="server">Priority</asp:TableCell>  
                           <asp:TableCell runat="server">Account Name</asp:TableCell>
                           <asp:TableCell runat="server">Physician Name</asp:TableCell>
                           <asp:TableCell runat="server">Template Name</asp:TableCell>
                            
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                        <asp:Label ID="lblTotJobs" runat="server" Font-Bold="True" CssClass="common"></asp:Label>
                        &nbsp; <strong>Total Mins:
                            <asp:Label ID="lblTotMins" runat="server" Font-Bold="True" CssClass="common"></asp:Label></strong></span></td>
            </tr>
              <tr>
                <td style="text-align: center" >
                    <asp:Button ID="submit" Font-Names="Trebuchet MS" Font-Size="8pt" runat="server" Text="Submit" /></td>
            </tr>
        </table>
        <br />
                        <div style="text-align:left">
                            <asp:Label ID="lblStatusMsg" runat="server" Text="" ForeColor="Firebrick" Font-Names="Trebuchet MS" Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </div>
        <br />
        <asp:HiddenField ID="HAccID" runat="server" />
         <asp:HiddenField ID="SrvStDate" runat="server" />
        <br />
        <asp:HiddenField ID="TotJobs" runat="server" />
    </form>
</body>
</html>
