<%@ Page Language="VB" AutoEventWireup="false" CodeFile="US_LeaveReport.aspx.vb" Inherits="LeaveAttendanceMainNew_Reports_USLeaveReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Report</title>
    <link href="../../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridView1').dataTable( {
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 6, "asc" ]]
				} );
			} );
</script>        

    <script language="javascript" type="text/javascript">
        function Chk()
        {
            if (document.getElementById('txtStartDate').value=='')
            {
                if (document.getElementById('txtEndDate').value=='')
                {
                    alert('Please select atleast one date')
                    return false;
                }
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <div>
    <center>
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ID="TableCell1" ColumnSpan="3" runat="server" CssClass="HeaderDiv">
                            Employees Leave Report 
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Start Date :
                            <asp:TextBox ID="txtStartDate" runat="server" Width="65px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        </asp:TableCell>
                        <asp:TableCell>
                            End Date : 
                            <asp:TextBox ID="txtEndDate" runat="server" Width="65px"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="Submit" runat="server" cssClass="button" Text="Submit" OnClientClick="javascript:return Chk();" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="ImgBntsDate" BehaviorID="CalendarExtender1" Enabled="True" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="ImgBnteDate" BehaviorID="CalendarExtender2" Enabled="True" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
            </center>
            <center>
                            <table id="Table3" runat="server">
                                <tr>
                                    <td align="left" style="border:0">
                                        <asp:LinkButton ID="LinkButton1" CssClass="common" runat="server">Export Results</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="border:0">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="Header">
                                                    <ItemTemplate><%#Eval("Employee Name")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="Header">
                                                    <ItemTemplate><%#Eval("ID")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leaves Applied" HeaderStyle-CssClass="Header">
                                                    <ItemTemplate><%#Eval("Leave Applied")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approved Leaves" HeaderStyle-CssClass="Header">
                                                    <ItemTemplate><%#Eval("approved")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Balance" HeaderStyle-CssClass="Header">
                                                    <ItemTemplate><%#Eval("Balance")%></ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>                                            
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        <%--</td>
                    </tr>
                </table>--%>

                
                
            </center>
    </div>
    </form>
</body>
</html>
