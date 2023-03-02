<%@ Page Language="VB" enableViewStateMac="False"  AutoEventWireup="false" CodeFile="Copy of ShowInvoiceDetails.aspx.vb" Inherits="Billing_Reports_Postbilling" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<style type="text/css" >
tr.tblbg {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg1 {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg2 {
	background: #e7e6e6 url(../images/tbbg2.jpg) repeat;
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
/* links */
a, a:visited {	
	color: #000000; 
	background: inherit;
	text-decoration: none;		
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
a:hover {
	color: #000000;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}

tr.tblbgbody {
	background: #e7e6e6 url(../images/bgorange1.JPG) repeat;
	
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
input.button { 
	font: bold 8pt Arial, Sans-serif; 
	height: 24px;
	margin: 0;
	padding: 2px 3px; 
	color: #ffffff;
	background: #E56717;
	border: 1px solid #dadada;
}

</style>
    <title>Post Invoice Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Summary</h1>
        
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="500">
             <tr>
                <td class="HeaderDiv" >
                    Search
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">
           <table width="500">
                <tr >
                    <td style="text-align: center" width="25%" class="alt1" >
                        Month
                    </td>
                    <td style="text-align: center" width="25%" class="alt1">
                        Year
                    </td>
                    <td style="text-align: center" width="25%" class="alt1">
                        Cycle
                    </td> 
                     <td style="text-align: center" width="25%" class="alt1">
                        Status
                    </td> 
               </tr>
            <tr >
                 <td style="text-align: center" width="25%" >
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                         
                     </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="25%">
                    <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                    
                    </asp:DropDownList>
                </td>
               <td style="text-align: center" width="25%">
                    <asp:DropDownList ID="DLCycle" runat="server" CssClass="common">
                        <asp:ListItem Text="Cycle1" Value="1"></asp:ListItem>  
                        <asp:ListItem Text="Cycle2" Value="2"></asp:ListItem>  
                        <asp:ListItem Text="Month" Value="3"></asp:ListItem>  
                     </asp:DropDownList>
                </td> 
                 <td style="text-align: center" width="25%">
                    <asp:DropDownList ID="DLStatus" runat="server" CssClass="common">
                        <asp:ListItem Text="Posted" Value="Posted"></asp:ListItem>  
                        <asp:ListItem Text="Not Posted" Selected="True"  Value="NotPosted"></asp:ListItem>  
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>  
                     </asp:DropDownList>
                </td> 
           </tr>
           <tr>
                <td style="text-align: center;" colspan="4" class="alt1" >
                    <asp:Button ID="tblsubmit" cssClass="button" runat="server" Text="Submit" />
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
         
              
            <asp:CompleteGridView ID="MyDataGrid" runat="server" AutoGenerateColumns="True" 
                     cellspacing="2" CellPadding="2" 
                     
                     EnableViewState="False"   
                     Width="100%"   CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom"   ShowCount="False"  >
                <AlternatingRowStyle cssclass="gridalt2"  ></AlternatingRowStyle>
                <RowStyle cssclass="gridalt1"  ></RowStyle>
                </asp:CompleteGridView>
                 <br />
       <asp:Table ID="Table1" runat="server" HorizontalAlign="Left"   >
       <asp:TableRow>
        <asp:TableCell ID="TCell2" runat="server">
                    <asp:Button ID="Button1" runat="server" Text="Export Result" Font-Size="8"  Font-Names="Arial" CssClass="button" />
                    </asp:TableCell>
                    </asp:TableRow> 
                    </asp:Table>
            <br />
    </div>
    </form>
</body>
</html>
