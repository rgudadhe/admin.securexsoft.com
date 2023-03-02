<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddCustomAttributes.aspx.vb" Inherits="ets.Attributes_Add" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add New Attribute</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Add New Custom Attribute</h1>
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table style="text-align:left">
            <tr>
                <td class="HeaderDiv" colspan="4">
                    Add New Attribute
                </td>
            </tr>
            <tr>
                <td class="alt1">                    
                        Attribute Name                    
                </td>
                <td class="alt1">                    
                        Attribute Caption                    
                </td>
                <td class="alt1">
                    Control Type
                </td>
                
            </tr>
            <tr >
                <td  >
                    <asp:TextBox ID="txtAttribName" runat="server" Width="170px" Wrap="false" CausesValidation="True"></asp:TextBox></td>
                <td  >
                    <asp:TextBox ID="txtAttribCaption" runat="server" Width="168px" CausesValidation="True"></asp:TextBox></td>
                <td ><asp:DropDownList ID="ddType" runat="server" Width="131px">
                    <asp:ListItem Selected="True" Value="TextBox">TextBox</asp:ListItem>
                                      <asp:ListItem Value="DropDown">DropDown</asp:ListItem>
                </asp:DropDownList></td>
             
            </tr>
            <tr>
                <td  >
                    <asp:RequiredFieldValidator  Display="None" ID="valAttribName" runat="server" ControlToValidate="txtAttribName" CssClass="Title"
                        ErrorMessage="Name can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                <td >
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAttribCaption" CssClass="Title"
                        ErrorMessage="Caption Can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                <td >
                    &nbsp
                </td>
                <td >
                    &nbsp
                </td>
            </tr>
            <tr >
                <td colspan="4" style="text-align: center;">
                    &nbsp;<asp:Button CssClass="button"  ID="cmdAdd" runat="server"  Text="Add"
                        Width="60px" /></td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    </div> 
        </div> 
        <div style="text-align:left">        <asp:RegularExpressionValidator  Display="None"
    id="RegtxtAttribName"  
    runat="server" 
    ControlToValidate="txtAttribName" 
    ValidationExpression="^[0-9a-zA-Z]+$"
    ErrorMessage="Attribute Name - Please enter valid input."
   />
    <asp:RegularExpressionValidator  Display="None"
    id="RegTxtFirstName"  
    runat="server" 
    ControlToValidate="txtAttribCaption" 
    ValidationExpression="^[0-9a-zA-Z]+$"
    ErrorMessage="Attribute Caption - Please enter valid input."
   />
</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
