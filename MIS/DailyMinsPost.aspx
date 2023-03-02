<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyMinsPost.aspx.vb" Inherits="MIS_DailyMins" %>

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
    <title>Daily Minutes</title>
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
                        <td colspan="3" class="HeaderDiv">
                            Daily Minutes Report - Post Date
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                        </td>
                    </tr>
                </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="100%">
            <tr>
                 <td  class="alt1" style="text-align:center"   >
                    Post Date
                    
                 </td>
                <td  class="alt1" style="text-align:center" >
                    Account
                </td>
                <td  class="alt1" style="text-align:center" >
                    Instance
                </td>
           </tr>
           <tr>
            <td style="text-align:center" class="alt1">
                <asp:TextBox ID="TxtDate" Width="100" runat="server"></asp:TextBox>
                <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" Format="MM/dd/yyyy" TargetControlID="TxtDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
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
                <td style="text-align: center;" class="alt1" colspan="3" >
                    <asp:CheckBox ID="CHZero" runat="server" Text="Show zero minute record" Checked="True" CssClass="common" />
                </td>
            </tr>
           <tr>
                <td style="text-align: center;" class="alt1" colspan="3" >
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
            <asp:Table ID="tblMins" runat="server" Width="100%" CellSpacing="2" CellPadding="10"  GridLines="Both"     >
                <asp:TableRow ID="TableRow1" runat="server" >
                    <asp:TableCell runat="server" ID="tblDtls" CssClass="HeaderDiv" ColumnSpan="10" Width="100%">Account Details</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server">
                    <asp:TableCell runat="server" ID="R1Cell1" CssClass="alt1" >Account Name</asp:TableCell>
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
                <asp:TableRow ID="TableRow4" runat="server">
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
                    <asp:RadioButtonList ID="RBPage" runat="server" Visible="False" RepeatColumns="2" Width="200px">
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
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  Display="None"
 ControlToValidate="txtDate"  
runat="server" 
ErrorMessage="Post Date - Please enter valid input.">
</asp:RequiredFieldValidator>
    
<asp:CompareValidator 
                      ControlToValidate="txtDate"
                      Display="None"
                      ErrorMessage="Post Date  - Please enter valid input."
                      ID="CompareValidator2"
                      Operator="DataTypeCheck"
                      Type="Date"
                      runat="server" /> 
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
    </form>
</body>
</html>
