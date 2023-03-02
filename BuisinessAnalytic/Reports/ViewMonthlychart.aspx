<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewMonthlychart.aspx.vb" Inherits="ViewUnitsTrend" %>

<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>

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
        <h1>Monthly Chart</h1>
        
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left"  width="100%">
           <table width="500">
             <tr>
                <td colspan="6" style="text-align: center" class="HeaderDiv" >
                    Search 
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Left" width="100%">
           <table width="500">
            <tr >
                 <td style="text-align: center" colspan="2"   class="alt1">
                    From
                 </td>
                <td style="text-align: center" colspan="2"  class="alt1">
                    To
                </td>
                <td style="text-align: center" rowspan="2"  class="alt1">
                    Value in
                </td>
                <td style="text-align: center" rowspan="2"  class="alt1">
                    Chart Type
                </td>
               
           </tr>
                <tr >
                 <td style="text-align: center"  class="alt1">
                    Month
                 </td>
                <td style="text-align: center"  class="alt1">
                    Year
                </td>
                <td style="text-align: center"  class="alt1">
                    Month
                 </td>
                <td style="text-align: center"  class="alt1">
                    Year
                </td>
               
           </tr>
            <tr >
                 <td style="text-align: center"  >
                  
                     <asp:DropDownList ID="DLMonth1" runat="server" CssClass="common">
                                      </asp:DropDownList>
                 </td>
                <td style="text-align: center" >
                    
                        <asp:DropDownList ID="DLYear1" runat="server" CssClass="common" >
                           
                           </asp:DropDownList>
                </td>
                <td style="text-align: center"  >
                  
                     <asp:DropDownList ID="DLMonth2" runat="server" CssClass="common">
                                     </asp:DropDownList>
                 </td>
                <td style="text-align: center" >
                    
                        <asp:DropDownList ID="DLYear2" runat="server" CssClass="common">
                          
                           </asp:DropDownList>
                </td>
                <td style="text-align: center" >
                    
                        <asp:DropDownList ID="DLTrend" runat="server" CssClass="common" >
                           <asp:ListItem Text="Units" Value="Units"></asp:ListItem>  
                           <asp:ListItem Text="Minutes" Value="Minutes"></asp:ListItem> 
                           <asp:ListItem Text="Revenue" Selected="True"   Value="Revenue"></asp:ListItem>  
                           </asp:DropDownList>
                </td>
                
                 <td style="text-align: center" >
                    
                        <asp:DropDownList ID="DLType" runat="server" CssClass="common">
                           <asp:ListItem Text="SmoothLine" Value="SmoothLine"></asp:ListItem>  
                           <asp:ListItem Text="Columnchart" Value="Columnchart"></asp:ListItem> 
                             
                           </asp:DropDownList>
                </td>
               
           </tr>
           
           <tr>
                <td style="text-align: center;" colspan="6" >
                    <asp:Button ID="btnsubmit" cssClass="button" runat="server" Text="Submit" />&nbsp;
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
        ExpandedImage="~/App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="~/App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
       </div>
       
            <br />
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
            &nbsp;<Web:ChartControl ID="ChartControl1" Width="900" runat="server" BorderStyle="Outset" BorderWidth="5px">
            </Web:ChartControl></asp:Panel>
        </div> 
        
    </form>
</body>
</html>
