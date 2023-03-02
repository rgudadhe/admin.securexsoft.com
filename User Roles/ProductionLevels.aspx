<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProductionLevels.aspx.vb" Inherits="Profuction_Levels_ProductionLevels" %>
<LINK href= "../styles/Main.css" type="text/css" rel="stylesheet">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table   border="2" cellpadding="2" width="100%">
               <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center">
                                         New User Role</td>
            </tr>
            </table> 
        <table>
            <tr>
                <td colspan="3" rowspan="3" >
        <table  border="2" cellpadding="2" width="100%">
            <tr>
                <td  class ="HeaderDiv">                    
                        Contractor                    
                    </td>
                <td  class ="HeaderDiv">                    
                        Level Name                    
                    </td>
                <td  class ="HeaderDiv">                    
                        Description                    
                </td>
                <td  class ="HeaderDiv">                    
                        Type                    
                   
                </td>
            </tr>
            <tr height="16">
             <td >
                    <asp:DropDownList ID="DLContractor" runat="server" Height="18px">
                     </asp:DropDownList></td>
                <td >
                    <asp:TextBox ID="txtLevelName" runat="server" 
                        Height="19px" Width="111px" Wrap="false"></asp:TextBox></td>
                <td >
                    <asp:TextBox ID="txtLevelDesc" runat="server" 
                        Height="19px" Width="221px"></asp:TextBox></td>
                <td >
                    <asp:DropDownList ID="cmbType" runat="server" Height="18px">
                        <asp:ListItem Selected="True" Value="1">Contractor</asp:ListItem>
                        <asp:ListItem Value="0">Sub-Contractor</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
           
            <tr height="16">
                <td colspan="4" style=" text-align: center;">
                    <asp:Button ID="cmdAdd" CssClass="button"  runat="server"  Text="Add"
                        Width="124px" /></td>
            </tr>
             <tr height="16">
                <td colspan="4" style="height: 10px; text-align: center">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLevelName"
                        ErrorMessage="Level Name can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
            </tr>
        </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
