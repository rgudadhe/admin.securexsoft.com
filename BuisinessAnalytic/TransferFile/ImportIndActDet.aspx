<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportIndActDet.aspx.vb" Inherits="Login_CreateUser" %>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
         <table width="100%">
            <tr>
                <td colspan="3" style="background-image:url(../images/DEMOgraphics.jpg); height:30px; text-align: left;">
                    <span style="font-size: 8pt; font-family: Arial"><strong>Import Invoice Sheets<span
                        style="font-size: 8pt"><span style="font-family: Arial">&nbsp;</span></span></strong></span></td>
            </tr>
            </table>
            
    <div>
        <table id="MainTable" width="40%" style="font-size: 8pt; font-family: 'Arial'; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td class="DEMO4" valign="top" colspan ="2" >
                    <span style="font-family: Arial; color: white;"><strong>Click on Browse button
                        and select the file.</strong></span></td>
               </tr>
                
            <tr>
                <td class="DEMO5" valign="top">
                    
                        <asp:FileUpload ID="FileUpload1" runat="server" />
        
                    
            </td>
            </tr>
        
        <tr>
                <td class="DEMO5" valign="top">
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                <p>
        <asp:Label ID="Label2" runat="server"></asp:Label></p>
                    </td>
            </tr>
           
        </table>
        <br />
        
        
        <asp:Label ID="Label1" runat="server" Font-Bold="True"   Font-Names="Arial" Font-Size="Smaller" ></asp:Label>&nbsp;<br />
        <br />
        <asp:Table ID="TblDetails" runat="server" Font-Names="Arial" Font-Size="8" CellPadding="2" CellSpacing="2" GridUnits="Both">
        <asp:TableRow>
        <asp:TableCell>Acount</asp:TableCell> 
        <asp:TableCell>Number</asp:TableCell> 
        <asp:TableCell>TYPE</asp:TableCell> 
        <asp:TableCell>DATE</asp:TableCell> 
        <asp:TableCell>Ref #</asp:TableCell> 
        <asp:TableCell>AMOUNT</asp:TableCell> 
        <asp:TableCell>COMMENTS</asp:TableCell> 
        <asp:TableCell>MONTH</asp:TableCell> 
        <asp:TableCell>YEAR</asp:TableCell> 
        <asp:TableCell>CYCLE</asp:TableCell> 
        <asp:TableCell>STATUS</asp:TableCell> 
        
        </asp:TableRow>
        
        </asp:Table>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="DispBox" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="8" ForeColor="#C00000"></asp:Label><asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HFoldName" runat="server" />
        <asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="DemoFieldText" runat="server" />
        <asp:HiddenField ID="DemoFieldValue" runat="server" />
        <asp:HiddenField ID="HDictCode" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        </div>
        
    </form>
</body>
</html>
