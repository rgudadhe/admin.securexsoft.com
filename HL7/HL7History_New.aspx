<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HL7History_New.aspx.vb" Inherits="HL7_HL7History_New" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rptHistory" runat="server">
<HeaderTemplate>
<table border="1">
            <TR bgcolor="#3399cc">
            <TH><div class="SMSelected">Status</div></TH>            
            <TH><div class="SMSelected">Date Modified</div></th>                        
            <TH><div class="SMSelected">User</div></th>            
            </TR>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("StatusDesc")%></td>
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("DateModified")%></td>                        
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("UserName")%></td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">

            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("StatusDesc")%></td>
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("DateModified")%></td>                        
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("UserName")%></td>
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
