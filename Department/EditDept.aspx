<%@ Page EnableEventValidation="False" Language="VB" AutoEventWireup="false" CodeFile="EditDept.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edit Department</title>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />    
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />    
<script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="DeptDet.aspx?DeptID="+ inpt;
   // alert(inpt);
    
	newwindow=window.open(url,'name','height=200,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script> 
</head>
<body >
     <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit Department</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:Label ID="Label1" runat="server"></asp:Label>
      <asp:Repeater id="DispData" runat="server" >
<HeaderTemplate>
<table width="60%" >

<tr align="center">
<td  colspan="3" class="HeaderDiv" style=" text-align:center; " >Department Details</td>
</tr>
<tr  style="text-align: center">
<td class="alt1" align="center">Name</td>
<td class="alt1" align="center">Description</td>
<td class="alt1" align="center">Edit</td>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
<td align="center" class="common"><%# DataBinder.Eval(Container.DataItem, "Name") %></span> </td>
<td align="center" class="common"><%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "Description")), "&nbsp;", DataBinder.Eval(Container.DataItem, "Description"))%> </span></td>
<td  align="center">
    <asp:Button ID="Button1" runat="server" Text="Edit" CommandName="OpenWin" CssClass="button" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DepartmentID")%> ' OnClientClick='<%# String.Concat("poptastic(""", Eval("DepartmentID"), """);") %>'   />
 </td>

</tr>
</ItemTemplate>

<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
<asp:Repeater id="Repeater2" runat="server"  >

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
