<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DutyRosterUserSelection.aspx.vb" Inherits="LeaveAttendanceMainNew_DutyRosterUserSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DutyRoster Assignment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function chkALL(str)
        {
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox')
                {
                    elm.checked = str.checked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
        <div id="cap"></div>
        <div id="main">
            <h1>Duty Roster Assignments</h1> 
            <div style="text-align:left">
                <asp:Panel ID="Panel1" runat="server">
                    <div style="text-align:left">
                        <table width="50%" style="text-align:left ">
                            <tr>
                                <td class="HeaderDiv" style="text-align: center">
                                    User Selecion
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Employee Name  : <asp:TextBox ID="txtEName" runat="server"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="text-align:center ">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search Users" CssClass="button" />
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
                                <asp:GridView ID="GrdViewUsers" runat="server" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="alt1" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input id="Checkbox1" type="checkbox" onclick="chkALL(this);"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <center>
                                                    <asp:CheckBox ID="ChkUsr" runat="server" />
                                                    <asp:HiddenField ID="hdnUserID" Value='<%#Eval("UserID")%>' runat="server" />
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center; border:0">
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" />
                            </td>
                        </tr>
                    </table> 
                </asp:Panel> 
            </div>
        </div>
    </div>
        <asp:HiddenField ID="hdnUserIds" runat="server" /> 
    </form>
</body>
</html>
