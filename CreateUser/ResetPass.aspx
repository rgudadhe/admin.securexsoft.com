<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResetPass.aspx.vb" Inherits="ChangePass" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <title>Reset Password</title>
</head>
<body>
    <form id="form1" runat="server">
   <div id="body">
    <div id="cap"></div>
    <div id="main" style="text-align:left;  " >
    <h1> Re-set User Password </h1>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <table id="MainTable" width="400px" >
        <tr>
        <TD class="alt1" >Username</TD>
        <td  class="alt1" style="text-align:left; ">
            <asp:DropDownList ID="DLUser" runat="server"  Width="256px" >
            <asp:ListItem Text="Select User" Selected="True" ></asp:ListItem>  
            </asp:DropDownList>
            </td></tr> 
         <%--<tr>
        <TD>
        Password
       </TD>
        <td>
            <asp:TextBox ID="TxtNPass" TextMode="Password" runat="server" Width="248px"></asp:TextBox>
        </td></tr> 
         <tr>
        <TD>
        Confirm Password
       </TD>
        <td>
            <asp:TextBox ID="TxtCPass" TextMode="Password" runat="server" Width="248px"></asp:TextBox>
        </td></tr>--%> 
         <tr>
        <TD colspan="2" style="text-align: center" >
            <asp:Button CssClass="button"  ID="Button1" runat="server" Text="Submit" />
            </td></tr> </table> 
               
        
      
        <asp:Label ID="lblMsg" runat="server" ></asp:Label><br />
   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select username. " ControlToValidate="DLUser"></asp:RequiredFieldValidator> <br />
  <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password is required " ControlToValidate="TxtNPass"></asp:RequiredFieldValidator> <br />
   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm Password is required " ControlToValidate="TxtCPass"></asp:RequiredFieldValidator> <br />
   --%>
    </div> 
    </div>
    </form>
</body>
</html>
