<%@ Page EnableEventValidation="False" Language="VB" AutoEventWireup="false" CodeFile="EditNavBar.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edit Navigation Bar</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />    
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />    
<script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="NavBarDet.aspx?CatID="+ inpt;
    //alert(inpt);
    
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
        <h1>View Navigation Bar</h1>
         <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Label ID="Label1" runat="server" CssClass="common"></asp:Label>   
            <asp:Repeater id="DispData" runat="server" >
<HeaderTemplate>
<table width="60%">

<tr align="center">
<td  colspan="3" class="HeaderDiv" style=" text-align:center; " >View Navigation Bar</td>
</tr>
<tr>
<td align="center" class="alt1">Navigation Bar Details</td>
<td align="center" class="alt1">Edit</td>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
<td align="center"><%#DataBinder.Eval(Container.DataItem, "Details")%></span> </td>
<td  align="center">
    <asp:Button ID="Button1" CssClass="button" runat="server" Text="Edit" CommandName="OpenWin" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "NavBarID")%> ' OnClientClick='<%# String.Concat("poptastic(""", Eval("NavBarID"), """);") %>' />
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
