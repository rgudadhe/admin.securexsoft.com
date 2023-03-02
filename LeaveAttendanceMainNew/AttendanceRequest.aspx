<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceRequest.aspx.vb" Inherits="AttendanceRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<script language="javascript" type="text/javascript">			
    function calendarPicker(strTxtRef)
	{
		window.open('Control.aspx?field=' + strTxtRef +'','calendarPopup','titlebar=no,left=470,top=100,width=300,height=250,resizable=no');				
	}
	
	
	
function ChkDate()
{
    if (document.form1.txtAttDate.value=='')
    {
        alert('Please select date for attendance')
        //document.form1.imgSDate.focus();
        return false
    }
}

//function Chk()
//{
//     if (document.form1.txtAttDate.value=='')
//     {
//        alert('Please select date for attendance');
//        document.form1.txtAttDate.focus();
//        return false;
//     }  
//     if (document.form1.txtInTime.value=='')
//     {
//        alert('Please select incoming time');
//        document.form1.txtInTime.focus();
//        return false;
//     }
//     if (document.form1.InTime.value=='')
//     {
//        alert('Please select incoming time');
//        document.form1.InTime.focus();
//        return false;
//     }
//     if (document.form1.txtOutTime.value=='')
//     {
//        alert('Please select out time');
//        document.form1.txtOutTime.focus();
//        return false;
//     }
//     if (document.form1.OutTime.value=='')
//     {
//        alert('Please select out time');
//        document.form1.OutTime.focus();
//        return false;
//     }
//     
//     return true;
//}
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attendance Request</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:left">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Firebrick" Text="" Font-Size="10"></asp:Label>    
    </div>
    <div style="text-align:left">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
            <asp:Table ID="Table1" runat="server" Width="540px">
                <asp:TableRow runat="server" >
                    <asp:TableCell runat="server"  HorizontalAlign="Center" CssClass="HeaderDiv" ColumnSpan="2">
                        Attendance Request
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell ColumnSpan="2" runat="server" HorizontalAlign="Center">
                        <center>
                        Select Attendance Date : <asp:TextBox ID="txtAttDate" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>    
                                            <asp:ImageButton ID="imgSDate" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtAttDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                            </center>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell HorizontalAlign="Left" runat="server" Width="50%">
                        In-Time : 
<%--                        <asp:TextBox ID="txtInTime" runat="server" Font-Names="Trebuchet MS" Width=80px></asp:TextBox>
                        hh AM/PM
--%>                        
                        <asp:DropDownList ID="txtInTime" runat="server" CssClass="common" Width="90">
                        </asp:DropDownList> &nbsp
                        <asp:DropDownList ID="InTime" runat="server" CssClass="common" Width="90">
                            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left" runat="server" Width="50%">
                        Out-Time :
                        <%--<asp:TextBox ID="txtOutTime" runat="server" Font-Names="Trebuchet MS" Width=80px></asp:TextBox>
                        hh AM/PM--%>
                        <asp:DropDownList ID="txtOutTime" runat="server" CssClass="common" Width="90">
                        </asp:DropDownList>&nbsp
                        <asp:DropDownList ID="OutTime" runat="server" CssClass="common" Width="90">
                            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="common" VerticalAlign="Top">
                        Reason :<br />
                        <textarea id="txtReason" rows="9" cols="110" runat="server" class="common"></textarea>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <center>
                            <asp:Button ID="SendRequest" CssClass="button"  runat="server" Text="Send Request" />
                        </center>
                    </asp:TableCell>
                </asp:TableRow>                
            </asp:Table>
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldAttDate" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />Attendance Date is required." ControlToValidate="txtAttDate"  BorderStyle="None" ></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE1" TargetControlID="RequiredFieldAttDate"  />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />InTime is required." ControlToValidate="txtInTime"  BorderStyle="None" ></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1"  />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />InTime Format is required." ControlToValidate="InTime"  BorderStyle="None" ></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2"  />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />OutTime is required." ControlToValidate="txtOutTime"  BorderStyle="None" ></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3"  />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />OutTime Format is required." ControlToValidate="OutTime"  BorderStyle="None" ></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator4"  />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
