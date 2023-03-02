<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyTranscRep.aspx.vb" Inherits="MIS_DailyMins" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
    <title>Productivity Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Producitivity Report</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%" HorizontalAlign="Left">
                <table width="100%">
                    <tr>
                        <td colspan="2" class="HeaderDiv">
                            Productivity Report
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                        </td>
                    </tr>
                </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%" HorizontalAlign="Left">
           <table width="100%">
            <tr>
                <td width="35%" class="alt1" style="text-align:center">
                    Start Post Date
                </td>
                <td width="35%" class="alt1" style="text-align:center">
                    End Post Date
                </td>
                <td width="30%" class="alt1" style="text-align:center">
                    Account
                </td>
           </tr>
           <tr>
                <td style="text-align:center">
                    <asp:TextBox ID="TXTSDate" Width="100" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTSDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                    </ajaxToolkit:CalendarExtender>    
                </td>
                <td>
                    <asp:TextBox ID="TXTEDate" Width="100" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTEDate" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender2" Enabled="True">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:DropDownList ID="DLAct" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>  
                    </asp:DropDownList>
                </td>
           </tr>
               <tr>
                    <td  style="text-align: center">
                        &nbsp;
                    </td>
                    <td  style="text-align: center" class="alt1">
                       Result Display Setting
                    </td>
                    <td style="text-align: center">
                        &nbsp;
                    </td>
               </tr>
               <tr>
               <td  style="text-align: center" >
                    &nbsp;
               </td>
               <td  style="text-align: center;" >
                       <asp:RadioButtonList ID="RB" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"  >
                  <asp:ListItem Text = "Display result on screen"  Value ="D"></asp:ListItem> 
                  <asp:ListItem Text = "Export result as excel" Selected="True" Value ="E"></asp:ListItem> 
                       </asp:RadioButtonList> </td>
                   <td style="text-align: center" width="50%">
                        &nbsp;
                   </td>
               </tr>
               
          <tr>
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
        ExpandedImage="../App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="../App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        
        
        
        
          <asp:CompleteGridView ID="MyDataGrid" runat="server" AutoGenerateColumns="True" 
                    AllowPaging="False" AllowSorting="True" 
                    EnableViewState="False"  
                    Width="100%"  CaptionAlign="Bottom"  DataMember="DefaultView" ShowCount="False"  >

                <FooterStyle CssClass="alt"></FooterStyle>
                <HeaderStyle CssClass="alt1"></HeaderStyle>
                <EditRowStyle BackColor="#999999"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove"  BorderColor="#E0E0E0" HorizontalAlign="Center" CssClass="alt"></PagerStyle>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" VerticalAlign="Middle"></AlternatingRowStyle>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                </asp:CompleteGridView>
        
            
      <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TXTSDate"
            ErrorMessage="Please enter start Date" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TXTEDate"
            ErrorMessage="Please enter end date" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        </div> 
        </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
