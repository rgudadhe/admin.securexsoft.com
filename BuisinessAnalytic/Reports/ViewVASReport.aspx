<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewVASReport.aspx.vb" Inherits="ViewVasReport" %>

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

    <title>View VAS Report</title>
</head>
<body style="text-align: center">
    <form id="BillDet" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View VAS Report</h1>
        <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">
            <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="500">
             <tr>
                <td colspan="3" style="text-align: center" class="HeaderDiv" >
                    Search VAS Report
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="500">
                <tr >
                 <td style="text-align: center" width="30%" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" width="30%" class="alt1">
                    Year
                </td>
               <td style="text-align: center" width="30%" class="alt1">
                   Account
                </td> 
           </tr>
            <tr >
                 <td style="text-align: center" width="30%" >
                  
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                                        </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="30%">
                    
                        <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                           
                           </asp:DropDownList>
                </td>
               <td style="text-align: center" width="30%">
                    <asp:DropDownList ID="DLAct" runat="server" CssClass="common">
                    </asp:DropDownList>
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
        ExpandedImage="~/App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="~/App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        
        
        <asp:Table ID="tblMins" runat="server" Width="100%" HorizontalAlign="Center" GridLines="Both" >
            <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" cssclass="HeaderDiv"  >
                <asp:TableCell runat="server" ID="TableCell9" cssclass="HeaderDiv" >Account Name</asp:TableCell>
                <asp:TableCell runat="server"  ID="TableCell8" cssclass="HeaderDiv" >Description</asp:TableCell>
                <asp:TableCell runat="server"  ID="TableCell7" cssclass="HeaderDiv" >Service Date</asp:TableCell> 
                <asp:TableCell runat="server"  ID="R1Cell2" cssclass="HeaderDiv" >Quantity</asp:TableCell>
                <asp:TableCell runat="server"  ID="R1Cell3" cssclass="HeaderDiv" >Rate</asp:TableCell>
                <asp:TableCell runat="server"  ID="R1Cell5" cssclass="HeaderDiv" >Amount</asp:TableCell>
                <asp:TableCell runat="server"  ID="R1Cell6" cssclass="HeaderDiv" >Status</asp:TableCell>
                <asp:TableCell runat="server"  ID="R1Cell10" cssclass="HeaderDiv" >Remove</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Button ID="btnEmail" runat="server"    CssClass="button" Text="E-Mail" Visible="False" />
        <asp:Button ID="btnPost" runat="server"  OnClientClick="PostBilling();return false;" CssClass="button" Text="Post" Visible="False" />
        </asp:Panel>
           </div> 
           </div> 
    </form>
</body>
</html>
