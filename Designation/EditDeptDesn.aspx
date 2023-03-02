<%@ Page EnableEventValidation="False" Language="VB" AutoEventWireup="false" CodeFile="EditDeptDesn.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edit Designations</title>
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />    
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />    
<script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="ViewDeptDesn.aspx?DesignID="+ inpt;
   //alert(inpt);
    
	newwindow=window.open(url,'name','height=300,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script> 
</head>
<body>
     <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit Designation</h1>
         <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Label ID="Label1" runat="server" CssClass="common"></asp:Label>
      <asp:Repeater id="DispData" runat="server" >
<HeaderTemplate>
<table width="80%">

<tr class="HeaderDiv">
<td align="center" colspan="4" class="HeaderDiv" >Designation Details</td>
</tr>
<tr style="text-align: center">
<td align="center" class="alt1">Name</td>
<td align="center" class="alt1">Description</td>
<td align="center" class="alt1">Department</td>
<td align="center" class="alt1">Edit</td>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
<td align="center"> <%# DataBinder.Eval(Container.DataItem, "Name") %></span> </td>
<td align="center"> <%#DataBinder.Eval(Container.DataItem, "Description")%> </span></td>
<td align="center"> <%#DataBinder.Eval(Container.DataItem, "DeptName")%> </span></td>
<td  align="center">
    <asp:Button ID="Button1" runat="server" Text="Edit" CssClass="button" CommandName="OpenWin" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DesignationID")%> ' OnClientClick='<%# String.Concat("poptastic(""", Eval("DesignationID"), """);") %>' />
 </td>

</tr>
</ItemTemplate>

<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
<asp:Repeater id="Repeater2" runat="server" >

<HeaderTemplate>
Company data:
</HeaderTemplate>

<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "Name") %> (<%# DataBinder.Eval(Container.DataItem, "Description") %>)
</ItemTemplate>

<SeparatorTemplate>, </SeparatorTemplate>
</asp:Repeater>
         </asp:Panel>
         </div> 
         </div> 
      </form>
</body>
</html>
