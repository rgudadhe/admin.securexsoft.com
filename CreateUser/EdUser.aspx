<%@ Page EnableEventValidation="False" Language="VB" AutoEventWireup="false" CodeFile="EdUser.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
    .hoverbutton { text-decoration:none;padding-top:33px; padding: 2px; border: solid 1px whitesmoke; font-size: 8pt;}
    .hoverbutton:hover { background: white url(images/menuhighlight.png);  border: solid 1px silver; }     
    .groupheader { background: steelblue; color: White; padding: 4px; margin-top: 10px; margin-bottom: 5px; font-weight: bold;}    
    .toolbarcontainer { background:#eeeeee;border: solid 1px silver;padding: 5px; }
    .linkitem, linkitemdisabled { border-bottom: dotted 1px teal;padding:10px;padding; }
    .linkitemdisabled { color: #eeeeee; text-decoration: none; }
    small { font-size: 8pt; }
</style>
<script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="EditUser.aspx?UserID="+ inpt;
    alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=800, left=100, top=100, ,scrollbars=yes');
	if (window.focus) {newwindow.focus()}
}

</script> 
</head>
<body style="background-color: whitesmoke" >
     <form id="form1" runat="server">
         <asp:Label ID="Label1" runat="server"></asp:Label>
      <asp:Repeater id="DispData" runat="server" >
<HeaderTemplate>
<table border="1" width="60%" style="left: 20%; position: relative; top: 10%;">

<tr class="SMSelected" style="text-align: center">
<td align="center" colspan="4" ><b><span style="font-size: 0.8em; font-family: Trebuchet MS">Department Details</span></b></td>
</tr>
<tr class="SMSelected" style="text-align: center">
<td align="center"><b><span style="font-size: 0.8em; font-family: Trebuchet MS">UserName</span></b></td>
<td align="center"><b><span style="font-size: 0.8em; font-family: Trebuchet MS">First Name</span></b></td>
<td align="center"><b><span style="font-size: 0.8em; font-family: Trebuchet MS">Last Name</span></b></td>
<td align="center"><b><span style="font-size: 0.8em; font-family: Trebuchet MS">Edit</span></b></td>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
<td align="center"> <span style="font-size: 0.8em; font-family: Trebuchet MS"><%#DataBinder.Eval(Container.DataItem, "Username")%></span> </td>
<td align="center"> <span style="font-size: 0.8em; font-family: Trebuchet MS"><%#DataBinder.Eval(Container.DataItem, "Firstname")%> </span></td>
<td align="center"> <span style="font-size: 0.8em; font-family: Trebuchet MS"><%#DataBinder.Eval(Container.DataItem, "LastName")%> </span></td>
<td  align="center">
<asp:ImageButton ID="ImageButton1" runat="server" 
                             ImageUrl="edit.gif" 
                             CommandName="OpenWin" 
                             CommandArgument='<%#DataBinder.Eval(Container.DataItem, "UserID")%> '  
                               
                               
                             OnClientClick='<%# String.Concat("poptastic(""", Eval("UserID"), """);") %>' />
 </td>

</tr>
</ItemTemplate>

<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>

      </form>


</body>
</html>
