<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Supervisor.aspx.vb" Inherits="LeaveAttendanceMainNew_Supervisor_Supervisor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Supervisor Access</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="JavaScript">
    function Test()
    {
        alert('Test');
        alert(window.event.srcElement.value);
        
        //alert(document.getElementById("Submit").)
        if (window.event.srcElement.value == "Get Weeks")
        {
            alert('Test');
            return true;
        }
        else
        {
            document.form2.target="bottom"
            document.form2.submit 
            document.form2.action="DutyRosterResultNew.aspx";
            return true;
        }

        //document.form2.action="DutyRosterResultNew.aspx";
        //document.form2.submit(); 
        //document.form2.target="bottom";
        
        //return false
    }
    
    function ChangeAll(formObj) 
	{
	    var elval
		if (document.form2.All.checked) 
		{
			elval = true;
		} 
		else 
		{
			elval = false;
		}
		for (var i=0;i < formObj.length;i++) 
		{
			fldObj = formObj.elements[i];
			if (fldObj.type == 'checkbox')
			{ 
				fldObj.checked =  elval
			}
		}
	}
	
	function ChkValidation()
	{
	    var varCount 
	    varCount=0
	   if (GetVal()!= null )
	   {
	        for (var i=0;i < form2.length;i++) 
		    {
			    fldObj = form2.elements[i];
			    if (fldObj.type == 'checkbox')
			    { 
				    if (fldObj.checked)
				        varCount = varCount + 1    
			    }
		    }  
		    if (varCount == 0)
		    {
		        alert('Please select employee for updation')
		        return false;
		    }
	   }
	   else
	   {
	        alert('Please select shift for updation')
	        return false;
	   }
	   return true;
	}
	
	function GetVal()
    {
        var a = null;
        var f = document.forms[0];
        var e = f.elements["RadioShift"];

        for (var i=0; i < e.length; i++)
        {
            if (e[i].checked)
            {
                a = e[i].value;
                break;
            }
        }
        return a;
    }

</script>
</head>
<body style="text-align:left">
    <form id="frmSupervisor" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Supervisor</h1>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="EmpTabContainer" runat="server" AutoPostBack="true" Width="100%" ScrollBars="None" ActiveTabIndex="0" >
            <Ajax:TabPanel ID="DailyAttendance" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblAttendance" runat="server" CssClass="common" Text="Daily Attendance"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelAttendance" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYear" CssClass="common" runat="server" Width="75" Height="20">
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonth" CssClass="common"  runat="server" Width="85" Height="20">
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height="20"/>
                            <asp:Table ID="tblMain" HorizontalAlign="Center"  runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrev" runat="server" CssClass="button" ForeColor="Goldenrod" Text="<<" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthName" CssClass="common" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNext" CssClass="button" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Table ID="tblMainCalendar" runat="server" CssClass="common" Width="100%">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="SendRequest1" runat="server" Height="700">
                <HeaderTemplate>
                    <asp:Label ID="Label2" runat="server" Text="Leave and Attendance Request" CssClass="common"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width="15%" height="410"  frameborder="0"  src="LeftRequestFrame.aspx" id="LeftFrame"></iframe>
                    <iframe width="80%" height="410"  frameborder="0" src="RigthReuestFrame.aspx"  id="RightFrame"></iframe>   
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="Approval" runat="server" Height="700">
                <HeaderTemplate>
                    <asp:Label ID="lblApproval" runat="server" Text="Approval"></asp:Label>    
                </HeaderTemplate>
                <ContentTemplate>
                    <iframe width=15% height="410"  frameborder="0"  src="LeftRequestFrameApproval.aspx" id="Iframe1"></iframe>
                    <iframe width=80% height="410"  frameborder="0" src="RigthReuestFrameApproval.aspx"   id="Iframe2"></iframe>   
                </ContentTemplate>
            </Ajax:TabPanel>            
            <Ajax:TabPanel ID="Status" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="LeaveStatus" runat="server" Text="Leave Status" CssClass="common"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownYearStatus" runat="server" Width="75" Height="20">
                            </asp:DropDownList> &nbsp 
                            <asp:DropDownList ID="DropDownMonthStatus" runat="server" Width="85" Height="20">
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnGoStatus" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height="20"/>
                            <asp:Table ID="tblMainStatus" HorizontalAlign="Center" runat="server" Width="100%" BorderColor="LightBlue" BorderWidth="1" >
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrevStatus" runat="server" CssClass="button" ForeColor="Goldenrod"  Text="<<" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthNameStatus" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNextStatus" CssClass="button" CausesValidation="false" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan=3>
                                        <asp:Table ID="tblMainCalendarStatus" runat="server" Width="100%">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="EmpStatus" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="EmpLeaveStatus" runat="server" Text="Employee OnLeave"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanelEmp" runat="server">
                        <ContentTemplate>
                            <asp:Table ID="Table3" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell BorderStyle="None" BorderWidth="0" HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownYearStatusEmp" CssClass="common" runat="server" Width="75" Height="20">
                                        </asp:DropDownList> &nbsp 
                                        <asp:DropDownList ID="DropDownMonthStatusEmp" CssClass="common" runat="server" Width="85" Height="20">
                                        </asp:DropDownList> &nbsp
                                        <asp:Button ID="btnGoStatusEmp" runat="server" Text="Go" CausesValidation="false" CssClass="button" Height="20"/>
                                    </asp:TableCell>
                                    <asp:TableCell ID="tblCellDept" BorderStyle="None" BorderWidth="0" HorizontalAlign="Right">
                                        <div style="text-align:right">
                                            <asp:DropDownList ID="DropDownDept" runat="server" AutoPostBack="true" Width="275" Height="20" CssClass="common">
                                            </asp:DropDownList>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tblMainStatusEmp" HorizontalAlign="Center" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnPrevStatusEmp" runat="server" CssClass="button" ForeColor="Goldenrod" Text="<<" CausesValidation="false" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center" Width="80%" CssClass="HeaderDiv">
                                        <asp:Label ID="lblMonthNameStatusEmp" CssClass="common" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Right" CssClass="HeaderDiv">
                                        <asp:Button ID="BtnNextStatusEmp" CssClass="button" CausesValidation="false" ForeColor="Goldenrod" runat="server" Text=">>" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Table ID="tblMainCalendarStatusEmp" runat="server" Width="100%">
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="DutyRoster" runat="Server" Width="100%" >
                <HeaderTemplate>
                    <asp:Label ID="lblDutyRoster" runat="server" Text="DutyRoster" CssClass="common" ></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>   
                    <iframe width=100% height="410" frameborder="0" src="ImportDutyRoster.aspx" id="DutyRosterframe"></iframe>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer> 
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
