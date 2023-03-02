<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DepartmentAssignments.aspx.vb" Inherits="WorkGroup_DepartmentAssignments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WorkGroups Department Assignments</title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Departments Assignments</h1>
        <div style="text-align:left">
            <table style="text-align:left " border="0">
        <tr>
            <td style="border:0">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="common" border="0">
                                <tr>
                                    <td colspan="3" style="border:0">
                                        <asp:DropDownList ID="ddlWorkGrp" Width="300" runat="server" CssClass="common" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="alt1">
                                        <div style="text-align:center">Assigned Departments</div>
                                    </td>
                                    <td class="alt1" align="center" style="width:30px">
                                        &nbsp;
                                    </td>
                                    <td class="alt1" align="center">
                                        <div style="text-align:center">Available Departments</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstAssigned" Height="400" CssClass="common" Width="300" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </td>
                                    <td style="width:30">
                                        <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/Images/right.jpg" />
    				                    <br /><br />
                                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/left.jpg" />
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstAvailable" Height="400" CssClass="common"  Width="300" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border:0">
                                       <div style="text-align:left">
                                            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="common" ForeColor="Firebrick"></asp:Label>
                                       </div> 
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
        <tr>
            <td style="border:0">
                <div style="text-align:center">
                    <asp:Button ID="btmSubmit" runat="server" Text="Submit" CssClass="button" />
                </div>
            </td>
        </tr>
    </table>
        </div> 
        </div> 
        </div> 
    </form>
</body>
</html>
