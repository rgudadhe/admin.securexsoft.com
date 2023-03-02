<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyTranscRep1.aspx.vb" Inherits="MIS_DailyTranscRep1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles1.css"/>
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
    <link href="../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
<style type="text/css" >
tr.tblbg {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg1 {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.alt1 {
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
	font: 13px 'Arial', Tahoma, arial, sans-serif;
}
a:hover {
	color: #383d44;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 13px 'Arial', Tahoma, arial, sans-serif;
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
                    <span style="font-size: 8pt; font-family: Arial"><strong>Daily Transcription
                        Report</strong></span>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2" style="font-family: 'Arial'; ">
            
            <tr  >
                 <td style="text-align: center" width="50%" class="alt1">
                     <strong><span style="font-size: 8pt; font-family: Arial">
                         Start Post Date:</span> </strong><br />
                 
                     <asp:TextBox ID="TXTSDate" Width="100" runat="server" Font-Names="Arial" Font-Size=8 ForeColor="Gray"></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTSDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                 </td>
                <td  style="text-align: center" width="50%" class="alt1">
                     <strong><span style="font-size: 8pt; font-family: Arial">
                        End Post Date:</span> </strong><br />
                  
                        <asp:TextBox ID="TXTEDate" Width="100" runat="server" Font-Names="Arial" Font-Size=8
                            ForeColor="Gray"></asp:TextBox>
                                     <asp:ImageButton ID="ImageButton1" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTEDate" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender2" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            </td>
                <td style="text-align: center" width="50%" class="alt1">
                    <strong><span style="font-size: 8pt; font-family: Arial">
                        Account:</span></strong><br /><asp:DropDownList ID="DLAct" runat="server"
                            Font-Names="Arial" Font-Size=8 ForeColor="Gray">
                           <asp:ListItem Text="All" Value=""></asp:ListItem>  
                        </asp:DropDownList>
                </td>
                
           </tr>
               <tr  >
               <td  style="text-align: center" class="alt1">
                      </td>
                   <td  style="text-align: center" class="alt1">
                       <span ><strong>Result Display Setting</strong></span></td>
                   <td style="text-align: center" width="50%" class="alt1">
                   </td>
               </tr>
               <tr  >
               <td  style="text-align: center" class="alt1">
                      </td>
                   <td  style="text-align: left" class="alt1">
                       <asp:RadioButtonList ID="RB" runat="server" Font-Names="Arial" Font-Size=8  >
                  <asp:ListItem Text = "Display result on screen"  Value ="D"></asp:ListItem> 
                  <asp:ListItem Text = "Export result as excel" Selected="True" Value ="E"></asp:ListItem> 
                       </asp:RadioButtonList> </td>
                   <td style="text-align: center" width="50%" class="alt1">
                   </td>
               </tr>
               
          <tr  >
                <td style="text-align: center;" colspan="3" class="alt1">
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
        
        
        <asp:Table ID="tblMins" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size=8 ForeColor="DimGray"  Width="100%" BorderColor="Silver" GridLines="both"  CellPadding="2" CellSpacing="2" HorizontalAlign="Center" CssClass="tblbg" >
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" CssClass="tblbg"  >
                <asp:TableCell runat="server" ID="tblDtls" CssClass="tblbg" ColumnSpan="31" Width="100%" Font-Size=8     >Account Details</asp:TableCell>
                </asp:TableRow>
            
            <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center" CssClass="tblbg1"  Width="100%" >
                <asp:TableCell runat="server" ID="R2Cell1">Job #</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell2">CustJob #</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell3">Account Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell4">Physician Name</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell22">Created Date</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell5">Submit Date</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell6">Post Date</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell7">Duration</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell8">Priority</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell9">MTID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell1">MTLINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell2">MTDATE</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell3">MTPLUSID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell4">MTPLUSLINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell5">MTPLUSDATE</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell6">QAID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell7">QALINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell8">QADATE</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell9">QABSEID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell10">QABSELINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell11">QABSEDATE</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell12">PPQAID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell13">PPQALINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell14">PPQADATE</asp:TableCell> 
                 <asp:TableCell runat="server" ID="TableCell15">MTBID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell16">MTBLINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell17">MTBDATE</asp:TableCell>
                 <asp:TableCell runat="server" ID="TableCell18">QABID</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell19">QABLINES</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell20">QABDATE</asp:TableCell>  
                <asp:TableCell runat="server" ID="TableCell21">Device</asp:TableCell>  
               
                <asp:TableCell runat="server" ID="TableCell23">Templates</asp:TableCell>  
                 <asp:TableCell runat="server" ID="TableCell24">BillingLC</asp:TableCell>  
                  <asp:TableCell runat="server" ID="TableCell25">VRS Job</asp:TableCell>  
                   <asp:TableCell runat="server" ID="TableCell26">Categoty</asp:TableCell>     
            </asp:TableRow>
        </asp:Table>
        
          <asp:CompleteGridView ID="MyDataGrid" runat="server" AutoGenerateColumns="True" 
                    AllowPaging="False" CellPadding="2" AllowSorting="True" 
                    Font-Names="Arial"  
                    Font-Size="8" EnableViewState="False"  
                     Width="100%"  Font-Italic="False" CaptionAlign="Bottom"  DataMember="DefaultView" ShowCount="False"  >

                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" CssClass="TransStatus"></FooterStyle>
                <HeaderStyle BackColor="#5D7B9D"  Font-Bold="True" ForeColor="black"   Font-Names="Arial" Font-Size="8" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TransStatus"></HeaderStyle>
                <EditRowStyle BackColor="#999999"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove"  BorderColor="#E0E0E0" HorizontalAlign="Center" CssClass="TransStatus" ForeColor="black"></PagerStyle>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" VerticalAlign="Middle"></AlternatingRowStyle>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                </asp:CompleteGridView>
        
             <asp:Table ID="Table3" runat="server" style="text-align: left" cellpadding="2" cellspacing="2" GridLines="Both">
                    <asp:TableRow ID="TableRow3" runat="server">
                    
                    <asp:TableCell ID="TCell2" runat="server">
                    <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TXTSDate"
            ErrorMessage="Please enter start Date" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
            Font-Size=8 SetFocusOnError="True"></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TXTEDate"
            ErrorMessage="Please enter end date" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
            Font-Size=8 SetFocusOnError="True"></asp:RequiredFieldValidator><br />
<asp:CompareValidator 
                      ControlToValidate="txtSDate"
                      Display="None"
                      ErrorMessage="Start date - Please enter valid input."
                      ID="CompareValidator3"
                      Operator="DataTypeCheck"
                      Type="Date"
                      runat="server" />
<asp:CompareValidator 
                      ControlToValidate="txtEDate"
                      Display="None"
                      ErrorMessage="End date - Please enter valid input."
                      ID="CompareValidator2"
                      Operator="DataTypeCheck"
                      Type="Date"
                      runat="server" /> 
<asp:CompareValidator ControlToCompare="txtSDate"
                      ControlToValidate="txtEDate"
                      Display="None"
                      ErrorMessage="Start date can not be greater than End date."
                      ID="CompareValidator1"
                      Operator="GreaterThanEqual"
                      Type="Date"
                      runat="server" /> 

<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /><br />

   
    </form>
</body>
</html>

