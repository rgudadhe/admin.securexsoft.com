<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditTempWise.aspx.vb" Inherits="EditTempWise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   <style type="text/css" >
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
   <style type="text/css" >
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

</head>
<body style="text-align: center">
    <form id="form1" runat="server">
   
     <div style="text-align: center">
    <table width="100%">
            <tr>
                <td colspan="5" style="background-image:url('../../../images/demographics.jpg'); height:30px; text-align: left">
                    <span style="font-size: 8pt; font-family: Arial"><strong>TemplateWise Account<span
                        style="font-size: 8pt"><span style="font-family: Arial">&nbsp;</span></span></strong></span></td>
            </tr>
            </table> 
            
            
    </div>
    
        
        <br />
          <asp:Table ID="Table2" runat="server" width="70%" GridUnits="Both"   cellpadding="2" cellspacing="2"   Font-Names="Arial" Font-Size="8" >
        <asp:TableRow CssClass="tblbgbody"  >
        <asp:TableCell Font-Names="Arial" Font-Size="8" Font-Bold="True"   ColumnSpan="6">Templates Group 
        </asp:TableCell> </asp:TableRow>
        <asp:TableRow CssClass="tblbg" >
        
        <asp:TableCell Font-Names="Arial" Font-Size="8"   HorizontalAlign="Right"   >Templates Group Name
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8"    HorizontalAlign="Left"   >
            <asp:TextBox ID="TxtGrpTempName" runat="server"></asp:TextBox>
        </asp:TableCell>
         <asp:TableCell Font-Names="Arial" Font-Size="8"   HorizontalAlign="Right"   >Description
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8"    HorizontalAlign="Left"   >
            <asp:TextBox ID="TxtDesc" runat="server"></asp:TextBox>
        </asp:TableCell>
         <asp:TableCell Font-Names="Arial" Font-Size="8"   HorizontalAlign="Right"   >Separate Invoice
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8"    HorizontalAlign="Left"   >
            <asp:DropDownList ID="DLSepInvoice" runat="server" CssClass="common" >
         <asp:ListItem Text="No" Value="0"></asp:ListItem> 
         <asp:ListItem Text="Yes" Value="1"></asp:ListItem> 
         
         </asp:DropDownList>
        </asp:TableCell>
        </asp:TableRow>
        </asp:Table> 
         <asp:Table ID="Table5" runat="server" width="70%" GridUnits="Both"   cellpadding="2" cellspacing="2"   Font-Names="Arial" Font-Size="8" >
        <asp:TableRow  CssClass="tblbg">
        <asp:TableCell Font-Names="Arial" Font-Size="8"  ColumnSpan="2" >Available Templates
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8"   ColumnSpan="2" >Assigned Templates
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow  CssClass="tblbg2">
        <asp:TableCell Font-Names="Arial" Font-Size="8"  >
        <asp:ListBox ID="ListBox1" EnableViewState="True"  SelectionMode="Multiple" runat="server" Height="144px" Width="208px" Font-Names="Arial" Font-Size="8" ForeColor="Firebrick"></asp:ListBox> 
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8" Width="8"   >
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/right.jpg" />
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8" Width="8"  >
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/left.jpg" />
        </asp:TableCell>
        <asp:TableCell Font-Names="Arial" Font-Size="8"  >
         <asp:ListBox ID="ListBox2" runat="server" EnableViewState="True"   SelectionMode="Multiple" Height="144px" Width="208px" Font-Names="Arial" Font-Size="8" ForeColor="Firebrick"></asp:ListBox>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow  CssClass="tblbg"> 
        <asp:TableCell Font-Names="Arial" Font-Size="8" ColumnSpan="4" >
             <asp:Button ID="btnAssign" CssClass="button"  runat="server" Text="Submit" /> 
        </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        
        <asp:Label ID="Label1"  ForeColor ="Red" runat="server" ></asp:Label>
        <asp:Label ID="Label2"   ForeColor ="Red" runat="server" ></asp:Label><asp:Label ID="Label3"   ForeColor ="Red" runat="server" ></asp:Label><asp:Label ID="Label4"   ForeColor ="Red" runat="server" ></asp:Label><asp:Label ID="Label5"   ForeColor ="Red" runat="server" ></asp:Label><asp:Label ID="Label6"   ForeColor ="Red" runat="server" ></asp:Label><asp:Label ID="Label7"   ForeColor ="Red" runat="server" ></asp:Label><asp:Label ID="Label8"   ForeColor ="Red" runat="server" ></asp:Label>
    </form>
</body>
</html>
