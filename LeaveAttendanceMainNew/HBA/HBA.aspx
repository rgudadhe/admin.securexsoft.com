<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HBA.aspx.vb" Inherits="LeaveAttendanceMainNew_HBA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HBA</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridViewCancel').dataTable( {
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 3, "asc" ]]
				} );
			} );
</script>
    <%--<LINK href= "CalendarTitle.css" type="text/css" rel="stylesheet">
    <LINK href= "Tab.css" type="text/css" rel="stylesheet">--%>
</head>

<body style=" text-align:left">
    <form id="frmHBA" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>HBA</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="HBATabContainer" AutoPostBack="true" runat="server" Width="100%" ScrollBars="None">
            <Ajax:TabPanel ID="SendRequest" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblRequest" runat="server" Text="Send Leave Request" CssClass="common"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <center>
                        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtStartDate" runat="server" ErrorMessage="Please Select Leave Start Date" SetFocusOnError="true" ControlToValidate="txtStartDate" ></asp:RequiredFieldValidator> <BR>
                        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtEndDate" runat="server" ErrorMessage="Please Select Leave End Date" SetFocusOnError="true" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator> <BR>
                        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldReason" runat="server" ErrorMessage="Please Enter Reason for Leave" SetFocusOnError="true" ControlToValidate="TextArea1"></asp:RequiredFieldValidator> <BR>
                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEndDate" ControlToCompare="txtStartDate" Type="Date" Operator="GreaterThanEqual" runat="server" SetFocusOnError="true"  ErrorMessage="LeaveEnd Date must be greater than LeaveStart Date"></asp:CompareValidator>
                    </center>
                    <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" Width="420px">
                        <asp:TableRow >
                            <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv" ColumnSpan="2">
                                ERSS Leave Registration
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="common">
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                                Select Start Date : <asp:TextBox ID="txtStartDate" runat="server" Width="70px" CssClass="common"></asp:TextBox>    
                                                    <asp:ImageButton ID="imgSDate" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Select End Date :   <asp:TextBox ID="txtEndDate" runat="server" Width="70px" CssClass="common"></asp:TextBox>
                                                    <asp:ImageButton ID="imgEDate" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" CausesValidation="false" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" Enabled="True" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top" CssClass="common">
                                Reason : 
                            </asp:TableCell>
                            <asp:TableCell >
                                <textarea id="TextArea1" class="common"  rows="10" cols="68" runat="server"></textarea>
                            </asp:TableCell>
                        </asp:TableRow>
                
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="2" >
                                <center>
                                    <asp:Button ID="SendLR" runat="server" Text="Register Leave" CssClass="button" /> 
                                </center>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <BR> <BR> <BR>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="CancelRequest" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblCanRequest" runat="server" Text="Cancel Leave"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=100% height="410" frameborder="0" src="CancelLeaveLst.aspx"  id="CancelLeaveLst"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Status" runat="server" >
                <HeaderTemplate>
                    <asp:Label ID="LeaveStatus" runat="server" Text="Leave Status" CssClass="common" ></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYear" runat="server" Width="75" Height="20" CssClass="common">
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonth" runat="server" Width="85" Height="20" CssClass="common">
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height="20" />
                            <asp:Table ID="tblMain" HorizontalAlign="Center" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrev" runat="server" CssClass="button" ForeColor="Goldenrod"  Text="<<" CausesValidation="false"/>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthName" runat="server" CssClass="common" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNext" CssClass="button" CausesValidation="false" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Table ID="tblMainCalendar" runat="server" Width="100%" CssClass="common">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer> 
    </div>
    </div> 
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
