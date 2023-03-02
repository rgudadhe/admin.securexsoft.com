<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyTransReport.aspx.vb" Inherits="MIS_DailyMins" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

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
           <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    <span style="font-size: 10pt; font-family: Trebuchet MS"><strong><em>Daily Productivity
                        Report
                        - Submit Date</em></strong></span>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2">
            
            <tr class="tblbg2">
                 <td style="text-align: center" width="50%" height="60">
                     <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em>
                         Start
                    Submit Date:</em></strong></span><br />
                     <asp:TextBox ID="TXTSDate" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox>
                 </td>
                <td height="60" style="text-align: center" width="50%">
                    <strong><em><span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS">
                        End Submit Date:<br />
                        <asp:TextBox ID="TXTEDate" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                            ForeColor="Gray"></asp:TextBox></span></em></strong></td>
                <td style="text-align: center" width="50%">
                    <strong><em><span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS">
                        Account:</span></em></strong><br /><asp:DropDownList ID="DLAct" runat="server"
                            Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                           <asp:ListItem Text="All" Value=""></asp:ListItem>  
                        </asp:DropDownList>
                </td>
                
           </tr>
           <tr class="tblbg2">
                <td style="text-align: center;" colspan="3" >
                    <asp:Button ID="tblsubmit" cssClass="button" runat="server" Text="Submit" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
     
     <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        
        
        <asp:Table ID="tblMins" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="DimGray"  Width="100%" BorderColor="Silver" GridLines="both"  CellPadding="2" CellSpacing="2" HorizontalAlign="Center" CssClass="tblbg" >
    
         
            <asp:TableRow runat="server" HorizontalAlign="Center" CssClass="tblbg1"  Width="100%" >
                <asp:TableCell runat="server" ID="R2Cell1">Jobnumber</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell2">Physician Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell3">Submit Date</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell4">Post Date</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell5">Account Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell6">Prioirty</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell7">TAT</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell8">Duration</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell9">
                    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
       <asp:Table ID="Table3" runat="server" style="text-align: left" cellpadding="2" cellspacing="2" GridLines="Both">
                    <asp:TableRow ID="TableRow2" runat="server">
                    
                    <asp:TableCell ID="TCell2" runat="server">
                    <asp:Button ID="Button1" runat="server" Text="Export Result" Font-Bold="True" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
      

   
    </form>
</body>
</html>
