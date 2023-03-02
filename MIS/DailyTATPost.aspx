<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyTATPost.aspx.vb" Inherits="MIS_DailyMins" %>

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
      <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles1.css"/>
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
    <link href="../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
    <title>Daily TAT By Post date</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Daily TAT By Post Date</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%" HorizontalAlign="Left">
           <table width="100%">
             <tr>
                <td colspan="2" class="HeaderDiv">
                    Daily TAT Report - Post Date
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)" />
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%" HorizontalAlign="Left">
           <table width="100%">
            <tr>
                 <td width="35%" class="alt1">
                    Start Post Date
                 </td>
                <td width="35%" class="alt1">
                    End Post Date
                </td>
                <td width="30%" class="alt1">
                    Account
                    
                </td>
                <td  class="tblbg2" style="text-align:center" >
                    Instance
                </td>
           </tr>
           <tr>
                <td style="text-align:center" class="alt1">
                    <asp:TextBox ID="TXTSDate" Width="100" runat="server" ></asp:TextBox>
                    <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  CssClass="cal_Theme1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTSDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                    </ajaxToolkit:CalendarExtender>    
                </td>
                <td style="text-align:center" class="alt1">
                    <asp:TextBox ID="TXTEDate" Width="100" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  CssClass="cal_Theme1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTEDate" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender2" Enabled="True">
                    </ajaxToolkit:CalendarExtender> 
                </td>
                <td style="text-align:center" class="alt1">
                    <asp:DropDownList ID="DLAct" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>  
                    </asp:DropDownList>
                </td>
                <td style="text-align:center" class="alt1">
                    <asp:DropDownList ID="DLInstance" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="1" Value="1" Selected="True"  ></asp:ListItem> 
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>  
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
                <td colspan="4" class="alt1">
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
            <asp:Table ID="tblMins" runat="server" Width="100%" HorizontalAlign="Center"  CellSpacing="2" CellPadding="10"  GridLines="Both" >
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell runat="server" ID="tblDtls" CssClass="HeaderDiv" ColumnSpan="19" Width="100%"  >Account Details</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center">
                    <asp:TableCell runat="server" ID="R1Cell1" CssClass="alt1" RowSpan="2"  >Account Name</asp:TableCell>
                    <asp:TableCell runat="server" ID="R1Cell2" CssClass="alt1" ColumnSpan="8" >Number Of Jobs</asp:TableCell>
                    <asp:TableCell runat="server" ID="R1Cell3" CssClass="alt1" ColumnSpan="5">Total Mins</asp:TableCell>
                    <asp:TableCell runat="server" ID="TableCell1" CssClass="alt1" ColumnSpan="5">Jobs Related</asp:TableCell> 
                </asp:TableRow>
                <asp:TableRow ID="TableRow4" runat="server" HorizontalAlign="Center" Width="100%" >
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell1">Total Jobs</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell2">TAT<=24</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell3">TAT<=48</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell4">TAT<=72</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell5">TAT>72</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell6">Average TAT</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell7">Min TAT</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell8">Max TAT</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell9">Total Mins</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="R2Cell10">Mins<=24</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell2">Mins<=48</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell3">Mins<=72</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell8">Mins>72</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell4">TAT% =24</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell5">TAT% <=48</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell6">TAT% <=72</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell7">TAT% >72</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" ID="TableCell9">Median TAT</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table3" runat="server">
                <asp:TableRow ID="TableRow2" runat="server">
                    <asp:TableCell ID="TCell2" runat="server">
                        <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                </asp:TableRow> 
            </asp:Table>
            <asp:Label ID="Lbl1" runat="server" CssClass="Title" ForeColor="firebrick"    ></asp:Label>  
        </asp:Panel>
        </div> 
        </div> 
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

<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 

    </form>
</body>
</html>
