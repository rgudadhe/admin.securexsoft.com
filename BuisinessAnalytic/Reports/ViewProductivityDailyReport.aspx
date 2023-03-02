<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewProductivityDailyReport.aspx.vb" Inherits="ViewUnitsTrend" %>

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
        <h1>Summary </h1>
        
            <asp:Panel ID="Panel2" HorizontalAlign="LEft"  runat="server" width="100%">
           <table width="500">
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv" >
                    Search 
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel4" HorizontalAlign="LEft" runat="server" width="100%">
           <table width="500">
            
                <tr >
                <td style="text-align: center" width="30%" class="alt1">
                    User
                 </td>
                 <td style="text-align: center" width="30%" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" width="30%" class="alt1">
                    Year
                </td>
               
               
           </tr>
            <tr >
                <td style="text-align: left; "  class="HeaderDiv" >
                    <asp:DropDownList ID="DLUser"  runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                 <td style="text-align: center" width="30%" >
                  
                     <asp:DropDownList ID="DLMonth1" runat="server" CssClass="common">
                 <asp:ListItem Text="January" Value="1"></asp:ListItem> 
                 <asp:ListItem Text="February" Value="2"></asp:ListItem> 
                 <asp:ListItem Text="March" Value="3"></asp:ListItem> 
                 <asp:ListItem Text="April" Value="4"></asp:ListItem> 
                 <asp:ListItem Text="May" Value="5"></asp:ListItem> 
                 <asp:ListItem Text="June" Value="6"></asp:ListItem> 
                 <asp:ListItem Text="July" Value="7"></asp:ListItem> 
                 <asp:ListItem Text="August" Value="8"></asp:ListItem> 
                 <asp:ListItem Text="September" Value="9"></asp:ListItem> 
                 <asp:ListItem Text="October" Value="10"></asp:ListItem> 
                  <asp:ListItem Text="November" Value="11"></asp:ListItem> 
                  <asp:ListItem Text="December" Value="12"></asp:ListItem>                         </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="30%">
                    
                        <asp:DropDownList ID="DLYear1" runat="server" CssClass="common">
                           <asp:ListItem Text="2009" Value="2009"></asp:ListItem>  
                           <asp:ListItem Text="2010" Value="2010"></asp:ListItem> 
                           <asp:ListItem Text="2011" Value="2011"></asp:ListItem>   <asp:ListItem Text="2012" Selected="True"   Value="2012"></asp:ListItem>  
                           </asp:DropDownList>
                </td>
               
               
           </tr>
          
           <tr>
                <td style="text-align: center;" colspan="2" >
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
