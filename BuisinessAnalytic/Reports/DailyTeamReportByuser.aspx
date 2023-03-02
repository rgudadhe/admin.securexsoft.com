<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyTeamReportByuser.aspx.vb" Inherits="DailyTeamReport" %>

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
 <title>Daily Team Report</title>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Daily Team Report</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td colspan="2" style="text-align: center"  class="HeaderDiv">
                    Daily Team Report   
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="100%" >
            
            <tr class="tblbg2">
                 <td style="text-align: center"  class="common">
                    
                    Post Date:
                     <asp:TextBox ID="TxtDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                 </td>
                
                   <td style="text-align: center"   class="common">
                    
                        Group By<asp:DropDownList ID="DLGroup" runat="server">
                           <asp:ListItem Text="Mode" Value="Desn"></asp:ListItem>  
                             <asp:ListItem Text="Platform" Value="Platform"></asp:ListItem>  
                        </asp:DropDownList>
                </td>
           </tr>
           <tr class="tblbg2">
                <td style="text-align: center;" colspan="2" >
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
        
        <br />
        <asp:Table ID="tblMins" runat="server" Width="1500"   >
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center"         >
                <asp:TableCell runat="server" ID="tblDtls"  CssClass="HeaderDiv" ColumnSpan="25"    >SecureXFlow</asp:TableCell>
                </asp:TableRow>
               <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center"        >
                <asp:TableCell runat="server" ID="TableCell9"  CssClass="alt1" ColumnSpan="25"     >Daily Team Report</asp:TableCell>
                </asp:TableRow>
               <asp:TableRow ID="TableRow4" runat="server" HorizontalAlign="Center"      Font-Size="Smaller"     >
                <asp:TableCell runat="server" ID="TableCell10"  CssClass="HeaderDiv" ColumnSpan="25"    >
                    <asp:Label ID="Lbldate" runat="server"  ></asp:Label></asp:TableCell>
                </asp:TableRow>  
            <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center"    >
                <asp:TableCell ColumnSpan="3"  runat="server" ID="R1Cell1" HorizontalAlign="Center" CssClass="HeaderDiv"  >User Details</asp:TableCell>
                <asp:TableCell ColumnSpan="11" runat="server" ID="R1Cell2" HorizontalAlign="Center" CssClass="HeaderDiv" >Daily Lines</asp:TableCell>
                <asp:TableCell ColumnSpan="11"  runat="server" ID="R1Cell3" HorizontalAlign="Center" CssClass="HeaderDiv" >Month To Date Lines</asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" HorizontalAlign="Center"  CssClass="HeaderDiv"   >
                <asp:TableCell runat="server" ID="R2Cell1"  CssClass="alt1" >Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell2"  CssClass="alt1" >UserID</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell3"  CssClass="alt1" >Level</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell4"  CssClass="alt1" >MT</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell5"  CssClass="alt1" >MT+</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell6" CssClass="alt1" >QA</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell7"  CssClass="alt1" >MT B</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell8" CssClass="alt1" >QA B</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell9" CssClass="alt1" >QABSE</asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell10" CssClass="alt1" >PPQA</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell1" CssClass="alt1" >Daily Points</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell2" CssClass="alt1" >Daily Target</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell3" CssClass="alt1" >% Tgt</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell4" CssClass="alt1" >Leave Status</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell11" CssClass="alt1" >MT</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell12" CssClass="alt1" >MT+</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell13" CssClass="alt1" >QA</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell14" CssClass="alt1" >MT B</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell15" CssClass="alt1" >QA B</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell16" CssClass="alt1" >QABSE</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell17" CssClass="alt1" >PPQA</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell5" CssClass="alt1" >Total Points</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell6" CssClass="alt1" >ADJ Target</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell7" CssClass="alt1" >% Tgt</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell8" CssClass="alt1" >LWP</asp:TableCell> 
               
            </asp:TableRow>
        </asp:Table>
        
       <asp:Table ID="Table3" runat="server" style="text-align: left" cellpadding="2" cellspacing="2" GridLines="Both">
                    <asp:TableRow ID="TableRow6" runat="server">
                 
                    <asp:TableCell ID="TCell2" runat="server">
                   <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
      
</div> 
        </div> 
   
    </form>
</body>
</html>
