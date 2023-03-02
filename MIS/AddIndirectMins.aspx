<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddIndirectMins.aspx.vb" Inherits="MIS_AddIndirectMins" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
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
	color: #326ea1; 
	background: inherit;
	text-decoration: none;		
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
}
a:hover {
	color: #383d44;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 13px 'Trebuchet MS', Tahoma, arial, sans-serif;
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
	font: bold 12px Arial, Sans-serif; 
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
        <table border="2" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td class="HeaderDiv" colspan="2" style="text-align: center">
                    <span style="font-size: 10pt; font-family: Trebuchet MS"><strong><em>Add Indirect Account Units</em></strong></span>
                  </td>
            </tr>
        </table>
        <br />

        <asp:Table ID="Table1" runat="server" GridLines="Both" HorizontalAlign="Center" Width="60%">
        <asp:TableRow HorizontalAlign="Center" runat="server" >
        <asp:TableCell runat="server" >  <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em> Indirect Accounts  </em></strong></span>   </asp:TableCell>        
        <asp:TableCell runat="server" >  
        <asp:DropDownList ID="DLAct" runat="server"   Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
        <asp:ListItem Text="Select Account" Value="" Selected="True"  ></asp:ListItem>
         </asp:DropDownList>  
                  </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow HorizontalAlign="Center" runat="server" >
        <asp:TableCell runat="server" > <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em>  Mode    </em></strong></span> </asp:TableCell>
                <asp:TableCell runat="server" > <asp:DropDownList ID="DLMode" runat="server"   Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                <asp:ListItem Text="Post Date" Value="Post"></asp:ListItem> 
                <asp:ListItem Text="Submit Date" Value="Submit"></asp:ListItem> 
            </asp:DropDownList>  
                  </asp:TableCell>     
        </asp:TableRow>
        <asp:TableRow HorizontalAlign="Center" runat="server" >
        <asp:TableCell runat="server" > <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em>  Date </em></strong></span>    </asp:TableCell>        <asp:TableCell runat="server" >   
            <asp:TextBox ID="txtDate" runat="server"   Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox>  
              </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow HorizontalAlign="Center" runat="server" >
        <asp:TableCell runat="server" > <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em>  Units </em></strong></span>    </asp:TableCell>        <asp:TableCell runat="server" >   
            <asp:TextBox ID="txtUnits" runat="server" Text = "0"  Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox>   
              </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow HorizontalAlign="Center" runat="server" >
        <asp:TableCell ColumnSpan="2" runat="server" >     
        <asp:Button id="btnsubmit" runat="server" Text="Submit" cssClass="button"></asp:Button>
           </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Italic="True" Font-Names="Trebuchet MS"></asp:Label>
        <br />
        
               <ajaxToolkit:FilteredTextBoxExtender
           ID="FilteredTextBoxExtender1"
           runat="server"
           TargetControlID="txtunits"
           FilterType="Numbers" />
    </form>
</body>
</html>
