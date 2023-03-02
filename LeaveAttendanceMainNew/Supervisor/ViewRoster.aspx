<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewRoster.aspx.vb" Inherits="LeaveAttendanceMainNew_Supervisor_ViewRoster" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

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
                <table id="LinkImport" runat="server" width="100%" visible="false" class="common"><tr><td align="left" style="border:0"><a href="ImportDutyRoster.aspx">Import Duty Roster</a><BR></td></tr></table>
                <asp:Table ID="tblDept" runat="server" CssClass="common" Width="400px" Visible="false" >
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" runat="server" CssClass="alt1">
                            Department : 
                            <asp:DropDownList ID="DropDownDept" runat="server" CssClass="common" Width="246px">
                            </asp:DropDownList>
                        </asp:TableCell>    
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table2" runat="server" Width="400px">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" CssClass="alt1">
                            Month :
                            <asp:DropDownList ID="DropDownMonth" CssClass="common" runat="server">
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
                        <asp:TableCell runat="server" CssClass="alt1">
                            Year : 
                            <asp:DropDownList ID="DropDownYear" CssClass="common" runat="server" Width="110px">
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell runat="server" CssClass="alt1">
                            <asp:Button ID="Submit" runat="server" cssClass="button" Text="Submit" OnClientClick="javascript:return Chk();" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </center>
            <center>
                <table id="Shift" runat="server" width="100%">
                    <tr>
                        <td align="left" class="common" style="border:0"><font color="purple">Shift Abbreviations - I: First , II: Second ,G: General ,N: Night, FN: Full Night, O: Off </font></td>
                    </tr>
                </table>
                <asp:Table ID="Table3" runat="server" Width="100%" HorizontalAlign="Center" GridLines="Both">
                    <asp:TableRow BackColor="#D4D4D4">
                        <asp:TableCell ID="ExportRes" HorizontalAlign="Left" CssClass="alt2" BorderStyle="None" BorderWidth="0">
                            <asp:LinkButton ID="ES" CssClass="common" runat="server">Export Results</asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <%--<asp:TableRow BackColor="#D4D4D4">
                        <asp:TableCell ID="tblExportCell" HorizontalAlign=Left>
                            <asp:LinkButton ID="LinkButton1" runat="server"><font face="Trebuchet MS" size="2px">Export Results</font></asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <%--<asp:TableRow ID="MainRow" runat=server CssClass="SMSelected" >
                        <asp:TableCell Font-Names="Trebuchet MS" Font-Size="12px">
                            Employee Name
                        </asp:TableCell>
                    </asp:TableRow>--%>
                </asp:Table>
            </center>
    </div>
    </form>
</body>
</html>
