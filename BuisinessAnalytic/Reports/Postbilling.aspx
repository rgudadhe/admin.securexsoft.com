<%@ Page Language="VB" enableViewStateMac="False"  AutoEventWireup="false" CodeFile="Postbilling.aspx.vb" Inherits="Billing_Reports_Postbilling" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <title>Post Invoice Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <table width="100%">
            <tr>
                <td colspan="5" style="background-image:url('../../../images/demographics.jpg'); height:30px; text-align: left">
                    <span style="font-size: 8pt; font-family: Arial"><strong>Post Invoice Details<span
                        style="font-size: 8pt"><span style="font-family: Arial">&nbsp;</span></span></strong></span></td>
            </tr>
            </table>  
            
               <asp:Table ID="tblInvoice" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="8"  Width="100%" BorderColor="Silver" GridUnits="both"  HorizontalAlign="Center" CssClass="tblbg" >
    
               
            <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" CssClass="tblbgbody"  >
                <asp:TableCell runat="server" ID="TableCell9">Account Name</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell8">Status</asp:TableCell>
                       
            </asp:TableRow>
           
        </asp:Table>
    </div>
    </form>
</body>
</html>
