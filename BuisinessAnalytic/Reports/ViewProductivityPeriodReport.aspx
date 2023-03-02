<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewProductivityPeriodReport.aspx.vb" Inherits="ViewUnitsTrend" %>

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
<script type="text/javascript">
    
 function ShowHideDiv(container) {
 var menu = document.getElementById(container);
 alert
 if (menu.style.display == 'none') {
 menu.style.display = 'block';
 }
 else {
 menu.style.display = 'none';
 }
 return false;
 }

 function GetCheckedValues() {
 var Csv = '';
 var chkbox = document.getElementById('CheckBoxList1');
 var inputArr = chkbox.getElementsByTagName('input');
 var labelArr = chkbox.getElementsByTagName('label');
 for (var i = 0; i < inputArr.length; i++) {
 var inputVal = inputArr[i];
 var labelVal = labelArr[i];
 if (inputVal.checked == true) {
 Csv = Csv + labelVal.innerText + ','
 }
 }
 document.getElementById('TextBox1').value = Csv;
 return false;

 }
 </script>
</head>
<body>
    <form id="BillDet" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Period</h1>
        
            <asp:Panel ID="Panel2" runat="server" width="100%" HorizontalAlign="Left" >
           <table width="900" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td colspan="2" style="text-align: center"  class="HeaderDiv">
                    Search  
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
          </asp:Panel> 
          <asp:Panel ID="Panel3" runat="server" width="100%" HorizontalAlign="Left">
           <table width="900" >
            
            <tr class="tblbg2">
              <td style="text-align: left"  class="common">Account: 
                  <asp:DropDownCheckBoxes ID="DropDownList1"  onchange="copy();"  runat="server">
                  <Style2 SelectBoxWidth="400" DropDownBoxBoxWidth="400" DropDownBoxBoxHeight="200" /> 
                  </asp:DropDownCheckBoxes>
              </td> 
              
                 <td style="text-align: center"  class="common">
                    
                    Start Date:
                     <asp:TextBox ID="TxtSDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTSDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                 </td>
                 <td style="text-align: center"  class="common">
                    
                    End Date:
                     <asp:TextBox ID="TxtEDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgEDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTEDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2s" Enabled="True">
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
         </div>
          <asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Bold="true"    CssClass="common" ></asp:Label>
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
        <div id="DivContatainer" style="display:none;border-style:double;border-color:Black;">
 <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="129px">
 <asp:ListItem>1</asp:ListItem>
 <asp:ListItem>2</asp:ListItem>
 <asp:ListItem>3</asp:ListItem>
 </asp:CheckBoxList>
 </div>
    </form>
</body>
</html>
