<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewMonthlyEMPPayrollTrend.aspx.vb" Inherits="ViewUnitsTrend" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
        <h1>Trend Employee</h1>
        
            <asp:Panel ID="Panel2" HorizontalAlign="Left"   runat="server" width="100%">
           <table width="500">
             <tr>
                <td colspan="5" style="text-align: center" class="HeaderDiv" >
                    Search 
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel4" HorizontalAlign="Left" runat="server" width="100%">
           <table width="500">
            <tr >
                 <td style="text-align: center" colspan="2"  class="alt1">
                    From
                 </td>
                <td style="text-align: center" colspan="2" class="alt1">
                    To
                </td>
                <td style="text-align: center" rowspan="2" class="alt1">
                    Trend By
                </td>
               
           </tr>
                <tr >
                 <td style="text-align: center" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" class="alt1">
                    Year
                </td>
                <td style="text-align: center" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" class="alt1">
                    Year
                </td>
               
           </tr>
            <tr >
                 <td style="text-align: center" >
                  
                     <asp:DropDownList ID="DLMonth1" runat="server" CssClass="common">
                                       </asp:DropDownList>
                 </td>
                <td style="text-align: center">
                    
                        <asp:DropDownList ID="DLYear1" runat="server" CssClass="common">
                        
                           </asp:DropDownList>
                </td>
                <td style="text-align: center" >
                  
                     <asp:DropDownList ID="DLMonth2" runat="server" CssClass="common">
                                     </asp:DropDownList>
                 </td>
                <td style="text-align: center">
                    
                        <asp:DropDownList ID="DLYear2" runat="server" CssClass="common">
                          
                           </asp:DropDownList>
                </td>
                <td style="text-align: center">
                    
                        <asp:DropDownList ID="DLTrend" runat="server" CssClass="common">
                           <asp:ListItem Text="Units" Value="Units"></asp:ListItem>  
                           <asp:ListItem Text="Points" Value="Points"></asp:ListItem> 
                           <asp:ListItem Text="Cost" Selected="True"   Value="Cost"></asp:ListItem>  
                           </asp:DropDownList>
                </td>
               
           </tr>
           <tr>
           <td colspan="5" >
           <asp:Panel ID="Panel3" runat="server" width="100%">
                        <table width="100%" cellpadding="1" cellspacing="1"   >
                        <tr>
                        <td  colspan="5" style="text-align: center"  class="HeaderDiv" >
                            <b>Advanced Search</b>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                        </td>
                        </tr>
                        </table> 
                      </asp:Panel> 
                      <asp:Panel ID="Panel5" runat="server" width="100%">
                      <table width="500">
            <tr >
                 <td style="text-align: Left" colspan="2"  class="alt1">
                 <table width="100%" border="1"><tr>
                 <td colspan ="5" style="text-align: Left" ><asp:CheckBox ID="CheckBox1" Text="Compare with"  runat="server" />
                   
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                                       </asp:DropDownList>
                 
                    
                        <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                          
                           </asp:DropDownList>
                </td></tr>
                <tr><td colspan="5" style="text-align: Left" >Show Changes in <asp:CheckBox ID="CheckBox2" Text="Difference"  runat="server" /><asp:CheckBox ID="CheckBox3" Text="Percentage"  runat="server" /></td></tr>
                <tr><td colspan="5" style="text-align: Left" ><asp:CheckBox ID="CheckBox4" Text="Show negative values in parentheses"  runat="server" /></td></tr></table>
                     
                </td>
               
           </tr>
           </table> 
                      </asp:Panel> 
           </td>
           </tr>
           <tr>
                <td style="text-align: center;" colspan="5" >
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
        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
        TargetControlID="Panel5"
        ExpandControlID="Panel3"
        CollapseControlID="Panel3" 
        Collapsed="True"
        ImageControlID="ImageButton2"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="~/App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="True" 
        EnableViewState="false" 
      /> </div>
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
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
            
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