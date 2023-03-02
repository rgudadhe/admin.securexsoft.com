<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewDailyProductivityReport.aspx.vb" Inherits="DailyTeamReport" %>

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
 <title>view Productivity Report</title>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Daily Productivity Report</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td colspan="3" style="text-align: center"  class="HeaderDiv">
                    profile
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
                  
                 
			                   
            <h1>
                &nbsp;</h1>
            <table style="color: #ff9933" width="100%">
           
            <tr>
                <td colspan="2" rowspan="12" style="text-align: center" valign="middle">
                    &nbsp;<asp:Image ID="Image2" runat="server" BorderStyle="Groove" BorderWidth="2"
                        Height="100" Width="100" /></td>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    First Name</span></td>
                <td style=" text-align: left; ">
                    <asp:textbox ID="TxtFirstName" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Last Name</span></td>
                <td style=" text-align: left; ">
                    <asp:textbox ID="TxtLastName" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    E-Dictate Mail ID</span></td>
                <td style=" height: 21px; text-align: left; ">
                    <asp:textbox ID="TxtEmail" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                    <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Date Of Birth</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtDOB" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                      <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Joining Date</span></td>
                <td style=" text-align: left; text-align: left; ">
                    <asp:textbox ID="TxtJoin" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
             <tr>
                <td style=" text-align: right; height: 26px; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Department</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtDept" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Designation</span></td>
                <td style=" text-align: left; text-align: left; ">
                    <asp:textbox ID="TxtDesn" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                  </tr>
            <tr> 
                <td style="text-align: right;  height: 21px;">
                    <span style="font-size: 10pt; font-family: Trebuchet MS">Category</span></td>
                <td style="  text-align: left;  height: 21px;">
                    <asp:textbox ID="TxtCategory" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" height: 21px; text-align: right; height: 30px; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Current Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:textbox ID="TxtAdd" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
       
            
       
       
            
            <tr>
                <td style=" height: 26px; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Tel#</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtTel" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Cell#</span></td>
                <td style=" text-align: left; height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtCell" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
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
       <div style="text-align:center " >
        <asp:Table ID="tblMins" runat="server"   >
        
               <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center"        >
                <asp:TableCell runat="server" ID="TableCell9"  CssClass="alt1" ColumnSpan="11"     >Productivity Report</asp:TableCell>
                </asp:TableRow>
              
            <asp:TableRow ID="TableRow5" runat="server" HorizontalAlign="Center"  CssClass="HeaderDiv"   >
                <asp:TableCell runat="server" ID="R2Cell1"  CssClass="alt1" >Transcription Date</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell11" CssClass="alt1" >MT</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell12" CssClass="alt1" >MT+</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell13" CssClass="alt1" >QA</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell14" CssClass="alt1" >MT B</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell15" CssClass="alt1" >QA B</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell16" CssClass="alt1" >QABSE</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell17" CssClass="alt1" >PPQA</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell5" CssClass="alt1" >Target</asp:TableCell> 
                <asp:TableCell runat="server" ID="TableCell6" CssClass="alt1" >Points</asp:TableCell> 
                <asp:TableCell runat="server" ID="TableCell7" CssClass="alt1" >% Tgt</asp:TableCell> 
              
               
            </asp:TableRow>
        </asp:Table>
        </div>
       <asp:Table ID="Table3" runat="server" style="text-align: left" cellpadding="2" cellspacing="2" GridLines="Both">
                    <asp:TableRow ID="TableRow6" runat="server">
                 
                    <asp:TableCell ID="TCell2" runat="server">
                   <asp:Button ID="Button1" runat="server" Text="Export Result"  cssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
      
</div> 
        </div> 
   
    </form>
</body>
</html>
