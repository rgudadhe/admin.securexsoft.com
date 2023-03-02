<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TrainingLogform.aspx.vb" Inherits="TrainingLogForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Comment</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript">
    function Chk()
    {
        if (document.getElementById('ddlCType').value == '')
        {
            alert('Please select comment type')
            return false;
        }
        return true;
    }
    </script>
</head>
<body>
    <form id="form1" method="post" target="ViewComment" runat="server">
    <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
    <div>
        <table style="background-color:WhiteSmoke; font-family:Trebuchet MS; font-size:small">
            <tr>
                <td>
                </td>
                <td style="width: 256px">
                    <asp:TextBox ID="TextBox4" runat="server" BorderStyle="None" ReadOnly="True" Text="User Name" Width="264px" CssClass="SearchCol" ></asp:TextBox>
                </td>
                <td colspan=2>
                    <asp:TextBox ID="TextBox12" runat="server" BorderStyle="None" ReadOnly="True" Text="Date Of Training" Width="150px" CssClass="SearchCol" ></asp:TextBox>
                </td>
<%--                <td>
                    <asp:TextBox ID="TextBox13" runat="server" BorderStyle="None" ReadOnly="True" Text="End Date" Width="75px" CssClass="SearchCol" ></asp:TextBox>
                </td>--%>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 256px">
                    <asp:TextBox ID="txtUName" runat="server" Width="272px" Font-Names="Trebuchet MS" Font-Size=Small Height="15px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" 
                                  MinimumPrefixLength="1" 
                                  CompletionSetCount="10" 
                                  runat="server" 
                                  TargetControlID="txtUName"
                                  ServicePath="../users/autocomplete.asmx"
                                  ServiceMethod="GetUsersAllLevel" EnableCaching="true"/>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Width="65px" Font-Names="Trebuchet MS" Font-Size=Small Height="15px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                </td>                        
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Width="65px" Font-Names="Trebuchet MS" Font-Size=Small Height="15px"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                    <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
                    targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"
                    targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>                    
                </td>
                <td width="20px">
                </td>
                <td>
                    <input name="SEARCH" type="submit" value="Search" style="font-family:Trebuchet MS ; font-size:small " id="Submit2" onclick="javascript:return Chk();" />            
                </td>
            </tr>
            
        </table>
        <iframe id="ViewComment" frameborder="0" name="ViewComment" src="TrainingLogResult.aspx"  style="width: 100%; height:420px;"></iframe>
    </div>
    </form>
</body>
</html>
