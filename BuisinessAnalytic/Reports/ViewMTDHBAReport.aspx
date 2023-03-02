<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewMTDHBAReport.aspx.vb" Inherits="ViewUnitsTrend" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles1.css"/>
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
<title>Billing Report</title>

</head>
<body>
    <form id="BillDet" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Month to Date Contractor</h1>
        
            <asp:Panel ID="Panel2" runat="server" width="100%" HorizontalAlign="Left">
           <table width="500" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td colspan="2" style="text-align: center"  class="HeaderDiv">
                    Search  
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel3" runat="server" width="100%" HorizontalAlign="Left">
           <table width="500" >
            
            <tr class="tblbg2">
                 <td style="text-align: center"  class="common">
                    
                    Post Date:
                     <asp:TextBox ID="TxtDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
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
        TargetControlID="Panel3"
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
         </div>
        <br />
       <asp:Table ID="Table1" runat="server" HorizontalAlign="Left"   >
       <asp:TableRow>
        <asp:TableCell ID="TCell2" runat="server">
                    <asp:Button ID="Button1" runat="server" Text="Export Result" Font-Size="8"  Font-Names="Arial" CssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
            <br />
            <br />
            <br />
            
     
           <asp:Panel ID="Panel31" runat="server" width="100%" HorizontalAlign="Left">  
          <asp:CompleteGridView ID="MyDataGrid" runat="server" AutoGenerateColumns="False" 
                     cellspacing="2" CellPadding="2" 
                     DataKeyNames="UserID" 
                     EnableViewState="False"  
                      CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom"   ShowCount="False"  >
                <AlternatingRowStyle cssclass="gridalt2"  ></AlternatingRowStyle>
                <RowStyle cssclass="gridalt1"  ></RowStyle>
                </asp:CompleteGridView>
       </asp:Panel> 
       </div>
      
    </form>
</body>
</html>
