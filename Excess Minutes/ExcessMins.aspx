<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExcessMins.aspx.vb" Inherits="Excess_Minutes_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
    <title>Excess Minutes</title>
         
</head>

<body>

    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Excess Minutes</h1>  
         <div style="text-align: left">
           <asp:Panel ID="Panel2" runat="server" width="80%">
           <table  width="300">
 
             <tr>
                <td style="text-align: center"  class="HeaderDiv">
                Excess Minutes Report
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
           </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="300">
            
             <tr>
                <td>
                    Submit Date</td>
                     
                
         
                <td>
                    <asp:TextBox ID="Date1" runat="server" CssClass="common"></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="ImageButton3" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" Height="19px" Width="24px" /><br />
        <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="Date1" 
            PopupButtonID="ImageButton3" />
                    </td>
                
                
                 
            </tr>
            
           <tr >
                <td colspan="2" class="alt1">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Submit" />
                </td>
            </tr>
        </table>
    </asp:Panel>
     
     <ajaxToolkit:CollapsiblePanelExtender ID="cpeDEMO" runat="Server"
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
    </div>
                        
                        
        <asp:Label ID="Lbl1" runat="server"  Font-Bold="True" ForeColor="Red" CssClass="common"></asp:Label><br />
        <br />
        <asp:Panel ID="Panel3" runat="server" width="99%">
            <table id="Table1" width="100%">
                <tr>
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        Excess Minutes Assignment</td>
                </tr>
                <tr>
                    <td  style="text-align: center" valign="top">
                        <asp:Table ID="Table2" runat="server">
                            
                                <asp:TableRow runat="server" style="text-align: center">
                                    <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Protocol Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Fresh Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Finished Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Not Finished Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Pending Mins</asp:TableCell>
                                    <asp:TableCell CssClass="alt1" runat="server">Excess Mins</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </tr>
                        </table> 
                        </asp:Panel> &nbsp;
                        </div> 
                        </div> 
    </form>
</body>
</html>
