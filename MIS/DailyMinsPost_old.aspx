<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyMinsPost_old.aspx.vb" Inherits="MIS_DailyMins" %>

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
<title>Daily Minutes By Post Date</title>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>DMR By Post Date</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%" HorizontalAlign="Left">
           <table width="100%">
             <tr>
                <td colspan="2" class="HeaderDiv">
                    Daily Minutes Report - Post Date &nbsp;<asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          
          <asp:Panel ID="Panel1" runat="server" width="100%" HorizontalAlign="Left">
           <table width="100%">
            
            <tr>
                 <td width="50%" class="alt1">
                         Post Date
                        
                 </td>
                <td width="50%" class="alt1">
                    Account
                </td>
           </tr>
           <tr>
            <td style="text-align:center">
                <asp:TextBox ID="TxtDate" Width="100" runat="server"></asp:TextBox>
                <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TxtDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td  style="text-align:center">
                <asp:DropDownList ID="DLAct" runat="server">
                    <asp:ListItem Text="All" Value=""></asp:ListItem>  
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
                <td class="alt1" colspan="2" >
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
        ExpandedImage="../App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="../App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        
        <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">
            <asp:Table ID="tblMins" runat="server" Width="100%">
       <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell runat="server" ID="tblDtls" ColumnSpan="10" Width="100%" CssClass="HeaderDiv" >Account Details</asp:TableCell>
                </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell runat="server" ID="R1Cell1" CssClass="alt1">Account Name</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell2" CssClass="alt1">Avg Mins/Day</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell3" CssClass="alt1">Avg Mins/Day</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell4" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell5" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell6" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell7" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell8" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell9" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell10" CssClass="alt1"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server" HorizontalAlign="Center" CssClass="tblbg1">
                <asp:TableCell runat="server" ID="R2Cell1" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell2" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell3" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell4" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell5" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell6" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell7" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell8" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell9" CssClass="alt1"></asp:TableCell>
                <asp:TableCell runat="server" ID="R2Cell10" CssClass="alt1"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table3" runat="server">
                    <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell ID="TCell1" runat="server">
                    <asp:RadioButtonList ID="RBPage" runat="server" RepeatColumns="2" Width="200px" Visible="False">
                                <asp:ListItem Value="CP">Current Page</asp:ListItem>
                                <asp:ListItem Value="AP">All Pages</asp:ListItem>
                    </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell ID="TCell2" runat="server">
                   <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
        </asp:Panel>
        </div>
        </div>
    </form>
</body>
</html>
