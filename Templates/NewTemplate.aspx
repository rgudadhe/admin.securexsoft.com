<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewTemplate.aspx.vb" Inherits="ets.Templates_NewTemplate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>New Template</h1>
        <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server">
            <table>
            <tr>
                <td class="HeaderDiv" colspan="4">
                    Template Information
                </td>
            </tr>
            <tr>
                <td class="alt1">
                    Template Name</td>
                <td class="alt1">
                    Template Type
                </td>    
                <td class="alt1">
                    Work Type
                </td>
                  <td class="alt1">
                    Form Template
                </td>
            </tr>
            <tr>
                <td >
                    <asp:TextBox ID="txtTemplateName" runat="server" Width="267px"></asp:TextBox>
                    <asp:RequiredFieldValidator  Display="None" ID="RFVName" runat="server" ControlToValidate="txtTemplateName" ErrorMessage="Template Name" Text="*" ></asp:RequiredFieldValidator>
                    </td>
                 <td >
                     <asp:TextBox ID="txtTypeName" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator  Display="None" ID="RFVTypeName" runat="server" ErrorMessage="Template Type" ControlToValidate="txtTypeName" Text="*" ></asp:RequiredFieldValidator>
                     <%--<asp:DropDownList ID="ddType" runat="server">
                     </asp:DropDownList>--%></td>
                <td >
                    <asp:TextBox ID="txtTypeLetter" runat="server" Width="80px"></asp:TextBox>
                    <asp:RequiredFieldValidator  Display="None" ID="RFVTypeLetter" runat="server" ErrorMessage="Work Type" ControlToValidate="txtTypeName" Text="*" ></asp:RequiredFieldValidator>
                </td>
                   <td >
                       <asp:DropDownList ID="DropDownList1" runat="server">
                       <asp:ListItem Text="No" Value="0">
                       </asp:ListItem>
                        <asp:ListItem Text="Yes" Value="1">
                       </asp:ListItem>
                       </asp:DropDownList>
                     </td>
            </tr>
            <tr>
                <td colspan="4">
                  
                </td>        
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:Button ID="btnAdd" runat="server" CssClass="button"  Text="Add Template" />&nbsp;</td>
            </tr>
        </table>
        </asp:Panel>
        
        </div> 
        </div>         
    <asp:ValidationSummary ID="VadlidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
