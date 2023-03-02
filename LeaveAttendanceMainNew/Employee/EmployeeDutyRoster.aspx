<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmployeeDutyRoster.aspx.vb" Inherits="LeaveAttendanceMainNew_Employee_EmployeeDutyRoster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Duty Roster</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function Chk()
        {
            if(document.getElementById('DropDownMonth').value=="")
            {
                alert('Please select month');
                return false;
            }
            if(document.getElementById('DropDownYear').value=="")
            {
                alert('Please select year');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Table ID="Table2" runat="server" Width="400px" CssClass="common">
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell1" runat="server" CssClass="alt1">
                            Month :
                            <asp:DropDownList ID="DropDownMonth" runat="server">
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server" CssClass="alt1">
                            Year : 
                            <asp:DropDownList ID="DropDownYear" runat="server" Width="110px">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server" CssClass="alt1">
                            <asp:Button ID="Submit" runat="server" cssClass="button" Text="Submit" OnClientClick="javascript:return Chk();" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
        </center>    
        <center>
                <table id="Shift" runat="server" width="100%">
                    <tr>
                        <td align="left" style="border:0"><font face="Arial" size="2px" color="purple">Shift Abbreviations - I: First , II: Second ,G: General ,N: Night, FN: Full Night, O: Off </font></td>
                    </tr>
                </table>
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%" CssClass="common">
                </asp:Table>
            </center>
    </div>
    </form>
</body>
</html>
