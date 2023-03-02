<%@ Page EnableEventValidation="False" Language="VB" AutoEventWireup="false" CodeFile="ViewNewMacros.aspx.vb" Inherits="ViewMacro" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Edit Category</title>
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    
<script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="ShowPDF.aspx?McID="+ inpt;


    newwindow = window.open(url, 'name', 'height=800,width=1200, left=100, top=100');
	if (window.focus) {newwindow.focus()}
}
function poptastic1(inpt) {
    url = "ShowNewMacro.aspx?McID=" + inpt;
  

    newwindow = window.open(url, 'name', 'height=800,width=1200, left=100, top=100');
    if (window.focus) { newwindow.focus() }
}

</script> 
</head>
<body >
     <form id="form1" runat="server">
     <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Macros</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Repeater id="DispData" runat="server" >
<HeaderTemplate>
<table  >

<tr align="center">
<td  colspan="7" class="HeaderDiv"  style=" text-align:center; padding: 5px; " >Macros</td>
</tr>
<tr  style="text-align: center">
<td align="center" class="alt1">Account</td>
<td align="center" class="alt1">Name</td>

<td align="center" class="alt1">SX User</td>
<td align="center" class="alt1">Date Updated</td>
<td align="center" class="alt1"></td>
<td align="center" class="alt1"></td>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
<td align="center"><%# DataBinder.Eval(Container.DataItem, "AccountName") %></span> </td>
<td align="center"><%#DataBinder.Eval(Container.DataItem, "FileName")%> </span></td>

<td align="center"><%#DataBinder.Eval(Container.DataItem, "UName")%> </span></td>
<td align="center"><%#DataBinder.Eval(Container.DataItem, "DateUpdated")%> </span></td>

 <td  align="center">
    <asp:Button ID="Button2" runat="server" Text="View" CssClass="button" CommandName="OpenWin" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "McID")%> ' OnClientClick='<%# String.Concat("poptastic1(""", Eval("McDet"), """);") %>'  />
 </td>
</tr>
</ItemTemplate>

<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater> 
            </asp:Panel>
         
      


</div> 
</div> 
      </form>


</body>
</html>
