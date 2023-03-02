<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpExcMins.aspx.vb" Inherits="Excess_Minutes_Default" EnableViewState="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
    <title>Excess Minutes</title>
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
      
            <table id="Table1" width="100%">
                <tr>
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        Excess Minutes Assignment</td>
                </tr>
                <tr>
                    <td  style="text-align: center" valign="top">
                        <asp:Table ID="Table2" runat="server"  Width="100%">
                                <asp:TableRow runat="server" style="text-align: center">
                                    <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Protocol Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Fresh Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Finished Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Not Finished Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Pending Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Excess Mins</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </tr>
                        </table> 
        <br />
        <table id="Table4" width="100%">
            <tr>
                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                    Dictation Details</td>
            </tr>
            <tr>
                <td  style="text-align: center" valign="top">
                    <asp:Table ID="Table3" runat="server"  Width="100%">
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell CssClass="alt1" runat="server">
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Job Number</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Duration</asp:TableCell>
                             <asp:TableCell CssClass="alt1" ID="TableCell3" runat="server">Status</asp:TableCell> 
                            <asp:TableCell CssClass="alt1" runat="server">Submit Date</asp:TableCell>
                           <asp:TableCell CssClass="alt1" ID="TableCell1" runat="server">TAT</asp:TableCell>  
                           <asp:TableCell CssClass="alt1" runat="server">Due Date</asp:TableCell> 
                           <asp:TableCell CssClass="alt1" ID="TableCell2" runat="server">Priority</asp:TableCell>  
                           <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                           <asp:TableCell CssClass="alt1" runat="server">Physician Name</asp:TableCell>
                           <asp:TableCell CssClass="alt1" runat="server">Template Name</asp:TableCell>
                            
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
                    Dictation Details<asp:Button ID="submit" CssClass="button" runat="server" Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <asp:HiddenField ID="HAccID" runat="server" />
         <asp:HiddenField ID="SrvStDate" runat="server" />
        <br />
        <asp:HiddenField ID="TotJobs" runat="server" />
    </form>
</body>
</html>
