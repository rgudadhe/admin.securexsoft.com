<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JobHistory.aspx.vb" Inherits="FaxPlus_JobHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job History</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rptHistory" runat="server">
<HeaderTemplate>
<table>
            <tr>
            <td class="HeaderDiv">Status</td>            
            <td class="HeaderDiv">Date Modified</td>            
            <td class="HeaderDiv">Ref. Physician</td>            
            <td class="HeaderDiv">User</td>            
            </tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td><%#Container.DataItem("Status")%></td>
            <td><%#Container.DataItem("DateModified")%></td>            
            <td><%#Container.DataItem("RPhy")%></td>
            <td><%#Container.DataItem("UserName")%></td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr>
            <td><%#Container.DataItem("Status")%></td>
            <td><%#Container.DataItem("DateModified")%></td>            
            <td><%#Container.DataItem("RPhy")%></td>
            <td><%#Container.DataItem("UserName")%></td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
    </div>
    </form>
</body>
</html>
