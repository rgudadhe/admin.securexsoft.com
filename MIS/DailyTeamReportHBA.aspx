<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyTeamReportHBA.aspx.vb" Inherits="MIS_DailyMins" %>

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
                <td colspan="3" style="text-align: center" class="HeaderDiv">
                    <span style="font-size: 10pt; font-family: Trebuchet MS"><strong><em>Daily Team Report    </em></strong></span>
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
                    Post Date:</em></strong></span><br />
                     <asp:TextBox ID="TxtDate" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                 </td>
                <td style="text-align: center" width="50%">
                    <strong><em><span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS">
                        Users:</span></em></strong><br /><asp:DropDownList ID="DLUsers" runat="server"
                            Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                           <asp:ListItem Text="All" Value=""></asp:ListItem>  
                        </asp:DropDownList>
                </td>
                   <td style="text-align: center" width="50%">
                    <strong><em><span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS">
                        Group By</span></em></strong><br /><asp:DropDownList ID="DLGroup" runat="server"
                            Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                           <asp:ListItem Text="Designation" Value="Desn"></asp:ListItem>  
                             <asp:ListItem Text="Platform" Value="Platform"></asp:ListItem>  
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
        
        
        <asp:Table ID="tblMins" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="Smaller"  ForeColor="black"   Width="100%" BorderColor="DimGray" BackColor="AliceBlue" gridlines="Both"    HorizontalAlign="Center" >
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" BackColor="CornflowerBlue" ForeColor="WhiteSmoke"  Font-Size="X-Small"        >
                <asp:TableCell runat="server" ID="tblDtls" CssClass="tblbg" ColumnSpan="18" Font-Size="Small"    >e-Dictate IT Solutions Pvt. Ltd.</asp:TableCell>
                </asp:TableRow>
               <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center" BackColor="CornflowerBlue" ForeColor="WhiteSmoke"       >
                <asp:TableCell runat="server" ID="TableCell9" CssClass="tblbg" ColumnSpan="18" Font-Size="Medium"    >Daily Team Report</asp:TableCell>
                </asp:TableRow>
               <asp:TableRow ID="TableRow4" runat="server" HorizontalAlign="Center" BackColor="CornflowerBlue" ForeColor="WhiteSmoke"     Font-Size="Smaller"     >
                <asp:TableCell runat="server" ID="TableCell10" CssClass="tblbg" ColumnSpan="18" Font-Size="Small"    >
                    <asp:Label ID="Lbldate" runat="server" Text="Label" ForeColor="WhiteSmoke"></asp:Label></asp:TableCell>
                </asp:TableRow>  
            <asp:TableRow runat="server" HorizontalAlign="Center"   BackColor="LightGray"  ForeColor="Black" >
                <asp:TableCell ColumnSpan="3"  runat="server" ID="R1Cell1" CssClass="tblbg" >User Details</asp:TableCell>
                <asp:TableCell ColumnSpan="11" runat="server" ID="R1Cell2">Daily Lines</asp:TableCell>
                <asp:TableCell ColumnSpan="4"  runat="server" ID="R1Cell3">Month To Date Lines</asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow runat="server" HorizontalAlign="Center"  BackColor="LightGray" ForeColor="Black"   >
                <asp:TableCell runat="server" ID="R2Cell1">Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell2">UserID</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell3">Level</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell4">MT</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell5">MT+</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell6">QA</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell7">MT B</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell8">QA B</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell9">QABSE</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell10">PPQA</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell1">Total Lines</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell2">Daily Target</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell3">% Tgt</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell4">Leave Status</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell5">Total Points</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell6">ADJ Target</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell7">% Tgt</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell8">LWP</asp:TableCell> 
               
            </asp:TableRow>
        </asp:Table>
        
       <asp:Table ID="Table3" runat="server" style="text-align: left" cellpadding="2" cellspacing="2" GridLines="Both">
                    <asp:TableRow ID="TableRow2" runat="server">
                 
                    <asp:TableCell ID="TCell2" runat="server">
                   <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
      

   
    </form>
</body>
</html>
