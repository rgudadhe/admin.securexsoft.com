<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HBACancelDateSelection.aspx.vb" Inherits="HBALeaves_CancelDateSelection" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">
    function calendarPicker(strTxtRef)
	{
		window.open('Control.aspx?field=' + strTxtRef +'','calendarPopup','titlebar=no,left=470,top=100,width=300,height=250,resizable=no');				
	}
	function cwindow()
    {
	    window.close();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Select Date</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Table runat="server" ID="tblMain" HorizontalAlign="Center">
            <asp:TableRow ID="tblNote"   >
                <asp:TableCell ColumnSpan="2" CssClass="HeaderDiv">
                    Please select cancel start and end date
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                    Start Date
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                    End Date
                </asp:TableCell >
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="txtStartDate" runat="server" Width="70px" CssClass="common"></asp:TextBox>    
                    <asp:ImageButton ID="imgSDate" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True" CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEndDate" runat="server" Width="70px" CssClass="common"></asp:TextBox>
                    <asp:ImageButton ID="imgEDate" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" CausesValidation="false" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" Enabled="True" CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>                
               </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">
                    <center>
                        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button" /> &nbsp;&nbsp;&nbsp
                        <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="button" OnClientClick="javascript:cwindow();" />
                    </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <center>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtStartDate" runat="server" ErrorMessage="Please enter start date" SetFocusOnError="true" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator><BR>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtEndDate" runat="server" ErrorMessage="Please enter end date" SetFocusOnError="true" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>
        </center>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
