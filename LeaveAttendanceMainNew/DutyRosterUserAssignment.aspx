<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DutyRosterUserAssignment.aspx.vb" Inherits="LeaveAttendanceMainNew_DutyRosterUserAssignment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DutyRoster Assignment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" language="javascript">
        function Chk()
        {
            if (document.getElementById('ddlWOff1').value=='')
            {
                alert('Please select weeklyoff1');
                return false;
            }
//            if (document.getElementById('ddlWOff2').value=='')
//            {
//                alert('Please select weeklyoff2');
//                return false;
//            }
            if (document.getElementById('ddlShift').value=='')
            {
                alert('Please select shift');
                return false;
            }
            if (document.getElementById('sDate').value=='')
            {
                alert('Please select startdate');
                return false;
            }
            if (document.getElementById('eDate').value=='')
            {
                alert('Please select enddate');
                return false;
            }
            return true;            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
        <div id="cap"></div>
        <div id="main">
            <h1>DutyRoster Assignments</h1> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div style="text-align: center">
                <asp:Panel ID="Panel2" runat="server" width="100%">
                    <table width="100%">
                        <tr>
                            <td class="HeaderDiv" style="text-align: center" >
                                Selected Users
                                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" />
                            </td>
                        </tr>
                    </table> 
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" width="100%" >
                    <asp:GridView ID="GrdSelUsers" runat="server" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <%#Eval("UserName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <%#Eval("EName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>    
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpesetting" runat="Server"
                        Collapsed="true"
                        TargetControlID="Panel3"
                        ExpandControlID="Panel2"
                        CollapseControlID="Panel2" 
                        ImageControlID="Image1"    
                        ExpandedText="(Hide Details...)"
                        CollapsedText="(Show Details...)"
                        ExpandedImage="../App_Themes/Images/collapse_blue.jpg"
                        CollapsedImage="../App_Themes/Images/expand_blue.jpg"
                        SuppressPostBack="true" EnableViewState="true" 
                />
            </div>
            
            
            <div style="text-align:left">
                <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="red" Font-Names="Trebuchet MS" Font-Size="10" Font-Bold="true"></asp:Label>
                <br />   
                <table style="text-align:left">
                    <tr>
                        <td class="alt1">WeeklyOff1</td>
                        <td class="alt1">WeeklyOff2</td>
                        <td class="alt1">Shift</td>
                        <td class="alt1">From</td>
                        <td class="alt1">To</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlWOff1" runat="server">
                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                <asp:ListItem Text="Wenesday" Value="Wenesday"></asp:ListItem>
                                <asp:ListItem Text="Thrusday" Value="Thrusday"></asp:ListItem>
                                <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWOff2" runat="server">
                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                <asp:ListItem Text="Wenesday" Value="Wenesday"></asp:ListItem>
                                <asp:ListItem Text="Thrusday" Value="Thrusday"></asp:ListItem>
                                <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlShift" runat="server">
                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                <asp:ListItem Text="II" Value="II"></asp:ListItem>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="FN" Value="FN"></asp:ListItem>
                                <asp:ListItem Text="G" Value="G"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="sDate" runat="server" Width="120px" TabIndex="6"></asp:TextBox>             
                            <asp:ImageButton ID="ImgBntsDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" CausesValidation="False" />         
                        </td>
                        <td>
                            <asp:TextBox ID="eDate" runat="server" Width="120px" TabIndex="7"></asp:TextBox>
                            <asp:ImageButton ID="ImgBnteDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" CausesValidation="False" />         
                        </td> 
                    </tr>
                </table>
            </div>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="sDate" PopupButtonID="ImgBntsDate" CssClass="cal_Theme1" />         
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="eDate" PopupButtonID="ImgBnteDate" CssClass="cal_Theme1" /><br />
            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="button" OnClientClick="javascript:return Chk();" />
        </div>
    </div>
        <asp:HiddenField ID="hdnSelectedUsers" runat="server" /> 
    </form>
</body>
</html>
