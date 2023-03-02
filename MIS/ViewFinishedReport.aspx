<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewFinishedReport.aspx.vb" Inherits="MIS_DailyMins" %>

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
                    <span style="font-size: 10pt; font-family: Trebuchet MS"><strong><em>View Finished Report</em></strong></span>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2" style="font-family: 'Trebuchet MS'; ">
            
            <tr  class="tblbg2">
                             <td style="text-align: center"  height="60">
                     <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em>
                         Level</em></strong></span><br />
                                                             <asp:DropDownList ID="DLStatus" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Names="Trebuchet MS" Font-Size="Small">
                                            <asp:ListItem Text="Select Level" Value=""></asp:ListItem>  
                                            
                                        </asp:DropDownList>
                 </td>
                                  <td style="text-align: center"  height="60">
                     <span style="font-size: 10pt; color: #ff9933; font-family: Trebuchet MS"><strong><em>
                         Username</em></strong></span><br />
                      <asp:DropDownList ID="DLUsers" runat="server"  Font-Bold="True"
                                            Font-Names="Trebuchet MS" Font-Size="Small">
                                            <asp:ListItem Text="Select User" Value=""></asp:ListItem>  
                                        </asp:DropDownList>
                 </td>

                 <td style="text-align: center;width:auto;" >
                     <strong><em><span >
                     <span style="font-size: 10pt; font-family: Trebuchet MS">
                         Start Post Date:<br />
                     </span>
                     </span></em></strong>
                     <asp:TextBox ID="TXTSDate" Width="70"  runat="server" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:TextBox>
                      <asp:ImageButton ID="ImageButton1" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="TxtSDate" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender2" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                 </td>
                <td  style="text-align: center" >
                    <strong><em><span style="font-size: 10pt;  font-family: Trebuchet MS">
                        <span >
                        End Post Date:<br />
                        </span></span></em></strong>
                        <asp:TextBox ID="TXTEDate" Width="70"  runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                            ForeColor="Gray"></asp:TextBox>
                             <asp:ImageButton ID="imgEDate" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TxtEDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender1" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                             </td>
                <td style="text-align: center" >
                    <strong><em><span style="font-size: 10pt; font-family: Trebuchet MS">
                        Account:</span></em></strong><br /><asp:DropDownList ID="DLAct" runat="server"
                            Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                           <asp:ListItem Text="All" Value=""></asp:ListItem>  
                        </asp:DropDownList>
                </td>
                
           </tr>
               <tr  class="tblbg2">
                              <td  style="text-align: center" >
                      </td>

                   <td  style="text-align: center" colspan="2" >
                       <span ><strong>Result Display Setting</strong></span></td>
                   <td style="text-align: center">
                   </td>
                                 <td  style="text-align: center" >
                      </td>
 
               </tr>
               <tr  class="tblbg2">
                              <td  style="text-align: center" >
                      </td>

                   <td  style="text-align: left" colspan="2" >
                       <asp:RadioButtonList ID="RB" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"  >
                  <asp:ListItem Text = "Display result on screen" Selected="True"  Value ="D"></asp:ListItem> 
                  <asp:ListItem Text = "Export result as excel" Value ="E"></asp:ListItem> 
                       </asp:RadioButtonList> </td>
                   <td style="text-align: center">
                   </td>
                                 <td  style="text-align: center" >
                      </td>
 
               </tr>
               
          <tr  class="tblbg2">
                <td style="text-align: center;" colspan="5" >
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
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" CssClass="tblbg"  >
                <asp:TableCell runat="server" ID="tblDtls" CssClass="tblbg" ColumnSpan="31" Width="100%" Font-Size="Small"     >Account Details</asp:TableCell>
                </asp:TableRow>
            
            <asp:TableRow runat="server" HorizontalAlign="Center" CssClass="tblbg1"  Width="100%" >
                <asp:TableCell runat="server" ID="R2Cell1">Job #</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell2">CustJob #</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell3">Account Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell4">Physician Name</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell3">Status</asp:TableCell>       
                <asp:TableCell runat="server" ID="R2Cell5">Submit Date</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell6">Post Date(IST)</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell7">Duration</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell8">Priority</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell9">MTID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell1">MTLINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell2">MTDATE(IST)</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell6">QAID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell7">QALINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell8">QADATE(IST)</asp:TableCell>
                 <asp:TableCell runat="server" ID="TableCell18">BBID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell19">BBLINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell20">BBDATE(IST)</asp:TableCell>  
                <asp:TableCell runat="server" ID="TableCell21">Device</asp:TableCell>  
                <asp:TableCell runat="server" ID="TableCell23">Templates</asp:TableCell>     
               
            </asp:TableRow>
        </asp:Table>
        
       <asp:Table ID="Table3" runat="server" style="text-align: left" cellpadding="2" cellspacing="2" GridLines="Both">
                    <asp:TableRow ID="TableRow2" runat="server">
                    
                    <asp:TableCell ID="TCell2" runat="server">
                    <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TXTSDate"
            ErrorMessage="Please enter start Date" Font-Bold="True" Font-Italic="True" Font-Names="Trebuchet MS"
            Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
           

   
    </form>
</body>
</html>
