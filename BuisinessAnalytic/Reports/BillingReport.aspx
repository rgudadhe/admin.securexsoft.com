<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BillingReport.aspx.vb" Inherits="MIS_DailyMins" %>

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
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>

    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
  
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Summary</h1>
           <asp:Panel ID="Panel2" runat="server" width="500">
           <table width="100%"   >
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    Summary Report
                        - Submit Date
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server"  width="500" >
           <table width="100%"  >
            
            <tr class="tblbg2">
                 <td style="text-align: center" width="50%" height="60" class="alt1">
                    
                         Start
                    Submit Date:<br />
                     <asp:TextBox ID="TXTSDate" Width="100" runat="server"  CssClass="common"></asp:TextBox>
                      <asp:ImageButton ID="imgSDate" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTSDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                 </td>
                <td height="60" style="text-align: center" width="50%" class="alt1">
                   
                        End Submit Date: <br />
                        <asp:TextBox ID="TXTEDate" Width="100" runat="server"  CssClass="common"></asp:TextBox>
                           <asp:ImageButton ID="ImageButton1" CausesValidation=False ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTEDate" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender2" Enabled="True">
                                            </ajaxToolkit:CalendarExtender> 
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
        <br />
        <div style="text-align:left  ">
         <asp:Table ID="tblMins" runat="server" HorizontalAlign="Center">
           <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
          <asp:TableCell runat="server" ID="tblDtls" ColumnSpan="2" CssClass="HeaderDiv">Units by period</asp:TableCell>
                </asp:TableRow>
        <asp:TableRow  >
                <asp:TableCell runat="server" ID="R1Cell1"  CssClass="alt1">Account Name</asp:TableCell>
               <%-- <asp:TableCell runat="server" ID="R1Cell2" >Number Of Jobs</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell3" >Mins</asp:TableCell>--%>
                <asp:TableCell runat="server" ID="TableCell1" CssClass="alt1">Amount</asp:TableCell> 
            </asp:TableRow>
                    
       
                    </asp:Table>
                    </div>
        <asp:Label ID="Lbl1" runat="server" Font-Names="8" Font-Bold="true" ForeColor="firebrick"    ></asp:Label>  

     </div> 
        </div> 
    </form>
</body>
</html>
