<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportChartScript.aspx.vb" Inherits="Login_CreateUser" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

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

    <title>Untitled Page</title>

</head>
<body>


    <form id="form1" runat="server">
    <asp:ScriptManager ID="Mdg" runat ="server" ></asp:ScriptManager>
    <div>
       <table width="100%">
            <tr>
                <td colspan="5" style="background-image:url('../../../images/demographics.jpg'); height:30px; text-align: left">
                    <span style="font-size: 8pt; font-family: Arial"><strong>Import Chartscript Lines<span
                        style="font-size: 8pt"><span style="font-family: Arial">&nbsp;</span></span></strong></span></td>
            </tr>
            </table>  
               
               
             <table  id="Table2" width="40%" style="font-size: 8pt; font-family: 'Arial'; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr class="tblbg">
                <td  valign="top" colspan ="2" >
                    <span style="font-family: Arial; "><strong>Click on Browse button
                        and select the file</strong></span></td>
               </tr>
                 <tr >
                 <td style="text-align: center"  width="30%" class="alt1">
                            Post Date
                 </td>
               
                <td  style="text-align: center" width="50%" class="alt1">
                   
                      
                        <asp:TextBox ID="TXTPDate" Width="100" runat="server"  CssClass="common"></asp:TextBox>
                           <asp:ImageButton ID="ImageButton1" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTPDate" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender2" Enabled="True">
                                            </ajaxToolkit:CalendarExtender> 
                          </td>
                
                
           </tr>
            <tr class="tblbg2">
                <td valign="top"  colspan ="2" >
                    
                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="8pt" />
        
                    
            </td>
            </tr>
        
        <tr class="tblbg">
                <td valign="top" style="text-align: center"  colspan ="2" >
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center"  colspan ="2" >
                <p>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"></asp:Label></p>
                    </td>
            </tr>
           
        </table>
        
        <br />
          <asp:Table ID="TblDetails" runat="server" Font-Names="Arial" Font-Size="8" CellPadding="2" CellSpacing="2" GridUnits="Both">
        <asp:TableRow cssclass="tblbgbody">
        <asp:TableCell>Sr No</asp:TableCell> 
        <asp:TableCell>UserName</asp:TableCell> 
        <asp:TableCell>Last Name</asp:TableCell> 
        <asp:TableCell>First Name</asp:TableCell> 
      <asp:TableCell>Level</asp:TableCell>
<asp:TableCell>Units</asp:TableCell>

<asp:TableCell>Post date</asp:TableCell>
 <asp:TableCell>Status</asp:TableCell> 
        
        </asp:TableRow>
        
        </asp:Table>     
       <%-- <br />
        <a  href="Platform Billing Lines - MM DD YYYY.xls"><span style="font-size: 8pt; font-family: Arial; color: #ff3333;">
            <strong>Click here</strong></span></a><span style="color: #ff3333"><span style="font-size: 8pt; font-family: Arial"><strong>
                to download template</strong></span> </span>--%>
    </div>
    </form>
</body>
</html>
