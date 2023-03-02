<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProductionLevels.aspx.vb" Inherits="Profuction_Levels_ProductionLevels" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>User Role</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div id="body">
            <div id="cap"></div>
            <div id="main">
            <h1>Production Levels</h1>
           <div>
            <table style="width: 201px; position: static;">
                <tr>
                    <td style="height: 2px; width: 249px;" class ="alt1">                    
                            Level Name                    
                        </td>
                    <td style="width: 2411px; height: 2px;" class ="alt1">                    
                            Description                    
                    </td>
                   <%-- <td style="width: 134217727px; text-align: center; height: 2px;" class ="alt1">                    
                            Type                    
                        &nbsp;
                    </td>--%>
                </tr>
                <tr>
                    <td style="width: 249px; height: 19px; text-align: left">
                        <asp:TextBox ID="txtLevelName" runat="server" CssClass="common" Width="111px" Wrap="false"></asp:TextBox></td>
                    <td style="width: 2411px; height: 19px; text-align: left">
                        <asp:TextBox ID="txtLevelDesc" runat="server" CssClass="common" Width="221px"></asp:TextBox></td>
                    <%--<td style="width: 134217727px; height: 19px; text-align: left">
                        <asp:DropDownList ID="cmbType" runat="server" Height="18px" CssClass="common">
                            <asp:ListItem Selected="True" Value="1">Contractor</asp:ListItem>
                            <asp:ListItem Value="0">Sub-Contractor</asp:ListItem>
                        </asp:DropDownList></td>--%>
                </tr>
                <tr>
                    <td colspan="3" style="height: 10px; text-align: center">
                        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLevelName"
                            ErrorMessage="Level Name can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 21px; text-align: center;">
                        <asp:Button ID="cmdAdd" runat="server" CssClass="button" Text="Add"
                            Width="124px" /></td>
                </tr>
            </table>
        </div> 
        </div> 
        </div> 
        </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
