<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DutyRosterTemplateAssignment.aspx.vb" Inherits="LeaveAttendanceMainNew_DutyRosterTemplateAssignment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DutyRoster Assignment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function Chk()
        {
            if (document.getElementById('ddlMonth').value=='')
            {
                alert('Please select month');
                return false;
            }
            if (document.getElementById('ddlYear').value=='')
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
    <div id="body">
        <div id="cap"></div>
        <div id="main">
            <h1>DutyRoster Templates Assignments</h1>                    
            <asp:Panel ID="Panel1" runat="server">
                <div style="text-align:left">
                    <table width="50%" style="text-align:left ">
                        <tr>
                            <td class="HeaderDiv" style="text-align: center">
                                Template Selecion
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Template Name  : <asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center ">
                                <asp:Button ID="btnSearch" runat="server" Text="Search Template" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div style="text-align:left">
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
                <table width="60%" style="border:0; text-align:left">
                    <tr>
                        <td style="border:0; text-align:left">
                            <asp:GridView ID="GrdViewMain" runat="server" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <input type="radio" name="gvradio" value="<%#Eval("TemplateID")%>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <%#Eval("Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WeeklyOffs" HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeeklyOffs"  Text='<%#GetWeeklyOffs(Eval("WO"))%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift" HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShift" Text='<%#Eval("Shift")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="border:0">
                            Month : 
                            <asp:DropDownList ID="ddlMonth" runat="server">
                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                            </asp:DropDownList> &nbsp;
                            Year : 
                            <asp:DropDownList ID="ddlYear" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;border:0">
                            <asp:Button ID="BtnNext" runat="server" Text="Next" CssClass="button" OnClientClick="javascript:return Chk();" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div> 
    </form>
</body>
</html>
