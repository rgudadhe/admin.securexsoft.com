<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileImportLogNew.aspx.vb" Inherits="ets.FileImportLogNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>File Import Log</title>    
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function Chk()
        {
            if(document.getElementById('txtCJNum').value=='' && document.getElementById('txtMD5').value=='' && document.getElementById('ddlStatus').value=='' && document.getElementById('sDate').value=='' && document.getElementById('eDate').value=='' && document.getElementById('txtClient').value=='')
            {
                alert('Please select search criteria')
                return false;
            }
            
            if(document.getElementById('sDate').value!='' && document.getElementById('eDate').value=='')
            {
                alert('Please select end date')
                return false;
            }
            if(document.getElementById('sDate').value=='' && document.getElementById('eDate').value!='')
            {
                alert('Please select start date')
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" target="Result" action="FileImportLogResultNew.aspx">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:Panel ID="Panel2" runat="server" Height="15%" Width="100%">
    <table id="MainTable" width="100%" border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="width: 100%; text-align: center;" valign="top" colspan ="2" class="HeaderDiv">
                    File Import Log</td>
               </tr>
               </table> 
        <table  width="100%" border ="2" cellpadding ="2" cellspacing ="2">
            <tr>
                <td class="SMSelected" style="text-align:center;  "  >
                    Customer Job# </td>
                <td class="SMSelected" style="text-align:center;  "    >
                    MD5 Value </td>    
                <td class="SMSelected" style="text-align:center;  "    >
                    Status </td>  
                     <td class="SMSelected" style="text-align:center;  "   >
                    Start Date </td>           
                 <td class="SMSelected" style="text-align:center;  "   >
                    End Date</td>   
                 <td class="SMSelected" style="text-align:center;  "   >
                    Client</td>   
             </TR>
              <tr>
                <td style="text-align:center;  " >
                    <asp:TextBox ID="txtCJNum" runat="server"></asp:TextBox></td>
                <td style="text-align:center;  " >
                    <asp:TextBox ID="txtMD5" runat="server"></asp:TextBox></td>
                <td style="text-align:center;  " >
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Selected="True" Value="" >Any</asp:ListItem>
                        <asp:ListItem Value="1">Imported</asp:ListItem>
                        <asp:ListItem Value="0">Not Imported</asp:ListItem>
                        <asp:ListItem Value="2">Duplicate</asp:ListItem>
                    </asp:DropDownList></td>
                                 
                 <td style="text-align:center;  " >
                     <asp:TextBox ID="sDate" runat="server" Width="120px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                    <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Start Date (MM/DD/YYYY)" ControlToValidate="sDate" ValidationExpression="\d{2}/\d{2}/\d{4}">*</asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RegularExpressionValidator1" />    
                 </td>           
                 <td style="text-align:center;  " >
                     <asp:TextBox ID="eDate" Width="120px" runat="server"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                    <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator2" runat="server" ControlToValidate="eDate" SetFocusOnError="true"
                        ErrorMessage="Invalid End Date (MM/DD/YYYY)" ValidationExpression="\d{2}/\d{2}/\d{4}"></asp:RegularExpressionValidator>&nbsp;
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator2" />    
                    <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
                        targetcontrolid="sDate" Format="MM/dd/yyyy"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" Format="MM/dd/yyyy" runat="server" popupbuttonid="ImgBnteDate"
                        targetcontrolid="eDate"></ajaxtoolkit:calendarextender>                                            
                 </td>   
                 <td style="text-align:center;  " >
                     <asp:TextBox ID="txtClient" runat="server"></asp:TextBox>&nbsp;<br />
                     <%--<asp:RequiredFieldValidator ID="RequiredFieldTo" runat="server" Display=None ErrorMessage="<b>Required Field Missing</b><br />To is required." Font-Names="Arial" Font-Size="8pt" Font-Italic=true SetFocusOnError=true ControlToValidate="txtTo"></asp:RequiredFieldValidator>&nbsp--%>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" ErrorMessage="End date can not be less than Start Date" ControlToCompare="sDate" ControlToValidate="eDate" Operator="GreaterThanEqual" SetFocusOnError=true Type="Date"></asp:CompareValidator></td>   
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE1" TargetControlID="CompareValidator1" />
            </tr>
            <%--<tr>
                <td colspan="5" style="width: 26161px; height: 21px">
                    &nbsp;&nbsp;
                </td>
            </tr>--%>
            <tr>
                <td colspan="6" style="text-align:center;  "  >
                    <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search" OnClientClick="javascript:return Chk();" />
                    
                </td>
                <td colspan=2 width="80%">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true" DisplayMode="BulletList"/>
                </td>
            </tr>
            <%--<tr>
                <td colspan="5" style="width: 26161px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>--%>
        </table>    
        </asp:Panel> 
        <%--<iframe style="width: 840px; height: 338px;" frameborder="0" name="LogResult1" src="FileImportLogResultNew.aspx"></iframe>
        --%></form>       
                
</body>
</html>
