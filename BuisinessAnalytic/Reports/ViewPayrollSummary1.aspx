<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewPayrollSummary1.aspx.vb" Inherits="MIS_DailyMins" %>

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
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
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
        <h1>View EmployeeWise CPL</h1>
        
            <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="500">
             <tr>
                <td colspan="7" style="text-align: center" class="HeaderDiv" >
                    Search
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel4" runat="server" width="100%">
           <table width="500">
                <tr >
                 
                 <td style="text-align: center" width="30%" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" width="30%" class="alt1">
                    Year
                </td>
               
           </tr>
            <tr >
            
                 <td style="text-align: center" width="30%" >
                  
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
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
                    
                        <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
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
        <br />
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="center">
            <asp:Table ID="tblMins" runat="server" HorizontalAlign="center">
                <asp:TableRow>
                    <asp:TableCell CssClass="HeaderDiv" ColumnSpan="12">
                        EmployeeWise CPL
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Left">
                    <asp:TableCell runat="server" CssClass="alt1" horizontalAlign="Center" ID="TableCell7" >Employee Name</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" horizontalAlign="Center" ID="TableCell2" >UserID</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" horizontalAlign="Center" ID="TableCell9" >Cost</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" horizontalAlign="Center" ID="TableCell8" >Lines</asp:TableCell>
                    <asp:TableCell runat="server" CssClass="alt1" horizontalAlign="Center" ID="TableCell1" >CPL</asp:TableCell>
                </asp:TableRow>
                
            </asp:Table>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
